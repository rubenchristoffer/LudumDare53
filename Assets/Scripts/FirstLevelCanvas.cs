using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstLevelCanvas : MonoBehaviour
{

    public GameState gameState;

    void Start () {
        Time.timeScale = 0f;

        if (gameState.currentStageNumber > 1) {
            gameObject.SetActive(false);
            Time.timeScale = 1f;
        }
    }

}
