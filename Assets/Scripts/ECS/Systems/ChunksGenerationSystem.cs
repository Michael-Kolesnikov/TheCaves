using UnityEngine;
using Leopotam.EcsLite;
using System.Collections.Generic;

public sealed class ChunksGenerationSystem : IEcsRunSystem,IEcsInitSystem
{
    GameObject _chunkStorage;
    List<Chunk> terrainChunksVisibleLastUpdate = new List<Chunk>();
    Vector2 currentVisibleChunkCoord;
    private int _chunksVisibleInViewDistance;

    float[,,] terrainMap;
    
    List<Vector3> vertices = new();
    List<int> triangles = new();

    float noiseScale = 0.09f;
    float terrainSurface = 0.19f;
    float threshold = 0.2f;
    public void Init(EcsSystems system)
    {
        var filter = system.GetWorld().Filter<PlayerVisibleChunksComponent>().End();
        foreach(var entity in filter)
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
        var filter = system.GetWorld().Filter<PlayerTag>().Inc<PlayerVisibleChunksComponent>().End();
        foreach(var entity in filter)
        {
            ref var movable = ref system.GetWorld().GetPool<MovableComponent>().Get(entity);
            ref var playerVisibleChunks = ref system.GetWorld().GetPool<PlayerVisibleChunksComponent>().Get(entity);
            var playerPosition = movable.characterController.transform.position;

            int currentChunkCoordX = Mathf.RoundToInt(playerPosition.x / Chunk.CHUNK_WIDTH);
            int currentChunkCoordZ = Mathf.RoundToInt(playerPosition.z / Chunk.CHUNK_WIDTH);

            _chunksVisibleInViewDistance = Mathf.RoundToInt(playerVisibleChunks.viewDistant / Chunk.CHUNK_WIDTH);

            for (int i = 0; i < terrainChunksVisibleLastUpdate.Count; i++)
            {
                terrainChunksVisibleLastUpdate[i].gameObject.SetActive(false);
            }
            terrainChunksVisibleLastUpdate.Clear();

            for (var offsetX = -_chunksVisibleInViewDistance; offsetX <= _chunksVisibleInViewDistance; offsetX++)
            {
                for (var offsetZ = -_chunksVisibleInViewDistance; offsetZ <= _chunksVisibleInViewDistance; offsetZ++)
                {
                    currentVisibleChunkCoord = new(currentChunkCoordX + offsetX, currentChunkCoordZ + offsetZ);

                    if(playerVisibleChunks.chunksDictionary.ContainsKey(currentVisibleChunkCoord))
                    {
                        float viewerDstFromNearestEdge = Mathf.Sqrt(playerVisibleChunks.chunksDictionary[currentVisibleChunkCoord].bounds.SqrDistance(new Vector3(playerPosition.x,0, playerPosition.z)));
                        bool visible = viewerDstFromNearestEdge <= playerVisibleChunks.viewDistant;
                        playerVisibleChunks.chunksDictionary[currentVisibleChunkCoord].gameObject.SetActive(visible);
                        if (playerVisibleChunks.chunksDictionary[currentVisibleChunkCoord].gameObject.activeSelf)
                            terrainChunksVisibleLastUpdate.Add(playerVisibleChunks.chunksDictionary[currentVisibleChunkCoord]);
                    }
                    else
                    {

                        GameObject chunk = new GameObject();
                        chunk.AddComponent<Chunk>();
                        chunk.GetComponent<Chunk>().bounds = new Bounds(new Vector3((currentChunkCoordX + offsetX) * Chunk.CHUNK_WIDTH,0, (currentChunkCoordZ + offsetZ)* Chunk.CHUNK_WIDTH), new Vector3(Chunk.CHUNK_WIDTH,0, Chunk.CHUNK_WIDTH));
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
                        playerVisibleChunks.chunksDictionary.Add(currentVisibleChunkCoord, chunk.GetComponent<Chunk>());
                    }
                }
            }
            //Debug.Log($"Текущие координаты игрока : {playerPosition.x}, {playerPosition.z} Текущий чанк - {currentChunkCoordX}, {currentChunkCoordZ}");
            //Debug.Log($"В словарике загружено {playerVisibleChunks.chunksDictionary.Count} чанков");
        }
    }
    
    private void BuildMesh(GameObject chunk)
    {
        vertices.Clear();
        triangles.Clear(); 
        CreateTerrainMap();
        for (int x = 0; x < Chunk.CHUNK_WIDTH; x++)
        {
            for (int y = 0; y < Chunk.CHUNK_HEIGHT; y++)
            {
                for (int z = 0; z < Chunk.CHUNK_WIDTH; z++)
                {
                    var cube = MarchingCubes.GenerateCube(terrainMap,x,y,z);
                    MarchingCubes.MarchCube(
                        vertices,
                        triangles,
                        new Vector3(x + currentVisibleChunkCoord.x * Chunk.CHUNK_WIDTH - Chunk.CHUNK_WIDTH / 2, y, z + currentVisibleChunkCoord.y * Chunk.CHUNK_WIDTH - Chunk.CHUNK_WIDTH / 2),
                        terrainMap,
                        cube,
                        terrainSurface,
                        threshold);
                }
            }
        }

        Mesh mesh = new Mesh();
        mesh.indexFormat = UnityEngine.Rendering.IndexFormat.UInt32;
        mesh.vertices = vertices.ToArray();
        mesh.triangles = triangles.ToArray();
        mesh.RecalculateNormals();
        mesh.RecalculateBounds();

        chunk.GetComponent<MeshFilter>().mesh = mesh;
    }
    private void CreateTerrainMap()
    {
        terrainMap = new float[Chunk.CHUNK_WIDTH + 1, Chunk.CHUNK_HEIGHT + 1, Chunk.CHUNK_WIDTH + 1];
        for (int x = 0; x < Chunk.CHUNK_WIDTH + 1; x++)
        {
            for (int z = 0; z < Chunk.CHUNK_WIDTH + 1; z++)
            {
                for (int y = 0; y < Chunk.CHUNK_HEIGHT + 1; y++)
                {

                    float thisHeight = PERLIN.GetPerlin((x + Chunk.CHUNK_WIDTH * currentVisibleChunkCoord.x) * noiseScale, (y) * noiseScale, (z + Chunk.CHUNK_WIDTH * currentVisibleChunkCoord.y) * noiseScale);
                    terrainMap[x, y, z] = thisHeight;
                }
            }
        }
    }

}
