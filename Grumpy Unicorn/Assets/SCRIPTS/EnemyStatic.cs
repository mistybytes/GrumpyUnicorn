using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyStatic : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        // Jeśli chcesz, aby tylko gracz mógł wywołać restart, odkomentuj poniższe linie:
        // if (!other.gameObject.CompareTag("Player")) return;

        RestartGame();
    }

    private void RestartGame()
    {
        // Zakładam, że GameSaveManager.Instance.ResetGame(); jest potrzebne,
        // jeśli nie, po prostu usuń tę linię.
        GameSaveManager.Instance.ResetGame();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}


