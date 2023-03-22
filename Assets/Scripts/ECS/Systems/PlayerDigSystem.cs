using Leopotam.EcsLite;
using UnityEngine;

/// <summary>
/// На данный момент не используется, требуется переработка
/// </summary>
public sealed class PlayerDigSystem : IEcsRunSystem
{
    private Vector3[] _modifVertices;
    public void Run(EcsSystems system)
    {
        var filter = system.GetWorld().Filter<PlayerTag>().End();

        foreach (var entity in filter)
        {
            float radius = 3f;

            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, 100))
            {
                if (Input.GetMouseButton(0))
                {
                    if (hit.collider.gameObject.GetComponent<Chunk2>() != null)
                    {
                        var mf = hit.collider.gameObject.GetComponent<MeshFilter>();
                        var chunkMesh = mf.mesh;
                        _modifVertices = chunkMesh.vertices;

                        for (var i = 0; i < _modifVertices.Length; i++)
                        {
                            Vector3 distance = _modifVertices[i] - hit.point;
                            if (distance.sqrMagnitude < radius)
                            {
                                _modifVertices[i] = _modifVertices[i] + new Vector3(1, 0, 1);
                            }
                        }
                        mf.mesh.vertices = _modifVertices;
                        mf.mesh.RecalculateNormals();
                        mf.mesh.RecalculateBounds();
                    }
                }
            }

        }

    }
}
