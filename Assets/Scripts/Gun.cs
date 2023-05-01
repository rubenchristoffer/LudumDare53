using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{

    public PlayerInventory playerInventory;
    public GameObject gunFireSound;
    public float timeBetweenFire = 0.1f;
    public Transform aimPoint;
    public Transform raycastAimPoint;
    public GameObject projectilePrefab;
    public LayerMask enemyLayerMask;
    public AmmoType ammoType;

    private Entity entity;
    private Projectile projectile;
    private float fireTimer;
    private HUD hud;

    public int ammo {
        get => ammoType switch {
            AmmoType.Unlimited => int.MaxValue,
            AmmoType.Uzi => playerInventory.uziAmmo,
            AmmoType.AK47 => playerInventory.akAmmo,
            _ => 0
        };

        set {
            switch (ammoType) {
                case AmmoType.Uzi: playerInventory.uziAmmo = value; break;
                case AmmoType.AK47: playerInventory.akAmmo = value; break;
            }
        }
    }

    public enum AmmoType {
        Unlimited,
        Uzi,
        AK47
    }

    void Awake()
    {
        entity = GetComponentInParent<Entity>();
        hud = FindObjectOfType<HUD>();

        projectile = projectilePrefab.GetComponent<Projectile>();
    }

    // Update is called once per frame
    void Update()
    {
        if (entity.isDead || GameManager.Instance.isLevelCleared || Time.timeScale <= 0.03f)
        {
            return;
        }

        if (ammo < int.MaxValue) {
            hud.ammoText.text = $"{ammo}";
        } else {
            hud.ammoText.text = "INF";
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

        if (Physics.Raycast(raycastAimPoint.position, aimPoint.transform.forward, out var hit, 1f, enemyLayerMask))
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
