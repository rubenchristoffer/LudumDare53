using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using static PlayerInventoryController;

public class WeaponBuyUI : MonoBehaviour
{

    public int cost = 20;
    public int unlockCost = 1000;
    public PlayerInventory playerInventory;
    public Button buyButton;
    public TextMeshProUGUI amountText;
    public TextMeshProUGUI priceText;
    public WeaponType weaponType;
    public bool isItem;

    private TextMeshProUGUI buyButtonText;
    private Shop shop;

    public int amount {
        get { 
            return weaponType switch {
                WeaponType.Pepperoni => playerInventory.pepperoniAmount,
                WeaponType.Grenade => playerInventory.grenadeAmount,
                WeaponType.Uzi => playerInventory.uziAmmo,
                WeaponType.AK47 => playerInventory.akAmmo,
                _ => 0
            };
        }

        set {
            switch (weaponType) {
                case WeaponType.Pepperoni: playerInventory.pepperoniAmount = value; break;
                case WeaponType.Grenade: playerInventory.grenadeAmount = value; break;
                case WeaponType.Uzi: playerInventory.uziAmmo = value; break;
                case WeaponType.AK47: playerInventory.akAmmo = value; break;
            }
        }
    }

    public bool isUnlocked
    {
        get
        {
            return weaponType switch
            {
                WeaponType.Uzi => playerInventory.hasUzi,
                WeaponType.AK47 => playerInventory.hasAK,
                _ => true
            };
        }
        set {
            switch (weaponType) {
                case WeaponType.Uzi: playerInventory.hasUzi = value; break;
                case WeaponType.AK47: playerInventory.hasAK = value; break;
            }
        }
    }

    public int currentCost => isUnlocked ? cost * (int)shop.buyBulkSlider.value : unlockCost;

    void Awake () {
        shop = GetComponentInParent<Shop>();
        buyButtonText = buyButton.GetComponentInChildren<TextMeshProUGUI>();

        buyButton.onClick.AddListener(() => {
            if (playerInventory.money >= currentCost) {
                playerInventory.money -= currentCost;

                if (isUnlocked) {
                    amount += (int)shop.buyBulkSlider.value;
                } else {
                    isUnlocked = true;
                }
            }
        });
    }

    void Update () {
        buyButtonText.text = GetBuyButtonText();

        if (amount > 999) {
            amountText.text = $"(999+)";
        } else {
            amountText.text = $"{amount}";
        }

        priceText.text = $"${currentCost}";

        buyButton.interactable = playerInventory.money >= currentCost;
    }

    string GetBuyButtonText () {
        if (isItem) {
            return "Buy";
        } else {
            return isUnlocked ? "Buy ammo" : "Unlock";
        }
    }

}