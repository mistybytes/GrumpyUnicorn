using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangePlayerSkinButton : MonoBehaviour
{
    public SkinManager skinManager;

    public void ChangePlayerSkin()
    {
        PlayerController playerController = FindObjectOfType<PlayerController>();
        if (playerController != null && skinManager != null)
        {
            skinManager.ChangePlayerSkin(playerController);
        }
        else
        {
            Debug.LogError("PlayerController or SkinManager not found.");
        }
    }
}

