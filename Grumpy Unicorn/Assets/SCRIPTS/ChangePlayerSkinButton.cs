using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangePlayerSkinButton : MonoBehaviour
{
    public PlayerController playerController;

    public void OnButtonClick()
    {
        if (playerController != null)
        {
            playerController.ChangePlayerSkin();
        }
        else
        {
            Debug.LogError("PlayerController not found.");
        }
    }
}
