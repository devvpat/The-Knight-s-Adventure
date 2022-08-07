using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleMenu : MonoBehaviour
{
    public void OnClickNewGame()
    {
        GameStateManager.NewGame();
    }

    public void OnClickCredits()
    {
        GameStateManager.Credits();
    }

    public void OnClickQuitGame()
    {
        Application.Quit();
    }

    public void OnClickResume()
    {
        GameStateManager.Resume();
    }
}
