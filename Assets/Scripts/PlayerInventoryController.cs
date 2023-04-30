using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventoryController : MonoBehaviour
{

    public PlayerInventory playerInventory;
    public GameState gameState;

    public Transform throwAimingPosition;
    public GameObject pepperoniPrefab;

    private Entity entity;
    private List<Weapon> weapons = new List<Weapon>();
    private Weapon currentlyEquippedWeapon;

    [System.Serializable]
    public class Weapon
    {
        public KeyCode keyCodeToEquip;
        public GameObject physicalObject;
        public bool isAvailable;
    }

    void Start()
    {
        GameManager.Instance.onLevelCleared.AddListener(() =>
        {
            playerInventory.money += gameState.moneyGained + gameState.currentStage.jobPrice;
        });

        entity = GetComponent<Entity>();

        weapons.Add(new Weapon
        {
            keyCodeToEquip = KeyCode.Alpha1,
            physicalObject = transform.FindAnyChildWithName("Pistol").gameObject,
            isAvailable = true
        });

        weapons.Add(new Weapon
        { 
            keyCodeToEquip = KeyCode.Alpha2, 
            physicalObject = transform.FindAnyChildWithName("Uzi").gameObject,
            isAvailable = playerInventory.hasUzi
            });

        foreach (var weapon in weapons)
        {
            weapon.physicalObject.SetActive(false);
        }

        weapons[0].physicalObject.SetActive(true);
        currentlyEquippedWeapon = weapons[0];
    }

    void Update()
    {
        if (entity.isDead || GameManager.Instance.isLevelCleared || Time.timeScale <= 0.03f)
        {
            return;
        }

        if (playerInventory.pepperoniAmount > 0 && Input.GetButtonDown("Pepperoni"))
        {
            Instantiate<GameObject>(pepperoniPrefab, throwAimingPosition.position, throwAimingPosition.rotation);

            Instantiate<GameObject>(pepperoniPrefab, throwAimingPosition.position, throwAimingPosition.rotation * Quaternion.Euler(0f, 11.25f, 0f));
            Instantiate<GameObject>(pepperoniPrefab, throwAimingPosition.position, throwAimingPosition.rotation * Quaternion.Euler(0f, -11.25f, 0f));

            Instantiate<GameObject>(pepperoniPrefab, throwAimingPosition.position, throwAimingPosition.rotation * Quaternion.Euler(0f, 22.5f, 0f));
            Instantiate<GameObject>(pepperoniPrefab, throwAimingPosition.position, throwAimingPosition.rotation * Quaternion.Euler(0f, -22.5f, 0f));

            playerInventory.pepperoniAmount--;
        }

        foreach (var weapon in weapons)
        {
            if (weapon.isAvailable && Input.GetKeyDown(weapon.keyCodeToEquip))
            {
                currentlyEquippedWeapon?.physicalObject.SetActive(false);
                currentlyEquippedWeapon = weapon;
                currentlyEquippedWeapon.physicalObject.SetActive(true);
            }
        }
    }

}
