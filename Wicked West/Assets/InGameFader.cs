// * Message on Code Comments in this File:
// ? INSTALL + APPLY the plugin 'Better Comments' by iguissouma to see color-differentiation between comments dependent on header sybmols (*, ?, !, etc.)

// * Usings section:
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;
using QFSW.QC;
using TMPro; // Required for TextMeshProUGUI

// * Fader for ingame day-time traversing and changing to and from nighttime.
public class InGameFader : MonoBehaviour {

    // * Fields section ([Editors] first):
    [HideInInspector] public bool start = false;
    [HideInInspector] public string fadeScene;
    [HideInInspector] public bool isFadeIn = false;

    [SerializeField] public float fadeDamp = 7.5f;
    [SerializeField] public float alpha = 0.0f; // ? Or maybe always should start 0.0f....
    [SerializeField] public Color fadeColor;
    public Image myImage; // * <- New
    public bool liveClockInactive = true;
    public ActiveNPCLogic npcMngr;

    [Header("TMP/Cycle/NPC Management")] // * assign below in inspector.
    [SerializeField] public TextMeshProUGUI fadeClockTxt;
    [SerializeField] public TextMeshProUGUI titleTxt;
    [SerializeField] public TextMeshProUGUI subtitleTxt;
    public GameObject nightCycleIncGO;
    public GameObject dayCycleIncGO;
    [SerializeField] public GameObject npcMngrGO;
    
    CanvasGroup myCanvas; // ? Old... but still ref'd for now:
    
    Image bg;
    public string fadeAction;
    float lastTime = 0;
    bool startedLoading = false;

    private void Start() {

        if (GameManager.Instance) {

            dayCycleIncGO = GameManager.Instance.transform.Find("Timer").gameObject;
            nightCycleIncGO = GameManager.Instance.transform.Find("NightTimer").gameObject;
            // ! No longer: npcMngrGO = GameManager.Instance.transform.Find("NPCStateHandler").gameObject;

        }

        else {

            Debug.LogError("!! GAMEMANGER FOUND MISSING");

        }


        // * Dont start it, but set up referrences or notify of an issue off start I believe
        myImage = gameObject.GetComponent<Image>(); // First, assign myImage...
        if (npcMngrGO) {
            
            npcMngr = npcMngrGO.GetComponent<ActiveNPCLogic>();

        }

        else {

            Debug.LogError("Halt thy horses- NPCStateHandler GameObject not found.");

        }

        InitiateIGFade(); // ? Maybe in awake in future unless this goes smoothly...

    }

    private void Update() {

        // * When displaying liveclock during mid-section of in-game fades, do:
        if (!liveClockInactive) {

            // * Set proper display time:
            // if (GameManager.Instance.isNighttime) {
            if (fadeAction == "SetToNight") {

                NightCycleIncrementer nightCycleScript = nightCycleIncGO.GetComponent<NightCycleIncrementer>();
                fadeClockTxt.text = string.Format("{0:D2}:{1:D2}{2}", nightCycleScript.hours, nightCycleScript.minutes, nightCycleScript.ampm);

                // Display desired subtitle, which could be overridden in the future if gametips or others are desired here.
                subtitleTxt.text = GameManager.Instance.Health.ToString() + " lives remain.";

            }

            // * Display other cycle clock otherwise.
            if (fadeAction == "SetToDay") {

                DayCycleIncrementer dayCycleScript = dayCycleIncGO.GetComponent<DayCycleIncrementer>();
                fadeClockTxt.text = string.Format("{0:D2}:{1:D2}{2}", dayCycleScript.hours, dayCycleScript.minutes, dayCycleScript.ampm);

                // Display desired subtitle, which could be overridden in the future if gametips or others are desired here.
                int aliveNPCCount = (GetAliveNPCCount() - 1); // Get the count of NPCs that are not in the DEAD state. // ! (-1 because monster is included)
                subtitleTxt.text = aliveNPCCount + " townsfolk remain.";

            }

            if (fadeAction == "FellAsleep") {

                NightCycleIncrementer nightCycleScript = nightCycleIncGO.GetComponent<NightCycleIncrementer>();
                fadeClockTxt.text = string.Format("{0:D2}:{1:D2}{2}", nightCycleScript.hours, nightCycleScript.minutes, nightCycleScript.ampm);

                // Display desired subtitle, which could be overridden in the future if gametips or others are desired here.
                subtitleTxt.text = "You fell asleep! " + GameManager.Instance.Health.ToString() + " lives remain.";

            }

            if (fadeAction == "SetToMorning") {

                DayCycleIncrementer dayCycleScript = dayCycleIncGO.GetComponent<DayCycleIncrementer>();
                fadeClockTxt.text = string.Format("{0:D2}:{1:D2}{2}", dayCycleScript.hours, dayCycleScript.minutes, dayCycleScript.ampm);
                // ***Subtitle text should already be set through the Happeningsmessage.

            }

            // ! Null handling:
            if (fadeAction == null) {

                // Debug.Log("FadeAction not yet found for fades...");
                // * Sets as this, day clock, but shouldn't show....
                DayCycleIncrementer dayCycleScript = dayCycleIncGO.GetComponent<DayCycleIncrementer>();
                fadeClockTxt.text = string.Format("{0:D2}:{1:D2}{2}", dayCycleScript.hours, dayCycleScript.minutes, dayCycleScript.ampm);

            }
            
        }

        // Update clocks infos
        if (GameManager.Instance.transform.Find("Timer").GetComponent<DayCycleIncrementer>().fadeToNightFADER) {

            Debug.Log("Trying to sleep at night now......");
            FallAsleep_ToNight();
            // fadeToNightAKASleep
            GameManager.Instance.transform.Find("Timer").GetComponent<DayCycleIncrementer>().fadeToNightFADER = false; // ! now on player

        }

        // Update clocks infos
        if (GameManager.Instance.transform.Find("NightTimer").GetComponent<NightCycleIncrementer>().fadeToDayFADER) {

            Debug.Log("Survived the night, it seems......");
            // FallAsleep_ToNight();
            FallAsleep_ToMorning("Daybreak comes as you survive this night, making it back to your lodging.");
            // fadeToNightAKASleep
            GameManager.Instance.transform.Find("NightTimer").GetComponent<NightCycleIncrementer>().fadeToDayFADER = false; // ! now on player

        }

    }

    // * Helper for subtitling
    private int GetAliveNPCCount()
    {
        int count = 0;

        // *Loop through each NPC state pair in the ActiveNPCLogic script to check if the state is not DEAD.
        foreach (var pair in npcMngr.gameObjectStatePairs)
        {

            // Check if the state is not DEAD.
            if (pair.state != ActiveNPCLogic.GameObjectState.DEAD)
            {
                count++;
            }
            
        }

        return count;
    }

    // ? Set callback needed or not..?
    // ! OLD from scene fader (Fader.cs):
    /*
    void OnEnable()
    {
        SceneManager.sceneLoaded += OnLevelFinishedLoading;
    }
    //Remove callback
    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnLevelFinishedLoading;
    }
    */

    // * Dev Commands Section:
    // ? Sets start variable.
    [Command]
    public void StartIGFade(bool startBool) {

        start = startBool;

    }

    // ? Initiates the in-game fade. Start must be set to true first.
    [Command]
    public void InitiateIGFade() {

        // * Get the visual elements
        if (transform.GetComponent<CanvasGroup>()) {

            myCanvas = transform.GetComponent<CanvasGroup>();

        }

        if (transform.GetComponentInChildren<Image>()) {

            bg = transform.GetComponent<Image>();
            bg.color = fadeColor;

        }

        liveClockInactive = true;

        // * Check and start coroutine:
        if (myCanvas && bg) {

            // ! [1/27 Debugging]:
            // myCanvas.alpha = 0.5f;
            myCanvas.alpha = 1.0f; // ! New [2/2]
            Debug.Log("Canv alpha = 1.");
            // myCanvas.alpha = 0.5f;


            // StartCoroutine(FadeItInGame());

        } else { Debug.LogWarning("[:c] In-Game Fader issue found..."); }

        // ! New image technique:
        // * Hope you have an Image component assigned to myImage
        if (myImage != null) {

            // Set the initial transparency
            SetTransparency(myImage, 0.0f); // Set alpha to any value between 0 (fully transparent) and 1 (fully opaque)
        
        } else { Debug.LogError("Image component not assigned!"); }

        // * Also initialize the TextMeshPro texts:
        if (fadeClockTxt) {
            SetTransparencyTMP(fadeClockTxt, 0.0f);
        } else { Debug.LogError("Image component not assigned!"); }
        
        if (titleTxt) {
            SetTransparencyTMP(titleTxt, 0.0f);
        } else { Debug.LogError("titleTxt component not assigned!"); }

        if (subtitleTxt) {
            SetTransparencyTMP(subtitleTxt, 0.0f);
        } else { Debug.LogError("subtitleTxt component not assigned!"); }


        // * Now that all assignments are done, start the coroutine:
        StartCoroutine(FadeItInGame());

    }

    // ? Start the fade-in test with a duration of x seconds
    [Command]
    public void TestIGFadeIn(float xFloat) {
    
        StartCoroutine(TestFadeIn(xFloat));

    }

    // ? Start the fade-out test with a duration of x seconds
    [Command]
    public void TestIGFadeOut(float xFloat) {
    
        StartCoroutine(TestFadeOut(xFloat));

    }

    // ! OLD (Fader.cs):
    /*
    public void InitiateFader()
    {

        DontDestroyOnLoad(gameObject);

        //Getting the visual elements
        if (transform.GetComponent<CanvasGroup>())
            myCanvas = transform.GetComponent<CanvasGroup>();

        if (transform.GetComponentInChildren<Image>())
        {
            bg = transform.GetComponent<Image>();
            bg.color = fadeColor;
        }
        //Checking and starting the coroutine
        if (myCanvas && bg)
        {
            myCanvas.alpha = 0.0f;
            StartCoroutine(FadeIt());
        }
        else
            Debug.LogWarning("Something is missing please reimport the package.");
    }
    */

    // * Fading function 1)
    IEnumerator FadeItInGame() {
        
        // * wait to start...
        while (!start) {

            yield return null;

        }

        liveClockInactive = false;
        Debug.Log("in FadeItInGame: Now its started...");

        // * Set fields:
        lastTime = Time.time;

        // float coDelta = lastTime;
        float coDelta = lastTime; // ? Patch

        bool hasFadedIn = false;

        while (!hasFadedIn) {

            coDelta = Time.time - lastTime;

            // ! Actually fade in:
            if (!isFadeIn) {
                
                // Debug.Log("IGF: Not faded in yet..");
                
                alpha = newAlpha(coDelta, 1, alpha);
                
                if (alpha == 1 && !startedLoading) {

                    Debug.Log("DEBUG: setting startedLoading to true...");

                    startedLoading = true;
                    // ! Not here: SceneManager.LoadScene(fadeScene);

                    // ? Also needed maybe:
                    // isFadeIn = true; //!^ (see below ! comment)
                    // ^^^^ removed [2/2]
                    StartCoroutine(WaitAndDo(3.65f));
                
                }

            }

            // ! Now, when startedLoading is true above, can send to a coroutine to display time before setting
            // ! ...^isFadeIn to true to trigger the else below here.

            // * hasFadedIn already:
            else {

                // Debug.Log("IGF: Its faded in already!");

                // Fade out:
                alpha = newAlpha(coDelta, 0, alpha);

                if (alpha == 0) {

                    hasFadedIn = true;

                    // ? Maybe needed for repeated fades:
                    isFadeIn = false;

                    StartIGFade(false); // *? Set start to false again here, I think.

                }

            }

            // Debugging...
            // Debug.Log("IGF: " + alpha);

            lastTime = Time.time;

            // ! OLD: myCanvas.alpha = alpha;
            SetTransparency(myImage, alpha); // * :new
            
            // * [2/2] Even newer:
            SetTransparencyTMP(fadeClockTxt, alpha);
            SetTransparencyTMP(titleTxt, alpha);
            SetTransparencyTMP(subtitleTxt, alpha);

            yield return null;

        }

        // ? Unnecessary here I believe...
        // Initiate.DoneFading();

        // ? Maybe not quite necessary...?:
        // Destroy(gameObject);

        // ! Adding in loop attempt:
        startedLoading = false;
        StartCoroutine(FadeItInGame());

        // Finally, return.
        yield return null;

    }

    IEnumerator WaitAndDo(float waitTime) {

        // ! First, go through possible fade cases:
        switch (fadeAction) {

            case "SetToDay":
            GameManager.Instance.isNighttime = false;
            break;

            case "SetToNight" :
            GameManager.Instance.isNighttime = true;
            break;

            case "FellAsleep": // * add sleep case"
            GameManager.Instance.isNighttime = true;
            Debug.Log("Fell asleep hit, isNighttime set to true");
            break;

            case "SetToMorning":
            GameManager.Instance.isNighttime = false;
            break;

        // I'll add more cases here as I continue....

            default:
            // Handle the default case 
            Debug.LogWarning("Unknown fade action: " + fadeAction);
            break;

        }

        // * Then, wait for the specified time.
        yield return new WaitForSeconds(waitTime);

        // * [2/2 relocation]:
        isFadeIn = true;

        // * For live clock:
        liveClockInactive = true;
                
    }

    // ! OLD (Fader.cs):
    /*
    IEnumerator FadeIt()
    {

        while (!start)
        {
            //waiting to start
            yield return null;
        }
        lastTime = Time.time;
        float coDelta = lastTime;
        bool hasFadedIn = false;

        while (!hasFadedIn)
        {
            coDelta = Time.time - lastTime;
            if (!isFadeIn)
            {
                //Fade in
                alpha = newAlpha(coDelta, 1, alpha);
                if (alpha == 1 && !startedLoading)
                {
                    startedLoading = true;
                    SceneManager.LoadScene(fadeScene);
                }

            }
            else
            {
                //Fade out
                alpha = newAlpha(coDelta, 0, alpha);
                if (alpha == 0)
                {
                    hasFadedIn = true;
                }


            }
            lastTime = Time.time;
            myCanvas.alpha = alpha;
            yield return null;
        }

        Initiate.DoneFading();

        // Debug.Log("Your scene has been loaded , and fading in has just ended");

        Destroy(gameObject);

        yield return null;
    }
    */

    // * Function to set transparency of an Image component
    void SetTransparency(Image image, float alpha) {

        // Get the current color of the Image component
        Color imageColor = image.color;

        // Set the alpha component to the desired transparency
        imageColor.a = alpha;

        // Apply the modified color back to the Image component
        image.color = imageColor;

    }

    // * Sets the transparency of text mesh pro objects:
    void SetTransparencyTMP(TextMeshProUGUI tmp, float alpha) {

        // Get the current color of the Image component
        Color imageColor = tmp.color;

        // Set the alpha component to the desired transparency
        imageColor.a = alpha;

        // Apply the modified color back to the Image component
        tmp.color = imageColor;

    }


    // * From Fader.cs, should be fine and work here:
    float newAlpha(float delta, int to, float currAlpha)
    {

        switch (to)
        {

            case 0:
                currAlpha -= fadeDamp * delta;
                if (currAlpha <= 0)
                    currAlpha = 0;

                break;
            case 1:
                currAlpha += fadeDamp * delta;
                if (currAlpha >= 1)
                    currAlpha = 1;

                break;
        }

        return currAlpha;

    }

    // * Testing fading:
    // Test function to gradually fade in the canvas
    public IEnumerator TestFadeIn(float duration)
    {
        float elapsedTime = 0f;
        
        // Ensure that the canvas is initially invisible
        myCanvas.alpha = 0f;

        while (elapsedTime < duration)
        {

            Debug.Log(myCanvas.alpha);

            // Incrementally increase alpha over time
            myCanvas.alpha = Mathf.Lerp(0f, 1f, elapsedTime / duration);

            // Update the elapsed time
            elapsedTime += Time.deltaTime;

            yield return null;
        }

        // Ensure that the canvas is fully visible
        myCanvas.alpha = 1f;
    }

    // Test function to gradually fade out the canvas
    public IEnumerator TestFadeOut(float duration)
    {
        float elapsedTime = 0f;

        // Ensure that the canvas is initially fully visible
        myCanvas.alpha = 1f;

        while (elapsedTime < duration)
        {
            // Incrementally decrease alpha over time
            myCanvas.alpha = Mathf.Lerp(1f, 0f, elapsedTime / duration);

            // Update the elapsed time
            elapsedTime += Time.deltaTime;

            yield return null;
        }

        // Ensure that the canvas is fully invisible
        myCanvas.alpha = 0f;
    }

    // ! OLD (Fader.cs):
    // ? Needed..? Could cause issues with removal...
    /*
    void OnLevelFinishedLoading(Scene scene, LoadSceneMode mode)
    {
        StartCoroutine(FadeIt());
        //We can now fade in
        isFadeIn = true;
    }
    */


    // ! ⁺˚⋆｡[2/2]°✩₊ Refactor elsewhere when scene changes can be made without causing potential conflict.
    // * To-night system for in-game button
    [Command]
    public void Do_toNightTransition() { // Call this with button, also.

        if (!GameManager.Instance.isNighttime) {

            // ? Attempt to initialize night clock:
            // GameManager.Instance.isNighttime = true;
            // Debug.Log("GMI isNighttime set to true.");

            // Increment night to 1 if labeled 0 at start (before first populated)
            if (GameManager.Instance.nights == 0) {

                GameManager.Instance.nights = 1;
                Debug.Log("GMI nights set to 1 from 0.");

            }

            // * Set proper display time:
            NightCycleIncrementer nightCycleScript = nightCycleIncGO.GetComponent<NightCycleIncrementer>();
            fadeClockTxt.text = string.Format("{0:D2}:{1:D2}{2}", nightCycleScript.hours, nightCycleScript.minutes, nightCycleScript.ampm);
            titleTxt.text = "NIGHT " + GameManager.Instance.nights;
            subtitleTxt.text = "Sub-text or nothing could go here.... maybe like a tip for the game or something.";
            // !^^^ above was: like HEALTH: " + GameManager.Instance.Health.ToString(); but Health is static... I will ask if that needs be.
            // ! ^^^^ Above can be changed when HEALTH is no longer static.

            fadeAction = "SetToNight";
            StartIGFade(true); // Set start to true.

        }

        else {

            // Handle potential logic for it being night when trying:
            Debug.Log("[e] trying to Do_toNightTransition(), but its already night!");

        }
        
    }

    // * To-day system possibly here, unless only triggered when the night is up.
    // Helper here in case:
    [Command]
    public void Do_toDayTransition() {

        if (GameManager.Instance.isNighttime) {

            // GameManager.Instance.isNighttime = false; // * Needed, I believe.

            DayCycleIncrementer dayCycleScript = dayCycleIncGO.GetComponent<DayCycleIncrementer>();
            fadeClockTxt.text = string.Format("{0:D2}:{1:D2}{2}", dayCycleScript.hours, dayCycleScript.minutes, dayCycleScript.ampm);
            titleTxt.text = "DAY " + GameManager.Instance.days;
            subtitleTxt.text = "Sub-text or nothing could go here.... maybe like a tip for the game or something.";

            fadeAction = "SetToDay";
            StartIGFade(true); // Set start to true.

        }

        else {

            // Handle potential logic for it being night when trying:
            // ? Wait... do anyways, and just adjust time in future maybe....
            Debug.Log("[e] trying to Do_toDayTransition(), but its already day!");

        }

    }

    // * To-night system for FALLING ASLEEP
    [Command]
    public void FallAsleep_ToNight() { // Call this with button, also.

        if (!GameManager.Instance.isNighttime) {

            // ? Attempt to initialize night clock:
            // GameManager.Instance.isNighttime = true;
            // Debug.Log("GMI isNighttime set to true.");

            // Increment night to 1 if labeled 0 at start (before first populated)
            if (GameManager.Instance.nights == 0) {

                GameManager.Instance.nights = 1;
                Debug.Log("GMI nights set to 1 from 0.");

            }

            else {

                GameManager.Instance.nights += 1;

            }

            // * Set proper display time:
            NightCycleIncrementer nightCycleScript = nightCycleIncGO.GetComponent<NightCycleIncrementer>();
            fadeClockTxt.text = string.Format("{0:D2}:{1:D2}{2}", nightCycleScript.hours, nightCycleScript.minutes, nightCycleScript.ampm);
            titleTxt.text = "NIGHT " + GameManager.Instance.nights;
            subtitleTxt.text = "You fell asleep.";

            

            fadeAction = "FellAsleep";
            Debug.Log("StartIGFade(true) passed");
            StartIGFade(true); // Set start to true.

        }

        else {

            // Handle potential logic for it being night when trying:
            Debug.Log("[e] trying to FallAsleep_ToNight(), but its already night!");

        }
        
    }

    // * To-day system for SURVIVING THE NIGHT.
    [Command]
    public void FallAsleep_ToMorning(string happeningMsg) { // Call this with button, also.

        if (GameManager.Instance.isNighttime) {

            // ? Attempt to initialize night clock:
            // GameManager.Instance.isNighttime = true;
            // Debug.Log("GMI isNighttime set to true.");

            // Increment days
            GameManager.Instance.days += 1;


            // * Set proper display time:
            DayCycleIncrementer dayCycleScript = dayCycleIncGO.GetComponent<DayCycleIncrementer>();
            fadeClockTxt.text = string.Format("{0:D2}:{1:D2}{2}", dayCycleScript.hours, dayCycleScript.minutes, dayCycleScript.ampm);
            titleTxt.text = "DAY " + GameManager.Instance.days;
            subtitleTxt.text = happeningMsg;


            fadeAction = "SetToMorning";
            StartIGFade(true); // Set start to true.

        }

        else {

            // Handle potential logic for it being night when trying:
            Debug.Log("[e] trying to FallAsleep_ToMorning(), but its already day!");

        }
        
    }


}
