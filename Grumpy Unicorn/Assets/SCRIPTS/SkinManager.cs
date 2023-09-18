using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkinManager : MonoBehaviour
{
    public GameObject playerObject;
    public Material[] playerSkins;

    private Renderer playerRenderer;
    private int currentSkinIndex = 0;

    private void Start()
    {
        playerRenderer = playerObject.GetComponent<Renderer>();
        if (playerRenderer == null)
        {
            Debug.LogError("Player object does not have a Renderer component.");
        }
        else
        {
            // Ustaw pocz�tkow� sk�rk� gracza
            SetPlayerSkin(currentSkinIndex);
        }
    }

    public void ChangePlayerSkin(PlayerController playerController)
    {
        // Zwi�ksz indeks sk�rki gracza
        currentSkinIndex++;
        if (currentSkinIndex >= playerSkins.Length)
        {
            currentSkinIndex = 0;
        }

        // Ustaw now� sk�rk� gracza
        SetPlayerSkin(currentSkinIndex);
    }

    private void SetPlayerSkin(int index)
    {
        if (index < 0 || index >= playerSkins.Length)
        {
            Debug.LogError("Invalid player skin index.");
            return;
        }

        // Ustaw nowy materia� na graczu
        playerRenderer.material = playerSkins[index];
    }
}
