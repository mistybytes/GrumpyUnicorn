using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSaveManager : MonoBehaviour
{
    public static GameSaveManager Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SaveGame(PlayerController playerController)
    {
        PlayerPrefs.SetInt("Carrots", playerController.CarrotsCollected);
        PlayerPrefs.Save();
    }

    public void LoadGame(PlayerController playerController)
    {
        playerController.CarrotsCollected = PlayerPrefs.GetInt("Carrots", 0);
    }

    public void ResetGame()
    {
        PlayerPrefs.DeleteKey("Carrots");
        PlayerPrefs.Save();
    }

    public void ResetAllSaves()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();
    }
}
