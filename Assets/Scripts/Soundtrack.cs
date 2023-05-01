using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soundtrack : MonoBehaviour
{

    public AudioSettings audioSettings;
    public bool fadeOut;

    private AudioSource audioSource;

    private float volume;
    private float maxVolume = 0.3f;
    private float fadeOutTime = 5f;

    void Awake () {
        audioSource = GetComponent<AudioSource>();
        volume = maxVolume;
    }

    void Update () {
        if (fadeOut) {
            volume = Mathf.MoveTowards(volume, 0f, maxVolume / fadeOutTime * Time.deltaTime);
        }

        audioSource.volume = volume * audioSettings.musicVolume;

        if (volume <= 0f) {
            if (audioSource.isPlaying) {
                audioSource.Stop();
            }
        }
    }

    public void StartFadeOut () {
        fadeOut = true;
    }

}
