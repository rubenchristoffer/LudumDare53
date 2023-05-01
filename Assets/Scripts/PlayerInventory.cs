using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerInventory", menuName = "ScriptableObjects/PlayerInventory")]
public class PlayerInventory : ScriptableObject
{

    public int money;
    public int pepperoniAmount;
    public int grenadeAmount;
    public bool hasFoundGPS;
    public bool hasUzi;
    public int uziAmmo;

    public void Reset () {
        money = 0;
        pepperoniAmount = 0;
        grenadeAmount = 0;
        hasFoundGPS = false;
        hasUzi = false;
        uziAmmo = 0;
    }

    public void Save () {
        PlayerPrefs.SetInt("Money", money);
        PlayerPrefs.SetInt("PepperoniAmount", pepperoniAmount);
        PlayerPrefs.SetInt("GrenadeAmount", grenadeAmount);
        PlayerPrefs.SetInt("HasUzi", hasUzi ? 1 : 0);
        PlayerPrefs.SetInt("UziAmmo", uziAmmo);
    }

    public void Load () {
        money = PlayerPrefs.GetInt("Money", 0);
        pepperoniAmount = PlayerPrefs.GetInt("PepperoniAmount", 0);
        grenadeAmount = PlayerPrefs.GetInt("GrenadeAmount", 0);
        hasUzi = PlayerPrefs.GetInt("HasUzi", 0) == 1;
        uziAmmo = PlayerPrefs.GetInt("UziAmmo", 0);
    }

}
