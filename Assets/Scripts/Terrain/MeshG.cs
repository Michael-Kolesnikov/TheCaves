using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshG : MonoBehaviour
{
    public GameObject go;
    private void Update()
    {
        var mF = go.GetComponent<MeshFilter>();
        var mesh = new Mesh();

        mesh.vertices = new Vector3[] {
        new Vector3(0,10* 4,0),
        new Vector3(20 * 4,10* 4,0),
        new Vector3(20* 4,10* 4,20* 4),
        new Vector3(0,10* 4,20* 4),
        new Vector3(10* 4,0,10* 4),

        };
        mesh.triangles = new int[] {0,4,1, 1,4,2, 2,4,3, 3,4,0, 0,1,3,3,1,2};
        mesh.RecalculateNormals();
        mesh.RecalculateBounds();
        mF.mesh = mesh;
    }
}

