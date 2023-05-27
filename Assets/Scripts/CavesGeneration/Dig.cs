using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Dig : MonoBehaviour
{
    List<Vector3> _vertices = new();
    List<int> _triangles = new();
    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, 100))
            {
                if (hit.collider.gameObject.GetComponent<Chunk>() != null)
                {
                    _vertices = new();
                    _triangles = new();
                    var chunk = hit.collider.gameObject;
                    var mesh = chunk.GetComponent<MeshFilter>().mesh;
                    var triangles = mesh.triangles;
                    var vertices = mesh.vertices;
                    //Debug.Log(triangles.Length + " " + vertices.Length);
                    var flooHit = new Vector3(Mathf.FloorToInt(hit.point.x) - chunk.GetComponent<Chunk>().currentVisibleChunkCoord.x + 1, Mathf.FloorToInt(hit.point.y), Mathf.FloorToInt(hit.point.z) - chunk.GetComponent<Chunk>().currentVisibleChunkCoord.y + 1);
                    //Debug.Log(flooHit);
                    //Debug.Log(chunk.GetComponent<Chunk>().terrainMap[(int)flooHit.x, (int)flooHit.y,(int)flooHit.z]);
                    //Debug.Log(chunk.GetComponent<Chunk>().terrainMap[12,31,5]);
                }
            }
        }
    }

    private void March(Chunk chunk, Vector3 hitPoint)
    {
        float _noiseScale = 0.07f;
        float _terrainSurface = 0.16f;
        float _threshold = 0.21f;
        var _terrainMap = chunk.terrainMap;
        _terrainMap[(int)hitPoint.x, (int)hitPoint.y, (int)hitPoint.z] = 23f;
        var _currentVisibleChunkCoord = chunk.currentVisibleChunkCoord;
        for (int x = 0; x < Chunk.CHUNK_WIDTH; x++)
        {
            for (int y = 0; y < Chunk.CHUNK_HEIGHT; y++)
            {
                for (int z = 0; z < Chunk.CHUNK_WIDTH; z++)
                {
                    //if(x == hitPoint.x && y == hitPoint.y && z == hitPoint.z)
                    if(x==12 && y==31 && z == 5)
                    {
                        MarchingCubes.MarchCube(
                        _vertices,
                        _triangles,
                        new Vector3(x + _currentVisibleChunkCoord.x * Chunk.CHUNK_WIDTH - Chunk.CHUNK_WIDTH / 2, y, z + _currentVisibleChunkCoord.y * Chunk.CHUNK_WIDTH - Chunk.CHUNK_WIDTH / 2),
                        _terrainMap,
                        new float[0],
                        _terrainSurface,
                        _threshold,
                        0
                        );
                    }
                    else
                    {
                        var cube = MarchingCubes.GenerateCube(_terrainMap, x, y, z);
                        MarchingCubes.MarchCube(
                            _vertices,
                            _triangles,
                            new Vector3(x + _currentVisibleChunkCoord.x * Chunk.CHUNK_WIDTH - Chunk.CHUNK_WIDTH / 2, y, z + _currentVisibleChunkCoord.y * Chunk.CHUNK_WIDTH - Chunk.CHUNK_WIDTH / 2),
                            _terrainMap,
                            cube,
                            _terrainSurface,
                            _threshold,
                            -1);
                    }
                    
                }
            }
        }

    }
}
