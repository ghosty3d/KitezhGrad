using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class GameCanvas : MonoBehaviour
{
    public static GameCanvas Instance;

    public AudioSource m_AudioSource;

    public MessagesPanel m_MessagesPanel;
    public WeaponsPanel m_WeaponsPanel;
    public DocumentPanel m_DocumentPanel;

    void Awake()
    {
        Instance = this;
    }

	void OnEnable() {
		//PlayerController.OnFire += 
	}

	void OnDisable() {
		//PlayerController.OnFire -= 
	}

	// Use this for initialization
	void Start ()
    {
        if (m_MessagesPanel == null)
        {
            m_MessagesPanel = GetComponentInChildren<MessagesPanel>();
        }

        if (m_WeaponsPanel == null)
        {
            m_WeaponsPanel = GetComponentInChildren<WeaponsPanel>();
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

    public void ShowDocumentPanel()
    {
        m_DocumentPanel.gameObject.SetActive(true);
        m_AudioSource.Play();
    }

    public void HideDocumentPanel()
    {
        m_AudioSource.Stop();
        m_DocumentPanel.gameObject.SetActive(false);
    }
}
