using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public void StartNewGame()
    {
        SceneManager.LoadScene("FARM 0");
    }
}

