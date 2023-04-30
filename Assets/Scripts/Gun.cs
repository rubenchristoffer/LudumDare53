using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{

    public PlayerInventory playerInventory;
    public GameObject gunFireSound;
    public float damage = 0.25f;
    public float timeBetweenFire = 0.1f;
    public Transform aimPoint;
    public GameObject projectilePrefab;
    public LayerMask enemyLayerMask;
    public AmmoType ammoType;

    private Entity entity;
    private Projectile projectile;
    private float fireTimer;

    public int ammo {
        get => ammoType switch {
            AmmoType.Unlimited => int.MaxValue,
            AmmoType.Uzi => playerInventory.uziAmmo,
            _ => 0
        };

        set {
            switch (ammoType) {
                case AmmoType.Uzi: playerInventory.uziAmmo = value; break;
            }
        }
    }

    public enum AmmoType {
        Unlimited,
        Uzi
    }

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

        if (fireTimer <= 0)
        {
            if (Input.GetMouseButton(0) && ammo > 0)
            {
                Shoot();
            }
        } else {
            fireTimer -= Time.deltaTime;
        }

    }

    void Shoot()
    {
        fireTimer = timeBetweenFire;
        ammo--;
        Instantiate(gunFireSound);

        if (Physics.Raycast(aimPoint.transform.position - aimPoint.transform.forward * 0.7f, aimPoint.transform.forward, out var hit, 1f, enemyLayerMask))
        {
            var enemy = hit.transform.GetComponentInParent<Enemy>();

            if (enemy != null)
            {
                enemy.InflictDamage(projectile.projectileDamage, projectile.pushForce * aimPoint.transform.forward);
                return;
            }
        }

        Instantiate(projectilePrefab, aimPoint.transform.position, aimPoint.transform.rotation);
    }

}
