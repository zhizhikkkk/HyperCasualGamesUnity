using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryWindow : MonoBehaviour
{
    [SerializeField] private Inventory targetInventory;
    [SerializeField] public RectTransform itemsPanel;

    private readonly List<GameObject> drawIcons = new List<GameObject>();


    void Start()
    {
        targetInventory.onItemAdded += OnItemAdded;
        Redraw();
    }
    public void OnItemAdded(InventItem item) => Redraw();
    private void Redraw()
    {
        ClearDrawn();
        for (var i = 0; i < targetInventory.inventoryItems.Count; i++)
        {
            var item = targetInventory.inventoryItems[i];
            var icon = new GameObject("Icon");
            icon.AddComponent<Image>().sprite = item.icon;
            icon.transform.SetParent(itemsPanel.gameObject.transform, false);
            drawIcons.Add(icon);
        }
    }


    void ClearDrawn()
    {
        for(int i = 0; i < drawIcons.Count; i++)
        {
            Destroy(drawIcons[i]);
        }
    }
}
