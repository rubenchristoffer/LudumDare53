using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameState", menuName = "ScriptableObjects/GameState")]
public class GameState : ScriptableObject
{

    public int killCount;
    public int moneyGained;

}
