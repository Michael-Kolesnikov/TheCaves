using Leopotam.EcsLite;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.VirtualTexturing;

sealed class ChunksGeneratorSystem : IEcsRunSystem, IEcsInitSystem
{
    private GameObject _chunkStorage;
    private List<Chunk2> _terrainChunksVisibleLastUpdate = new();

    public void Init(EcsSystems systems)
    {
        var filter = systems.GetWorld().Filter<PlayerVisibleChunksComponent>().End();
        foreach (var entity in filter)
        {
            ref var playerVisibleChunks = ref systems.GetWorld().GetPool<PlayerVisibleChunksComponent>().Get(entity);
            playerVisibleChunks.visibleChunks = new();
            playerVisibleChunks.chunksDictionary = new();
        }
        //create storage containing all chunks
        _chunkStorage = new GameObject("Chunk Storage");
    }

    public void Run(EcsSystems systems)
    {
        var filter = systems.GetWorld().Filter<PlayerTag>().Inc<MovableComponent>().Inc<PlayerVisibleChunksComponent>().End();


        foreach (var entity in filter)
        {
            ref var movable = ref systems.GetWorld().GetPool<MovableComponent>().Get(entity);
            ref var playerVisibleChunks = ref systems.GetWorld().GetPool<PlayerVisibleChunksComponent>().Get(entity);
            var playerPosition = movable.characterController.transform.position;

            int currentChunkCoordX = Mathf.RoundToInt(playerPosition.x / Chunk2.CHUNK_WIDTH);
            int currentChunkCoordZ = Mathf.RoundToInt(playerPosition.z / Chunk2.CHUNK_WIDTH);

            var _chunksVisibleInViewDistance = Mathf.RoundToInt(playerVisibleChunks.viewDistant / Chunk2.CHUNK_WIDTH);
            for (int i = 0; i < _terrainChunksVisibleLastUpdate.Count; i++)
            {
                _terrainChunksVisibleLastUpdate[i].gameObject.SetActive(false);
            }
            _terrainChunksVisibleLastUpdate.Clear();

            //очень долго нужно переделать

            for (var offsetX = -_chunksVisibleInViewDistance; offsetX <= _chunksVisibleInViewDistance; offsetX++)
            {
                for (var offsetZ = -_chunksVisibleInViewDistance; offsetZ <= _chunksVisibleInViewDistance; offsetZ++)
                {
                    Vector2 _currentVisibleChunkCoord = new(currentChunkCoordX + offsetX, currentChunkCoordZ + offsetZ);

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
                        GameObject chunk = GameObject.CreatePrimitive(PrimitiveType.Cube);
                        chunk.transform.localScale = new Vector3(8, 8, 8);
                        chunk.transform.parent = _chunkStorage.transform;
                        chunk.transform.position = new Vector3(_currentVisibleChunkCoord.x,20,_currentVisibleChunkCoord.y);
                        chunk.AddComponent<Chunk2>();
                        chunk.GetComponent<Chunk2>().bounds = new Bounds(new Vector3((currentChunkCoordX + offsetX) * Chunk2.CHUNK_WIDTH, 0, (currentChunkCoordZ + offsetZ) * Chunk2.CHUNK_WIDTH), new Vector3(Chunk2.CHUNK_WIDTH, 0, Chunk2.CHUNK_WIDTH));

                        playerVisibleChunks.chunksDictionary.Add(_currentVisibleChunkCoord,chunk.GetComponent<Chunk2>());
                    }
                }
            }
        }
    }
}
