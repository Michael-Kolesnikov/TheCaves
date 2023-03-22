using Leopotam.EcsLite;
using log4net.Util;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering.VirtualTexturing;

public class LKM : IEcsRunSystem
{

    public void Run(EcsSystems systems)
    {
        var filter = systems.GetWorld().Filter<PlayerTag>().End();
        foreach(var entity in filter)
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, 100))
            {
                if (Input.GetMouseButton(0))
                {
                    if (hit.collider.gameObject.GetComponent<MeshFilter>() != null)
                    {

                    }
                }
            }
        }
    }
    public float deformationRadius = 2f;
    public float deformationDepth = 1f;
}
