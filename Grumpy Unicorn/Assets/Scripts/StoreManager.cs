using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoreManager : MonoBehaviour
{
    public List<StoreItem> storeItems; // list of items in the store
    public PlayerController playerController; // reference to the player controller
    public GameObject storePanel; // reference to the store panel UI

    public bool BuyItem(StoreItem item)
    {
        // check if player has enough carrots
        if (playerController.carrotsCollected >= item.itemCost)
        {
            // deduct the cost and return true
            playerController.carrotsCollected -= item.itemCost;
            return true;
        }
        else
        {
            // player doesn't have enough carrots, return false
            return false;
        }
    }

    public void OpenStore()
    {
        // set the store panel active
        storePanel.SetActive(true);
    }

    public void CloseStore()
    {
        // set the store panel inactive
        storePanel.SetActive(false);
    }
}
