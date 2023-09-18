using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StoreManager : MonoBehaviour
{
    public int coins;
    public TMP_Text coinUI;
    public StoreItem[] shopItemsSO;
    public GameObject[] shopPanelGO;
    public ShopTemplate[] shopPanels;
    public Button[] muPurchaseBtns;

    void Start()
    {
        for (int i = 0; i < shopItemsSO.Length; i++)
        {
            shopPanelGO[i].SetActive(true);
        }

        coinUI.text = "Dollars: " + coins.ToString();
        LoadPanels();
        CheckPurchaseble();
    }

    public void AddCoins()
    {
        coins++;
        coinUI.text = "Dollars: " + coins.ToString();
        CheckPurchaseble();
    }

    public void CheckPurchaseble()
    {
        for (int i = 0; i < shopItemsSO.Length; i++)
        {
            if (shopItemsSO[i] != null && muPurchaseBtns[i] != null)
            {
                muPurchaseBtns[i].interactable = coins >= shopItemsSO[i].baseCost;
            }
        }
    }

    public void PurchaseItem(int btnNo)
    {
        if (shopItemsSO[btnNo] != null && coins >= shopItemsSO[btnNo].baseCost)
        {
            coins -= shopItemsSO[btnNo].baseCost;
            coinUI.text = "Coins: " + coins.ToString();
            CheckPurchaseble();
        }
    }

    public void LoadPanels()
    {
        for (int i = 0; i < shopItemsSO.Length; i++)
        {
            if (shopItemsSO[i] != null && shopPanels[i] != null)
            {
                shopPanels[i].titleTxt.text = shopItemsSO[i].title;
                shopPanels[i].descriptionTxt.text = shopItemsSO[i].description;
                shopPanels[i].costTxt.text = "Coins: " + shopItemsSO[i].baseCost.ToString();
            }
        }
    }
}
