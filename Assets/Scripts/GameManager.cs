using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System.Linq;

public class GameManager : MonoBehaviour
{

    private static GameManager _instance;

    public static GameManager Instance {
        get {
            if (_instance == null) {
                _instance = GameObject.FindObjectOfType<GameManager>();
            }

            return _instance;
        }
    }

    public UnityEvent onLevelCleared { get; private set; } = new UnityEvent();

    public bool isLevelCleared { get; private set; }
    public bool hasFoundGPS { get; private set; }

    private EnemySpawner[] enemySpawners;
    private int currentSpawnerIndex;
    private float enemySpawnTimer;

    public void SetLevelCleared () {
        if (!isLevelCleared) {
            isLevelCleared = true;
            onLevelCleared.Invoke();

            Debug.Log("level cleared");
        }
    }

    public void SetGPSFound () {
        hasFoundGPS = true;
    }

    void Awake () {
        LevelGenerator.Instance.GenerateMap(8);

        Transform player = GameObject.FindWithTag("Player").transform;

        player.position = FindObjectsOfType<SpawnPoint>()
            .Where(sp => sp.isActivated)
            .First().transform.position;

        enemySpawners = FindObjectsOfType<EnemySpawner>();
        enemySpawners = enemySpawners
            .OrderBy(key => Random.Range(0, enemySpawners.Length - 1))
            .ToArray();
    }

    void Update () {
        if (enemySpawnTimer <= 0f) {
            enemySpawners[currentSpawnerIndex].Spawn();

            currentSpawnerIndex = (currentSpawnerIndex + 1) % enemySpawners.Length;

            enemySpawnTimer = 0.01f;
        } else {
            enemySpawnTimer -= Time.deltaTime;
        }
    }

}