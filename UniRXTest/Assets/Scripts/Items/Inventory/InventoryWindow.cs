using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryWindow : MonoBehaviour
{
    [SerializeField] private Inventory targetInventory;
    [SerializeField] private RectTransform itemsPanel;


    void Start()
    {
        
        Redraw();
    }
    private void Redraw()
    {
        for(var i = 0; i < targetInventory.inventoryItems.Count; i++)
        {
            Debug.Log("sdfsdf");
            var item = targetInventory.inventoryItems[i];
            var icon = new GameObject("Icon");
            icon.AddComponent<Image>().sprite = item.icon;
            icon.transform.SetParent(itemsPanel, false);
        }
    }
}
