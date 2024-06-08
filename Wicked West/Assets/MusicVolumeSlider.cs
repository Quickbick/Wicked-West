using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MusicVolumeSlider : MonoBehaviour {

    // * Assign through inspector:
    [SerializeField] public Slider slider;

    private void Start() {

        slider.onValueChanged.AddListener(ChangeMusicVolume);

    }

    private void ChangeMusicVolume(float value) {

        // * hopefully I set up audiomanagerscript instance correct.....
        AudioManagerScript audioManager = AudioManagerScript.instance;
        if (audioManager != null)
        {
            audioManager.SetMusicVolume(value);
        }
        else
        {
            Debug.LogWarning("[!!!!] AudioManager instance not found.");
        }

    }

}
