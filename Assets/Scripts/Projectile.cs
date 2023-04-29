using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{

    public float projectileSpeed = 40f;
    public float projectileDamage = 10f;
    public float pushForce = 2f;

    private Rigidbody _rigidbody;
    private bool _hasHitSomething;

    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();

        _rigidbody.velocity = transform.forward * projectileSpeed;
    }

    void OnTriggerEnter(Collider collider) {
        if (_hasHitSomething) {
            return;
        }

        var enemy = collider.GetComponentInParent<Enemy>();


        if (enemy != null) {
            if (enemy.isDead) {
                return;
            }

            enemy.InflictDamage(projectileDamage, transform.forward * pushForce);
        }

        _hasHitSomething = true;
        Destroy(gameObject);
    }
}
