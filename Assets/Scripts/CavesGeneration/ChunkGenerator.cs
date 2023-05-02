using System.Collections.Generic;
using Unity.Collections;
using Unity.Jobs;
using Unity.Mathematics;
using UnityEngine;

public class ChunkGenerator : MonoBehaviour
{
    const int chunkWidth = 8;
    const int chunkHeight = 256;
    float[] terrainMap = new float[(chunkWidth + 1) * (chunkWidth + 1) * (chunkHeight + 1)];
    void Start()
    {

    }
    private void Update()
    {
        var startTime = Time.realtimeSinceStartup;
        PopulateTerrainMap();
        NativeList<float3> vertices  = new NativeList<float3>(Allocator.TempJob);
        NativeList<float3> triangles  = new NativeList<float3>(Allocator.TempJob);
        var job = new MarchCubes
        {
            chunkWidth = chunkWidth,
            chunkHeight = chunkHeight,
            chunkData = new NativeArray<float>(terrainMap, Allocator.TempJob),
            vertices = vertices,
            triangles = triangles,
            threshold = 0.2f,
            isoLevel = 0.19f

        };
        var handle = job.Schedule();
        handle.Complete();
        Debug.Log("----------------------");
        Debug.Log(job.vertices.Length);
        Debug.Log(job.triangles.Length);
        job.chunkData.Dispose();
        vertices.Dispose();
        job.triangles.Dispose();
        Debug.Log(((Time.realtimeSinceStartup - startTime) * 1000f) + "ms");
    }
    public void PopulateTerrainMap()
    {
        for (var x = 0; x < chunkWidth; x++)
        {
            for (var y = 0; y < chunkHeight; y++)
            {
                for (var z = 0; z < chunkWidth; z++)
                {
                    int index = x * chunkHeight * chunkWidth + y * chunkWidth + z;
                    //terrainMap[index] = (float)NoiseS3D.Noise(x, y, z);
                    terrainMap[index] = GetPerlin(x, y, z);
                }
            }
        }
    }
    public static float GetPerlin(float x, float y, float z)
    {
        // offset from 0,0 coordinates for avoding symmetry
        x += 1000000;
        z += 1000000;
        var ab = Mathf.PerlinNoise(x, y);
        var bc = Mathf.PerlinNoise(y, z);
        var ac = Mathf.PerlinNoise(x, z);

        var ba = Mathf.PerlinNoise(y, x);
        var cb = Mathf.PerlinNoise(z, y);
        var ca = Mathf.PerlinNoise(z, x);

        return (ab + bc + ac + ba + cb + ca) / 6f;
    }
}
