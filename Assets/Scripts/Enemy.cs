using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Entity
{

    public float speed = 2f;

    private Transform _player;
    private Rigidbody _rigidbody;
    private Collider[] _colliders;

    private Vector3 velocity;
    private Vector3 targetVelocity;

    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.FindWithTag("Player").transform;
        _rigidbody = GetComponent<Rigidbody>();
        _colliders = GetComponentsInChildren<Collider>(true);

        onEntityDie.AddListener(() => {
            _rigidbody.freezeRotation = false;
            _rigidbody.useGravity = true;
        });
    }

    // Update is called once per frame
    void Update()
    {
        if (isDead) {
            return;
        }

        Vector3 dir = (_player.position - transform.position).normalized;
        targetVelocity = dir * speed;

        velocity = Vector3.MoveTowards(_rigidbody.velocity, targetVelocity, Time.deltaTime * 200f);
        
        transform.forward = dir;
    }

    void FixedUpdate () {
        if (isDead) {
            return;
        }

        _rigidbody.velocity = velocity;
    }
}
