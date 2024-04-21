using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName="ItemData", menuName = "Inventory/Item")]
public class InventItem : ScriptableObject
{
    public string Name = "Item";
    public Sprite icon;
    public int price;
}
