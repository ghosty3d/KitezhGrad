using UnityEngine;
using System.Collections;

[System.Serializable]
public class InventoryItem
{
    public int ItemID;
    public string ItemName;
    public string ItemDescripction;
    public PickableItem.ItemType CurrentItemType;

    public InventoryItem(int a_ID, string a_Name, string a_Description, PickableItem.ItemType a_Type)
    {
        ItemID = a_ID;
        ItemName = a_Name;
        ItemDescripction = a_Description;
        CurrentItemType = a_Type;
    }
}