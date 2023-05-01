using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSounds : MonoBehaviour
{

    public AudioSource[] hitSounds;

    private Entity entity;

    void Awake()
    {
        entity = GetComponentInParent<Entity>();

        entity.onEntityTakeDamage.AddListener((damage, pushForce) => {
            var sound = hitSounds[Random.Range(0, hitSounds.Length)];
            sound.pitch = Random.Range(0.9f, 1.1f);

            sound.Play();
        });
    }

}
