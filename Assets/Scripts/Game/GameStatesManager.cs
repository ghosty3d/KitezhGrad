using UnityEngine;
using System.Collections;

public class GameStatesManager : MonoBehaviour
{
    public static GameStatesManager Instance;

    public IGameState m_CurrentGameState;

    //Game States
    public GamePlayState m_GamePlayState;
    public TextState m_TextState;
    public GameDocumentState m_DocumentState;

    void Awake()
    {
        Instance = this;

        m_GamePlayState = new GamePlayState();
        m_TextState = new TextState();
        m_DocumentState = new GameDocumentState();

        m_CurrentGameState = m_GamePlayState;
    }

    void Update()
    {
        if (m_CurrentGameState != null)
        {
            m_CurrentGameState.UpdateState();
        }
    }

    public void SetGamePlayState()
    {
        m_CurrentGameState.DisableState();
        m_CurrentGameState = m_GamePlayState;
        m_CurrentGameState.EnableState();
    }

    public void SetTextState()
    {
        m_CurrentGameState.DisableState();
        m_CurrentGameState = m_TextState;
        m_CurrentGameState.EnableState();
    }

    public void SetDocumentState()
    {
        Debug.Log("Now is Document State!");

        m_CurrentGameState.DisableState();
        m_CurrentGameState = m_DocumentState;
        m_CurrentGameState.EnableState();
    }
}
