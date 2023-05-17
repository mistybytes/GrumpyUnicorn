using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenuManager : MonoBehaviour
{
    public GameObject pauseMenu;
    public PlayerController playerController;
    private bool isPaused = false;

    private void Start()
    {
        if (pauseMenu.activeSelf == true)
        {
            pauseMenu.SetActive(false);
        }
    }

    public void SaveButton()
    {
        if (playerController != null)
        {
            GameSaveManager.Instance.SaveGame(playerController.CarrotsCollected);
        }
    }

    public void LoadButton()
    {
        if (playerController != null)
        {
            GameSaveManager.Instance.LoadGame(playerController);
            playerController.UpdateCarrotText();
        }
    }

    public void ResumeGame()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1;
        isPaused = false;
    }
}
