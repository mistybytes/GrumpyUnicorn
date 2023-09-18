using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupController : MonoBehaviour
{
    public GameObject popupPanel; // przypisz w inspektorze obiekt Panel
    public float delay = 3.0f;   // czas po jakim popup zniknie

    void Start()
    {
        StartCoroutine(ShowAndHidePopup());
    }

    IEnumerator ShowAndHidePopup()
    {
        popupPanel.SetActive(true);
        yield return new WaitForSeconds(delay);
        popupPanel.SetActive(false);
    }
}
