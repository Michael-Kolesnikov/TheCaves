using UnityEngine;

public class Squat : MonoBehaviour
{
    public Transform camera;
    public Transform character;
    private Vector3 startPosition;
    private float startHeight;
    private void Start()
    {
        startPosition = camera.localPosition;
        startHeight = character.GetComponent<CharacterController>().height;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            camera.transform.localPosition = new Vector3(startPosition.x, startPosition.y - 2.3f, startPosition.z);
            character.GetComponent<CharacterController>().height = 1.44f;
            character.GetComponent<CharacterController>().center = new Vector3(0, -0.67f, 0);
        }
        if (Input.GetKeyUp(KeyCode.LeftControl))
        {
            camera.transform.localPosition = startPosition;
            character.GetComponent<CharacterController>().height = startHeight;
            character.GetComponent<CharacterController>().center = new Vector3(0, 0, 0);
        }
    }
}
