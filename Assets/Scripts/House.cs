using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class House : MonoBehaviour
{

    private HUD hud;

    public Transform door;

    private Transform _player;

    public bool correctHouse;
    public bool knocked;

    void Awake () {
        hud = GameObject.FindAnyObjectByType<HUD>();
        _player = GameObject.FindWithTag("Player").transform;
    }

    void LateUpdate () {
        if (knocked) {
            return;
        }

        float distance = Vector3.Distance(door.position, _player.position);

        if (distance < 20) {
            Debug.Log(distance);
        }

        if (distance <= 5f) {
            hud.displayKnockText = true;

            if (Input.GetButton("Interact")) {

                knocked = true;

                if (correctHouse) {
                    GameManager.Instance.SetLevelCleared();
                }
            }
        }
    }

}
