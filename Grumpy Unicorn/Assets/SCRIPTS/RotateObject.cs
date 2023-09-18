using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateObject : MonoBehaviour
{
    public float rotateSpeed = 50f; // Szybko�� obrotu w stopniach na sekund�

    void Update()
    {
        // Obracaj obiekt wok� osi Y
        transform.Rotate(0, rotateSpeed * Time.deltaTime, 0);
    }
}

