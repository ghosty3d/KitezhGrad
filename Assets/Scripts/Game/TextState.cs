using UnityEngine;
using System.Collections;

public class TextState : IGameState
{
    public bool DisplayedText = false;

    public void EnableState()
    {
        DisplayedText = false;

        if (GameCanvas.Instance.m_MessagesPanel != null)
        {
            GameCanvas.Instance.ShowMessageArea();
        }

        Time.timeScale = 0f;
    }

    public void UpdateState()
    {
        if(Input.GetButtonDown("Action") && !DisplayedText)
        {
            DisplayedText = true;
        }
        else if(Input.GetButtonDown("Action") && DisplayedText)
        {
            GameStatesManager.Instance.SetGamePlayState();
        }
    }

    public void DisableState()
    {
        Time.timeScale = 1f;

        if (GameCanvas.Instance.m_MessagesPanel != null)
        {
            GameCanvas.Instance.HideMessageArea();
        }
    }
}
