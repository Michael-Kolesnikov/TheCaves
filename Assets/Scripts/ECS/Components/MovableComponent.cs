using System;
using UnityEngine;

[Serializable]
public struct MovableComponent
{
    public CharacterController characterController;
    public float speed;
    public float gravity;
    public Vector3 velocity;
}
