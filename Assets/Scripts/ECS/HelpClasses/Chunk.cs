using UnityEngine;

public sealed class Chunk : MonoBehaviour
{
    public const int CHUNK_WIDTH = 8;// 64;
    public const int CHUNK_HEIGHT = 128;

    //public MeshCollider meshCollider;
    //public Mesh mesh;
    //public MeshFilter meshFilter;
    public Vector2 chunkPosition;
    public Bounds bounds;
    public bool isVisible;
}
