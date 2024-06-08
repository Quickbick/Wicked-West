using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

// Responsible for when the player gets hit, component will be called
public class PlayerHealth : MonoBehaviour
{
    public GameManager manager;
    public Image healthBar;

    public GameObject screenflash;

    // audio clips for taking damage
    private AudioSource audioSource;
    public AudioClip scream;
    [SerializeField] public GameObject IGFaderGO;


    // Start is called before the first frame update
    void Start()
    {
        manager = GameManager.Instance; // for referencing the health variable

        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        // debugging
        // if (Input.GetKeyDown(KeyCode.P)) takeDamage();

    }

    private void FixedUpdate()
    {
        healthBar.fillAmount = manager.Health / 3f;
    }

    public void takeDamage()
    {
        manager.Health -= 1f;

        if (!GameManager.Instance.bossActive)
        {
            StartCoroutine(ShowAndHide(screenflash, 5.0f));
        }

        audioSource.clip = scream;
        audioSource.Play();

    }

    IEnumerator ShowAndHide(GameObject sc, float delay)
    {
        sc.SetActive(true);

        // * new with player ending up back in bed:
        yield return new WaitForSeconds(delay * 0.8f);
        
        // ! [JOHN] put teleport to morning here , now that the -1 has occurred.
        // GameObject.GetComponent<
        if (IGFaderGO) {

            GameManager.Instance.transform.Find("NightTimer").GetComponent<NightCycleIncrementer>().timer = 0f; // ! Please work!
            Debug.Log("Attempted to reset night timer w/ death");

            IGFaderGO.GetComponent<InGameFader>().FallAsleep_ToMorning("The monster lashes out at you and you pass out. (-1 Life)");

        }

        else {

            Debug.LogError("Ingame fader not found, this should be in the main scene.");

        }

        yield return new WaitForSeconds(delay * 0.2f);

        // reset health if zero and set to lose scene
        if (GameManager.Instance.Health <= 0)
        {

            // WAS: GameManager.Instance.Health = 3;
            // * Trying deletion, since switching back to outside scenes is based off save/loads:
            GameManager.DestroyInstance(); //  Since DestroyInstance() is a static method, you should call it directly on the class without referencing an instance.

            // Now, lose scene.
            SceneManager.LoadScene("LoseScene");
        }

        sc.SetActive(false);
        
    }

}
