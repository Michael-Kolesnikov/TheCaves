 using UnityEngine;

public class RotateSpawner : MonoBehaviour
{
    public Transform spawnObjectPrefab;
    public float floatStrength = 1f; // Сила подъема объекта
    public float rotationSpeed = 10f; // Скорость вращения объекта

    private Vector3 startPosition;

    void Start()
    {
        startPosition = transform.position; // Сохраняем начальную позицию объекта
    }

    void FixedUpdate()
    {
        // Поднимаем объект вверх и вниз
        transform.position = new Vector3(transform.position.x,
                                         startPosition.y + ((float)Mathf.Sin(Time.time) * floatStrength) * 0.4f,
                                         transform.position.z);

        // Вращаем объект вокруг своей оси
        transform.Rotate(Vector3.up * Time.deltaTime * rotationSpeed);
    }
}
