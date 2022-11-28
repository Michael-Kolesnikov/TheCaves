 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PERLIN : MonoBehaviour
{
    public GameObject prefab;
    [SerializeField]
    float noiseScale = 0.17f;
    [SerializeField]
    int length = 50;
    [SerializeField] [Range(-1, 1)]
    float ignoreValue;
    private List<GameObject> list = new List<GameObject>();
    [SerializeField]
    private int offsetX;

    public static float GetPerlin(float x, float y, float z)
    {
        var ab = Mathf.PerlinNoise(x, y);
        var bc = Mathf.PerlinNoise(y, z);
        var ac = Mathf.PerlinNoise(x, z);


        var ba = Mathf.PerlinNoise(y, x);
        var cb = Mathf.PerlinNoise(z, y);
        var ca = Mathf.PerlinNoise(z, x);

        return (ab + bc + ac + ba + cb + ca) / 6f;
    }
    private void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            Debug.Log("CAL");
            foreach (var item in list)
                Destroy(item);

            for (int x = 0 + offsetX; x < length + offsetX; x++)
            {
                for (int y = 0; y < length; y++)
                {
                    for (int z = 0; z < length; z++)
                    {
                        if (GetPerlin(x * noiseScale, y * noiseScale, z * noiseScale) >= ignoreValue)
                        {
                            var ob = Instantiate(prefab, new Vector3(x - offsetX, y, z), Quaternion.identity);
                            list.Add(ob);
                        }
                        if(y == length && (new System.Random()).Next(0,2) > 0)
                        {
                            var ob = Instantiate(prefab, new Vector3(x - offsetX, y, z), Quaternion.identity);
                        }
                    }
                }
            }
            offsetX++;
    }
}
}
