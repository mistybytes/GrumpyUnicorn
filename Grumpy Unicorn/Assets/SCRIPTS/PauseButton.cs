using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseButton : MonoBehaviour
{
    public GameObject pauseMenu;
    private bool isPaused = false;

    public void OnClick()
    {
        StartCoroutine(TogglePauseMenuWithDelay());
    }

    private IEnumerator TogglePauseMenuWithDelay()
    {
        yield return new WaitForSecondsRealtime(0.1f);
        TogglePauseMenu();
    }

    private void TogglePauseMenu()
    {
        isPaused = !isPaused;
        pauseMenu.SetActive(isPaused);
        Time.timeScale = isPaused ? 0f : 1f;
    }
}
