using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManger : Singleton<GameManger>
{
    public enum GameState
    {
        PREGAME,
        RUNNING,
        POSTGAME
    }

    public Events.EventGameState OnGameStateChanged;

    [SerializeField] GameObject[] systemPrefabs;
    List<GameObject> instancedSystemPrefabs;
    List<AsyncOperation> loadOperations;

    GameState currentGameState = GameState.PREGAME;
    public GameState CurrentGameState
    {
        get { return currentGameState; }
        private set { currentGameState = value; }
    }


    //private float racesPlayed;

     public int currentLevel = 1;

    public float[] topScoreBoard;

    void Start()
    {
        DontDestroyOnLoad(gameObject);

        LoadSettings();

        Events.Instance.QuitGameSequenceInitiated.AddListener(QuitGame);

        instancedSystemPrefabs = new List<GameObject>();
        loadOperations = new List<AsyncOperation>();

        InstantiateSystemPrefabs();


    }


    private void InstantiateSystemPrefabs()
    {
        GameObject instancedPrefab;
        for (int i = 0; i < systemPrefabs.Length; i++)
        {
            instancedPrefab = Instantiate(systemPrefabs[i]);
            instancedSystemPrefabs.Add(instancedPrefab);
        }
    }




    public void UpdateGameState(GameState state)
    {
        GameState previousGameState = currentGameState;
        currentGameState = state;

        switch (currentGameState)
        {
            case GameState.PREGAME:
                Time.timeScale = 1.0f;
                break;

            case GameState.RUNNING:
                Time.timeScale = 1.0f;
                break;

            case GameState.POSTGAME:
                Time.timeScale = 0.25f;
                break;

            default:
                break;
        }
        Debug.Log("GameState updated to " + currentGameState);
        OnGameStateChanged.Invoke(currentGameState, previousGameState);
    }


    public void LoadLevel(int levelIndex)
    {
        AsyncOperation ao = SceneManager.LoadSceneAsync(levelIndex, LoadSceneMode.Additive);
        if (ao == null)
        {
            Debug.LogError("[GameManager] Error loading level." + levelIndex);
            return;
        }
        loadOperations.Add(ao);
        currentLevel = levelIndex;
        ao.completed += OnLevelLoadComplete;
    }



    public void UnLoadLevel(int levelIndex)
    {
        AsyncOperation ao = SceneManager.UnloadSceneAsync(levelIndex);

        ao.completed += OnUnLevelLoadComplete;
    }


    private void OnLevelLoadComplete(AsyncOperation ao)
    {
        if (loadOperations.Contains(ao))
        {
            loadOperations.Remove(ao);

            if (loadOperations.Count == 0)
            {
                UpdateGameState(GameState.RUNNING);
            }
        }

        Debug.Log("Level load complete!");
    }
    private void OnUnLevelLoadComplete(AsyncOperation ao)
    {
        Debug.Log("Level unloaded complete!");
    }


    private void SaveSettings()
    {
        SaveManager.SavePersistantData(this);
    }

    private void LoadSettings()
    {
        SaveData data = SaveManager.LoadPersistantData();
        if (data == null)
        {
            SetDefaultSettings();

        }
        else
        {
            for (int i = 0; i < topScoreBoard.Length; i++)
            {
                topScoreBoard[i] = data.topScoreBoard[i];
                Debug.Log(topScoreBoard[i]);
            }
        }
    }


    private void SetDefaultSettings()
    {
        for (int i = 0; i < topScoreBoard.Length; i++)
        {
            topScoreBoard[i] = float.NaN;
        }
    }

    public void AddScoreToBoard(float finishTime)
    {
        for (int i = 0; i < topScoreBoard.Length; i++)
        {
            if (float.IsNaN(topScoreBoard[i]))
            { 
                topScoreBoard[i] = finishTime;
                Array.Sort(topScoreBoard);
                return;
            }
        }
        if (finishTime < topScoreBoard[topScoreBoard.Length - 1])
        {
            topScoreBoard[topScoreBoard.Length - 1] = finishTime;
            Array.Sort(topScoreBoard);
        }


    }

    public void StartGame()
    {
        LoadLevel(currentLevel);
    }

    public void RestartLevel()
    {
        UnLoadLevel(currentLevel);
        LoadLevel(currentLevel);

    }

    public void GoToNextLevel()
    {
        UnLoadLevel(currentLevel);
        LoadLevel(currentLevel + 1);
    }

    public void QuitGame()
    {
        Debug.Log("Game is quitting");
        Application.Quit();
    }

    override protected void OnDestroy()
    {
        SaveSettings();
        base.OnDestroy();
    }
}
