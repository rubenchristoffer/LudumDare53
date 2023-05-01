using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Shop : MonoBehaviour
{

    public PlayerInventory playerInventory;
    public GameState gameState;
    public TextMeshProUGUI moneyText;
    public TextMeshProUGUI buyBulkText;
    public Slider buyBulkSlider;
    public Button continueButton;

    void Awake () {
        playerInventory.Load();
        gameState.Load();

        continueButton.onClick.AddListener(() => {
            playerInventory.Save();

            SceneManager.LoadScene("Game");
        });
    }

    void Update () {
        moneyText.text = $"${playerInventory.money}";
        buyBulkText.text = $"Buy bulk: x {(int)buyBulkSlider.value}";
    }

}
