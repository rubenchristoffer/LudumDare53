using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : Item
{
    
    public int moneyToGain = 5;
    
    public GameState gameState;

    protected override bool OnPickup(Collider collider)
    {
        if (collider.GetComponentInParent<PlayerMovement>()) {
            gameState.moneyGained += moneyToGain;
            return true;
        }

        return false;
    }

}
