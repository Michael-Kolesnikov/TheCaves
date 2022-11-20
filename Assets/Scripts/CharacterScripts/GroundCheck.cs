    using UnityEngine;

public class GroundCheck : MonoBehaviour
{

    public bool isGrounded ;
    public Transform groundCheck;
    private float groundDistance = 0.2f;
    public LayerMask groundLayerMask;

    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundLayerMask);
    }
}
