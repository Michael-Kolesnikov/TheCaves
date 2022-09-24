using UnityEngine;

public class CharacterMoving : MonoBehaviour
{
    public CharacterController characterController;
    GroundCheck check;
    private Vector3 velocity;
    private float runSpeed;
    public float moveSpeed = 5f;
    private float gravity = -19.62f;
    private float jumpHeight = 1f;
    public static bool isMove = true;

    public KeyCode runningKey = KeyCode.LeftShift;

    private void Start()
    {
        check = GetComponentInChildren<GroundCheck>();
        runSpeed = moveSpeed * 1.15f;
    }
    void Update()
    {
        if (!isMove) return;
        float characterSpeed =  moveSpeed;
        velocity.y += gravity * Time.deltaTime;

        Vector3 move = Input.GetAxis("Horizontal") * transform.right + Input.GetAxis("Vertical") * transform.forward;
        if (Input.GetKey(runningKey))
        {
            characterSpeed = runSpeed;
        }
        move = Vector3.ClampMagnitude(move, moveSpeed);
        characterController.Move(move * characterSpeed * Time.deltaTime);



        if (Input.GetButtonDown("Jump") && check.isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }
        if (check.isGrounded && velocity.y < 0)
        {
            velocity.y = 0f;
        }

        characterController.Move(velocity * Time.deltaTime);
    }
}