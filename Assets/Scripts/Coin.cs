using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : Item
{
    
    public int moneyToGain = 5;
    
    public GameState gameState;

    protected override void OnPickup(Collider collider)
    {
        gameState.moneyGained += moneyToGain;
    }

}
