using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public void StartNewGame()
    {
        GameSaveManager.Instance.ResetAllSaves();
        SceneManager.LoadScene("SCENE1");
    }
}

