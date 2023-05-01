using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameState", menuName = "ScriptableObjects/GameState")]
public class GameState : ScriptableObject
{

    public int killCount;
    public int moneyGained;
    public Stage currentStage;
    public int currentStageNumber;
    public int livingEnemies;

    public void ResetCounters () {
        killCount = 0;
        moneyGained = 0;
        livingEnemies = 0;
    }

    public void Reset () {
        ResetCounters();
        currentStage = null;
        currentStageNumber = 0;
    }

    public void Save () {
        PlayerPrefs.SetInt("CurrentStage", currentStageNumber);
        PlayerPrefs.Save();
    }

    public void Load () {
        currentStageNumber = PlayerPrefs.GetInt("CurrentStage", 0);
    }

}
