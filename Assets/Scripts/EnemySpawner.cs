using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{

    public GameObject enemyPrefab;
    public GameObject effectPrefab;
    public GameState gameState;

    private Transform _player;

    void Awake () {
        _player = GameObject.FindWithTag("Player").transform;
    }

    public void Spawn()
    {
        if (!isActiveAndEnabled || GameManager.Instance.isLevelCleared || gameState.livingEnemies >= 1000) {
            return;
        }

        Vector3 spawnPosition = transform.position + Random.insideUnitSphere * 5f;
        spawnPosition.y = transform.position.y;

        var enemy = Instantiate<GameObject>(enemyPrefab, spawnPosition, Quaternion.identity);
        var effect = Instantiate<GameObject>(effectPrefab, spawnPosition, Quaternion.identity);

        effect.transform.SetParent(enemy.transform);
    }

}
