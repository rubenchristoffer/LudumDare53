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

    void Awake () {
        GameManager.Instance.onLevelCleared.AddListener(() => {
            playerInventory.money += gameState.moneyGained + gameState.currentStage.jobPrice;
        });

        entity = GetComponent<Entity>();
    }

    void Update () {
        if (entity.isDead || GameManager.Instance.isLevelCleared || Time.timeScale <= 0.03f) {
            return;
        }

        if (playerInventory.pepperoniAmount > 0 && Input.GetButtonDown("Pepperoni")) {
            Instantiate<GameObject>(pepperoniPrefab, throwAimingPosition.position, throwAimingPosition.rotation);

            Instantiate<GameObject>(pepperoniPrefab, throwAimingPosition.position, throwAimingPosition.rotation * Quaternion.Euler(0f, 11.25f, 0f));
            Instantiate<GameObject>(pepperoniPrefab, throwAimingPosition.position, throwAimingPosition.rotation * Quaternion.Euler(0f, -11.25f, 0f));

            Instantiate<GameObject>(pepperoniPrefab, throwAimingPosition.position, throwAimingPosition.rotation * Quaternion.Euler(0f, 22.5f, 0f));
            Instantiate<GameObject>(pepperoniPrefab, throwAimingPosition.position, throwAimingPosition.rotation * Quaternion.Euler(0f, -22.5f, 0f));

            playerInventory.pepperoniAmount--;
        }
    }

}
