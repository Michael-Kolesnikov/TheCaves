using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct PlayerVisibleChunksComponent
{
    public float viewDistant;
    [HideInInspector]
    public List<Chunk2> visibleChunks;
    [HideInInspector]
    public Dictionary<Vector2, Chunk2> chunksDictionary;
    public float noiseScale;
    public float terrainSurface;
    public float threshold;
}
