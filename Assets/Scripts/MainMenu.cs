using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    public AudioSettings audioSettings;
    public GameState gameState;
    public PlayerInventory playerInventory;

    public Button playButton;
    public Button quitButton;
    
    public Button resetProgressButton;

    void Awake () {
        gameState.Reset();
        playerInventory.Reset();

        gameState.Load();
        playerInventory.Load();

        playButton.onClick.AddListener(() => {
            SceneManager.LoadScene("Game");
        });

        quitButton.onClick.AddListener(() => {
            if (Application.platform != RuntimePlatform.WebGLPlayer) {
                Application.Quit();
            }
        });
        
        resetProgressButton.onClick.AddListener(() => {
            gameState.Reset();
            playerInventory.Reset();

            gameState.Save();
            playerInventory.Save();
        });
    }

}
