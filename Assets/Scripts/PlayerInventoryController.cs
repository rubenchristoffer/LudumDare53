using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventoryController : MonoBehaviour
{

    public PlayerInventory playerInventory;
    public GameState gameState;

    public Transform throwAimingPosition;
    public GameObject pepperoniPrefab;
    public GameObject grenadePrefab;

    private Entity entity;
    private List<Weapon> weapons = new List<Weapon>();
    private Weapon _currentlyEquippedWeapon;

    public Weapon currentlyEquippedWeapon => _currentlyEquippedWeapon;

    [System.Serializable]
    public class Weapon
    {
        public WeaponType weaponType;
        public KeyCode keyCodeToEquip;
        public GameObject physicalObject;
        public bool isAvailable;
    }

   public enum WeaponType {
        Pistol,
        Uzi,
        AK47,

        Pepperoni,
        Grenade
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
            weaponType = WeaponType.Pistol,
            keyCodeToEquip = KeyCode.Alpha1,
            physicalObject = transform.FindAnyChildWithName("Pistol").gameObject,
            isAvailable = true
        });

        weapons.Add(new Weapon
        { 
            weaponType = WeaponType.Uzi,
            keyCodeToEquip = KeyCode.Alpha2, 
            physicalObject = transform.FindAnyChildWithName("Uzi").gameObject,
            isAvailable = playerInventory.hasUzi
            });

        weapons.Add(new Weapon
        { 
            weaponType = WeaponType.AK47,
            keyCodeToEquip = KeyCode.Alpha3, 
            physicalObject = transform.FindAnyChildWithName("AK47").gameObject,
            isAvailable = playerInventory.hasAK
            });

        foreach (var weapon in weapons)
        {
            weapon.physicalObject.SetActive(false);
        }

        weapons[0].physicalObject.SetActive(true);
        _currentlyEquippedWeapon = weapons[0];
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

        if (playerInventory.grenadeAmount > 0 && Input.GetButtonDown("Grenade"))
        {
            Instantiate<GameObject>(grenadePrefab, throwAimingPosition.position, throwAimingPosition.rotation * Quaternion.Euler(-22.5f, 0f, 0f));

            playerInventory.grenadeAmount--;
        }

        foreach (var weapon in weapons)
        {
            if (weapon.isAvailable && Input.GetKeyDown(weapon.keyCodeToEquip))
            {
                _currentlyEquippedWeapon?.physicalObject.SetActive(false);
                _currentlyEquippedWeapon = weapon;
                _currentlyEquippedWeapon.physicalObject.SetActive(true);
            }
        }
    }

}
