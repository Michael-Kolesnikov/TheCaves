using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckCaves : MonoBehaviour
{
    private GameObject _chunkStorage;
    private List<Chunk> _terrainChunksVisibleLastUpdate = new();
    private List<Chunk> visibleChunks;
    private Dictionary<Vector2, Chunk> chunksDictionary;
    private Vector2 _currentVisibleChunkCoord;
    private int _chunksVisibleInViewDistance;

    private float[,,] _terrainMap;

    private List<Vector3> _vertices = new();
    private List<int> _triangles = new();

    public float _noiseScale = 0.07f;
    public float _terrainSurface = 0.16f;
    public float _threshold = 0.21f;

    private float viewDistant = 100;
    private void Start()
    {
        Debug.Log("start");

        visibleChunks = new();
        chunksDictionary = new();
        _chunkStorage = new GameObject("Chunk Storage");
        Build();

    }
    private void Update()
    {
        if (false)
        {
            Destroy(_chunkStorage);
            _terrainChunksVisibleLastUpdate.Clear();
            visibleChunks.Clear();
            chunksDictionary.Clear();
            _vertices.Clear();
            _triangles.Clear();

            _chunkStorage = new GameObject("Chunk Storage");
            Build();
        }
    }
    public void Build()
    {
        var playerPosition = Vector3.zero;
        int currentChunkCoordX = Mathf.RoundToInt(playerPosition.x / Chunk.CHUNK_WIDTH);
        int currentChunkCoordZ = Mathf.RoundToInt(playerPosition.z / Chunk.CHUNK_WIDTH);

        _chunksVisibleInViewDistance = Mathf.RoundToInt(viewDistant / Chunk.CHUNK_WIDTH);

        for (int i = 0; i < _terrainChunksVisibleLastUpdate.Count; i++)
        {
            _terrainChunksVisibleLastUpdate[i].gameObject.SetActive(false);
        }
        _terrainChunksVisibleLastUpdate.Clear();

        for (var offsetX = -3; offsetX <= 3; offsetX++)
        {
            for (var offsetZ = -3; offsetZ <= 3; offsetZ++)
            {
                _currentVisibleChunkCoord = new(currentChunkCoordX + offsetX, currentChunkCoordZ + offsetZ);

                if (chunksDictionary.ContainsKey(_currentVisibleChunkCoord))
                {
                    float viewerDstFromNearestEdge = Mathf.Sqrt(chunksDictionary[_currentVisibleChunkCoord].bounds.SqrDistance(new Vector3(playerPosition.x, 0, playerPosition.z)));
                    bool visible = viewerDstFromNearestEdge <= viewDistant;
                    chunksDictionary[_currentVisibleChunkCoord].gameObject.SetActive(visible);
                    if (chunksDictionary[_currentVisibleChunkCoord].gameObject.activeSelf)
                        _terrainChunksVisibleLastUpdate.Add(chunksDictionary[_currentVisibleChunkCoord]);
                }
                else
                {

                    GameObject chunk = new GameObject();
                    chunk.AddComponent<Chunk>();
                    chunk.GetComponent<Chunk>().bounds = new Bounds(new Vector3((currentChunkCoordX + offsetX) * Chunk.CHUNK_WIDTH, 0, (currentChunkCoordZ + offsetZ) * Chunk.CHUNK_WIDTH), new Vector3(Chunk.CHUNK_WIDTH, 0, Chunk.CHUNK_WIDTH));
                    chunk.transform.parent = _chunkStorage.transform;
                    chunk.AddComponent<MeshFilter>();
                    chunk.AddComponent<MeshCollider>();
                    chunk.AddComponent<MeshRenderer>();
                    var mat = Resources.Load("TerrainMaterialCave", typeof(Material)) as Material;
                    chunk.GetComponent<MeshRenderer>().material = mat;
                    chunk.layer = 6; // Ground
                    BuildMesh(chunk);

                    var mesh = chunk.GetComponent<MeshFilter>().mesh;
                    mesh.RecalculateBounds();
                    mesh.RecalculateTangents();
                    mesh.RecalculateNormals();
                    var col = chunk.GetComponent<MeshCollider>();
                    col.sharedMesh = mesh;
                    chunk.GetComponent<Chunk>().terrainMap = _terrainMap;
                    chunk.GetComponent<Chunk>().currentVisibleChunkCoord = _currentVisibleChunkCoord;
                    chunksDictionary.Add(_currentVisibleChunkCoord, chunk.GetComponent<Chunk>());
                }
            }
        }
    }
    private void BuildMesh(GameObject chunk)
    {
        _vertices.Clear();
        _triangles.Clear();
        CreateTerrainMap();
        //for (int x = 0; x < Chunk.CHUNK_WIDTH; x++)
        //{
        //    for (int y = 0; y < Chunk.CHUNK_HEIGHT; y++)
        //    {
        //        for (int z = 0; z < Chunk.CHUNK_WIDTH; z++)
        //        {
        //            var cube = MarchingCubes.GenerateCube(_terrainMap, x, y, z);
        //            MarchingCubes.MarchCube(
        //                _vertices,
        //                _triangles,
        //                new Vector3(x + _currentVisibleChunkCoord.x * Chunk.CHUNK_WIDTH - Chunk.CHUNK_WIDTH / 2, y, z + _currentVisibleChunkCoord.y * Chunk.CHUNK_WIDTH - Chunk.CHUNK_WIDTH / 2),
        //                _terrainMap,
        //                cube,
        //                _terrainSurface,
        //                _threshold);
        //        }
        //    }
        //}
        for (int x = 0; x < Chunk.CHUNK_WIDTH; x++)
        {
            for (int y = 0; y < Chunk.CHUNK_HEIGHT; y++)
            {
                for (int z = 0; z < Chunk.CHUNK_WIDTH; z++)
                {
                    //if(x == hitPoint.x && y == hitPoint.y && z == hitPoint.z)
                    //if((x == 12 && y == 30 && z == 5) || x %2 == 0 && y %2 ==0)
                    //    //|| (x == 12 && y == 29 && z == 5) 
                    //    //|| (x == 11 && y == 29 && z == 5) 
                    //    //|| (x == 13 && y == 29 && z == 5) 
                    //    //|| (x == 11 && y == 29 && z == 6) 
                    //    //|| (x == 13 && y == 29 && z == 6))
                    //{
                    //    MarchingCubes.MarchCube(
                    //    _vertices,
                    //    _triangles,
                    //    new Vector3(x + _currentVisibleChunkCoord.x * Chunk.CHUNK_WIDTH - Chunk.CHUNK_WIDTH / 2, y, z + _currentVisibleChunkCoord.y * Chunk.CHUNK_WIDTH - Chunk.CHUNK_WIDTH / 2),
                    //    _terrainMap,
                    //    new float[0],
                    //    _terrainSurface,
                    //    _threshold,
                    //    0
                    //    );
                    //}
                    //else
                    //{
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
                    //}
                    
                }
            }
        }
        Mesh mesh = new Mesh();
        mesh.indexFormat = UnityEngine.Rendering.IndexFormat.UInt32;
        mesh.vertices = _vertices.ToArray();
        mesh.triangles = _triangles.ToArray();
        mesh.RecalculateNormals();
        mesh.RecalculateBounds();

        chunk.GetComponent<MeshFilter>().mesh = mesh;
    }
    private void CreateTerrainMap()
    {
        _terrainMap = new float[Chunk.CHUNK_WIDTH + 1, Chunk.CHUNK_HEIGHT + 1, Chunk.CHUNK_WIDTH + 1];
        for (int x = 0; x < Chunk.CHUNK_WIDTH + 1; x++)
        {
            for (int z = 0; z < Chunk.CHUNK_WIDTH + 1; z++)
            {
                for (int y = 0; y < Chunk.CHUNK_HEIGHT + 1; y++)
                {

                    float thisHeight = PerlinNoise.GetPerlin((x + Chunk.CHUNK_WIDTH * _currentVisibleChunkCoord.x) * _noiseScale, y * _noiseScale, (z + Chunk.CHUNK_WIDTH * _currentVisibleChunkCoord.y) * _noiseScale);
                    _terrainMap[x, y, z] = thisHeight;
                }
            }
        }
    }
}
