using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    public GameState gameState;
    public PlayerInventory playerInventory;

    public Button playButton;
    public Button quitButton;

    void Awake () {
        gameState.Reset();
        playerInventory.Reset();

        playButton.onClick.AddListener(() => {
            SceneManager.LoadScene("Game");
        });

        quitButton.onClick.AddListener(() => {
            if (Application.platform != RuntimePlatform.WebGLPlayer) {
                Application.Quit();
            }
        });
    }

}
