using System.Collections.Generic;
using UnityEngine;

public class Generator : MonoBehaviour
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

    private readonly float _noiseScale = 0.09f;
    private readonly float _terrainSurface = 0.19f;
    private readonly float _threshold = 0.21f;

    private float viewDistant = 100;
    public Transform character;
    private void Start()
    {
        visibleChunks = new();
        chunksDictionary = new();
        _chunkStorage = new GameObject("Chunk Storage");
        //var playerPosition = character.transform.position;
        //int currentChunkCoordX = Mathf.RoundToInt(playerPosition.x / Chunk.CHUNK_WIDTH);
        //int currentChunkCoordZ = Mathf.RoundToInt(playerPosition.z / Chunk.CHUNK_WIDTH);

        //_chunksVisibleInViewDistance = Mathf.RoundToInt(viewDistant / Chunk.CHUNK_WIDTH);

        //for (int i = 0; i < _terrainChunksVisibleLastUpdate.Count; i++)
        //{
        //    _terrainChunksVisibleLastUpdate[i].gameObject.SetActive(false);
        //}
        //_terrainChunksVisibleLastUpdate.Clear();

        //for (var offsetX = -15; offsetX <= 15; offsetX++)
        //{
        //    for (var offsetZ = -15; offsetZ <= 15; offsetZ++)
        //    {
        //        _currentVisibleChunkCoord = new(currentChunkCoordX + offsetX, currentChunkCoordZ + offsetZ);

        //        if (chunksDictionary.ContainsKey(_currentVisibleChunkCoord))
        //        {
        //            float viewerDstFromNearestEdge = Mathf.Sqrt(chunksDictionary[_currentVisibleChunkCoord].bounds.SqrDistance(new Vector3(playerPosition.x, 0, playerPosition.z)));
        //            bool visible = viewerDstFromNearestEdge <= viewDistant;
        //            chunksDictionary[_currentVisibleChunkCoord].gameObject.SetActive(visible);
        //            if (chunksDictionary[_currentVisibleChunkCoord].gameObject.activeSelf)
        //                _terrainChunksVisibleLastUpdate.Add(chunksDictionary[_currentVisibleChunkCoord]);
        //        }
        //        else
        //        {

        //            GameObject chunk = new GameObject();
        //            chunk.AddComponent<Chunk>();
        //            chunk.GetComponent<Chunk>().bounds = new Bounds(new Vector3((currentChunkCoordX + offsetX) * Chunk.CHUNK_WIDTH, 0, (currentChunkCoordZ + offsetZ) * Chunk.CHUNK_WIDTH), new Vector3(Chunk.CHUNK_WIDTH, 0, Chunk.CHUNK_WIDTH));
        //            chunk.transform.parent = _chunkStorage.transform;
        //            chunk.AddComponent<MeshFilter>();
        //            chunk.AddComponent<MeshCollider>();
        //            chunk.AddComponent<MeshRenderer>();
        //            var mat = Resources.Load("TerrainMaterialCave", typeof(Material)) as Material;
        //            chunk.GetComponent<MeshRenderer>().material = mat;
        //            chunk.layer = 6; // Ground
        //            BuildMesh(chunk);

        //            var mesh = chunk.GetComponent<MeshFilter>().mesh;
        //            mesh.RecalculateBounds();
        //            mesh.RecalculateTangents();
        //            mesh.RecalculateNormals();
        //            var col = chunk.GetComponent<MeshCollider>();
        //            col.sharedMesh = mesh;
        //            chunksDictionary.Add(_currentVisibleChunkCoord, chunk.GetComponent<Chunk>());
        //        }
        //    }
        //}
    }
    private void Update()
    {
        var playerPosition = character.transform.position;
        int currentChunkCoordX = Mathf.RoundToInt(playerPosition.x / Chunk.CHUNK_WIDTH);
        int currentChunkCoordZ = Mathf.RoundToInt(playerPosition.z / Chunk.CHUNK_WIDTH);

        _chunksVisibleInViewDistance = Mathf.RoundToInt(viewDistant / Chunk.CHUNK_WIDTH);

        for (int i = 0; i < _terrainChunksVisibleLastUpdate.Count; i++)
        {
            _terrainChunksVisibleLastUpdate[i].gameObject.SetActive(false);
        }
        _terrainChunksVisibleLastUpdate.Clear();

        for (var offsetX = -_chunksVisibleInViewDistance; offsetX <= _chunksVisibleInViewDistance; offsetX++)
        {
            for (var offsetZ = -_chunksVisibleInViewDistance; offsetZ <= _chunksVisibleInViewDistance; offsetZ++)
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

                    Vector3[] vertices = mesh.vertices;
                    Vector2[] uvs = new Vector2[vertices.Length];

                    for (int i = 0; i < uvs.Length; i++)
                    {
                        uvs[i] = new Vector2(vertices[i].x, vertices[i].z);
                    }
                    mesh.uv = uvs;

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
        for (int x = 0; x < Chunk.CHUNK_WIDTH; x++)
        {
            for (int y = 0; y < Chunk.CHUNK_HEIGHT; y++)
            {
                for (int z = 0; z < Chunk.CHUNK_WIDTH; z++)
                {
                    var cube = MarchingCubes.GenerateCube(_terrainMap, x, y, z);
                    MarchingCubes.MarchCube(
                        _vertices,
                        _triangles,
                        new Vector3(x + _currentVisibleChunkCoord.x * Chunk.CHUNK_WIDTH - Chunk.CHUNK_WIDTH / 2, y, z + _currentVisibleChunkCoord.y * Chunk.CHUNK_WIDTH - Chunk.CHUNK_WIDTH / 2),
                        _terrainMap,
                        cube,
                        _terrainSurface,
                        _threshold);
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