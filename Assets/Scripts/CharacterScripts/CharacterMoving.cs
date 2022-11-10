using UnityEngine;

public class CharacterMoving : MonoBehaviour
{
    public CharacterController characterController;
    Player player;
    GroundCheck check;
    private Vector3 _velocity;
    private float _runSpeed;
    public float moveSpeed = 5f;
    private float gravity = -19.62f;
    private float jumpHeight = 1f;
    public static bool isMove = true;
    public bool canRun;

    public KeyCode runningKey = KeyCode.LeftShift;

    private void Start()
    {
        check = GetComponentInChildren<GroundCheck>();
        player = GetComponentInParent<Player>();
        _runSpeed = moveSpeed * 1.15f;
    }
    void Update()
    {
        if (!isMove)
            return;
        Run();
        Jump();
        VelocityMove();
        ChangeRunningState();
    }
    private void Run()
    {
        float characterSpeed = moveSpeed;
        Vector3 move = Input.GetAxis("Horizontal") * transform.right + Input.GetAxis("Vertical") * transform.forward;

        if (Input.GetKey(runningKey) && canRun)
        {
            characterSpeed = _runSpeed;
            player.DecreaseStamina(0.2f);
        }
        move = Vector3.ClampMagnitude(move, moveSpeed);
        characterController.Move(move * characterSpeed * Time.deltaTime);
    }
    private void Jump()
    {
        if (Input.GetButton("Jump") && check.isGrounded)
        {  
            _velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }
    }
    private void VelocityMove()
    {
        _velocity.y += gravity * Time.deltaTime;
        if (check.isGrounded && _velocity.y < 0)
            _velocity.y = 0f;
        characterController.Move(_velocity * Time.deltaTime);
    }
    private void ChangeRunningState()
    {
        canRun = player.CurrentStamina <=0 ? false : true;
    }
}