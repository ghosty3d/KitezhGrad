using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerInventory : MonoBehaviour
{
    public static PlayerInventory Instance;

    public List<InventoryItem> ItemsList = new List<InventoryItem>();

    void Awake()
    {
        Instance = this;
    }

    // Use this for initialization
	void Start ()
    {
	
	}

    public void AddItem(int a_ID, string a_Name, string a_Description, PickableItem.ItemType a_Type)
    {
        InventoryItem l_NewItem = new InventoryItem(a_ID, a_Name, a_Description, a_Type);
        ItemsList.Add(l_NewItem);
    }

    public bool HasItem(int a_ItemID)
    {
        bool l_ItemFound = false;

        for(int i = 0; i < ItemsList.Count; i++)
        {
            if (ItemsList[i].ItemID == a_ItemID)
            {
                l_ItemFound = true;
                break;
            }
        }

        return l_ItemFound;
    }

    public void RemoveItem()
    {

    }
}
