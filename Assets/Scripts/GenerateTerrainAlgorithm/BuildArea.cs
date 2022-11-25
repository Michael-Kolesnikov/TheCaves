using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildArea : MonoBehaviour
{
    [Range(0,1f)]
    [SerializeField] float isoLevel = 0.8f;
    public List<Vector3> vertex = new();
    public List<int> triangles = new();
    public bool interpolate = false;

    
    public void StartBuilding()
    {
        for (int x = 0; x < 10; x++)
        {
            for (int y = 0; y < 1; y++)
            {
                for (int z = 0; z < 10; z++)
                {
                    Vector4[] cube = new Vector4[8];
                    cube[0] = ComputeVertex(x, y, z);
                    cube[1] = ComputeVertex(x + 1, y, z);
                    cube[2] = ComputeVertex(x + 1, y, z + 1);
                    cube[3] = ComputeVertex(x, y, z + 1);
                    cube[4] = ComputeVertex(x, y + 1, z);
                    cube[5] = ComputeVertex(x + 1, y + 1, z);
                    cube[6] = ComputeVertex(x + 1, y + 1, z + 1);
                    cube[7] = ComputeVertex(x, y + 1, z + 1);
                    CalculateVertex(cube);
                }
            }
        }
    }

    private Vector4 ComputeVertex(int x, int y, int z)
    {
        return new Vector4(x,y,z, PerlinNoise3D.GetPerlinNoise3D(new Vector3(x, y, z), 2.31f));
    }

    private void CalculateVertex(Vector4[] cube)
    {
        int cubeIndex = 0;
        if (cube[0].w < isoLevel) cubeIndex |= 1;
        if (cube[1].w < isoLevel) cubeIndex |= 2;
        if (cube[2].w < isoLevel) cubeIndex |= 4;
        if (cube[3].w < isoLevel) cubeIndex |= 8;
        if (cube[4].w < isoLevel) cubeIndex |= 16;
        if (cube[5].w < isoLevel) cubeIndex |= 32;
        if (cube[6].w < isoLevel) cubeIndex |= 64;
        if (cube[7].w < isoLevel) cubeIndex |= 128;

        ///Create triangles
        for (int i = 0; Table.triangulation[cubeIndex,i] != -1; i += 3)
        {
            // Get indices of corner points A and B for each of the three edges
            // of the cube that need to be joined to form the triangle.
            int a0 = Table.cornerIndexAFromEdge[Table.triangulation[cubeIndex,i]];
            int b0 = Table.cornerIndexBFromEdge[Table.triangulation[cubeIndex,i]];

            int a1 = Table.cornerIndexAFromEdge[Table.triangulation[cubeIndex,i + 1]];
            int b1 = Table.cornerIndexBFromEdge[Table.triangulation[cubeIndex,i + 1]];

            int a2 = Table.cornerIndexAFromEdge[Table.triangulation[cubeIndex,i + 2]];
            int b2 = Table.cornerIndexBFromEdge[Table.triangulation[cubeIndex,i + 2]];

            //Мидл можно будет измениь на интерпоялцию
            //Поиск середины
            vertex.Add(new Vector3(cube[a0].x,cube[a0].y,cube[a0].z) - MiddlePoint(Table.cubeCorners[a0], Table.cubeCorners[b0]));
            triangles.Add(vertex.Count - 1);
            vertex.Add(new Vector3(cube[a0].x, cube[a0].y, cube[a0].z) - MiddlePoint(Table.cubeCorners[a1], Table.cubeCorners[b1]));
            triangles.Add(vertex.Count - 1);
            vertex.Add(new Vector3(cube[a0].x, cube[a0].y, cube[a0].z) - MiddlePoint(Table.cubeCorners[a2], Table.cubeCorners[b2]));
            triangles.Add(vertex.Count - 1);

        }
    }
    public Vector3 MiddlePoint(Vector3 v1, Vector3 v2)
    {
        return new Vector3(v1.x+v2.x,v1.y+v2.y, v1.z+ v2.z)/2;
    }
    public Mesh BuildMesh()
    {
        var meshGenerated = new Mesh();
        //Vector3[] meshVert = new Vector3[vertex.Count];
        //int[] meshTriangles = new int[vertex.Count];

        //for (var i = 0; i < vertex.Count; i++)
        //{
        //    meshVert[i] = vertex[i];
        //    meshTriangles[i] = i;
        //}
        meshGenerated.vertices = vertex.ToArray();
        meshGenerated.triangles = triangles.ToArray();
        meshGenerated.RecalculateNormals();
        meshGenerated.RecalculateTangents();

        return meshGenerated;
    }
    MeshFilter meshFilter;
    private void Start()
    {
        meshFilter = GetComponent<MeshFilter>();
        StartBuilding();
        var mesh = BuildMesh();
        meshFilter.mesh = mesh;
    }
    private void Update()
    {
        
    }
}
