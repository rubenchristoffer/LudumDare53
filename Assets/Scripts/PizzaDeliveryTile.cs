using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PizzaDeliveryTile : MonoBehaviour
{

    private Transform _player;

    void Awake () {
        _player = GameObject.FindWithTag("Player").transform;
    }

    void Update () {
        if (Vector3.Distance(_player.position, transform.position) < 5f) {
            GameManager.Instance.SetLevelCleared();
        }
    }

}
