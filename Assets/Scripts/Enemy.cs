using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Entity
{

    public float strength = 10f;
    public float speed = 2f;
    public float attackDistance = 1f;
    public GameState gameState;

    private Entity _player;
    private Rigidbody _rigidbody;
    private Collider[] _colliders;

    private Vector3 velocity;
    private Vector3 targetVelocity;
    private float attackCooldown = 1f;
    private float attackDelay = 0.4f;
    private float attackTimer;

    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.FindWithTag("Player").GetComponent<Entity>();
        _rigidbody = GetComponent<Rigidbody>();
        _colliders = GetComponentsInChildren<Collider>(true);

        speed = Random.Range(2f, 10f);

        onEntityDie.AddListener(() => {
            _rigidbody.freezeRotation = false;
            _rigidbody.useGravity = true;

            gameState.killCount++;
        });
    }

    // Update is called once per frame
    void Update()
    {
        if (isDead) {
            return;
        }

        Vector3 dir = (_player.transform.position - transform.position).normalized;
        targetVelocity = dir * speed;

        velocity = Vector3.MoveTowards(_rigidbody.velocity, targetVelocity, Time.deltaTime * 200f);
        
        transform.forward = dir;

        if (Vector3.Distance(_player.transform.position, transform.position) > attackDistance) {
            if (attackTimer < attackDelay) {
                attackTimer = attackDelay;  
            }
        }

        if (attackTimer <= 0) {
            _player.InflictDamage(strength, transform.forward * 20f);
            attackTimer = attackCooldown;
        } else {
            attackTimer -= Time.deltaTime;
        }
    }

    void FixedUpdate () {
        if (isDead) {
            return;
        }

        _rigidbody.velocity = velocity;
    }
}
