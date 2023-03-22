using Leopotam.EcsLite;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public sealed class ChunksGenerationSystem : IEcsRunSystem, IEcsInitSystem
{
    private GameObject _chunkStorage;
    private List<Chunk2> _terrainChunksVisibleLastUpdate = new();
    private Vector2 _currentVisibleChunkCoord;
    private int _chunksVisibleInViewDistance;

    private float[,,] _terrainMap;

    private List<Vector3> _vertices = new();
    private List<int> _triangles = new();

    private readonly float _noiseScale = 0.09f;
    private readonly float _terrainSurface = 0.19f;
    private readonly float _threshold = 0.2f;
    public void Init(EcsSystems system)
    {
        var filter = system.GetWorld().Filter<PlayerVisibleChunksComponent>().End();
        foreach (var entity in filter)
        {
            ref var playerVisibleChunks = ref system.GetWorld().GetPool<PlayerVisibleChunksComponent>().Get(entity);
            playerVisibleChunks.visibleChunks = new();
            playerVisibleChunks.chunksDictionary = new();
        }
        //create storage containing all chunks
        _chunkStorage = new GameObject("Chunk Storage");
    }
    public void Run(EcsSystems system)
    {
        var filter = system.GetWorld().Filter<PlayerTag>().Inc<MovableComponent>().Inc<PlayerVisibleChunksComponent>().End();
        foreach (var entity in filter)
        {
            ref var movable = ref system.GetWorld().GetPool<MovableComponent>().Get(entity);
            ref var playerVisibleChunks = ref system.GetWorld().GetPool<PlayerVisibleChunksComponent>().Get(entity);
            var playerPosition = movable.characterController.transform.position;

            int currentChunkCoordX = Mathf.RoundToInt(playerPosition.x / Chunk2.CHUNK_WIDTH);
            int currentChunkCoordZ = Mathf.RoundToInt(playerPosition.z / Chunk2.CHUNK_WIDTH);

            _chunksVisibleInViewDistance = Mathf.RoundToInt(playerVisibleChunks.viewDistant / Chunk2.CHUNK_WIDTH);

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

                    if (playerVisibleChunks.chunksDictionary.ContainsKey(_currentVisibleChunkCoord))
                    {
                        float viewerDstFromNearestEdge = Mathf.Sqrt(playerVisibleChunks.chunksDictionary[_currentVisibleChunkCoord].bounds.SqrDistance(new Vector3(playerPosition.x, 0, playerPosition.z)));
                        bool visible = viewerDstFromNearestEdge <= playerVisibleChunks.viewDistant;
                        playerVisibleChunks.chunksDictionary[_currentVisibleChunkCoord].gameObject.SetActive(visible);
                        if (playerVisibleChunks.chunksDictionary[_currentVisibleChunkCoord].gameObject.activeSelf)
                            _terrainChunksVisibleLastUpdate.Add(playerVisibleChunks.chunksDictionary[_currentVisibleChunkCoord]);
                    }
                    else
                    {

                        GameObject chunk = new GameObject();
                        chunk.AddComponent<Chunk2>();
                        chunk.GetComponent<Chunk2>().bounds = new Bounds(new Vector3((currentChunkCoordX + offsetX) * Chunk2.CHUNK_WIDTH, 0, (currentChunkCoordZ + offsetZ) * Chunk2.CHUNK_WIDTH), new Vector3(Chunk2.CHUNK_WIDTH, 0, Chunk2.CHUNK_WIDTH));
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
                        playerVisibleChunks.chunksDictionary.Add(_currentVisibleChunkCoord, chunk.GetComponent<Chunk2>());
                    }
                }
            }
            //Debug.Log($"Òåêóùèå êîîðäèíàòû èãðîêà : {playerPosition.x}, {playerPosition.z} Òåêóùèé ÷àíê - {currentChunkCoordX}, {currentChunkCoordZ}");
            //Debug.Log($"Â ñëîâàðèêå çàãðóæåíî {playerVisibleChunks.chunksDictionary.Count} ÷àíêîâ");
        }
    }

    private void BuildMesh(GameObject chunk)
    {
        _vertices.Clear();
        _triangles.Clear();
        CreateTerrainMap();
        for (int x = 0; x < Chunk2.CHUNK_WIDTH; x++)
        {
            for (int y = 0; y < Chunk2.CHUNK_HEIGHT; y++)
            {
                for (int z = 0; z < Chunk2.CHUNK_WIDTH; z++)
                {
                    var cube = MarchingCubes.GenerateCube(_terrainMap, x, y, z);
                    MarchingCubes.MarchCube(
                        _vertices,
                        _triangles,
                        new Vector3(x + _currentVisibleChunkCoord.x * Chunk2.CHUNK_WIDTH - Chunk2.CHUNK_WIDTH / 2, y, z + _currentVisibleChunkCoord.y * Chunk2.CHUNK_WIDTH - Chunk2.CHUNK_WIDTH / 2),
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
        _terrainMap = new float[Chunk2.CHUNK_WIDTH + 1, Chunk2.CHUNK_HEIGHT + 1, Chunk2.CHUNK_WIDTH + 1];
        for (int x = 0; x < Chunk2.CHUNK_WIDTH + 1; x++)
        {
            for (int z = 0; z < Chunk2.CHUNK_WIDTH + 1; z++)
            {
                for (int y = 0; y < Chunk2.CHUNK_HEIGHT + 1; y++)
                {

                    float thisHeight = PerlinNoise.GetPerlin((x + Chunk2.CHUNK_WIDTH * _currentVisibleChunkCoord.x) * _noiseScale, y * _noiseScale, (z + Chunk2.CHUNK_WIDTH * _currentVisibleChunkCoord.y) * _noiseScale);
                    _terrainMap[x, y, z] = thisHeight;
                }
            }
        }
    }

}