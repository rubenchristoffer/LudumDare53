using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class House : MonoBehaviour
{

    public AudioSource knockAudio;
    public bool correctHouse;
    public bool knocked;
    public Transform door;

    private HUD hud;
    private Transform _player;

    void Awake() {
        hud = GameObject.FindAnyObjectByType<HUD>();
        _player = GameObject.FindWithTag("Player").transform;
    }

    void LateUpdate(){
        if (knocked) {
            return;
        }

        float distance = Vector3.Distance(door.position, _player.position);

        if (distance <= 5f) {
            hud.displayKnockText = true;

            if (Input.GetButton("Interact")) {
                knockAudio.Play();
                knocked = true;

                if (correctHouse)
                {
                    GameManager.Instance.SetLevelCleared();
                } else {
                    hud.nobodyRespondsText.GetComponent<Animator>().SetTrigger("Active");
                }
            }
        }
    }

}
