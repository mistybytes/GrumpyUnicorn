using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target;
    public float smoothing = 5f;
    public Vector3 offset; // teraz mo�esz ustawi� ten offset w inspektorze

    void Start()
    {
        // Je�li nie ustawisz offsetu w inspektorze, ten kod b�dzie dzia�a� jak wcze�niej
        if (offset == Vector3.zero)
        {
            offset = transform.position - target.position;
        }
    }

    void FixedUpdate()
    {
        Vector3 targetCamPos = target.position + offset;
        transform.position = Vector3.Lerp(transform.position, targetCamPos, smoothing * Time.deltaTime);
        transform.LookAt(target); // Upewnij si�, �e kamera patrzy na cel
    }
}



