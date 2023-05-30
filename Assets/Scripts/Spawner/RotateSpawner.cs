using UnityEngine;

public class RotateSpawner : MonoBehaviour
{
    public Transform spawnObjectPrefab;
    public float floatStrength = 1f;
    public float rotationSpeed = 10f;

    private Vector3 startPosition;

    void Start()
    {
        startPosition = transform.position;
    }

    void FixedUpdate()
    {
        transform.position = new Vector3(transform.position.x, startPosition.y + ((float)Mathf.Sin(Time.time) * floatStrength) * 0.4f, transform.position.z);
        transform.Rotate(Vector3.up * Time.deltaTime * rotationSpeed);
    }
}
