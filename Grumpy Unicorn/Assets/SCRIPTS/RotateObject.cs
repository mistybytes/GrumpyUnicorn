using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateObject : MonoBehaviour
{
    public float rotateSpeed = 50f; // Szybkoœæ obrotu w stopniach na sekundê

    void Update()
    {
        // Obracaj obiekt wokó³ osi Y
        transform.Rotate(0, rotateSpeed * Time.deltaTime, 0);
    }
}

