using UnityEngine;
using System.Collections;

[System.Serializable]
public abstract class Item
{
    public int ItemID;
    public string ItemName;
    public enum ItemType
    {
        Key,
        Ammo,
        Health,
        QuestItem
    }

    public ItemType CurrentItemType;

//    public Item(int a_ID, string a_Name, ItemType a_Type)
//    {
//        ItemID = a_ID;
//        ItemName = a_Name;
//        CurrentItemType = a_Type;
//    }
}