using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarrotPickup : MonoBehaviour
{
    private bool isCollected = false;

    public void Collect()
    {
        isCollected = true;
    }

    public bool IsCollected()
    {
        return isCollected;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerController player = other.GetComponent<PlayerController>();
            if (player != null && !IsCollected())
            {
                player.AddCarrots(1);
                Collect();
                Destroy(gameObject);
            }
        }
    }
}
