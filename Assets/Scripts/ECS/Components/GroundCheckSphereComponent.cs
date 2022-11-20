using System;
using UnityEngine;

[Serializable]
public struct GroundCheckSphereComponent
{
    public LayerMask groundMask;
    public Transform groundCheckSphere;
    public float groundCheckRadius;
    public bool isGrounded;
}
