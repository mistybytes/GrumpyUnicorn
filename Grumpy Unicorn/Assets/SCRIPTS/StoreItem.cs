using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "shopMenu", menuName = "ScriptableObjects/New Shop Item", order = 1)]
public class StoreItem : ScriptableObject
{
    public string title;
    public string description;
    public int baseCost;  // Zmieni³em nazwê na 'baseCost'
}

