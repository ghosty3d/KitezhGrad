using UnityEngine;
using System.Collections;

public class GamePlayState : IGameState
{
    public void EnableState()
    {
        Time.timeScale = 1f;
    }

    public void UpdateState()
    {
        
    }

    public void DisableState()
    {
        
    }
}
