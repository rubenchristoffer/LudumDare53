using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System.Linq;

public class GameManager : MonoBehaviour
{

    private static GameManager _instance;

    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<GameManager>();
            }

            return _instance;
        }
    }

    public GameState gameState;
    public PlayerInventory playerInventory;

    public UnityEvent onLevelCleared { get; private set; } = new UnityEvent();

    public bool isLevelCleared { get; private set; }
    public bool hasLevelFailed { get; private set; }

    private EnemySpawner[] enemySpawners;
    private int currentSpawnerIndex;
    private float enemySpawnTimer;

    public void SetLevelCleared()
    {
        if (!isLevelCleared)
        {
            isLevelCleared = true;
            onLevelCleared.Invoke();
        }
    }

    public void SetLevelFailed()
    {
        if (!hasLevelFailed)
        {
            hasLevelFailed = true;
        }
    }

    void Awake()
    {
        playerInventory.hasFoundGPS = false;
        gameState.ResetCounters();
        gameState.currentStageNumber++;
        gameState.currentStage = Stage.GetStage(gameState.currentStageNumber);

        LevelGenerator.Instance.GenerateMap(gameState.currentStage.mapSizeFactor);
    }

    void Start () {
        Transform player = GameObject.FindWithTag("Player").transform;

        player.position = GameObject.FindObjectsOfType<SpawnPoint>()
            .Where(sp => sp.isActivated)
            .First().transform.position;

        enemySpawners = GameObject.FindObjectsOfType<EnemySpawner>();
        enemySpawners = enemySpawners
            .OrderBy(key => Random.Range(0, enemySpawners.Length - 1))
            .ToArray();
    }

    void Update()
    {
        if (enemySpawnTimer <= 0f)
        {
            enemySpawners[currentSpawnerIndex].Spawn();

            currentSpawnerIndex = (currentSpawnerIndex + 1) % enemySpawners.Length;

            enemySpawnTimer = gameState.currentStage.spawnCooldown;
        }
        else
        {
            enemySpawnTimer -= Time.deltaTime;
        }
    }

}