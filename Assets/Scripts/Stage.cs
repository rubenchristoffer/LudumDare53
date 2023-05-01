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
        int jobPrice = Mathf.FloorToInt(200 * Mathf.Pow(1.4f, stage));

        if (jobPrice < 0) {
            jobPrice = 1000000;
        }

        return new Stage {
            jobPrice = jobPrice,
            mapSizeFactor = GetMapSizeFactor(stage),
            spawnCooldown = Mathf.Max(7f * Mathf.Pow(0.75f, stage), 0f)
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

        return Mathf.Min(stage, 15);
    }

}
