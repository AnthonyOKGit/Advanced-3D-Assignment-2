using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventoryItem: MonoBehaviour
{
    public string itemName;
    public TextMeshProUGUI itemText;
    public int amount;

    public void SetItem(string name, int amount)
    {
        itemName = name;
        this.amount = amount;
    }

    public void AddAmount(int amount)
    {
        this.amount += amount;
    }

    void Update()
    {
        itemText.text = itemName + " x" + amount;
    }
}
