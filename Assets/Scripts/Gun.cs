using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{

    public GameObject gunFireSound;
    public float damage = 0.25f;
    public Transform aimPoint;
    public GameObject projectilePrefab;
    public LayerMask enemyLayerMask;

    private Entity entity;
    private Projectile projectile;

    // Start is called before the first frame update
    void Start()
    {
        entity = GetComponentInParent<Entity>();

        projectile = projectilePrefab.GetComponent<Projectile>();
    }

    // Update is called once per frame
    void Update()
    {
        if (entity.isDead || GameManager.Instance.isLevelCleared || Time.timeScale <= 0.03f)
        {
            return;
        }

        if (Input.GetMouseButtonDown(0))
        {
            Shoot();
        }
    }

    void Shoot()
    {
        Instantiate(gunFireSound);

        if (Physics.Raycast(aimPoint.transform.position - aimPoint.transform.forward * 0.7f, aimPoint.transform.forward, out var hit, 1f, enemyLayerMask))
        {
            var enemy = hit.transform.GetComponentInParent<Enemy>();

            if (enemy != null) {
                enemy.InflictDamage(projectile.projectileDamage, projectile.pushForce * aimPoint.transform.forward);
                return;
            }
        }

        Instantiate(projectilePrefab, aimPoint.transform.position, aimPoint.transform.rotation);
    }

}
