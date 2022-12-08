using Leopotam.EcsLite;
using System.Linq;
using UnityEngine;
using System.Collections.Generic;

public sealed class PlayerDigSystem : IEcsRunSystem
{
    private Vector3[] modifVertices; 
    public void Run(EcsSystems system)
    {
        var filter = system.GetWorld().Filter<PlayerTag>().End();

        foreach(var entity in filter)
        {
            float radius = 3f;

            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, 100))
            {
                if(Input.GetMouseButton(0))
                {
                    if (hit.collider.gameObject.GetComponent<Chunk>() != null)
                    {
                        var mf = hit.collider.gameObject.GetComponent<MeshFilter>();
                        var chunkMesh = mf.mesh;
                        modifVertices = chunkMesh.vertices;

                        for (var i = 0; i < modifVertices.Length; i++)
                        {
                            Vector3 distance = modifVertices[i] - hit.point;
                            if (distance.sqrMagnitude < radius)
                            {
                                modifVertices[i] = modifVertices[i] + new Vector3(1, 0, 1);
                            }
                        }
                        mf.mesh.vertices = modifVertices;
                        hit.collider.gameObject.GetComponent<MeshCollider>().sharedMesh = mf.mesh;
                        mf.mesh.RecalculateNormals();
                        mf.mesh.RecalculateBounds();




                    }
                }
            }

        }

    }
}
