using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class LevelGenerator : MonoBehaviour
{

    private static LevelGenerator _instance;

    public static LevelGenerator Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<LevelGenerator>();
            }

            return _instance;
        }
    }

    public GameObject mapSegmentPrefab;
    public GameObject wallPrefab;
    public TileSpawnInfo[] tileSpawnInfoArray;

    [System.Serializable]
    public class TileSpawnInfo
    {
        public GameObject prefab;
        public int spawnWeight = 1;
        public int maxPerSegment = 4;
        public int index { get; set; }
    }

    public Vector3 GetWorldPosition(int xCoordinate, int yCoordinate)
    {
        return new Vector3(xCoordinate * 60, 0, yCoordinate * 60);
    }

    public float GetMapSize(int sizeFactor)
    {
        return sizeFactor * 60f;
    }

    public void GenerateMap(int sizeFactor)
    {
        List<TileSpawnInfo> weightedList = new List<TileSpawnInfo>();

        for (int i = 0; i < tileSpawnInfoArray.Length; i++)
        {
            tileSpawnInfoArray[i].index = i;

            for (int x = 0; x < tileSpawnInfoArray[i].spawnWeight; x++)
            {
                weightedList.Add(tileSpawnInfoArray[i]);
            }
        }

        Vector2Int playerSpawnPoint = new Vector2Int(
            Random.Range(1, sizeFactor - 1),
            Random.Range(1, sizeFactor - 1)
        );

        for (int x = 0; x < sizeFactor; x++)
        {
            for (int y = 0; y < sizeFactor; y++)
            {
                GameObject obj = Instantiate<GameObject>(mapSegmentPrefab, GetWorldPosition(x, y), Quaternion.identity);

                if (x == playerSpawnPoint.x && y == playerSpawnPoint.y)
                {
                    obj.GetComponentInChildren<SpawnPoint>().isActivated = true;
                }

                List<TileSpawnInfo> usedTiles = new List<TileSpawnInfo>();

                foreach (var tile in obj.GetComponentsInChildren<Tile>())
                {
                    TileSpawnInfo randomTile;

                    if (x >= 2 && y >= 2 && x <= sizeFactor - 3 && y <= sizeFactor - 3)
                    {
                        while (true)
                        {
                            randomTile = weightedList[Random.Range(0, weightedList.Count)];

                            if (usedTiles.Where(tile => tile.index == randomTile.index).Count() <= randomTile.maxPerSegment)
                            {
                                break;
                            }
                        }
                    } else {
                        randomTile = tileSpawnInfoArray[0];
                    }


                    usedTiles.Add(randomTile);

                    tile.PopulateTile(randomTile.prefab);
                }
            }
        }

        var houses = GameObject.FindObjectsOfType<House>();

        houses[Random.Range(0, houses.Length)].correctHouse = true;

        float mapSize = GetMapSize(sizeFactor);

        SpawnWall(sizeFactor, new Vector3(mapSize / 2f - 30f, 0f, 0f), Vector3.zero);
        SpawnWall(sizeFactor, new Vector3(mapSize - 60f - 30f, 0f, mapSize / 2f - 60f), new Vector3(0f, -90f, 0f));

        SpawnWall(sizeFactor, new Vector3(mapSize / 2f - 30f, 0f, mapSize - 60f * 2f), Vector3.zero);
        SpawnWall(sizeFactor, new Vector3(30f, 0f, mapSize / 2f - 60f), new Vector3(0f, -90f, 0f));
    }

    private void SpawnWall(int sizeFactor, Vector3 position, Vector3 localEulerAngles)
    {
        GameObject wall = Instantiate<GameObject>(wallPrefab,
            position,
            Quaternion.identity
        );

        wall.transform.localScale = new Vector3(GetMapSize(sizeFactor - 2), 1f, 1f);
        wall.transform.localEulerAngles = localEulerAngles;
    }

}