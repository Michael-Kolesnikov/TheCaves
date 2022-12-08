using UnityEngine;

public class PERLIN
{
    public static float GetPerlin(float x, float y, float z)
    {
        // offset from 0,0 coordinates for avoding symmetry
        x += 1000000;
        z += 1000000;
        var ab = Mathf.PerlinNoise(x, y);
        var bc = Mathf.PerlinNoise(y, z);
        var ac = Mathf.PerlinNoise(x, z);

        var ba = Mathf.PerlinNoise(y, x);
        var cb = Mathf.PerlinNoise(z, y);
        var ca = Mathf.PerlinNoise(z, x);

        return (ab + bc + ac + ba + cb + ca) / 6f;
    }
    
}
