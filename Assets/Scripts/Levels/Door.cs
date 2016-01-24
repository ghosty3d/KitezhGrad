using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

[RequireComponent(typeof(SphereCollider))]
public class Door : MonoBehaviour
{

    private SphereCollider m_DoorTrigger;

    [SerializeField]
    private int m_LevelID = 0;
    public int LevelID
    {
        get{ return m_LevelID; }
        set{ m_LevelID = value; }
    }
    [SerializeField]
    private bool m_Is_Locked = false;
    public bool IsLocked
    {
        get{ return m_Is_Locked; }
        set{ m_Is_Locked = value; }
    }
    [SerializeField]
    private int m_KeyID = 0;
    public int KeyID
    {
        get{ return m_KeyID; }
        set{ m_KeyID = value; }
    }

    void OnEnable()
    {
        if (m_DoorTrigger == null)
        {
            m_DoorTrigger = GetComponent<SphereCollider>();
            m_DoorTrigger.isTrigger = true;
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player" && Input.GetKeyDown(KeyCode.F))
        {
            if (IsLocked && PlayerInventory.Instance.HasItem(KeyID))
            {
                UnlockDoor(KeyID);
            }
            else if (IsLocked && !PlayerInventory.Instance.HasItem(KeyID))
            {
                GameStatesManager.Instance.SetTextState();
                GameCanvas.Instance.m_MessagesPanel.ShowTextMessage("Door is locked, you need to find a key!");
            }
            else
            {
                PassThroughtDoor(0);
            }
        }
    }

    public void PassThroughtDoor(int a_LevelID)
    {
        SceneManager.LoadScene(a_LevelID);
    }
	
    public void UnlockDoor(int a_KeyID)
    {
        //Show Text Message
        GameStatesManager.Instance.SetTextState();
        GameCanvas.Instance.m_MessagesPanel.ShowTextMessage("You unlocked this door with a key!");

        //Remove Key From Player's Inventory
        PlayerInventory.Instance.RemoveItem();

        //Mark Door as Unlocekd
        IsLocked = false;
    }
}