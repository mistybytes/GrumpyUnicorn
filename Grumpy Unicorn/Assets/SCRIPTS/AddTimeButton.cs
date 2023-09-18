using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddTimeButton : MonoBehaviour
{
    public float additionalTime = 5f;

    public void OnButtonPress()
    {
        var levelTimer = FindObjectOfType<LevelTimer>();
        if (levelTimer != null)
        {
            levelTimer.AddTime(additionalTime);
        }
    }
}
