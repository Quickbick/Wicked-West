using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; // Required for TextMeshProUGUI
using QFSW.QC;

// * Timer section:
public class DayCycleIncrementer : MonoBehaviour {

    private GameManager gameManager; // !old inspector-assigned Reference to the GameManager
    private float timer = 0f; // Timer to track elapsed time
    public float secondsElapsedTotal = 0;
    private float proportionTimePassed = 0; // Proportion of time passed relative to incrementInterval
    public int hours;
    public int minutes;
    public string ampm;

    // UI text to display the clock // ? Old i think, may revise after presentations
    [SerializeField] public TextMeshProUGUI clockText;

    public bool fadeToNightAKASleep;
    public bool fadeToNightFADER;
    public bool fadeToNightAKAHaltMovement;
    
    [SerializeField] public Material redSkybox; // Daytime skybox material
    [SerializeField] public Material nightSkybox; // Nighttime skybox material
    [HideInInspector] public Material activeSkybox; // Holds the active skybox material

    // Interval for incrementing days (in seconds)
    // ! private float incrementInterval = 300f; // 300 seconds = 5 minutes
    [Header("SET THIS TO SOMETHING VERY HIGH TO MAKE DAYS LONG- SHORT TO MAKE SHORT")]
    [SerializeField]private float incrementInterval = 20f; // * 60 seconds days for now for demo'ing this functionality.

    // UI text to display the clock

    public float ProportionTimePassed {
        get { return (secondsElapsedTotal / (incrementInterval)); }
        set { proportionTimePassed = value; } // ! Don't set actually
    }

    private void Start() {

        // Get a reference to the GameManager using the Singleton pattern
        // Debug.Log("Working....");
        gameManager = GameManager.Instance;

        fadeToNightAKASleep = false;
        fadeToNightFADER = false;
        fadeToNightAKAHaltMovement = false;

        // * Initialize the clock text
        UpdateClockText();

    }

    private void Update() {

        // !! Testing:
        // Debug.Log(secondsElapsedTotal);

        // ? Paused? If so, then...
        if (GameManager.Instance.isNighttime) { // ? Maybe this will work well to halt time at start of night timer.

            // * Can do anything here, but for now do nothing.
            bool isGMSgivingPause = true; // * for example....

        }

        // * Not paused sequence of time:
        else {

            if (1 == 1){ // !! REPLACE WITH CYCLES PARENT CLASS CHECK 
            // if (GameManager.Instance.isNighttime) // ? OR this could work maybe with the current logic..?

                // Update the timer with the time since the last frame
                timer += Time.deltaTime;
                // * Also update total seconds:
                secondsElapsedTotal += Time.deltaTime; // ! Don't uncomment this!

                // Check if the timer has reached the incrementInterval
                if (timer >= ((incrementInterval / 1.24))) { // * [c: fine->] Updated to be incrementInterval / 2 to account only for 12 hours now before falling asleep suddenly, hopefully....
                // * The above passed you out just after 1am is hit..... c:

                    // Reset the timer
                    timer = 0f;

                    Debug.Log("Fall asleep....");
                    // * I THINK HERE, SWITCHING TO NIGHT CALL TO CYCLES COULD OCCUR.....

                    // Increment the 'day' variable in the GameManager
                    // gameManager.days += 1;
                    // GameManager.Instance.days += 1; // ? WHAT IF I REMOVE THIS?
                    // secondsElapsedTotal = 0; // ??

                    // fadeToNightAKASleep = true; // !!! THIS IS THE TELE TRIGGER
                    StartFadeToNightCoroutine(1.1f); // ^ \/ Contains above and below now, incremented on times.
                    // fadeToNightFADER = true;

                    // Print the current number of days to the console
                    // Debug.Log("DAYS:  " + gameManager.days);
                
                }

                // * Update clock text.
                UpdateClockText();

            }

        }

        // Also, handle skyboxes here
        if (GameManager.Instance.isNighttime && (activeSkybox != nightSkybox))
        {
            ChangeSkybox(nightSkybox);
            Debug.Log("Skybox changed to nighttime");
        }

        if (!GameManager.Instance.isNighttime && (activeSkybox != redSkybox))
        {
            Debug.Log("DAYTRIGGER");
            ChangeSkybox(redSkybox);
            Debug.Log("Skybox changed to red (daytime)");
        }

    }

    public void ChangeSkybox(Material skyboxMaterial)
    {
        // Set the new skybox material in the RenderSettings
        RenderSettings.skybox = skyboxMaterial;
        activeSkybox = skyboxMaterial;
        
        // ! TRYING:
        GameManager.Instance.transform.Find("NightTimer").GetComponent<NightCycleIncrementer>().activeSkybox = skyboxMaterial;
    
    }

    public void ChangeSkyboxNEWtoNight() {

        RenderSettings.skybox = nightSkybox;
        activeSkybox = nightSkybox;

    }

    public void ChangeSkyboxNEWtoDay() {

        RenderSettings.skybox = redSkybox;
        activeSkybox = redSkybox;

    }

    // Call this method to start the coroutine with a specified number of seconds
    public void StartFadeToNightCoroutine(float coSec)
    {
        StartCoroutine(FadeToNightCoroutine(coSec));
        StartCoroutine(AKASleepCoroutine(coSec));
        Debug.Log("STARTEDCOROUTINES-DETECTED");
    }

    // * Update display of clock text frfom 6am to 12am
    private void UpdateClockText()
    {
        // Calculate the time of day in hours (from 0 to 18)
        float timeOfDayHours = (timer / (incrementInterval)) * 24;  // ?? Should be timer actually?

        // Convert time to 12-hour format (6am to 6 the next day)
        hours = Mathf.FloorToInt((timeOfDayHours + 6) % 12); // +18 to get to 6am I think.....?
        if (hours == 0)
        {
            hours = 12;
        }

        // Calculate minutes
        minutes = Mathf.FloorToInt(((timeOfDayHours + 6) % 1) * 60); // ? +6 optional here actually i'd believe.

        // Determine AM or PM
        ampm = (((timer / (incrementInterval)) * 24) < 6 || ((timer / (incrementInterval)) * 24) >= 18) ? "AM" : "PM";

        // Update the clock text
        clockText.text = string.Format("{0:D2}:{1:D2} {2}", hours, minutes, ampm);

    }

    // ! [2/21] Method to set the clock text reference
    public void SetClockText(TextMeshProUGUI clockText)
    {
        Debug.Log("For John: Setting clock reference here...?");
        this.clockText = clockText;
        UpdateClockText(); // Update the clock text immediately after setting the reference
    }

    // Coroutine that waits for a specified number of seconds and then sets fadeToNightFADER to true
    private IEnumerator FadeToNightCoroutine(float coSec)
    {
        yield return new WaitForSeconds(coSec); // Wait for coSec seconds
        fadeToNightFADER = true; // Set fadeToNightFADER to true after waiting
    }

    private IEnumerator AKASleepCoroutine(float coSecSplice)
    {
        fadeToNightAKAHaltMovement = true;
        yield return new WaitForSeconds(coSecSplice / 0.9f); // Wait for coSec seconds // ! RESET SOON
        fadeToNightAKASleep = true; // !!! THIS IS THE TELE TRIGGER
    }
     

    [Command]
    public void CheckProportionTimePassed() {

        Debug.Log("proportionTimePassed: " + this.ProportionTimePassed);

    }

    [Command]
    public void ToNight() {

        if (!GameManager.Instance.isNighttime) {

            GameManager.Instance.transform.Find("Timer").GetComponent<DayCycleIncrementer>().timer = 0f;
            GameManager.Instance.transform.Find("Timer").GetComponent<DayCycleIncrementer>().StartFadeToNightCoroutine(1.1f);

        }

        else {

            Debug.Log("Already night detected, failed to switch.");

        }

    }

    [Command]
    public void DayCycleSetIncrementInterval(float valueSet) {

        GameManager.Instance.transform.Find("Timer").GetComponent<DayCycleIncrementer>().incrementInterval = valueSet;

    }

}
