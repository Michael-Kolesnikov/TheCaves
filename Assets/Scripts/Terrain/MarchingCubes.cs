using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarchingCubes : MonoBehaviour
{
    List<Vector3> verts = new();
    List<int> triangles = new();
    float terrainSurface = 0.5f;
    [SerializeField] int width = 32;
    [SerializeField] int height = 16;
    float[,,] terrainMap;
        
    public void AddVerticles(float[] cube, Vector3 position)
    {
        //// ��������  ������ ������������
        //for(var i = 0; i < 8; i++)
        //{
        //    if(cube[i] < isolevel)
        //        cubeIndex |= 1 << i;
        //}
        // �� 5�� ������������� � ����
        // ������ ���, ����� ����� ���������� �����, ��� ����������� ���������� ��� 
        int configIndex = GetCubeConfiguration(cube);
        //for (var i = 0; i < 5; i++)
        //{
        //    if (Table.triangles[configIndex, i * 3] < 0)
        //        break;
        //    for (var j = 0; j < 3; j++)
        //    {
        //        var vert = Table.triangles[configIndex, 3 * i + j];
        //        int indexA = Table.cornerIndexAFromEdge[vert];
        //        int indexB = Table.cornerIndexBFromEdge[vert];
        //        Vector3 vertexPos = (position + position + Table.corner[indexA] + Table.corner[indexB]) / 2;
        //        verts.Add(vertexPos);
        //        triangles.Add(verts.Count - 1);
        //    }
        //}
        for (int i = 0; Table.triangles[configIndex,i] != -1; i += 3)
        {
            // Get indices of corner points A and B for each of the three edges
            // of the cube that need to be joined to form the triangle.
            int a0 = Table.cornerIndexAFromEdge[Table.triangles[configIndex,i]];
            int b0 = Table.cornerIndexBFromEdge[Table.triangles[configIndex,i]];

            int a1 = Table.cornerIndexAFromEdge[Table.triangles[configIndex,i + 1]];
            int b1 = Table.cornerIndexBFromEdge[Table.triangles[configIndex,i + 1]];

            int a2 = Table.cornerIndexAFromEdge[Table.triangles[configIndex,i + 2]];
            int b2 = Table.cornerIndexBFromEdge[Table.triangles[configIndex,i + 2]];

            //tri.vertexA = interpolateVerts(cubeCorners[a0], cubeCorners[b0]);
            //tri.vertexB = interpolateVerts(cubeCorners[a1], cubeCorners[b1]);
            //tri.vertexC = interpolateVerts(cubeCorners[a2], cubeCorners[b2]);
            Vector3 vp = (Table.corner[a0] + Table.corner[b0]) / 2;
            verts.Add(vp);
            triangles.Add(verts.Count - 1);
        }
    }
    //public float interpolateVerts(Vector4 v1, Vector4 v2)
    //{
    //    float t = (terrainSurface - v1.w) / (v2.w - v1.w);
    //    return v1.xyz + t * (v2.xyz - v1.xyz);
    //}

    public GameObject go;
    public void CreateMeshData()
    {
        for (var x = 0; x < width; x++)
        {
            for (var y = 0; y < height; y++)
            {
                for (var z = 0; z < width; z++)
                {
                    float[] cube = new float[8];
                    for (var i = 0; i < 8; i++)
                    {
                        Vector3Int corner = new Vector3Int(x, y, z) + Table.corner[i]; 
                        cube[i] = terrainMap[corner.x, corner.y, corner.z];
                    }
                    AddVerticles(cube, new Vector3(x, y, z));
                }
            }
        }

                    
    }
    public void ClearMesh()
    {
        triangles.Clear();
        verts.Clear();
    }
    private void Start()
    {
        ClearMesh();
        terrainMap = new float[width + 1, height + 1, width + 1];
        GenerateTerrainMap();

        CreateMeshData();
        BuildMesh();
    }
    public void BuildMesh()
    {
        var mF = go.GetComponent<MeshFilter>();
        Mesh mesh = new Mesh();
        mesh.vertices = verts.ToArray();
        mesh.triangles = triangles.ToArray();
        mF.mesh = mesh;
        mesh.RecalculateNormals();
        var meshCollider = go.GetComponent<MeshCollider>();
        meshCollider.sharedMesh = mesh;
    }
    public void GenerateTerrainMap()
    {
        for (var x = 0; x < width + 1; x++)
        {
            for (var y = 0; y < height + 1; y++)
            {
                for(var z = 0; z < width + 1; z++ )
                {
                    var tH = GenerateCubes.GetPerlinNoise3D(new Vector3((float)x, (float)y,(float)z), 1.01f) * height;
                    //height * PerlinNoise3D.GeneratePerlineNoise((float)x / 16f * 1.5f + 0.01f,(float)y/16* 1.5f + 0.01f, (float)z / 16f * 1.5f + 0.01f);
                    float point = 0;
                    if (y <= tH - 0.5f)
                        point = 0f;
                    else if (y > tH + 0.5f)
                        point = 1f;
                    else if (y > tH)
                        point = (float)y - tH;
                    else
                        point = tH - (float)y;

                    terrainMap[x, y, z] = point;
                }
            }
        }
    }
    public int GetCubeConfiguration(float[] cube)
    {
        int configurationIndex = 0;
        for(var i = 0; i < 8; i++)
        {
            if (cube[i] > terrainSurface)
                configurationIndex |= 1 << i;
        }
        return configurationIndex;
    }
}
