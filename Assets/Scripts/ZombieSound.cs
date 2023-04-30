using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieSound : MonoBehaviour
{

    public AudioSource[] normalSounds;
    public AudioSource[] attackSounds;
    public float playDistance = 30f;

    private float noiseTimer;
    private Transform player;
    private Enemy enemy;

    void Awake () {
        player = GameObject.FindWithTag("Player").transform;
        enemy = GetComponentInParent<Enemy>();
        noiseTimer = 1f;

        enemy.onAttack.AddListener(() => {
            var sound = attackSounds[Random.Range(0, attackSounds.Length)];
            sound.pitch = Random.Range(0.9f, 1.1f);

            sound.Play();
        });
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(player.position, transform.position) > playDistance || enemy.isDead) {
            return;
        }

        if (noiseTimer <= 0f) {
            var sound = normalSounds[Random.Range(0, normalSounds.Length)];
            sound.pitch = Random.Range(0.9f, 1.1f);

            sound.Play();
            noiseTimer = Random.Range(2.8f, 3.2f);
        } else {
            noiseTimer -= Time.deltaTime;
        }
    }
}
