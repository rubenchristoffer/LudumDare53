using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundPanelUI : MonoBehaviour
{

    public AudioSettings audioSettings;
    public Slider masterSlider;
    public Slider musicSlider;

    void Awake()
    {
        masterSlider.value = PlayerPrefs.GetFloat("MasterVolume", 1f);
        musicSlider.value = PlayerPrefs.GetFloat("MusicVolume", 1f);

        AudioListener.volume = masterSlider.value;
        audioSettings.musicVolume = musicSlider.value;

         masterSlider.onValueChanged.AddListener((float value) => {
            PlayerPrefs.SetFloat("MasterVolume", value);
            AudioListener.volume = value;
            PlayerPrefs.Save();
        });

        musicSlider.onValueChanged.AddListener((float value) => {
            audioSettings.musicVolume = value;
            PlayerPrefs.SetFloat("MusicVolume", value);
            PlayerPrefs.Save();
        });
    }
}
