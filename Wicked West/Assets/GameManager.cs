// * Usings:
using System; // For Lazy.
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using QFSW.QC;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using BitSplash.AI.GPT; // RateGPT

// * Message on Code Comments in this File:
// ? INSTALL + APPLY the plugin 'Better Comments' by iguissouma to see color-differentiation between comments dependent on header sybmols (*, ?, !, etc.)
// ! This isn't mandatory, just a note about file comments, but it is quick for anyone to add (<1 minute).

// * GameManager class section:
public class GameManager : MonoBehaviour { // ? Do extend monoBehavior for Destroy and gameObject names to exist in the current context.
    
    // * Going with a singleton pattern for our game manager. This way, a gameobject isn't needed.
    // * Singleton definition at bottom of this document.
    private static GameManager _instance;

    // ? New Lazy Singleton stuff I'm trying:
    /*
    private static readonly Lazy<GameManager> lazyInstance = new Lazy<GameManager>(() => new GameManager()); // using the Lazy<T> class to ensure thread-safety during instance creation.
    public static GameManager Instance => lazyInstance.Value; // using the Lazy<T> class to ensure thread-safety during instance creation. Public above private methods in a Unity Project for reason of this isn't bothersome.
    */
    // * OLDish Lazy initialization for the Singleton pattern... same as above old one I think.?
    // private static readonly Lazy<GameManager> lazyInstance = new Lazy<GameManager>(() => new GameManager());
    // public static GameManager Instance => lazyInstance.Value;
    // ! New tries:
    public static GameManager Instance { 

        get {return _instance;}

    }
    
    private bool _isGameOver;
    public GameObject crosshairGO; // * Half done, implementing with new inventoryCanvObj refactor since old didn't work....
    // public GameObject player; // ! Player field removed [01/21/24]
    // public Button pauseButtonPassiveOnScreen;
    public CurrentGameState gameStateRn = CurrentGameState.Active;
    
    public bool _isDevMode = false;
    public bool isPaused = false;
    public bool isHiding = false;
    public bool inDialogue = false;
    public bool justLoggedOn = true; // ! SAVES ACTUAL.
    public float timer = 0f;

    // * Keeps track of if the player has entered the mine
    public bool hasEnteredMine = false;

    // * Keeps track of if the player is in a building
    // * Used to turn off fog
    public bool inBuilding = false;

    // * Keeps track of if the player had collected all the items
    // * Used to activate altar and altar lights
    public bool hasAllItems = false;

    // * Refactored from timer:
    public float secondsElapsedTotal;
    public float incrementInterval = 60f; // * 60 seconds days for now for demo'ing this functionality.
    public float timerR2 = 0f; // Timer to track elapsed time

    // Health and Stamina Managers
    public float Health = 3; // Player health will persist between days, no way to get back
    public float Stamina = 1000; // Player Stamina
    // ? Future add maybe: public string awokenScene; // ! This is to be assigned from ScenesSetupActivots currentScene string.

    // Night objective // Determines whether the night objective has been completed or not. Might trigger different behaviors in the monster
    public bool objComplete = false;
    public List<int> attacked = new List<int>();

    // Variable for completed player objective (all 7 items, altar activated. Updated in player interaction.)
    public bool bossActive = false;
    public int phase = 0;
    public string lastQuestionAskedByPlayer = "";

    //Inventory Variables
    public List<Item> Inventory;
    private int inventoryCurr;

    //Item Manager Variables
    public bool[] ItemsAcquired = new bool[7] {false, false, false, false, false, false, false};
    public GameObject beer;
    public GameObject camera;
    public GameObject dynamite;
    public GameObject holywater;
    public GameObject key;
    public GameObject metal;
    public GameObject vial;
    public string givingItemGM;

    [Header("RateGPT References")]
    [SerializeField] public GameObject rateGPTGO;
    [SerializeField] public RateGPT rateGPTScript;


    // ? Why is this script on a GameObject? So it can be easily located and clicked without browsing the directory.
    // ! GameObjects with DontDestroyOnLoad tend to have a big drawback: You have to start a game from 
    // ! Scene 0 (and not the current one you are editing) in order to be there or add them manually to 
    // ! everyscene which is kind of redudant, especially if you have sound, music, game manangers.

    // ? What if i justt removed.....
    /*
    private GameManager() { // make sure the constructor is private, so it can only be instantiated here
        // * initialize game manager here.
        // * Do not reference to GameObjects here (i.e. GameObject.Find etc.)
        // * because the game manager will be created before the objects.

        // Setup sets variables like days to 0, and more.
        Setup(); // ! was // this.Setup();

    } 
    */

    // * newly:


    // Update and start private unless otherwise (for my sanity/conventions sake pls)
    private void Start() {

        // * Moving to awake....:
        /*
        StartCoroutine(UpdateTime());
        StartCoroutine(UpdateTimeRefactored2());
        */
        Inventory = new List<Item>();
        for (int i = 0; i <= 7; i++)
        {
            attacked.Add(i);
        }
        beer = GameObject.Find("beerbottle");
        beer.SetActive(false);
        camera = GameObject.Find("Items/camera");
        camera.SetActive(false);
        dynamite = GameObject.Find("Items/dynamite");
        dynamite.SetActive(false);
        holywater = GameObject.Find("Items/holywater");
        holywater.SetActive(false);
        key = GameObject.Find("Items/key");
        key.SetActive(false);
        metal = GameObject.Find("Items/metal");
        metal.SetActive(false);
        vial = GameObject.Find("Items/vial");
        vial.SetActive(false);
    }

    // * Update time in seconds. timer used because float needed but saved not as float.
    private IEnumerator UpdateTime()
    {
        while (true)
        {
            // Check if not paused before incrementing timeElapsedInSec
            if (!isPaused)
            {
                timer += Time.deltaTime;

                // Check if one second has elapsed
                if (timer >= 1f)
                {
                    timeElapsedInSec++;
                    timer = 0f;
                }
            }

            yield return null; // Yielding null to update every frame
        }
    }

    private IEnumerator UpdateTimeRefactored2() {

        while (true) {

            if (!isPaused) {

                // Update the timer with the time since the last frame
                timerR2 += Time.deltaTime;
                // * Also update total seconds:
                secondsElapsedTotal += Time.deltaTime;

                // Check if the timer has reached the incrementInterval
                if (timerR2 >= incrementInterval) {

                    // Reset the timer
                    timerR2 = 0f;

                    // Increment the 'day' variable in the GameManager
                    // days += 1; // ! Removed and refactored to Fader

                    // Print the current number of days to the console
                    // Debug.Log("DAYS:  " + days);
                
                }

            }

            yield return null; // Yielding null to update every frame
            // ? or maybe..?

        }

    }

    private void Update() {
        /*
        if (ES3.KeyExists("GameSavedLoadingSequence")) {
            // ...
        }
        // * Always update saves now:
        if (ES3.Load<int>("WhatIsCurrentDay") != days) {
            ES3.Save("WhatIsCurrentDay", days);
        }
        if (ES3.Load<int>("WhatIsCurrentNight") != nights) {
            ES3.Save("WhatIsCurrentNight", nights);
        }
        if (ES3.Load<int>("WhatIsCurrentHealth") != Health) {
            ES3.Save("WhatIsCurrentHealth", Health);
        }
        */

    }

    // * Publically-accessable Variables section:
    // * [IMPLEMENTATION ASSISTANCE] To access these from another script, do something like:
    /*
    // Example script: PlayerController.cs
    using UnityEngine;

    public class PlayerController : MonoBehaviour {
        private void Start() {
            // Accessing the GameManager instance and modifying the 'days' variable
            GameManager.Instance.day += 1; // Increment the 'days' variable by 1 // ! GameManager.Instance.VARIABLEWANTED
        }
    }
    */

    
    [Header("Days/Nights Section")]
    public int days; // Publicly accessible variable to track days
    public int nights = 0;  // Publicly accessible variable to track nights
    public bool isNighttime = false; // * Bool for whether its night or day actively.
    public bool turningToNight = false; // * Bool for turning into night....
    public int timeElapsedInSec;
    public GameObject objectToActivate;

    // * This Setup() is used as what would be called on Start if this scene was the first thing in game. Saves/Loads make seperating this useful to be grabbable later.
    public void Setup() {

        // Debug.Log("This part works.");
        _isGameOver = false;
        // days = 1;

        // * LOAD SAVED VALUES IN SETUP HERE:
        if (ES3.KeyExists("gmFieldDays")) {

            Debug.Log("ES3 gmFieldDays:::: " + ES3.Load<int>("gmFieldDays"));
            days = ES3.Load<int>("gmFieldDays");

            Debug.Log("days locally: " + days);

        }

        if (ES3.KeyExists("gmFieldTimeElapsedInSec")) {

            Debug.Log("[?] well, gmFieldTimeElapsedInSec key was found......."); // * Debugging....

            Debug.LogWarning("[?] value of key is: " + ES3.Load<int>("gmFieldTimeElapsedInSec"));
            Debug.LogWarning("[?] gmFieldTimeElapsedInSec locally is: " + timeElapsedInSec);

            // * Update.
            timeElapsedInSec = ES3.Load<int>("gmFieldTimeElapsedInSec");

            Debug.LogWarning("[?] gmFieldTimeElapsedInSec now locally is: " + timeElapsedInSec);

        }

        if (ES3.KeyExists("gmFieldTimer")) {

            timer = ES3.Load<float>("gmFieldTimer");

        }

        if (ES3.KeyExists("gmFieldTimerR2")) {

            timerR2 = ES3.Load<float>("gmFieldTimerR2");

        }

        if (ES3.KeyExists("gmFieldSecondsElapsedTotal")) {

            secondsElapsedTotal = ES3.Load<float>("gmFieldSecondsElapsedTotal");

        }

        // *.... and more saves in setup will go here......

    }

    [Command]
    public void SetDayCount(int desiredDay) {

        days = desiredDay;

    }

    [Command]
    public int PrintDayCount() {

        return days;

    }

    [Command]
    public void Toggle_isNightTime(bool TorF) {

        isNighttime = TorF;

    }

    [Command]
    public void SetHealth(int desiredLives) {

        GameManager.Instance.Health = desiredLives;

    }

    [Command]
    public void GiveItems()
    {
        GameManager.Instance.Inventory.Add(beer.GetComponent<Item>());
        GameManager.Instance.ItemsAcquired[beer.GetComponent<Item>().managerValue] = true;
        GameManager.Instance.Inventory.Add(camera.GetComponent<Item>());
        GameManager.Instance.ItemsAcquired[camera.GetComponent<Item>().managerValue] = true;
        GameManager.Instance.Inventory.Add(dynamite.GetComponent<Item>());
        GameManager.Instance.ItemsAcquired[dynamite.GetComponent<Item>().managerValue] = true;
        GameManager.Instance.Inventory.Add(holywater.GetComponent<Item>());
        GameManager.Instance.ItemsAcquired[holywater.GetComponent<Item>().managerValue] = true;
        GameManager.Instance.Inventory.Add(key.GetComponent<Item>());
        GameManager.Instance.ItemsAcquired[key.GetComponent<Item>().managerValue] = true;
        GameManager.Instance.Inventory.Add(metal.GetComponent<Item>());
        GameManager.Instance.ItemsAcquired[metal.GetComponent<Item>().managerValue] = true;
        GameManager.Instance.Inventory.Add(vial.GetComponent<Item>());
        GameManager.Instance.ItemsAcquired[vial.GetComponent<Item>().managerValue] = true;
    }

    public void GiveItemSolo(GameObject itemGO) {

        GameManager.Instance.Inventory.Add(itemGO.GetComponent<Item>());
        GameManager.Instance.ItemsAcquired[itemGO.GetComponent<Item>().managerValue] = true;

    }



    [Command]
    public void setBoss()
    {
        GameManager.Instance.bossActive = true;
    }

    [Command]
    public void disableTrees()
    {
        GameObject.Find("foliage").SetActive(false);
    }

    // ! Removed b/c should keep only the Lazy<GameManager>-based Instance property now added above.
    /*
    public static GameManager Instance {
        get {
            if(_instance==null) {
                _instance = new GameManager();
            }
 
            return _instance;
        }
    }
    */

    // * Make the Awake method even more robust by checking if _instance is already set before assigning it. This way, you avoid accidentally overwriting it if another GameManager object is present in the scene
    private void Awake()
    {
        
        ReassignRefs(); // * Before Setup (which is just ES3 loads I believe).

        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);

        }
        else
        {
            Destroy(gameObject); // Destroy the duplicate GameManager
            return;
        }

        Setup();

        // * Moved from Start:
        StartCoroutine(UpdateTime());
        StartCoroutine(UpdateTimeRefactored2());

    }

    
    [Command]
    private void ReassignRefs() { // * Since instance, reassign references on awake..... can't have references get assigned in the inspector.

        GameObject canvasGO = GameObject.Find("Canvas");
        Debug.Log("[H] canvas: " + canvasGO + " found.....");

        GameObject menuPlusDisplaysGO = canvasGO.transform.GetChild(0).gameObject;
        Debug.Log("[H] menuPlusDisplays: " + menuPlusDisplaysGO + " found.....");

        crosshairGO = canvasGO.transform.GetChild(1).gameObject;
        // player = GameObject.Find("Player Prefab"); // ! Player field removed [01/21/24].

        // ? Trying removal of 4 below lines in RateGPT 1.2 implementation:
        /*
        rateGPTGO = GameObject.Find("RateGPTManager");
        Debug.Log("[H] rateGPTGO: " + rateGPTGO + " found.");

        rateGPTScript = rateGPTGO.GetComponent<RateGPT>();
        Debug.Log("[H] rateGPTScript: " + rateGPTScript + " found.");
        */

        /*
        gamePauseCanvasObject = menuPlusDisplaysGO.transform.GetChild(3).gameObject;
        gamePauseCanvasObject.SetActive(false);
        inventoryCanvasObject = menuPlusDisplaysGO.transform.GetChild(4).gameObject;
        inventoryCanvasObject.SetActive(false); // *? And same with inventory
        */
        // pauseButtonPassiveOnScreen = menuPlusDisplaysGO.transform.GetChild(2).GetComponent<Button>(); // * that works for Buttons.
        // pauseButtonPassiveOnScreen = GameObject.Find("BTN_Pause").GetComponent<Button>();


        // * Things that were assigned in inspector before:
        // gamePauseCanvasObject
        // inventoryCanvasObject
        // crosshairGO
        // player
        // pauseButtonPassiveOnScreen // ! Button, not GameObject.
        


    }


    /*
    [Command]
    private void ReassignRefs()
    {
        Transform canvasTransform = GameObject.Find("Canvas");

        if (canvasTransform != null)
        {
            GameObject canvasGO = canvasTransform.gameObject;
            Debug.Log("[H] canvas: " + canvasGO + " found.....");

            if (canvasTransform.childCount > 0)
            {
                GameObject menuPlusDisplaysGO = canvasTransform.GetChild(0).gameObject;
                Debug.Log("[H] menuPlusDisplays: " + menuPlusDisplaysGO + " found.....");
            }
            else
            {
                Debug.LogWarning("Canvas does not have any children.");
            }

            // Now you can proceed with accessing other GameObjects or components.
            // Ensure that they exist and are properly named.
        }
        else
        {
            Debug.LogWarning("Canvas not found in the hierarchy.");
        }
    }
    */


 
    // * Add game mananger members here:
    // * GameManager is set up to be seen, so if we create a public method in the class, others can access it.
    // * That is this section:
    public void Pause() {

        // * Pause stuff can either be made here, made elsewhere and refrenced here, or refactored wherever. I am cool with any option desired.
        // Pause/unpause code.....

        // Turn on Pause:
        if (!this.isPaused) {
            TogglePause();
            // pauseButtonPassiveOnScreen.interactable = false;
            this.gameStateRn = CurrentGameState.Halted;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            // crosshairGO.SetActive(false); //! MOVED TO ReRefManager.cs [01/21/24].

        }
        // Turn off pause
        else { 
            TogglePause();
            // pauseButtonPassiveOnScreen.interactable = true; this.gameStateRn = CurrentGameState.Active;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            // crosshairGO.SetActive(true); } //! MOVED TO ReRefManager.cs [01/21/24].

        }

    }

    // * function in your PauseManager script to toggle the pause state.
    // use the Time.timeScale property to control the time scale which affects the
    // physics and animations. Set it to 0 when paused and 1 when unpaused.
    public void TogglePause() {

        isPaused = !isPaused;

        // In Unity, Time.timeScale is a property that represents the global time scale of your game. 
        // It controls the speed at which time progresses in the game. A value of 1.0f means that time 
        // progresses at the normal rate, while a value of 0.0f effectively stops time.
        Time.timeScale = isPaused ? 0f : 1f; // 0 when paused and 1 when unpaused.

        // **Note: crucial part of implementing a pause feature because it ensures that the game's physics and animations behave correctly when the game is paused and unpaused.

    }

    public void GameOver(bool flag) {

        _isGameOver = flag;

    }
    public bool IsGameOver() { // public Bool in the GameManager to return the GameOver state to any class that needs it.

        return _isGameOver; // could do ' while (GameManager.Instance.IsGameOver()) ' from other classes.

    }

    // ! This is all so that in MonoBehaviour scripts, the following form can be used without inspector/transform.Find stuffs needed each time:
    /*
    GameManager.Instance.Pause(true); // * Always use the .Instance Property to get the current instance of the GameMananger)
        foreach (var slot in slots){
            if(slot.inventorySprite == null){ //selects first empty slot to fill
                        slot.inventorySprite = itemToAdd.inventorySprite;
                        slot.itemName = itemToAdd.itemName;
                        slot.itemFlavorText = itemToAdd.itemFlavorText;
                        break;
                    }
                }
    */

    // * Might not have to be refactored because I think its from like an e key being pressed scrip part.
    /*public void InventoryPressed(GameObject inventoryCanvasObject){
        //setup for inventory use
        inventoryCanvasObject.SetActive(true);
        //load inventory menu
        foreach(Transform slot in inventoryCanvasObject.transform.Find("ItemList").transform) {
            if(slot.gameObject.GetComponent<Item>().inventorySprite == null){
                slot.gameObject.GetComponent<Item>().inventorySprite = Inventory[inventoryCurr].inventorySprite;
                slot.gameObject.GetComponent<Item>().itemName = Inventory[inventoryCurr].itemName;
                slot.gameObject.GetComponent<Item>().itemFlavorText = Inventory[inventoryCurr].itemFlavorText;
                inventoryCurr++;
                slot.gameObject.GetComponent<Image>().color = Color.white;
                slot.gameObject.GetComponent<Image>().sprite = slot.gameObject.GetComponent<Item>().inventorySprite;
            }
        }
        //load item 1 as default selected item
        Item selectedItem = inventoryCanvasObject.transform.Find("ItemList").transform.Find("Item1").GetComponent<Item>();
        loadInventorySelectedItem(selectedItem, inventoryCanvasObject);
    }*/

    //builds inventory canvas from data
    public void InventoryPressedFromManager(GameObject inventoryCanvasObject){
        //setup for inventory use
        inventoryCanvasObject.SetActive(true);
        GameObject[] ItemList = new GameObject[7] {beer, camera, dynamite, holywater, key, metal, vial};
        int i  = 0;
        //load inventory menu
        foreach(Transform slot in inventoryCanvasObject.transform.Find("ItemList").transform) {
            if(this.ItemsAcquired[i] == true){
                slot.gameObject.GetComponent<Item>().inventorySprite = ItemList[i].GetComponent<Item>().inventorySprite;
                slot.gameObject.GetComponent<Item>().itemName = ItemList[i].GetComponent<Item>().itemName;
                slot.gameObject.GetComponent<Item>().itemFlavorText = ItemList[i].GetComponent<Item>().itemFlavorText;
                slot.gameObject.GetComponent<Image>().color = Color.white;
                slot.gameObject.GetComponent<Image>().sprite = ItemList[i].GetComponent<Item>().inventorySprite;
            }
            i++;
        }
        //load item 1 as default selected item
        Item selectedItem = inventoryCanvasObject.transform.Find("ItemList").transform.Find("Item1").GetComponent<Item>();
        loadInventorySelectedItem(selectedItem, inventoryCanvasObject);
    }

    public void loadInventorySelectedItem(Item selectedItem, GameObject inventoryCanvasObject){
        if (selectedItem.inventorySprite != null){
            inventoryCanvasObject.transform.Find("Selected Item").GetComponent<Image>().sprite = selectedItem.inventorySprite;
            inventoryCanvasObject.transform.Find("Selected Item").GetComponent<Image>().color = Color.white;
            inventoryCanvasObject.transform.Find("Selected Item Title").GetComponent<TextMeshProUGUI>().text = selectedItem.itemName;
            inventoryCanvasObject.transform.Find("Flavor Text").GetComponent<TextMeshProUGUI>().text = selectedItem.itemFlavorText;
        }
    }

        public void loadInventorySelectedItem(Item selectedItem){
        GameObject inventoryCanvasObject = GameObject.Find("Inventory");
        if (selectedItem.inventorySprite != null){
            inventoryCanvasObject.transform.Find("Selected Item").GetComponent<Image>().sprite = selectedItem.inventorySprite;
            inventoryCanvasObject.transform.Find("Selected Item").GetComponent<Image>().color = Color.white;
            inventoryCanvasObject.transform.Find("Selected Item Title").GetComponent<TextMeshProUGUI>().text = selectedItem.itemName;
            inventoryCanvasObject.transform.Find("Flavor Text").GetComponent<TextMeshProUGUI>().text = selectedItem.itemFlavorText;
        }
    }

    // Pull up journal
    public void journalPressed(GameObject journalCanvasObject)
    {
        //setup for inventory use
        journalCanvasObject.SetActive(true);
        //other journal stuff
    }

    // ! OLD- works, but can't be referenced from Inspector so GameManagerButtonHandler.cs
    // ! under GMIButtonHandler gameobject was created....
    public void QuitPressed() {

        Debug.Log("GMS: this.gameStateRn = CurrentGameState.Quit;");
        this.gameStateRn = CurrentGameState.Quit;
        Debug.LogWarning("[Testing....] ACTUAL gameStateRn:: " + gameStateRn + "!!!"); // * Testing.....

        // * Now, exit:
        // Inside the function, we use preprocessor directives (#if UNITY_EDITOR) to handle two different cases:
        // If you're running the application inside the Unity Editor, it will stop playing the current scene using 
        //   UnityEditor.EditorApplication.isPlaying.
        // If you're running a standalone build (e.g., a compiled executable), it will quit the application using Application.Quit().
        #if UNITY_EDITOR
                // If running in Unity Editor, stop playing the scene.
                UnityEditor.EditorApplication.isPlaying = false;
        #else
                // If running a standalone build, quit the application.
                Application.Quit();
        #endif

    }

    // ! OLD- works, but can't be referenced from Inspector so GameManagerButtonHandler.cs
    // ! under GMIButtonHandler gameobject was created....
    public void MainMenuPressed(){

        // * Fields:
        Color loadToColor = Color.black;
        string sceneToLoad = "MainMenuScene";
        float speedOfFade = 3.25f;

        // Upause first:
        // this.UnpausePressed();

        // ? Or possibly just:
        TogglePause(); // now to set  Time.TimeScale to 1 for fading to occur. Maybe after below line though.

        // * Faded into main menu scene:
        Initiate.Fade(sceneToLoad, loadToColor, speedOfFade); // Hopefully goes well when unpaused.

    }

    // * Method to destroy the instance reference
    public static void DestroyInstance()
    {
        _instance = null; // Hopefully brings inter-scene compatability resolutions.
    }

    // * RATE GPT Helpers Section:
    [Header("Rate GPT Helpers")]
    private float confidence;
    private bool returnedConfYet = false;

    public float Confidence {
        get { return confidence; }
        set { confidence = value; }
    }

    public bool ReturnedConfYet {
        get { return returnedConfYet; }
        set { returnedConfYet = value; }
    }

    public void GiveItem(string phrase) {
        
        if (rateGPTScript) {

            rateGPTScript.phraseInput = phrase;
            rateGPTScript.StartRateConversationGiveItem();
            
            // * Start coroutine too.... not anymore here. Refactored.
            // StartCoroutine(WaitForConfirmation());

        }

        else {

            Debug.Log("[!!] rateGPTScript not found!!");

        }

    }

    public void TakeItem() {
        // Implement logic to take item
    }

    public void RecieveConfidence(string inputString) {

        confidence = ConvertToFloat(inputString);
        returnedConfYet = true;

    }

    public float RecieveError(string confAsString) {

        Debug.Log("Error in RateGPT to float.");
        return ConvertToFloat(confAsString);

    }

    public float ConvertToFloat(string input) {

        float result;

        if (float.TryParse(input, out result)) {
            // Conversion successful
            return result;

        } else {

            // Conversion failed, handle error gracefully
            Debug.LogWarning("Conversion failed for input: " + input);
            return 0f;

        }

    }

    

}



// * singleton definition:
/*
A Singleton is a class/script that exists only once in your game and there are no 
other copies of it being made or referenced. Such classes can be the GameManager, 
the UIManager, or any other Manager class that you will only have once in the game. 
This allows us to set a static reference in the Singleton class that all other scripts 
will be able to see without having a dedicated reference for. Let’s take a look.
*/
// from [https://foxxthom.medium.com/game-manager-one-manager-to-rule-them-all-1c06afa72b23].



// * Enums created for GMS (GameManagerScript) section:
public enum CurrentGameState
{
    Active,
    Halted,
    Quit
}

public enum ItemGiving
        {
            None,
            Beer,
            Camera,
            Dynamite,
            HolyWater,
            Key,
            Metal,
            Vial
        }


// * Timer section:
// ! TIMER SECTION HAS BEEN REFACTORED TO DayCycleIncrementer Script in GameManager's Timer child GameObject.
/*
public class DayIncrementer : MonoBehaviour {

    private GameManager gameManager; // Reference to the GameManager
    private float timer = 0f; // Timer to track elapsed time

    // Interval for incrementing days (in seconds)
    // ! private float incrementInterval = 300f; // 300 seconds = 5 minutes
    private float incrementInterval = 20f; // * 20 seconds days for now for demo'ing this functionality.

    private void Start() {

        // Get a reference to the GameManager using the Singleton pattern
        gameManager = GameManager.Instance;

    }

    private void Update() {

        // Update the timer with the time since the last frame
        timer += Time.deltaTime;

        // Check if the timer has reached the incrementInterval
        if (timer >= incrementInterval) {

            // Reset the timer
            timer = 0f;

            // Increment the 'day' variable in the GameManager
            gameManager.days += 1;

            // Print the current number of days to the console
            Debug.Log("Days: " + gameManager.days);
        
        }

        Debug.Log("WORKING?");

    }

}
*/