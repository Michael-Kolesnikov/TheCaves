using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct PlayerVisibleChunksComponent
{
    public float viewDistant;
    [HideInInspector]
    public List<Chunk> visibleChunks;
    [HideInInspector]
    public Dictionary<Vector2, Chunk> chunksDictionary;
}
