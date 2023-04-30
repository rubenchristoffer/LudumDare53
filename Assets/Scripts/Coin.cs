using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    
    public int moneyToGain = 5;
    public float rotateSpeed = 180f;
    public GameState gameState;
    public LayerMask groundMask;

    private bool activated;
    private bool flipped;
    private Transform child;

    void Awake () {
        child = transform.GetChild(0);

        if (Physics.Raycast(transform.position + Vector3.up * 10f, Vector3.down, out var hit, 100f, groundMask)) {
            transform.position = hit.point;
        }

        if (Random.Range(0, 2) == 0) {
            flipped = true;
        }
    }

    void Update () {
        child.Rotate(new Vector3(0f, 0f, rotateSpeed * Time.deltaTime * (flipped ? -1f : 1f)));
    }

    void OnTriggerEnter (Collider collider) {
        if (activated) {
            return;
        }

        if (collider.GetComponentInParent<PlayerMovement>()) {
            gameState.moneyGained += moneyToGain;
            activated = true;
            Destroy(gameObject);
        }
    }

}
