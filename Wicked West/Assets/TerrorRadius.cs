using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Class will check for the player within the radius and play effects accordingly.
public class TerrorRadius : MonoBehaviour
{
    GameObject player;
    float distance;
    public AudioSource audioSource;

    [SerializeField] float radiusFar;
    [SerializeField] float radiusClose;

    public AudioClip hb;
    public AudioClip hbfast;

    public GameObject spotlight;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        // audioSource = GetComponent<AudioSource>();
        spotlight = GameObject.Find("spookylight");
    }

    // 
    void FixedUpdate()
    {
        checkRadius();
    }

    private void Update()
    {
        if (GameManager.Instance.isPaused == true)
        {
            audioSource.Pause();
        }
    }

    void checkRadius()
    {
        // Get the distance between the player and the monster
        float distance = Vector3.Distance(player.transform.position, transform.position);

        if (distance > radiusFar) // too far!
        {
            far();
        }
        else if (distance > radiusClose)// if close enough to be within patrol range, but not within chase range... 
        {
            close();
        }
        else // Player is near/in attack range!
        {
            danger();
        }
    }

    private void far()
    {
        if (audioSource.isPlaying)
            audioSource.Stop();

        spotlight.SetActive(false);
    }

    private void close()
    {
        audioSource.clip = hb;
        // play sound effect at a moderate pace!
        if (!audioSource.isPlaying)
            audioSource.Play();
        // add vignette!

        spotlight.SetActive(true);
    }

    private void danger()
    {
        audioSource.clip = hbfast;
        if (!audioSource.isPlaying)
            audioSource.Play();
    }
}
