using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Entity : MonoBehaviour
{

    private const float defaultHealth = 100f;

    public float health = defaultHealth;
    public float maxHealth = defaultHealth;

    public bool isDead { get; private set; }

    public UnityEvent onEntityDie { get; private set; } = new UnityEvent(); 
    public UnityEvent<float, Vector3> onEntityTakeDamage { get; private set; } = new UnityEvent<float, Vector3>();

    private Rigidbody _rigidBody;

    void Awake() {
        _rigidBody = GetComponent<Rigidbody>();
    }

    public void InflictDamage (float damageToInflict, Vector3 pushForce) {
        if (GameManager.Instance.isLevelCleared) {
            return;
        }

        health -= damageToInflict;
        onEntityTakeDamage.Invoke(damageToInflict, pushForce);

        _rigidBody.AddForce(pushForce, ForceMode.Impulse);

        if (health <= 0 && !isDead) {
            isDead = true;
            onEntityDie.Invoke();
        }
    }

}
