using System.Collections;
using System.Collections.Generic;
using Leopotam.EcsLite;
using UnityEngine;

public class PKM : IEcsRunSystem
{
    public void Run(EcsSystems systems)
    {
        var filter = systems.GetWorld().Filter<PlayerTag>().End();
        foreach (var entity in filter)
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, 100))
            {
                if (Input.GetMouseButton(1))
                {
                    if (hit.collider.gameObject.GetComponent<MeshFilter>() != null)
                    {
                        Debug.Log("PRAVAYA");
                        Mesh terrainMesh = hit.collider.gameObject.GetComponent<MeshFilter>().mesh;
                        Vector3[] vertices = terrainMesh.vertices;
                        var newVert = new Vector3[vertices.Length + 1];
                        Vector3 direction = hit.point - Camera.main.transform.position;
                        direction.Normalize();
                        for (var i = 0; i< vertices.Length; i++)
                        {
                            float distance = Vector3.Distance(hit.point, hit.collider.gameObject.transform.TransformPoint(vertices[i]));
                            if (distance < 0.4f)
                            {
                                //ѕробегатьс€ циклом по вершине и одновремнно циклом по треугольнкиам, в радиусе переделать то что нужно.
                            }
                        }

                        terrainMesh.vertices = newVert;
                        terrainMesh.RecalculateNormals();
                        terrainMesh.RecalculateBounds();
                        terrainMesh.Optimize();
                        hit.collider.gameObject.GetComponent<MeshFilter>().sharedMesh = terrainMesh;

                    }
                }
            }
        }
    }

}
