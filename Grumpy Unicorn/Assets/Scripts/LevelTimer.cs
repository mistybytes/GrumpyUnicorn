using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LevelTimer : MonoBehaviour
{
    public float timeLeft = 120f;
    public TextMeshProUGUI timerText;

    void Update()
    {
        timeLeft -= Time.deltaTime;
        timerText.text = "Czas: " + Mathf.Max(0, timeLeft).ToString("0");

        if (timeLeft <= 0)
        {
            var playerController = FindObjectOfType<PlayerController>();
            if (playerController != null)
            {
                playerController.GameOver();
            }
        }
    }

    public void AddTime(float additionalTime)
    {
        timeLeft += additionalTime;
    }
}
