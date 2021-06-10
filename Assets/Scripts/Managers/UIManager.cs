using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager>
{
    [Tooltip("The menu that pops up after the race is finished.")]
    [SerializeField] private GameObject finishMenu;

    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject timer;


    [SerializeField] private Camera dummyCamera;



    private void Start()
    {
        Events.Instance.RaceStarted.AddListener(HandleRaceStarted);
        GameManger.Instance.OnGameStateChanged.AddListener(HandleGameStateChanged);
    }

    private void HandleRaceStarted()
    {
        timer.SetActive(true);

    }


    private void HandleGameStateChanged(GameManger.GameState newState, GameManger.GameState previousState)
    {
        mainMenu.SetActive(newState == GameManger.GameState.PREGAME ? true : false);
        dummyCamera.gameObject.SetActive(newState == GameManger.GameState.PREGAME ? true : false);

        finishMenu.SetActive(newState == GameManger.GameState.POSTGAME ? true : false);

        if (previousState == GameManger.GameState.RUNNING)
        {
            timer.SetActive(false);
        }
        
    }




}
