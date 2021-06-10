using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private Button playButton;
    [SerializeField] private Button helpButton;
    [SerializeField] private Button quitButton;

    [SerializeField] private GameObject quitCheckPop;
    [SerializeField] private Button noButton;
    [SerializeField] private Button yesButton;


    private void Start()
    {
        playButton.onClick.AddListener(HandlePlayButtonClicked);
        helpButton.onClick.AddListener(HandleHelpButtonClicked);
        quitButton.onClick.AddListener(HandleQuitButtonClicked);

        noButton.onClick.AddListener(HandleNoButtonClicked);
        yesButton.onClick.AddListener(HandleYesButtonClicked);

    }

    private void HandlePlayButtonClicked()
    {

        GameManger.Instance.StartGame();
    }

    private void HandleHelpButtonClicked()
    {
    }

    private void HandleQuitButtonClicked()
    {
        quitCheckPop.SetActive(true);
    }

    private void HandleNoButtonClicked ()
    {
        quitCheckPop.SetActive(false);

    }

    private void HandleYesButtonClicked()
    {
        quitCheckPop.SetActive(false);
        Events.Instance.QuitGameSequenceInitiated.Invoke();
    }
}
