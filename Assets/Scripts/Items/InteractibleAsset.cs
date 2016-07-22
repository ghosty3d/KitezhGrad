using UnityEngine;
using System.Collections;
[RequireComponent(typeof(SphereCollider))]
public class InteractibleAsset : MonoBehaviour
{
    public string m_AssetMessage = "What the hell?! It seams like somebody was bleeding here!!!";

    void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player" && Input.GetButtonDown("Action"))
        {
            if (GameStatesManager.Instance.m_CurrentGameState != GameStatesManager.Instance.m_TextState)
            {
                GameCanvas.Instance.ShowMessageArea();
                GameCanvas.Instance.m_MessagesPanel.ShowTextMessage(m_AssetMessage);
                GameStatesManager.Instance.SetTextState();  
            }
        }
    }
}