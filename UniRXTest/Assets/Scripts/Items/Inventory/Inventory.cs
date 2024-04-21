using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
   public List<InventItem> startItems = new List<InventItem>();
   public List<InventItem> inventoryItems = new List<InventItem>();

    public void Awake()
    {
        for(int i = 0; i < startItems.Count; i++) {
            AddItem(startItems[i]);
        }
    }


    public void AddItem(InventItem item)
    {
        inventoryItems.Add(item);
    }
}

