using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    Transform character;
    public float mouseSensitivity = 300f;
    private float xRotation = 0f;
    public static bool isMove = true;
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }
     
    void Update()
    {
        if (!isMove) return;
        float MouseX = Input.GetAxisRaw("Mouse X") * mouseSensitivity * Time.deltaTime;
        float MouseY = Input.GetAxisRaw("Mouse Y") * mouseSensitivity * Time.deltaTime;

        //поворот вверх вниз
        xRotation -= MouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        character.Rotate(Vector3.up * MouseX);

    }
}
