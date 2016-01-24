using UnityEngine;
using System.Collections;

public class GameStatesManager : MonoBehaviour
{
    public static GameStatesManager Instance;

    public IGameState m_CurrentGameState;

    //Game States
    public GamePlayState m_GamePlayState;
    public TextState m_TextState;

    void Awake()
    {
        Instance = this;

        m_GamePlayState = new GamePlayState();
        m_TextState = new TextState();

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
        m_TextState.DisableState();

        m_CurrentGameState = m_GamePlayState;
        m_CurrentGameState.EnableState();
    }

    public void SetTextState()
    {
        m_GamePlayState.DisableState();

        m_CurrentGameState = m_TextState;
        m_CurrentGameState.EnableState();
    }
}
