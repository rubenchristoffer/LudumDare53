using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{

    public GameObject enemyPrefab;

    private Transform _player;

    public void Spawn()
    {
        if (!isActiveAndEnabled || GameManager.Instance.isLevelCleared) {
            return;
        }

        Vector3 spawnPosition = transform.position + Random.insideUnitSphere * 5f;
        spawnPosition.y = transform.position.y;

        Instantiate<GameObject>(enemyPrefab, spawnPosition, Quaternion.identity);
    }

}
