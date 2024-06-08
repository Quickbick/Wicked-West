// * Usings section:
using System;
using System.Linq;
using UnityEngine;

// * AND:
using UnityEngine;
using UnityEngine.Audio;

// * Message on Code Comments in this File:
// ? INSTALL + APPLY the plugin 'Better Comments' by iguissouma to see color-differentiation between comments dependent on header sybmols (*, ?, !, etc.)
// ! This isn't mandatory, just a note about file comments, but it is quick for anyone to add (<1 minute).

// * AudioManager class:
public class AudioManagerScript : MonoBehaviour
{
    // * New Fields for music and sound effects
    public Sound[] musicSounds; // For background music
    public Sound[] soundEffects; // For sound effects
    //! Old: // public Sound[] sounds;
    public static AudioManagerScript instance;

    // * When awake, create the instance of yourself.
    void Awake() {

        if (instance == null)
        {
            instance = this;
        }

        else
        {
            Destroy(gameObject);
            return;
        }

        // lives through transitioning
        DontDestroyOnLoad(gameObject);
        // * Initialize music sounds
        InitializeSounds(musicSounds);
        // * Initialize sound effects
        InitializeSounds(soundEffects);

        // ! Old:
        /*
        foreach (Sound sound in sounds) {

            sound.source = gameObject.AddComponent<AudioSource>();
            sound.source.clip = sound.clip;

            sound.source.volume = sound.volume;
            sound.source.pitch = sound.pitch;
            sound.source.loop = sound.loop;

        }
        */

    }

    // * Begin on start.
    void Start() {

        // * Play background music
        PlayMusic("bg_music");

    }

    // ! New for sliders and differentiation:
    // * Play background music
    public void PlayMusic(string name)
    {
        Sound snd = Array.Find(musicSounds, sound => sound.name == name);
        Play(snd);
        Debug.Log("Played music...");
    }

    // * Play sound effect
    public void PlaySoundEffect(string name)
    {
        Sound snd = Array.Find(soundEffects, sound => sound.name == name);
        Play(snd);
    }

    // ! Old Play function:
    /*
    public void Play(string name) {

        Sound snd = Array.Find(sounds, sound => sound.name == name);

        try {

            snd.source.Play();

        }

        catch (Exception e) {

            Debug.LogWarning("sound not found");

        }
        
    }
    */
    // * New play function for both music and sound effects: 
    private void Play(Sound snd) {

        try { snd.source.Play(); }
        catch (Exception e) {
            
            Debug.LogWarning("Sound not found");

        }

    }

    // New method to adjust the global volume for music
    public void SetMusicVolume(float volume)
    {
        AdjustVolume(musicSounds, volume);
    }

    // New method to adjust the global volume for sound effects
    public void SetSoundEffectVolume(float volume)
    {
        AdjustVolume(soundEffects, volume);
    }

    // Helper method to adjust the volume of all sounds
    private void AdjustVolume(Sound[] sounds, float volume)
    {
        foreach (Sound sound in sounds)
        {
            sound.source.volume = volume;
            Debug.Log("ADJUSTED A SOUND.");
        }
    }

    // Helper method to initialize sounds
    private void InitializeSounds(Sound[] sounds)
    {
        foreach (Sound sound in sounds)
        {
            sound.source = gameObject.AddComponent<AudioSource>();
            sound.source.clip = sound.clip;
            sound.source.volume = sound.volume;
            sound.source.pitch = sound.pitch;
            sound.source.loop = sound.loop;
        }
    }

}

// * Thus, to play sounds from other scripts:
// ! FindObjectOfType<AudioManager>().Play("Click sound");
// ? FindObjectOfType<AudioManagerScript>().Play("Click sound"); <- Actually now i think this maybe

// * Make sound, can be refactored elsewhere if desired.
[System.Serializable]
public class Sound {

    public string name;
    public AudioClip clip;
    
    [Range(0f,1f)]
    public float volume;
    [Range(0f,3f)]
    public float pitch;

    public bool loop;

    [HideInInspector] public AudioSource source;

}