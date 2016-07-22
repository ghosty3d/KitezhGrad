using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class MessagesPanel : MonoBehaviour
{
    public Text m_MessagesText;
	
    // Use this for initialization
	void Start () {
        if (m_MessagesText == null)
        {
            m_MessagesText = GetComponentInChildren<Text>();
        }
	}
	
    public void ShowTextMessage(string a_Message)
    {
        m_MessagesText.text = a_Message;
    }
}
