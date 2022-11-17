using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dig : MonoBehaviour
{
    Camera mainCamera;
    float reachDistance = 3f;
    private void Start()
    {
        mainCamera = Camera.main;
    }
    private void Update()
    {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, reachDistance))
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                var hittingObject = hit.collider?.gameObject.GetComponent<DestroyObject>();
                if (hittingObject != null)
                    Debug.Log("МОжно разрушить");
                else
                    Debug.Log("Нельзя разрушить");
            }
        }
    }
}
