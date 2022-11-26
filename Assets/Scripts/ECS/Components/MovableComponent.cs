using System;
using UnityEngine;
using Leopotam.EcsLite;

[Serializable]
public struct MovableComponent
{
    public CharacterController characterController;
    public float defaultSpeed;
    public float gravity;
    public Vector3 velocity;
    public bool canMove;

}
