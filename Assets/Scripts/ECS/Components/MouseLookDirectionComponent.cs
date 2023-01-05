using System;
using UnityEngine;

[Serializable]
public struct MouseLookDirectionComponent
{
    public Transform cameraTransform;
    [HideInInspector] public Vector2 direction;
    public float mouseSensitivity;
    public bool canMove;
}
