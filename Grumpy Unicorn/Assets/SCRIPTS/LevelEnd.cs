using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelEnd : MonoBehaviour
{
    public int requiredCarrots;
    public string nextSceneName;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerController player = other.GetComponent<PlayerController>();
            if (player.CarrotsCollected < requiredCarrots)
            {
                RestartGame();
            }
            else
            {
                SceneManager.LoadScene(nextSceneName);
            }
        }
    }

    private void RestartGame()
    {
        GameSaveManager.Instance.ResetGame();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
