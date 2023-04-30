using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Stage
{

    public float spawnCooldown;
    public int mapSizeFactor;
    public int jobPrice;

    public static Stage GetStage (int stage) {
        return new Stage {
            jobPrice = Mathf.Min(Mathf.FloorToInt(1000 * Mathf.Pow(1.4f, stage)), 1000000),
            mapSizeFactor = GetMapSizeFactor(stage),
            spawnCooldown = Mathf.Max(7f * Mathf.Pow(0.85f, stage), 0.4f)
        };
    }

    public static int GetMapSizeFactor (int stage) {
        if (stage < 3) {
            return 6;
        }

        if (stage < 5) {
            return 8;
        }

        if (stage < 10) {
            return 10;
        }

        return Mathf.Min(stage, 20);
    }

}
