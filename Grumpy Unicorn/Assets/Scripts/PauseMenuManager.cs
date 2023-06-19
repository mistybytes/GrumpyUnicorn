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

        if (store.activeSelf == true) // Upewniamy siê, ¿e sklep jest niewidoczny na pocz¹tku
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
            // Je¿eli potrzebujesz odœwie¿yæ grê po wczytaniu, zrób to tutaj.
        }
    }

    public void ResumeGame()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1;
        isPaused = false;
    }

    public void OpenStore() // Nowa metoda otwieraj¹ca sklep
    {
        store.SetActive(true);
        pauseMenu.SetActive(false);
    }
}
