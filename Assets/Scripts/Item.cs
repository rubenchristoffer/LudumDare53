using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class Item : MonoBehaviour
{

    public GameObject pickupAudio;
    public float rotateSpeed = 180f;

    public LayerMask groundMask;
    
    public UnityEvent<bool> onPickedUp { get; private set; } = new UnityEvent<bool>();

    private Transform child;
    private bool flipped;
    private bool activated;

    protected void Awake () {
        child = transform.GetChild(0);

        if (Physics.Raycast(transform.position + Vector3.up * 10f, Vector3.down, out var hit, 100f, groundMask)) {
            transform.position = hit.point;
        }

        if (Random.Range(0, 2) == 0) {
            flipped = true;
        }
    }

    protected void Update () {
        child.Rotate(new Vector3(0f, 0f, rotateSpeed * Time.deltaTime * (flipped ? -1f : 1f)));
    }

    protected void OnTriggerEnter (Collider collider) {
        if (activated) {
            return;
        }

        activated = collider.GetComponentInParent<PlayerMovement>();

        if (activated) {
            Instantiate(pickupAudio);
            OnPickup(collider);
            Destroy(gameObject);
        }
    }

    protected abstract void OnPickup (Collider collider);

}
