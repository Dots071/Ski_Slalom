using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable] public class SaveData 
{
    public float[] topScoreBoard;


    public SaveData (GameManger gameManager)
    {
        topScoreBoard = gameManager.topScoreBoard;

    }
}
