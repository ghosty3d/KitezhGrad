using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class GameCanvas : MonoBehaviour
{
    public static GameCanvas Instance;

    public MessagesPanel m_MessagesPanel;

    void Awake()
    {
        Instance = this;
    }

	// Use this for initialization
	void Start ()
    {
        if (m_MessagesPanel == null)
        {
            m_MessagesPanel = GetComponentInChildren<MessagesPanel>();
        }

        HideMessageArea();
	}
	
    public void ShowMessageArea()
    {
        m_MessagesPanel.gameObject.SetActive(true);
    }

    public void HideMessageArea()
    {
        m_MessagesPanel.gameObject.SetActive(false);
    }
}
