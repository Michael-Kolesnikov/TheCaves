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
    public bool canRun;
    Player player;

    public KeyCode runningKey = KeyCode.LeftShift;

    private void Start()
    {
        check = GetComponentInChildren<GroundCheck>();
        player = GetComponentInParent<Player>();
        runSpeed = moveSpeed * 2.15f;
    }
    void Update()
    {
        if (!isMove) return;
        float characterSpeed =  moveSpeed;
        velocity.y += gravity * Time.deltaTime;

        Vector3 move = Input.GetAxis("Horizontal") * transform.right + Input.GetAxis("Vertical") * transform.forward;
        //Run
        if (Input.GetKey(runningKey) && canRun)
        {
            characterSpeed = runSpeed;
            player.DecreaseStamina(10f * Time.deltaTime);
        }
        move = Vector3.ClampMagnitude(move, moveSpeed);
        characterController.Move(move * characterSpeed * Time.deltaTime);


        //Jump
        if (Input.GetButton("Jump") && check.isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }
        if (check.isGrounded && velocity.y < 0)
        {
            velocity.y = 0f;
        }

        characterController.Move(velocity * Time.deltaTime);

        canRun = !(player.CurrentStamina == 0);
        Debug.Log(canRun);
    }
}