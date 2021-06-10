using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FinishRaceMenu : MonoBehaviour
{
    [SerializeField] private Button restartButton;
    [SerializeField] private Button nextLevelButton;
    [SerializeField] private Button quitGameButton;


    private void Start()
    {
        restartButton.onClick.AddListener(HandleRestartButtonClicked);
        nextLevelButton.onClick.AddListener(HandleNextLevelButtonClicked);
        quitGameButton.onClick.AddListener(HandleQuitGameButtonClicked);
    }

    private void HandleRestartButtonClicked()
    {

        GameManger.Instance.RestartLevel();
    }

    private void HandleNextLevelButtonClicked()
    {
        GameManger.Instance.GoToNextLevel();
    }

    private void HandleQuitGameButtonClicked()
    {
        GameManger.Instance.UnLoadLevel(GameManger.Instance.currentLevel);

        GameManger.Instance.UpdateGameState(GameManger.GameState.PREGAME);
    }
}
