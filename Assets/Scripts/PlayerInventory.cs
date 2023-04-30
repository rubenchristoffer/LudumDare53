using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerInventory", menuName = "ScriptableObjects/PlayerInventory")]
public class PlayerInventory : ScriptableObject
{

    public int money;
    public int pepperoniAmount;
    public bool hasFoundGPS;

    public void Reset () {
        money = 0;
        pepperoniAmount = 0;
        hasFoundGPS = false;
    }

    public void Save () {
        PlayerPrefs.SetInt("Money", money);
        PlayerPrefs.SetInt("PepperoniAmount", pepperoniAmount);
    }

    public void Load () {
        money = PlayerPrefs.GetInt("Money", 0);
        pepperoniAmount = PlayerPrefs.GetInt("PepperoniAmount", 0);
    }

}
