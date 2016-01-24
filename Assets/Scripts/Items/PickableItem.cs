using UnityEngine;
using System.Collections;

[RequireComponent(typeof(SphereCollider))]
public class PickableItem : MonoBehaviour, IPickableItem
{
    private SphereCollider m_Item_Trigger;

    public int ItemID;
    public string ItemName;
    public string ItemDescripction;
    public enum ItemType
    {
        Key,
        Ammo,
        Health,
        QuestItem
    }

    public ItemType CurrentItemType;

	// Use this for initialization
	void Start () {
        if (m_Item_Trigger == null)
        {
            m_Item_Trigger = GetComponent<SphereCollider>();
            m_Item_Trigger.isTrigger = true;
        }
	}
	
    void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player" && Input.GetKeyDown(KeyCode.F))
        {
            PickItem();
        }
    }

    public void PickItem()
    {
        //Add new Inventory Item to Players Inventory
        PlayerInventory.Instance.AddItem(ItemID, ItemName, ItemDescripction, CurrentItemType);

        //Show Text Message
        GameStatesManager.Instance.SetTextState();
        GameCanvas.Instance.m_MessagesPanel.ShowTextMessage("You picked " + this.gameObject.name.ToUpper() + ". ");

        //Remove Item From the Scene
        Destroy(this.gameObject);
    }
}
