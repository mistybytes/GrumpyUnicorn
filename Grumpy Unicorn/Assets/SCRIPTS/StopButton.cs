using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopButton : MonoBehaviour
{
    public void OnButtonPress()
    {
        StartCoroutine(StopEnemies());
    }

    private IEnumerator StopEnemies()
    {
        // Find all enemies and set isStopped to true
        EnemyController[] enemies = FindObjectsOfType<EnemyController>();
        foreach (EnemyController enemy in enemies)
        {
            enemy.isStopped = true;
        }

        yield return new WaitForSeconds(5f); // Wait for 5 seconds

        // After 5 seconds, set isStopped back to false
        foreach (EnemyController enemy in enemies)
        {
            enemy.isStopped = false;
        }
    }
}
