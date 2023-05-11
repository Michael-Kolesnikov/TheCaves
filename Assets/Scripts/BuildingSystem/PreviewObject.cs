using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static PlasticGui.LaunchDiffParameters;

public class PreviewObject : MonoBehaviour
{
    public Material green;
    public Material red;
    public bool isBuildeable;

    public List<Collider> colliders = new();
    private void Update()
    {
        ChangeColor();
    }
    private void OnTriggerEnter(Collider other)
    {
        colliders.Add(other);
    }
    private void OnTriggerExit(Collider other)
    {
        colliders.Remove(other);
    }

    private void ChangeColor()
    {
        if(colliders.Count != 0)
        {
            isBuildeable = false;
            ChangechildrenColor(red);

        }
        else
        {
            isBuildeable = true;
            ChangechildrenColor(green);
        }
    }
    private void ChangechildrenColor(Material material)
    {
        if(this.transform.childCount != 0)
        {
            foreach (Transform child in this.transform)
            {
                var materials = child.GetComponent<Renderer>().materials;
                for (var i = 0; i < materials.Length; i++)
                {
                    materials[i] = material;
                }
                child.GetComponent<Renderer>().materials = materials;
            }
        }
        else
        {
            var materials = this.GetComponent<Renderer>().materials;

            for (var i = 0; i < materials.Length; i++)
            {
                materials[i] = material;
            }
            this.GetComponent<Renderer>().materials = materials;
        }
        
    }
}
