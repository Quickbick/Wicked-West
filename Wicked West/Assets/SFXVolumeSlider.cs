using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SFXVolumeSlider : MonoBehaviour {

    // * Assign through inspector:
    public Slider slider;

    private void Start() {

        slider.onValueChanged.AddListener(ChangeSFXVolume);

    }

    private void ChangeSFXVolume(float value) {

        AudioManagerScript audioManager = AudioManagerScript.instance;
        if (audioManager != null)
        {
            audioManager.SetSoundEffectVolume(value);
        }
        else
        {
            Debug.LogWarning("[!!!!] AudioManager instance not found.");
        }

    }

}

