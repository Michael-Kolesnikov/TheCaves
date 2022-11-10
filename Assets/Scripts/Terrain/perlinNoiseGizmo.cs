using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


public class perlinNoiseGizmo : MonoBehaviour
{
    public bool cubeBoundries = false;
    public int size = 10;
    public int height = 20;
    public int width = 20;
    public int depth = 20;

    public float frequency = 1.01f;
    private Vector3[] points;

    void Start()
    {
        InstantiatePoints();
    }

    private void FixedUpdate()
    {
        UpdateMesh();
    }
    void UpdateMesh()
    {
        if (cubeBoundries)
        {
            height = size;
            width = size;
            depth = size;
        }
        InstantiatePoints();
    }

    void InstantiatePoints()
    {
        points = new Vector3[height * width * depth];

        for (int z = 0, i = 0; z < depth; z++)
        {
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    points[i] = new Vector3(x, y, z);
                    i++;
                }
            }
        }
    }

    private void OnDrawGizmos()
    {
        if (points != null)
        {
            Debug.Log(points.Length);
            for (int i = 0; i < points.Length; i++)
            {

                float sample = perlinNoise.get3DPerlinNoise(points[i], frequency);

                sample = (sample + 1f) / 2f;
                Gizmos.color = new Color(sample, sample, sample, 1);

                Gizmos.DrawSphere(points[i], 0.3f);
            }
        }
    }

    

}
