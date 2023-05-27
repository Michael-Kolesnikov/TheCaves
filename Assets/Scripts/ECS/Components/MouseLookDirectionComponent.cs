using System;
using TMPro;
using UnityEngine;

[Serializable]
public struct MouseLookDirectionComponent
{
    public Transform cameraTransform;
    [HideInInspector]
    public Vector2 direction;
    public Transform mouseSensitivityNumberText;
    [HideInInspector]
    public float mouseSensitivity => int.Parse(mouseSensitivityNumberText.GetComponent<TMP_Text>().text);
    public bool canMove;
}
