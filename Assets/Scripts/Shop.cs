using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{

    public PlayerInventory playerInventory;
    public TextMeshProUGUI moneyText;

    // Pepperoni
    public const int pepperoniCost = 20;
    public TextMeshProUGUI pepperoniAmountText;
    public TextMeshProUGUI pepperoniCostText;
    public Button pepperoniBuyButton;

    void Awake () {
        pepperoniCostText.text = $"${pepperoniCost}";
        pepperoniBuyButton.onClick.AddListener(() => {
            if (playerInventory.money >= pepperoniCost) {
                playerInventory.pepperoniAmount++;
                playerInventory.money -= pepperoniCost;
            }
        });
    }

    void Update () {
        moneyText.text = $"${playerInventory.money}";

        pepperoniAmountText.text = $"({playerInventory.pepperoniAmount})";
        pepperoniBuyButton.interactable = playerInventory.money >= pepperoniCost;
    }

}
