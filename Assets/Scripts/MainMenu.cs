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

    public Slider masterSlider;
    public Slider musicSlider;
    
    public Button resetProgressButton;

    void Awake () {
        masterSlider.value = PlayerPrefs.GetFloat("MasterVolume", 1f);
        musicSlider.value = PlayerPrefs.GetFloat("MusicVolume", 1f);

        AudioListener.volume = masterSlider.value;
        audioSettings.musicVolume = musicSlider.value;

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

        masterSlider.onValueChanged.AddListener((float value) => {
            PlayerPrefs.SetFloat("MasterVolume", value);
            AudioListener.volume = value;
        });

        musicSlider.onValueChanged.AddListener((float value) => {
            audioSettings.musicVolume = value;
            PlayerPrefs.SetFloat("MusicVolume", value);
        });

        resetProgressButton.onClick.AddListener(() => {
            gameState.Reset();
            playerInventory.Reset();

            gameState.Save();
            playerInventory.Save();
        });
    }

}
