using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using static PlayerInventoryController;

public class WeaponUI : MonoBehaviour
{

    public PlayerInventory playerInventory;
    public TextMeshProUGUI amountText;
    public TextMeshProUGUI keyText;
    public Image iconImage;
    public WeaponType type;
    public bool notEquippable;

    private Image panelImage;

    private PlayerInventoryController playerInventoryController;

    public int amount {
        get => type switch {
            WeaponType.Pepperoni => playerInventory.pepperoniAmount,
            WeaponType.Grenade => playerInventory.grenadeAmount,
            WeaponType.Uzi => playerInventory.uziAmmo,
            _ => 0
        };
    }

    public bool canUse {
        get => type switch {
            WeaponType.Uzi => playerInventory.hasUzi,
            _ => true
        };
    }

    public bool isEquipped {
        get => type == playerInventoryController.currentlyEquippedWeapon.weaponType;
    }

    void Awake () {
        panelImage = GetComponent<Image>();

        if (type != WeaponType.Pistol && (amount == 0 || !canUse)) {
            gameObject.SetActive(false);
        }

        playerInventoryController = GameObject.FindWithTag("Player").GetComponent<PlayerInventoryController>();
    }

    void Update () {
        if (type == WeaponType.Pistol) {
            amountText.text = "Inf";
        } else {
            if (amount > 999) {
                amountText.text = "999+";
            }  else {
                amountText.text = amount.ToString();
            }
        }

        if (!notEquippable) {
            panelImage.color = ChangeAlpha(panelImage.color, isEquipped ? 1f : 128f / 255f);
            amountText.color = ChangeAlpha(amountText.color, isEquipped ? 1f : 128f / 255f);
            keyText.color = ChangeAlpha(keyText.color, isEquipped ? 1f : 128f / 255f);
        }
    }

    Color ChangeAlpha (Color color, float a) {
        return new Color(color.r, color.g, color.b, a);
    }

}
