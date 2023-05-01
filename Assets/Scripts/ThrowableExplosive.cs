using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ThrowableExplosive : MonoBehaviour
{

    public float baseDamage = 150f;
    public float projectileSpeed = 50f;
    public float fuseTime = 3f;
    public float explosionRadius;
    public GameObject explodeEffectPrefab;

    private Rigidbody _rigidbody;

    void Awake () {
        _rigidbody = GetComponent<Rigidbody>();

        _rigidbody.velocity = transform.forward * projectileSpeed;      
        _rigidbody.AddTorque(Random.insideUnitSphere * 20f); 
        StartCoroutine(ExplodeAfterTime());
    }

    IEnumerator ExplodeAfterTime () {
        yield return new WaitForSeconds(fuseTime);
        Explode();
    }

    void Explode () {
        var enemiesHit = Physics.OverlapSphere(transform.position, explosionRadius)
            .Select(col => col.GetComponentInParent<Enemy>())
            .Where(enemy => enemy != null)
            .Distinct();

        foreach (var enemy in enemiesHit) {
            var distance = Vector3.Distance(transform.position, enemy.transform.position);
            var damageFactor = 1f - (distance / explosionRadius);

            enemy.InflictDamage(damageFactor * baseDamage, Vector3.zero);
        }

        Instantiate<GameObject>(explodeEffectPrefab, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

}
