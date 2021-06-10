using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    private float time;
    private TimeSpan raceTime;
    private float missedFlagPenalty = 2;

    private bool isRacing;

    private float[] topScoreBoard;

    [SerializeField] private GameObject topScoreMenu;
    [SerializeField] private Button TopScoreBoardButton;

    [SerializeField] private TextMeshProUGUI[] bestScoresTextsArray;
    [SerializeField] private TextMeshProUGUI timeText;


    private void Start()
    {

        Events.Instance.RaceFinished.AddListener(HandleRaceFinished);
        Events.Instance.missedFlag.AddListener(HandleMissedFlag);

        TopScoreBoardButton.onClick.AddListener(TopScoreButtonPressed);


    }

    private void OnEnable()
    {
        isRacing = true;
        time = 0;
    }

    private void Update()
    {
        if (isRacing)
        {
            time += Time.deltaTime;
            raceTime = TimeSpan.FromSeconds(time);
            timeText.text = raceTime.ToString("mm':'ss':'ff");
            
        }
    }


    private void HandleRaceFinished()
    {
        isRacing = false;

        GameManger.Instance.AddScoreToBoard(time);


        Debug.Log("Time: " + time);
        topScoreMenu.SetActive(true);
        GetScoreBoard(time);

/*
        for (int i = 0; i < bestScoresTextsArray.Length; i++)
         {
             Debug.Log("Top Score #" + (i + 1) + ": " + topScoreBoard[i]);
         }




        for (int i = 0; i < bestScoresTextsArray.Length; i++)
        {
            bestScoresTextsArray[i].text = "#" + (i + 1) + "  " + topScoreBoard[i];
        }*/

    }

    private void TopScoreButtonPressed()
    {
        topScoreMenu.SetActive(false);
        GameManger.Instance.UpdateGameState(GameManger.GameState.POSTGAME);
    }


    private void GetScoreBoard(float finishTime)
    {
        TimeSpan temp;
        float score;

        for (int i = 0; i < bestScoresTextsArray.Length; i++)
        {
            score = GameManger.Instance.topScoreBoard[i];
            print(score);

            if (float.IsNaN(score))
            {
                bestScoresTextsArray[i].text = "#" + (i + 1) + "  " + "----No Score----";

            } else
            {
                temp = TimeSpan.FromSeconds(score);
                bestScoresTextsArray[i].text = "#" + (i + 1) + "  " + temp.ToString("mm':'ss':'ff");
                if (score == finishTime)
                {
                    bestScoresTextsArray[i].color = Color.green;
                }
            }
        }
       //  topScoreBoard = GameManger.Instance.topScoreBoard;
    }


    private void HandleMissedFlag()
    {
        time += missedFlagPenalty;

    }


}
