using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateObject : MonoBehaviour
{
    public enum MovementAxis { X, Y }

    public float rotateSpeed = 50f; // Szybkość obrotu w stopniach na sekundę
    public float verticalMovementRange = 0.5f; // Zakres ruchu góra-dół
    public float verticalMovementSpeed = 1.0f; // Szybkość ruchu góra-dół
    public MovementAxis movementAxis = MovementAxis.X; // Oś ruchu góra-dół

    private Vector3 initialPosition;

    void Start()
    {
        initialPosition = transform.position; // Zapisujemy początkową pozycję obiektu
    }

    void Update()
    {
        // Obracaj obiekt wokół osi Y
        transform.Rotate(0, rotateSpeed * Time.deltaTime, 0);

        // Ruch góra-dół wzdłuż wybranej osi
        float movementValue = Mathf.Sin(Time.time * verticalMovementSpeed) * verticalMovementRange;

        switch (movementAxis)
        {
            case MovementAxis.X:
                transform.position = new Vector3(initialPosition.x + movementValue, transform.position.y, transform.position.z);
                break;
            case MovementAxis.Y:
                transform.position = new Vector3(transform.position.x, initialPosition.y + movementValue, transform.position.z);
                break;
        }
    }
}
