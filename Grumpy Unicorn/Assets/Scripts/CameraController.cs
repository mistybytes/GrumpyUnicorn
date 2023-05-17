using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target;
    public float smoothing = 5f;
    public Vector3 offset; // teraz mo¿esz ustawiæ ten offset w inspektorze

    void Start()
    {
        // Jeœli nie ustawisz offsetu w inspektorze, ten kod bêdzie dzia³a³ jak wczeœniej
        if (offset == Vector3.zero)
        {
            offset = transform.position - target.position;
        }
    }

    void FixedUpdate()
    {
        Vector3 targetCamPos = target.position + offset;
        transform.position = Vector3.Lerp(transform.position, targetCamPos, smoothing * Time.deltaTime);
        transform.LookAt(target); // Upewnij siê, ¿e kamera patrzy na cel
    }
}



