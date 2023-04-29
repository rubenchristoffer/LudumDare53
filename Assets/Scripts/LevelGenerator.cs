using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{

    public GameObject mapSegmentPrefab;
    public GameObject wallPrefab;
    public GameObject[] tilePrefabs;

    public List<GameObject> mapSegments;

    void Awake()
    {
        GenerateMap(8);    
    }

    public Vector3 GetWorldPosition(int xCoordinate, int yCoordinate) {
        return new Vector3(xCoordinate * 60, 0, yCoordinate * 60);
    }

    public float GetMapSize (int sizeFactor) {
        return sizeFactor * 60f;
    }

    public void GenerateMap (int sizeFactor) {
        for (int x = 0; x < sizeFactor; x++) {
            for (int y = 0; y < sizeFactor; y++) {
                GameObject obj = Instantiate<GameObject>(mapSegmentPrefab, GetWorldPosition(x, y), Quaternion.identity);

                foreach (var tile in obj.GetComponentsInChildren<Tile>()) {
                    tile.PopulateTile(tilePrefabs[Random.Range(0, tilePrefabs.Length)]);
                }
            }
        }

        float mapSize = GetMapSize(sizeFactor);

        SpawnWall(sizeFactor, new Vector3(mapSize / 2f - 30f, 0f, 0f), Vector3.zero);
        SpawnWall(sizeFactor, new Vector3(mapSize - 60f - 30f, 0f, mapSize / 2f - 60f), new Vector3(0f, -90f, 0f));

        SpawnWall(sizeFactor, new Vector3(mapSize / 2f - 30f, 0f, mapSize - 60f * 2f), Vector3.zero);
        SpawnWall(sizeFactor, new Vector3(30f, 0f, mapSize / 2f - 60f), new Vector3(0f, -90f, 0f));
    }

    private void SpawnWall (int sizeFactor, Vector3 position, Vector3 localEulerAngles) {
        GameObject wall = Instantiate<GameObject>(wallPrefab, 
            position,
            Quaternion.identity
        );

        wall.transform.localScale = new Vector3(GetMapSize(sizeFactor - 2), 1f, 1f);
        wall.transform.localEulerAngles = localEulerAngles;
    }
}