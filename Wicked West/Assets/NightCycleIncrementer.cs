using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; // Required for TextMeshProUGUI
using QFSW.QC;

// * [NEW(3/1/24)]:
public class NightCycleIncrementer : MonoBehaviour
{
    private GameManager gameManager; // Reference to the GameManager
    public float timer = 0f; // Timer to track elapsed time // ! Public for pres
    private float secondsElapsedTotal = 0; // Total seconds elapsed
    [SerializeField]private float incrementInterval = 720f; // 720 seconds for 2 hours (3am to 5am)
    public int hours;
    public int minutes;
    public string ampm;

    // UI text to display the clock
    [SerializeField] public TextMeshProUGUI clockText;
    [SerializeField] public Material redSkybox; // Daytime skybox material
    [SerializeField] public Material nightSkybox; // Nighttime skybox material
    [HideInInspector] public Material activeSkybox; // Holds the active skybox material
    public bool fadeToDayFADER;
    public bool fadeToDayAKAHaltMovement;
    public bool fadeToDayAKASleep;


    private void Start()
    {
        // Get a reference to the GameManager using the Singleton pattern
        gameManager = GameManager.Instance;

        fadeToDayAKASleep = false;
        fadeToDayFADER = false;
        fadeToDayAKAHaltMovement = false;

        // Setting the vertex color alpha to be completely clear (transparent)
        // ? Needed?> Color textColor = clockText.color;
        // ? Needed?> textColor.a = 0.95f; // Set alpha to 0 for complete transparency when night mode in 100%.
        // ? Needed?> clockText.color = textColor;

        // Initialize the clock text
        UpdateClockText();

    }

    private void Update()
    {
        if (!GameManager.Instance.isNighttime)
        {
            // Handle pause logic here if needed
        }
        else
        {
            // Update the timer with the time since the last frame
            timer += Time.deltaTime;
            // Also update total seconds:
            secondsElapsedTotal += Time.deltaTime;

            // Check if the timer has reached the incrementInterval
            if (timer >= incrementInterval)
            {
                // Reset the timer
                timer = 0f;

                // Increment the 'night' variable in the GameManager
                // GameManager.Instance.nights += 1;
                StartFadeToDayCoroutine(1.3f); 
                // Print the current number of nights to the console
                // Debug.Log("NIGHTS:  " + gameManager.nights);
            }

            // Update clock text.
            UpdateClockText();
        }

        // Also, handle skyboxes here
        if (GameManager.Instance.isNighttime && (activeSkybox != nightSkybox))
        {
            ChangeSkybox(nightSkybox);
            Debug.Log("Skybox changed to nighttime");
        }

        if (!GameManager.Instance.isNighttime && (activeSkybox != redSkybox))
        {
            ChangeSkybox(redSkybox);
            Debug.Log("Skybox changed to red (daytime)");
        }
    }

    [Command]
    public void ToMorning() {

        
        if (GameManager.Instance.isNighttime) {

            GameManager.Instance.transform.Find("NightTimer").GetComponent<NightCycleIncrementer>().timer = 0f;
            GameManager.Instance.transform.Find("NightTimer").GetComponent<NightCycleIncrementer>().StartFadeToDayCoroutine(1.3f); 

        }

        else {

            Debug.Log("Already day detected, failed to switch.");

        }

    }

    public void StartFadeToDayCoroutine(float coSec) {

        StartCoroutine(FadeToDayCoroutine(coSec));

    }

    // Coroutine that waits for a specified number of seconds and then sets fadeToDayFADER to true
    private IEnumerator FadeToDayCoroutine(float coSec)
    {

        // * Restrict movement:
        fadeToDayAKAHaltMovement = true;

        yield return new WaitForSeconds(coSec * 0.8f);

        // * Trigger sleepvar I think now:
        fadeToDayAKASleep = true;

        yield return new WaitForSeconds(coSec * 0.2f); // Wait for coSec seconds
        fadeToDayFADER = true; // Set fadeToNightFADER to true after waiting
    }


    // Update display of clock text from 3am to 5am
    private void UpdateClockText()
    {
        // Calculate the time of night in hours (from 0 to 2)
        float timeOfNightHours = (timer / incrementInterval) * 2;

        // Convert time to 12-hour format (3am to 5am)
        hours = Mathf.FloorToInt(timeOfNightHours) + 3; // Add 3 to start from 3am
        if (hours >= 12)
        {
            hours -= 12; // Convert to 12-hour format
        }
        if (hours == 0)
        {
            hours = 12;
        }

        // Calculate minutes
        minutes = Mathf.FloorToInt((timeOfNightHours % 1) * 60);

        // Determine AM or PM (night doesn't change)
        ampm = "AM";

        // Update the clock text
        clockText.text = string.Format("{0:D2}:{1:D2} {2}", hours, minutes, ampm);
    }

    // Method to set the clock text reference
    public void SetClockText(TextMeshProUGUI clockText)
    {
        this.clockText = clockText;
        UpdateClockText(); // Update the clock text immediately after setting the reference
    }

    // Function to change the skybox material
    [Command]
    public void ChangeSkybox(Material skyboxMaterial)
    {
        // Set the new skybox material in the RenderSettings
        RenderSettings.skybox = skyboxMaterial;
        activeSkybox = skyboxMaterial;

        // ! TRYING:
        GameManager.Instance.transform.Find("Timer").GetComponent<DayCycleIncrementer>().activeSkybox = skyboxMaterial;
    }

    // DEV CONSOLE COMMANDS:
    [Command]
    public void ChangeSkyboxToRed()
    {
        ChangeSkybox(redSkybox);
        Debug.Log("Skybox changed to red (daytime)");
    }

    [Command]
    public void ChangeSkyboxToNight()
    {
        ChangeSkybox(nightSkybox);
        Debug.Log("Skybox changed to nightmaterial (nighttime)");
    }

    [Command]
    public void NightCycleSetIncrementInterval(float valueSet) {

        GameManager.Instance.transform.Find("NightTimer").GetComponent<NightCycleIncrementer>().incrementInterval = valueSet;

    }

}


// ! [OLD]:
/*
public class NightCycleIncrementer : MonoBehaviour
{
    private GameManager gameManager; // Reference to the GameManager
    private float timer = 0f; // Timer to track elapsed time
    private float secondsElapsedTotal = 0; // Total seconds elapsed
    private float incrementInterval = 1895f; // 360 seconds for 6 minutes (12am to 6am)
    public int hours;
    public int minutes;
    public string ampm;

    // UI text to display the clock
    [SerializeField] public TextMeshProUGUI clockText;
    [SerializeField] public Material redSkybox; // * daytime one, but really like the red (2)
    [SerializeField] public Material nightSkybox; // * Will be "Create - Surface Shader Scene" for now.
    [HideInInspector] public Material activeSkybox; // ! Hopefully holds the active choice.

    private void Start()
    {
        // Get a reference to the GameManager using the Singleton pattern
        gameManager = GameManager.Instance;

        // Setting the vertex color alpha to be completely clear (transparent)
        Color textColor = clockText.color;
        textColor.a = 0.95f; // ? Set alpha to 0 for complete transparency when night mode in 100%.
        clockText.color = textColor;

        // * Set it to be fully opaque in day mode. Not in night.
        // * textColor.a = 1f; // Set alpha to 1 for full opacity

        // Initialize the clock text
        UpdateClockText();
    }

    private void Update() {

        // * NEXT: replace with if (!GameManager.Instance.isNighttime)
        if (GameManager.Instance.isPaused)
        {
            // Handle pause logic here if needed
        }

        else
        {
            // Update the timer with the time since the last frame
            timer += Time.deltaTime;
            // Also update total seconds:
            secondsElapsedTotal += Time.deltaTime;

            // Check if the timer has reached the incrementInterval
            if (timer >= incrementInterval)
            {
                // Reset the timer
                timer = 0f;

                // Increment the 'night' variable in the GameManager
                GameManager.Instance.nights += 1;

                // Print the current number of nights to the console
                Debug.Log("NIGHTS:  " + gameManager.nights);
            }

            // Update clock text.
            UpdateClockText();
        }

        // * Also, handle skyboxes in here, too:
        if (GameManager.Instance.isNighttime && (activeSkybox != nightSkybox)) {

            ChangeSkybox(nightSkybox);
            Debug.Log("Skybox changed to nighttime");

        }

        if (!GameManager.Instance.isNighttime && (activeSkybox != redSkybox)) {

            ChangeSkybox(redSkybox);
            Debug.Log("Skybox changed to red (daytime)");

        }

    }

    // Update display of clock text from 12am to 6am
    private void UpdateClockText() {

        // Calculate the time of night in hours (from 0 to 6)
        float timeOfNightHours = (secondsElapsedTotal / incrementInterval) * 6;

        // Convert time to 12-hour format (12am to 6am)
        hours = Mathf.FloorToInt(timeOfNightHours);
        if (hours == 0)
        {
            hours = 12;
        }

        // Calculate minutes
        minutes = Mathf.FloorToInt((timeOfNightHours % 1) * 60);

        // Determine AM or PM (night doesn't change)
        ampm = "AM";

        // Update the clock text
        clockText.text = string.Format("{0:D2}:{1:D2} {2}", hours, minutes, ampm);
    
    }

    // ![2/21] Method to set the clock text reference
    public void SetClockText(TextMeshProUGUI clockText)
    {
        this.clockText = clockText;
        UpdateClockText(); // Update the clock text immediately after setting the reference
    }

    // * Function to change the skybox material
    [Command]
    public void ChangeSkybox(Material skyboxMaterial) {

        // Set the new skybox material in the RenderSettings
        RenderSettings.skybox = skyboxMaterial;

        activeSkybox = skyboxMaterial;
    
    }  // ! Example usage: call this function to change the skybox dynamically

    // * DEV CONSOLE COMMANDS:
    [Command]
    public void ChangeSkyboxToRed() {

        ChangeSkybox(redSkybox);
        Debug.Log("Skybox changed to red (daytime)");

    }

    [Command]
    public void ChangeSkyboxToNight() {

        ChangeSkybox(nightSkybox);
        Debug.Log("Skybox changed to nightmaterial (nighttime)");
        
    }

    
    //void UpdateSkybox(string caseOp) {

        // ToDo: Assuming you have a reference to the new skybox material
        // You can retrieve it from the Resources folder or other sources
        // Material newSkyboxMaterial = Resources.Load<Material>("NewSkyboxMaterial");

        // Call the function to change the skybox
       //ChangeSkybox(newSkyboxMaterial);
    
    // }
    

}
*/
