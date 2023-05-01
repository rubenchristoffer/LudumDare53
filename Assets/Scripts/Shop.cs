using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Shop : MonoBehaviour
{

    public PlayerInventory playerInventory;
    public TextMeshProUGUI moneyText;
    public Button continueButton;

    void Awake () {
        continueButton.onClick.AddListener(() => {
            SceneManager.LoadScene("Game");
        });
    }

    void Update () {
        moneyText.text = $"${playerInventory.money}";
    }

}
