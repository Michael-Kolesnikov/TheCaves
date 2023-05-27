using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Falled : MonoBehaviour
{
    private void Update()
    {
        if(this.transform.position.y < -200)
        {
            Destroy(this.gameObject);
        }
    }
}
