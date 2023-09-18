using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowDownButton : MonoBehaviour
{
    public void OnButtonPress()
    {
        StartCoroutine(SlowDownEnemies());
    }

    private IEnumerator SlowDownEnemies()
    {
        // Find all enemies and set isSlowed to true
        EnemyController[] enemies = FindObjectsOfType<EnemyController>();
        foreach (EnemyController enemy in enemies)
        {
            enemy.isSlowed = true;
        }

        yield return new WaitForSeconds(10f); // Wait for 10 seconds

        // After 10 seconds, set isSlowed back to false
        foreach (EnemyController enemy in enemies)
        {
            enemy.isSlowed = false;
        }
    }
}
