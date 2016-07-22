using UnityEngine;
using System.Collections;

public class DocumentItem : MonoBehaviour {

    void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player" && Input.GetButtonDown("Action"))
        {
            //GameCanvas.Instance.ShowDocumentPanel();
            if(GameStatesManager.Instance.m_CurrentGameState != GameStatesManager.Instance.m_DocumentState && PlayerController.Instance.m_CurrentPlayerState == PlayerController.PlayerStates.Idle)
            {
                GameStatesManager.Instance.SetDocumentState();  
            }
        }
    }
}
