using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenuManager : MonoBehaviour
{
    public GameObject pauseMenu;
    public GameObject store; // Nowa zmienna dla obiektu sklepu
    public PlayerController playerController;
    private bool isPaused = false;

    private void Start()
    {
        if (pauseMenu.activeSelf == true)
        {
            pauseMenu.SetActive(false);
        }

        if (store.activeSelf == true) // Upewniamy si�, �e sklep jest niewidoczny na pocz�tku
        {
            store.SetActive(false);
        }
    }

    public void SaveButton()
    {
        if (playerController != null)
        {
            GameSaveManager.Instance.SaveGame(playerController);
        }
    }


    public void LoadButton()
    {
        if (playerController != null)
        {
            GameSaveManager.Instance.LoadGame(playerController);
            playerController.UpdateCarrotText();
            // Je�eli potrzebujesz od�wie�y� gr� po wczytaniu, zr�b to tutaj.
        }
    }

    public void ResumeGame()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1;
        isPaused = false;
    }

    public void OpenStore() // Nowa metoda otwieraj�ca sklep
    {
        store.SetActive(true);
        pauseMenu.SetActive(false);
    }
}
