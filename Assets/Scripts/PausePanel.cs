using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PausePanel : MonoBehaviour
{

    public GameState gameState;
    public Button continueButton;
    public Button quitButton;

    private bool paused;
    private Transform child;
    private HUD hud;

    void Awake () {
        child = transform.GetChild(0);
        hud = FindObjectOfType<HUD>();

        continueButton.onClick.AddListener(() => {
            SetPaused(false);
        });

        quitButton.onClick.AddListener(() => {
            SetPaused(false);
            hud.gameObject.SetActive(false);
            gameState.currentStageNumber--;

            SceneManager.LoadScene("MainMenu");
        });
    }

    void Update () {
        if (Input.GetButtonDown("Pause")) {
            SetPaused(!paused);
        }

        child.gameObject.SetActive(paused);
    }
    
    public void SetPaused (bool pause) {
        paused = pause;
        Time.timeScale = paused ? 0f : 1f;
    }

}
