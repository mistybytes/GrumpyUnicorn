using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class StoreItem
{
    public string itemName; // name of the item
    public int itemCost; // cost of the item in carrots

    public StoreItem(string name, int cost)
    {
        itemName = name;
        itemCost = cost;
    }
}
