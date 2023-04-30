using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerInventory", menuName = "ScriptableObjects/PlayerInventory")]
public class PlayerInventory : ScriptableObject
{

    public int money;
    public int pepperoniAmount;

    public void Reset () {
        money = 0;
        pepperoniAmount = 0;
    }

}
