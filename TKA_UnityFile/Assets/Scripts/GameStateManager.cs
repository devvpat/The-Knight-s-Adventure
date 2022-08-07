using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStateManager : MonoBehaviour
{
    [SerializeField]
    private string mainLevel;
    [SerializeField]
    private string titleSceneName;
    [SerializeField]
    private string creditsSceneName;
    [SerializeField]
    private Texture2D cursorTexture;
    [SerializeField]
    private int startingLives;

    private static GameStateManager _instance;

    enum GAMESTATE
    {
        MENU,
        PLAYING,
        PAUSED,
        GAMEOVER,
        CREDITS
    }
    private static GAMESTATE state;

    public static bool IsPaused { get; set; }
    public static float SpeedMod { get; set; }
    public static float JumpMod { get; set; }
    public static int LivesRemaining { get; set; }

    //tracks how long the current "run" is
    public static float timePlayed;

    //creates a GSM if there is none
    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(_instance);
            Cursor.SetCursor(cursorTexture, Vector2.zero, CursorMode.Auto);
            PlayerPrefs.DeleteAll();
        }
        else
        {
            Destroy(this);
        }
    }

    //escape pauses, R restarts level, M returns to menu
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            RestartCurrentLevel();
        }
        if (Input.GetKeyDown(KeyCode.M))
        {
            SaveGame();
            state = GAMESTATE.MENU;
            QuitToTitle();
        }
    }

    //starts a new game with player stats (checkpoints, lives, time) cleared
    public static void NewGame()
    {
        state = GAMESTATE.PLAYING;
        LivesRemaining = _instance.startingLives;
        timePlayed = 0;
        _instance.ClearPlayerStats();
        PlayerPrefs.SetInt("hasSavedGame", 1);
        if (_instance.mainLevel != null)
        {
            SceneManager.LoadScene(_instance.mainLevel);            
        }
    }

    //reloads the game level with the player's saved stats (checkpoint, time, lives)
    public static void Resume()
    {
        if (PlayerPrefs.GetInt("hasSavedGame") == 1)
        {
            state = GAMESTATE.PLAYING;
            if (_instance.mainLevel != null)
            {
                SceneManager.LoadScene(_instance.mainLevel);
                LivesRemaining = PlayerPrefs.GetInt("Lives");
            }
        }
    }

    //saves player stats (lives, checkpoint, time)
    public static void SaveGame()
    {
        PlayerPrefs.SetInt("Lives", LivesRemaining);        
        //checkpoints are already saved, so need to save them here
        //time is saved in the GSM, so also no need to save them here
    }

    //saves the player's current score (time) and updates high score if their current time is faster than previous fastest time
    public static void Victory()
    {
        float finishedTime = timePlayed;
        float curHighScore = PlayerPrefs.GetFloat("HighScore");
        if (curHighScore == 0)
        {
            PlayerPrefs.SetFloat("HighScore", finishedTime);
        }
        else if (finishedTime < curHighScore)
        {
            PlayerPrefs.SetFloat("HighScore", finishedTime);
        }

    }

    //switches to credit scene
    public static void Credits()
    {
        state = GAMESTATE.CREDITS;
        SceneManager.LoadScene(_instance.creditsSceneName);
    }

    //returns to the main menu
    public static void QuitToTitle()
    {
        state = GAMESTATE.MENU;
        SceneManager.LoadScene(_instance.titleSceneName);
    }

    //toggles a paused state
    public static void TogglePause()
    {
        if (state == GAMESTATE.PLAYING)
        {
            state = GAMESTATE.PAUSED;
            Time.timeScale = 0;
            IsPaused = true;
        }
        else if (state == GAMESTATE.PAUSED)
        {
            state = GAMESTATE.PLAYING;
            Time.timeScale = 1;
            IsPaused = false;
        }
    }

    //restarts level
    public static void RestartCurrentLevel()
    {
        GameStateManager.LivesRemaining--;
        if (LivesRemaining <= 0)
        {
            _instance.ClearPlayerStats();
            QuitToTitle();
        }
        else
        {
            string scene = SceneManager.GetActiveScene().name;
            SceneManager.LoadScene(scene);
        }
    }

    //resets player stats (lives, checkpoint, hasSavedGame) to defaults, keeps highscore
    private void ClearPlayerStats()
    {
        PlayerPrefs.SetInt("Lives", 0);
        PlayerPrefs.SetInt("Checkpoint", 0);
        PlayerPrefs.SetInt("hasSavedGame", 0);
    }

    //notes:
    //all playerprefs variables: Lives(int), Checkpoint(int), hasSavedGame(int), HighScore(float)
}
