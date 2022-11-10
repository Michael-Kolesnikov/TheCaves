using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateCubes : MonoBehaviour
{
    public int depth;
    public int width;
    public int height;
    public int scale;
    public float offSet;

    public float frequency = 1.01f;

    public GameObject item;

    /// <summary>
    /// ѕросто способ хранени€ позиции всех векторов
    /// </summary>
    List<Vector3> vectors = new List<Vector3>();
    List<GameObject> items = new List<GameObject>();

    private void Start()
    {
        UpdateMesh();
    }
    private void Update()
    {
        //UpdateMesh();
    }
    private void UpdateMesh()
    {
        ClearVectors();
        InitiatePoints();
        InitiateGameObjects();
    }

    private void ClearVectors()
    {
        foreach(var item in items)
            Destroy(item);
        vectors.Clear();
    }

    private void InitiatePoints()
    {
        for(var x = 0; x < width; x++)
            for(var y = 0; y < height; y++)
                for(var z =0; z < depth; z++)
                    vectors.Add(new Vector3(x, y, z));

    }
    private void InitiateGameObjects()
    {
        int index = 1;
        string output1  ="";
        string output2 = "";
        foreach (var vector in vectors)
        {
            var currentGameObject = Instantiate(item, vector + new Vector3(offSet, offSet, offSet), Quaternion.Euler(0, 0, 0));
            items.Add(currentGameObject);

            float sample = GetPerlinNoise3D(vector, frequency);
            output1 = output1 + sample.ToString() + " ";
            sample = (sample + 1f) / 2f;
            output2 = output2 + sample.ToString() + " ";

            if(index % 5 ==0)
            {
                Debug.Log(output1);
                Debug.Log(output2);
            }
            index++;
            currentGameObject.GetComponent<MeshRenderer>().materials[0].color = new Color(sample, sample, sample, 1);
        }
    }

    public static float GetPerlinNoise3D(Vector3 point, float frequency)
    {
        point *= frequency;

        int flooredPointX0 = Mathf.FloorToInt(point.x);
        int flooredPointY0 = Mathf.FloorToInt(point.y);
        int flooredPointZ0 = Mathf.FloorToInt(point.z);

        float distanceX0 = point.x - flooredPointX0;
        float distanceY0 = point.y - flooredPointY0;
        float distanceZ0 = point.z - flooredPointZ0;

        float distanceX1 = distanceX0 - 1f;
        float distanceY1 = distanceY0 - 1f;
        float distanceZ1 = distanceZ0 - 1f;

        flooredPointX0 &= permutationCount;
        flooredPointY0 &= permutationCount;
        flooredPointZ0 &= permutationCount;

        int flooredPointX1 = flooredPointX0 + 1;
        int flooredPointY1 = flooredPointY0 + 1;
        int flooredPointZ1 = flooredPointZ0 + 1;

        int permutationX0 = permutation[flooredPointX0];
        int permutationX1 = permutation[flooredPointX1];

        int permutationY00 = permutation[permutationX0 + flooredPointY0];
        int permutationY10 = permutation[permutationX1 + flooredPointY0];
        int permutationY01 = permutation[permutationX0 + flooredPointY1];
        int permutationY11 = permutation[permutationX1 + flooredPointY1];
        
        Vector3 direction000 = directions[permutation[permutationY00 + flooredPointZ0] & directionCount];
        Vector3 direction100 = directions[permutation[permutationY10 + flooredPointZ0] & directionCount];
        Vector3 direction010 = directions[permutation[permutationY01 + flooredPointZ0] & directionCount];
        Vector3 direction110 = directions[permutation[permutationY11 + flooredPointZ0] & directionCount];
        Vector3 direction001 = directions[permutation[permutationY00 + flooredPointZ1] & directionCount];
        Vector3 direction101 = directions[permutation[permutationY10 + flooredPointZ1] & directionCount];
        Vector3 direction011 = directions[permutation[permutationY01 + flooredPointZ1] & directionCount];
        Vector3 direction111 = directions[permutation[permutationY11 + flooredPointZ1] & directionCount];

        float value000 = scalar(direction000, new Vector3(distanceX0, distanceY0, distanceZ0));
        float value100 = scalar(direction100, new Vector3(distanceX1, distanceY0, distanceZ0));
        float value010 = scalar(direction010, new Vector3(distanceX0, distanceY1, distanceZ0));
        float value110 = scalar(direction110, new Vector3(distanceX1, distanceY1, distanceZ0));
        float value001 = scalar(direction001, new Vector3(distanceX0, distanceY0, distanceZ1));
        float value101 = scalar(direction101, new Vector3(distanceX1, distanceY0, distanceZ1));
        float value011 = scalar(direction011, new Vector3(distanceX0, distanceY1, distanceZ1));
        float value111 = scalar(direction111, new Vector3(distanceX1, distanceY1, distanceZ1));

        float smoothDistanceX = smoothDistance(distanceX0);
        float smoothDistanceY = smoothDistance(distanceY0);
        float smoothDistanceZ = smoothDistance(distanceZ0);

        return Mathf.Lerp(
            Mathf.Lerp(Mathf.Lerp(value000, value100, smoothDistanceX), Mathf.Lerp(value010, value110, smoothDistanceX), smoothDistanceY),
            Mathf.Lerp(Mathf.Lerp(value001, value101, smoothDistanceX), Mathf.Lerp(value011, value111, smoothDistanceX), smoothDistanceY),
            smoothDistanceZ);
    }
    private static int[] permutation = {
        151,160,137, 91, 90, 15,131, 13,201, 95, 96, 53,194,233,  7,225,
        140, 36,103, 30, 69,142,  8, 99, 37,240, 21, 10, 23,190,  6,148,
        247,120,234, 75,  0, 26,197, 62, 94,252,219,203,117, 35, 11, 32,
         57,177, 33, 88,237,149, 56, 87,174, 20,125,136,171,168, 68,175,
         74,165, 71,134,139, 48, 27,166, 77,146,158,231, 83,111,229,122,
         60,211,133,230,220,105, 92, 41, 55, 46,245, 40,244,102,143, 54,
         65, 25, 63,161,  1,216, 80, 73,209, 76,132,187,208, 89, 18,169,
        200,196,135,130,116,188,159, 86,164,100,109,198,173,186,  3, 64,
         52,217,226,250,124,123,  5,202, 38,147,118,126,255, 82, 85,212,
        207,206, 59,227, 47, 16, 58, 17,182,189, 28, 42,223,183,170,213,
        119,248,152,  2, 44,154,163, 70,221,153,101,155,167, 43,172,  9,
        129, 22, 39,253, 19, 98,108,110, 79,113,224,232,178,185,112,104,
        218,246, 97,228,251, 34,242,193,238,210,144, 12,191,179,162,241,
         81, 51,145,235,249, 14,239,107, 49,192,214, 31,181,199,106,157,
        184, 84,204,176,115,121, 50, 45,127,  4,150,254,138,236,205, 93,
        222,114, 67, 29, 24, 72,243,141,128,195, 78, 66,215, 61,156,180,

        151,160,137, 91, 90, 15,131, 13,201, 95, 96, 53,194,233,  7,225,
        140, 36,103, 30, 69,142,  8, 99, 37,240, 21, 10, 23,190,  6,148,
        247,120,234, 75,  0, 26,197, 62, 94,252,219,203,117, 35, 11, 32,
         57,177, 33, 88,237,149, 56, 87,174, 20,125,136,171,168, 68,175,
         74,165, 71,134,139, 48, 27,166, 77,146,158,231, 83,111,229,122,
         60,211,133,230,220,105, 92, 41, 55, 46,245, 40,244,102,143, 54,
         65, 25, 63,161,  1,216, 80, 73,209, 76,132,187,208, 89, 18,169,
        200,196,135,130,116,188,159, 86,164,100,109,198,173,186,  3, 64,
         52,217,226,250,124,123,  5,202, 38,147,118,126,255, 82, 85,212,
        207,206, 59,227, 47, 16, 58, 17,182,189, 28, 42,223,183,170,213,
        119,248,152,  2, 44,154,163, 70,221,153,101,155,167, 43,172,  9,
        129, 22, 39,253, 19, 98,108,110, 79,113,224,232,178,185,112,104,
        218,246, 97,228,251, 34,242,193,238,210,144, 12,191,179,162,241,
         81, 51,145,235,249, 14,239,107, 49,192,214, 31,181,199,106,157,
        184, 84,204,176,115,121, 50, 45,127,  4,150,254,138,236,205, 93,
        222,114, 67, 29, 24, 72,243,141,128,195, 78, 66,215, 61,156,180
    };
    const int permutationCount = 255;
    private static Vector3[] directions = {
        new Vector3( 1f, 1f, 0f),
        new Vector3(-1f, 1f, 0f),
        new Vector3( 1f,-1f, 0f),
        new Vector3(-1f,-1f, 0f),
        new Vector3( 1f, 0f, 1f),
        new Vector3(-1f, 0f, 1f),
        new Vector3( 1f, 0f,-1f),
        new Vector3(-1f, 0f,-1f),
        new Vector3( 0f, 1f, 1f),
        new Vector3( 0f,-1f, 1f),
        new Vector3( 0f, 1f,-1f),
        new Vector3( 0f,-1f,-1f),

        new Vector3( 1f, 1f, 0f),
        new Vector3(-1f, 1f, 0f),
        new Vector3( 0f,-1f, 1f),
        new Vector3( 0f,-1f,-1f)
    };
    private const int directionCount = 15;
    private static float scalar(Vector3 a, Vector3 b)
    {
        return a.x * b.x + a.y * b.y + a.z * b.z;
    }
    private static float smoothDistance(float d)
    {
        return d * d * d * (d * (d * 6f - 15f) + 10f);
    }
    //public GameObject sphere;
    //float offset = 0;
    ////public int width;
    ////public int height;
    //public float scale = 10f;
    //public GameObject[,,] cubes;
    //public float frequency = 1.01f;


    //public int size = 10;
    //public int height = 20;
    //public int width = 20;
    //public int depth = 20;
    //Vector3[] vectors;
    //GameObject[] cubess;

    //private void Start()
    //{
    //    vectors = new Vector3[width * height * depth];
    //    cubess = new GameObject[width * height * depth];
    //    cubes = new GameObject[width, height, width];
    //    //for (int z = 0; z < width; z++)
    //    //{
    //    //    for (int y = 0; y < height; y++)
    //    //    {
    //    //        for (int x = 0; x < width; x++)
    //    //        {
    //    //            Vector3 pos = new Vector3(x + offset, y + offset, z + offset);
    //    //            cubes[x, y, z] = Instantiate(sphere,pos, Quaternion.Euler(0, 0, 0));
    //    //            cubes[x, y, z].GetComponent<MeshRenderer>().materials[0].color = GainColor(pos);
    //    //        }
    //    //    }
    //    //}
    //    for (int z = 0,i = 0; z < depth; z++)
    //    {
    //        for (int y = 0; y < height; y++)
    //        {
    //            for (int x = 0; x < width; x++)
    //            {
    //                Vector3 pos = new Vector3(x + offset, y + offset, z + offset);
    //                //cubes[z, y, x] = Instantiate(sphere,pos, Quaternion.Euler(0, 0, 0));
    //                vectors[i] = pos;
    //                i++;
    //            }
    //        }
    //    }
    //    for(var i = 0;i < vectors.Length;i++)
    //    {
    //        float sample = perlinNoise.get3DPerlinNoise(vectors[i], frequency);

    //        sample = (sample + 1f) / 2f;
    //        cubess[i] = Instantiate(sphere, vectors[i], Quaternion.Euler(0, 0, 0));
    //        cubess[i].GetComponent<MeshRenderer>().materials[0].color = new Color(sample, sample, sample, 1);

    //    }
    //    Debug.Log(cubes.Length);
    //}
    //private void Update()
    //{
    //    //for (int z = 0; z < width; z++)
    //    //{
    //    //    for (int y = 0; y < height; y++)
    //    //    {
    //    //        for (int x = 0; x < width; x++)
    //    //        {
    //    //            Vector3 pos = new Vector3(x + offset, y + offset, z + offset);
    //    //            cubes[z, y, x].GetComponent<MeshRenderer>().materials[0].color = GainColor(pos);
    //    //        }
    //    //    }
    //    //}
    //}
    //public Color GainColor(Vector3 point)
    //{
    //    float noiseY = get3DPerlinNoise(point, frequency);
    //    //float noise = PerlinNoise3D(new Vector3(xCoord, yCoord, zCoord), frequancy);
    //    noiseY = (noiseY + 1f) / 2f;
    //    return new Color(noiseY, noiseY, noiseY);
    //}
    ////public float PerlinNoise3D(Vector3 point,float frequancy)
    ////{
    ////    point *= frequancy;

    ////    int flooredPointX0 = Mathf.FloorToInt(point.x);
    ////    int flooredPointY0 = Mathf.FloorToInt(point.y);
    ////    int flooredPointZ0 = Mathf.FloorToInt(point.z);

    ////    flooredPointX0 &= permutation.Length;
    ////    flooredPointY0 &= permutation.Length;
    ////    flooredPointZ0 &= permutation.Length;

    ////    float distanceX0 = point.x - flooredPointX0;
    ////    float distanceY0 = point.y - flooredPointY0;
    ////    float distanceZ0 = point.z - flooredPointZ0;

    ////    int flooredPointX1 = flooredPointX0 + 1;
    ////    int flooredPointY1 = flooredPointY0 + 1;
    ////    int flooredPointZ1 = flooredPointZ0 + 1;


    ////    int permutationX0 = permutation[flooredPointX0];
    ////    int permutationX1 = permutation[flooredPointX1];

    ////    int permutationY00 = permutation[permutationX0 + flooredPointY0];
    ////    int permutationY01 = permutation[permutationX0 + flooredPointY1];
    ////    int permutationY10 = permutation[permutationX1 + flooredPointY0];
    ////    int permutationY11 = permutation[permutationX1 + flooredPointY1];

    ////    int permutationZ000 = permutation[permutationY00 + flooredPointZ0];
    ////    int permutationZ001 = permutation[permutationY00 + flooredPointZ1];
    ////    int permutationZ010 = permutation[permutationY01 + flooredPointZ0];
    ////    int permutationZ011 = permutation[permutationY01 + flooredPointZ1];
    ////    int permutationZ100 = permutation[permutationY10 + flooredPointZ0];
    ////    int permutationZ101 = permutation[permutationY10 + flooredPointZ1];
    ////    int permutationZ110 = permutation[permutationY11 + flooredPointZ0];
    ////    int permutationZ111 = permutation[permutationY11 + flooredPointZ1];

    ////    return Mathf.Lerp(
    ////        Mathf.Lerp(Mathf.Lerp(permutationZ000,permutationZ100,distanceX0),Mathf.Lerp(permutationZ010,permutationZ110,distanceX0),distanceY0),
    ////        Mathf.Lerp(Mathf.Lerp(permutationZ001,permutationZ101,distanceX0),Mathf.Lerp(permutationZ011,permutationZ111,distanceX0),distanceY0),
    ////        distanceZ0
    ////        ) * (1f / permutation.Length);
    ////}
    //public int[] permutation = new int[] 
    //{
    //    78, 66, 116, 225, 87, 142, 183, 46, 73, 43, 25, 69,
    //    118, 97, 145, 98, 190, 103, 114, 24, 9, 159, 15,
    //    31, 71, 235, 228, 222, 233, 5, 162, 69, 221, 215,
    //    131, 146, 193, 25, 36, 191, 56, 64, 158, 27, 15,
    //    185, 245, 119, 180, 182, 215, 178, 107, 43, 170,
    //    225, 179, 197, 87, 98, 251, 151, 229, 234, 200,
    //    109, 21, 90, 210, 209, 175, 67, 73, 109, 13, 111,
    //    33, 52, 115, 202, 231, 64, 221, 31, 232, 3, 27,
    //    228, 138, 172, 90, 66, 24, 170, 250, 67, 175, 140,
    //    62, 101, 249, 32, 78, 240, 178, 122, 8, 61, 76, 252,
    //    139, 94, 81, 191, 92, 70, 15, 64, 129, 197, 20, 245,
    //    37, 174, 238, 130, 160, 209, 85, 71, 22, 116, 105, 136,
    //    76, 101, 203, 133, 143, 55, 79, 229, 1, 116, 135, 72, 141,
    //    40, 125, 55, 94, 13, 149, 137, 48, 242, 134, 127, 152, 214,
    //    103, 84, 83, 212, 173, 105, 20, 20, 157, 252, 178, 165, 26,
    //    135, 161, 92, 77, 132, 79, 198, 240, 224, 18, 183, 141, 201,
    //    244, 70, 235, 58, 221, 183, 234, 187, 216, 169, 125, 161, 92,
    //    113, 149, 51, 1, 27, 166, 95, 50, 135, 189, 82, 59, 19, 11,
    //    175, 151, 31, 1, 137, 85, 60, 181, 170, 69, 164, 84, 144, 33,
    //    135, 157, 20, 218, 38, 225, 120, 79, 182, 21, 161, 16, 37, 82,
    //    23, 226, 100, 165, 18, 145, 28, 153, 155, 106, 12, 32, 43, 98
    //};
    //const int permutationCount = 255;

    //private static Vector3[] directions = {
    //    new Vector3( 1f, 1f, 0f),
    //    new Vector3(-1f, 1f, 0f),
    //    new Vector3( 1f,-1f, 0f),
    //    new Vector3(-1f,-1f, 0f),
    //    new Vector3( 1f, 0f, 1f),
    //    new Vector3(-1f, 0f, 1f),
    //    new Vector3( 1f, 0f,-1f),
    //    new Vector3(-1f, 0f,-1f),
    //    new Vector3( 0f, 1f, 1f),
    //    new Vector3( 0f,-1f, 1f),
    //    new Vector3( 0f, 1f,-1f),
    //    new Vector3( 0f,-1f,-1f),

    //    new Vector3( 1f, 1f, 0f),
    //    new Vector3(-1f, 1f, 0f),
    //    new Vector3( 0f,-1f, 1f),
    //    new Vector3( 0f,-1f,-1f)
    //};

    //private const int directionCount = 15;

    //private float scalar(Vector3 a, Vector3 b)
    //{
    //    return a.x * b.x + a.y * b.y + a.z * b.z;
    //}

    //private float smoothDistance(float d)
    //{
    //    return d * d * d * (d * (d * 6f - 15f) + 10f);
    //}
    //public float get3DPerlinNoise(Vector3 point, float frequency)
    //{
    //    point *= frequency;

    //    int flooredPointX0 = Mathf.FloorToInt(point.x);
    //    int flooredPointY0 = Mathf.FloorToInt(point.y);
    //    int flooredPointZ0 = Mathf.FloorToInt(point.z);

    //    float distanceX0 = point.x - flooredPointX0;
    //    float distanceY0 = point.y - flooredPointY0;
    //    float distanceZ0 = point.z - flooredPointZ0;

    //    float distanceX1 = distanceX0 - 1f;
    //    float distanceY1 = distanceY0 - 1f;
    //    float distanceZ1 = distanceZ0 - 1f;

    //    flooredPointX0 &= permutationCount;
    //    flooredPointY0 &= permutationCount;
    //    flooredPointZ0 &= permutationCount;

    //    int flooredPointX1 = flooredPointX0 + 1;
    //    int flooredPointY1 = flooredPointY0 + 1;
    //    int flooredPointZ1 = flooredPointZ0 + 1;

    //    int permutationX0 = permutation[flooredPointX0];
    //    int permutationX1 = permutation[flooredPointX1];

    //    int permutationY00 = permutation[permutationX0 + flooredPointY0];
    //    int permutationY10 = permutation[permutationX1 + flooredPointY0];
    //    int permutationY01 = permutation[permutationX0 + flooredPointY1];
    //    int permutationY11 = permutation[permutationX1 + flooredPointY1];
    //    /*
    //            int permutationZ000 = permutation[permutationY00 + flooredPointZ0];
    //            int permutationZ100 = permutation[permutationY10 + flooredPointZ0];
    //            int permutationZ010 = permutation[permutationY01 + flooredPointZ0];
    //            int permutationZ110 = permutation[permutationY11 + flooredPointZ0];
    //            int permutationZ001 = permutation[permutationY00 + flooredPointZ1];
    //            int permutationZ101 = permutation[permutationY01 + flooredPointZ1];
    //            int permutationZ011 = permutation[permutationY10 + flooredPointZ1];
    //            int permutationZ111 = permutation[permutationY11 + flooredPointZ1];
    //    */
    //    Vector3 direction000 = directions[permutation[permutationY00 + flooredPointZ0] & directionCount];
    //    Vector3 direction100 = directions[permutation[permutationY10 + flooredPointZ0] & directionCount];
    //    Vector3 direction010 = directions[permutation[permutationY01 + flooredPointZ0] & directionCount];
    //    Vector3 direction110 = directions[permutation[permutationY11 + flooredPointZ0] & directionCount];
    //    Vector3 direction001 = directions[permutation[permutationY00 + flooredPointZ1] & directionCount];
    //    Vector3 direction101 = directions[permutation[permutationY10 + flooredPointZ1] & directionCount];
    //    Vector3 direction011 = directions[permutation[permutationY01 + flooredPointZ1] & directionCount];
    //    Vector3 direction111 = directions[permutation[permutationY11 + flooredPointZ1] & directionCount];

    //    /*
    //            Vector3 direction000 = directions[permutationZ000 & directionCount];
    //            Vector3 direction100 = directions[permutationZ100 & directionCount];
    //            Vector3 direction010 = directions[permutationZ010 & directionCount];
    //            Vector3 direction110 = directions[permutationZ110 & directionCount];
    //            Vector3 direction001 = directions[permutationZ001 & directionCount];
    //            Vector3 direction101 = directions[permutationZ101 & directionCount];
    //            Vector3 direction011 = directions[permutationZ011 & directionCount];
    //            Vector3 direction111 = directions[permutationZ111 & directionCount];
    //    */

    //    float value000 = scalar(direction000, new Vector3(distanceX0, distanceY0, distanceZ0));
    //    float value100 = scalar(direction100, new Vector3(distanceX1, distanceY0, distanceZ0));
    //    float value010 = scalar(direction010, new Vector3(distanceX0, distanceY1, distanceZ0));
    //    float value110 = scalar(direction110, new Vector3(distanceX1, distanceY1, distanceZ0));
    //    float value001 = scalar(direction001, new Vector3(distanceX0, distanceY0, distanceZ1));
    //    float value101 = scalar(direction101, new Vector3(distanceX1, distanceY0, distanceZ1));
    //    float value011 = scalar(direction011, new Vector3(distanceX0, distanceY1, distanceZ1));
    //    float value111 = scalar(direction111, new Vector3(distanceX1, distanceY1, distanceZ1));

    //    float smoothDistanceX = smoothDistance(distanceX0);
    //    float smoothDistanceY = smoothDistance(distanceY0);
    //    float smoothDistanceZ = smoothDistance(distanceZ0);

    //    return Mathf.Lerp(
    //        Mathf.Lerp(Mathf.Lerp(value000, value100, smoothDistanceX), Mathf.Lerp(value010, value110, smoothDistanceX), smoothDistanceY),
    //        Mathf.Lerp(Mathf.Lerp(value001, value101, smoothDistanceX), Mathf.Lerp(value011, value111, smoothDistanceX), smoothDistanceY),
    //        smoothDistanceZ);
    //}


}
