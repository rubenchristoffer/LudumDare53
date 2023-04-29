using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{

    public GameObject mapSegmentPrefab;
    public GameObject[] tilePrefabs;

    public List<GameObject> mapSegments;

    void Awake()
    {
        GenerateMap(10);    
    }

    public Vector3 GetWorldPosition(int xCoordinate, int yCoordinate) {
        return new Vector3(xCoordinate * 60, 0, yCoordinate * 60);
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
    }
}