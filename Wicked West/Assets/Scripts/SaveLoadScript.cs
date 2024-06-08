// * Usings:
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PixelCrushers;
using QFSW.QC;

// * Message on Code Comments in this File:
// ? INSTALL + APPLY the plugin 'Better Comments' by iguissouma to see color-differentiation between comments dependent on header sybmols (*, ?, !, etc.)
// ! This isn't mandatory, just a note about file comments, but it is quick for anyone to add (<1 minute).

// * This is an implementation of using the asset Easy Save 3 from a script. It can also be used from PlayMaker.
// Assembly deefinitions at bottom of page: [https://docs.moodkie.com/easy-save-3/getting-started/]

public class SaveLoadScript : MonoBehaviour {

    // * Inspector grabs:
    [SerializeField] public GameObject playerGO; // Currently grabs the "Player" child (child 0) of "Player Controller" from inspector.
    public PlayerMovement playerMovementScript;
    [SerializeField] public GameObject playerInteractionGO;
    // [SerializeField] public GameObject managerGO; // * Instances WORKING AGAIN c:
    [SerializeField] public GameObject playerPrefabGO; // ! The prefab one with children Player and Camera.
    private SkyTeleporter skyTeleporterScript;


    // ToDo: Saved Fields section
    // ToDo: here...


    public float loadedMoveSpeed;// first one

    private void Start() {

        playerMovementScript = playerGO.GetComponent<PlayerMovement>();
        skyTeleporterScript = playerPrefabGO.GetComponent<SkyTeleporter>();

    }

    private void Update() {}


    // * SAVES AND LOADS WITH EASY SAVE 3:
    // Easy Save stores data as keys and values, much like a Dictionary.
    /*
    To save a value, use ES3.Save
    To load a value, use ES3.Load
    */

    //?~ For example, to save an integer to a key named myInt and load it back again, you could do:
    /*
    ES3.Save("myInt", 123);
    myInt = ES3.Load<int>("myInt", defaultValue);
    */
    //?~ If there’s no data to load, it will return the defaultValue.
    //?~ If you don’t specify a defaultValue, you must ensure that there is data to load (for example using ES3.KeyExists):
    /*
    if(ES3.KeyExists("myInt"))
    myInt = ES3.Load<int>("myInt");
    */

    // * SAVING SECTION:
    public void SaveAll() {
        
        // ! Trying anything at this point honestly.
        // managerGO = GameManager.Instance;

        // Save gamestate:// ! This will be changed when game states are discussed and possible enums are made.
        string gameState = GameManager.Instance.IsGameOver() ? "GAME OVER" : "ACTIVE";
        Debug.Log("[2345] days from instance: " + GameManager.Instance.days);
        Debug.Log("[%@#%] gameState as string: " + gameState);

        ES3.Save("gamestate", gameState);
        ES3.Save("GameSavedLoadingSequence", true);

        // Save gamestate, but if we use enumes for gamestates:
        GameState gameStateAsEnum = GameManager.Instance.IsGameOver() ? GameState.GAMEOVER : GameState.PLAYING;
        ES3.Save("gamestateAsEnum", gameStateAsEnum);

        // * Saving player location:
        // ? Not needed...?
        /*
        if (playerGO != null) { // Check that it is indeed assigned in the inspector....

            Vector3 playerPosition = playerGO.transform.position;
            // ? Or just: ... 
            // Vector3 playerPosition = playerGO.position; 
            // ? ...Because PlayerMovement script has 'public Transform position' as of 10/17;
            ES3.Save("playerLocation", playerPosition);

        }
        */

        // *now, save fields also:
        SavePlayerFields();
        
        // ! new with new menu:
        SaveDays(); // * Also does time.
        // SaveName("Player #0001");

        LastSaves();

    }

    // * SAVING PLAYER FIELDS SUBSECTION:
    [Command]
    public void SavePlayerFields() {

        // TODO: Saving Player fields (update whenever PlayerMovement has additional fields):
        if (playerMovementScript != null) { // Check that it is indeed assigned in the inspector...

            Transform playerPosition = playerGO.transform;
            Transform playerRotation = playerMovementScript.orientation;

            // "pField" stands for Player Field, as in the Fields of the Player as set in PlayerMovement.cs
            ES3.Save("pFieldPlayerLocation", playerPosition); // ? I think might not work well.....
            ES3.Save("pFieldPlayerRotation", playerRotation);
            SavePlayerTransform();


            
            

        }

        // ! [c:] Working position stuff...... [new 1/4/2024]
        if (skyTeleporterScript) { // * Check script exists.

            // ! NEW SKY TELEPORTER STUFF:\
            /* // ! Old:
            ES3.Save("nppFieldTargetX", playerPrefabGO.transform.position.x); // was skyTeleporterScript
            ES3.Save("nppFieldTargetZ", playerPrefabGO.transform.position.y);
            ES3.Save("nppFieldTargetSKY", playerPrefabGO.transform.position.z); // * Sky is Y/height.
            */
            // * New position (working):
            ES3.Save("nppFieldTargetX", playerGO.transform.position.x); // was skyTeleporterScript
            ES3.Save("nppFieldTargetSKY", playerGO.transform.position.y);
            ES3.Save("nppFieldTargetZ", playerGO.transform.position.z); // * Sky is Y/height.
            // * New rotation :: Save the Euler angles (rotation) in degrees
            ES3.Save("nppFieldRotationX", playerGO.transform.eulerAngles.x);
            ES3.Save("nppFieldRotationY", playerGO.transform.eulerAngles.y);
            ES3.Save("nppFieldRotationZ", playerGO.transform.eulerAngles.z);

        }

        if (playerInteractionGO != null) {

            // List<DialogueSystem> dialogueSysListToSave = playerInteractionGO.GetComponent<PlayerInteraction>().dialogueSysList;
            List<PixelCrushers.DialogueSystem.Conversation> dialogueSysListToSave = playerInteractionGO.GetComponent<PlayerInteraction>().dialogueSysList;
            ES3.Save("piFieldDialogueSysList" , dialogueSysListToSave);

            //updated with inventory refactoring
            List<Item> inventoryToSave = GameManager.Instance.Inventory;
            ES3.Save("pFieldInventory", inventoryToSave); // ! Inventory save here...

        }

    }

        // When saving the player's transform fields
    [Command]
    public void SavePlayerTransform() {

        // ? [01/21/24]: Check if this has already been refactored, and that's why its no longer ref saves.... was that all completed?
        ES3.Save("pFieldPlayerPosition", transform.position);
        ES3.Save("pFieldPlayerRotation", transform.rotation);
        ES3.Save("pFieldPlayerScale", transform.localScale);

        Debug.Log("Saved player's transform fields.");
    }

    [Command]
    public void SaveDays() {

        if (GameManager.Instance) {

            
            // ES3.Save("gmFieldDays", managerGO.GetComponent<GameManager>().days);
            ES3.Save("gmFieldDays", GameManager.Instance.days);
            
            // * Testing purposes:
            // Debug.LogWarning(managerGO.GetComponent<GameManager>().days);

            // ES3.Save("gmFieldTimeElapsedInSec", managerGO.GetComponent<GameManager>().timeElapsedInSec);
            ES3.Save("gmFieldTimeElapsedInSec", GameManager.Instance.timeElapsedInSec);

            // * Float timers:
            /*
            ES3.Save("gmFieldTimer", managerGO.GetComponent<GameManager>().timer);
            ES3.Save("gmFieldTimerR2", managerGO.GetComponent<GameManager>().timerR2);
            ES3.Save("gmFieldSecondsElapsedTotal", managerGO.GetComponent<GameManager>().secondsElapsedTotal);
            */
            ES3.Save("gmFieldTimer", GameManager.Instance.timer);
            ES3.Save("gmFieldTimerR2", GameManager.Instance.timerR2);
            ES3.Save("gmFieldSecondsElapsedTotal", GameManager.Instance.secondsElapsedTotal);

        }

        else {

            // ! B/c not instance rn....
            Debug.Log("GMI not found"); // GMI stands for GameManager.Instance

        }

    }

    public void LastSaves() {

        // * GM GENERAL:
        ES3.Save("WhatIsCurrentDay", GameManager.Instance.days);
        ES3.Save("WhatIsCurrentNight", GameManager.Instance.nights);
        ES3.Save("WhatIsCurrentHealth", GameManager.Instance.Health);

        // * NPCS:
        if (ES3.KeyExists("WasHunterDeadF")) {
            ES3.Save("WasHunterDead", true);
        }
        if (ES3.KeyExists("WasMinerDeadF")) {
            ES3.Save("WasMinerDead", true);
        }
        if (ES3.KeyExists("WasSalesmanDeadF")) {
            ES3.Save("WasSalesmanDead", true);
        }
        if (ES3.KeyExists("WasSmithDeadF")) {
            ES3.Save("WasSmithDead", true);
        }
        if (ES3.KeyExists("WasBartenderDeadF")) {
            ES3.Save("WasBartenderDead", true);
        }
        if (ES3.KeyExists("WasPastorDeadF")) {
            ES3.Save("WasPastorDead", true);
        }
        if (ES3.KeyExists("WasSheriffDeadF")) {
            ES3.Save("WasSheriffDead", true);
        }
        if (ES3.KeyExists("WasResearcherDeadF")) {
            ES3.Save("WasResearcherDead", true);
        }

        // * INVENTORY:
        if (GameManager.Instance.ItemsAcquired[0]) {
            ES3.Save("WasFound0", true);
        }
        if (GameManager.Instance.ItemsAcquired[1]) {
            ES3.Save("WasFound1", true);
        }
        if (GameManager.Instance.ItemsAcquired[2]) {
            ES3.Save("WasFound2", true);
        }
        if (GameManager.Instance.ItemsAcquired[3]) {
            ES3.Save("WasFound3", true);
        }
        if (GameManager.Instance.ItemsAcquired[4]) {
            ES3.Save("WasFound4", true);
        }
        if (GameManager.Instance.ItemsAcquired[5]) {
            ES3.Save("WasFound5", true);
        }
        if (GameManager.Instance.ItemsAcquired[6]) {
            ES3.Save("WasFound6", true);
        }

    }

    [Command]
    public void SaveName(string idDesired) {

        if (GameManager.Instance) {

            ES3.Save("gmFieldID", idDesired);

        }

        else {

            // ! B/c not instance rn....
            Debug.Log("GMI not found");

        }

    }

    // Check loads helper:
    [Command]
    public void CheckLoaded(string keyToLoad) {

        // If found:
        if (ES3.KeyExists(keyToLoad)) {

            Debug.Log(ES3.Load(keyToLoad).ToString());

        }

        // And if they key does not exist:
        else {

            Debug.Log("No key under term " + keyToLoad + "has been saved yet");

        }

    }

    [Command]
    public void DelKey(string keyToDel) {

        // If found:
        if (ES3.KeyExists(keyToDel)) {

            Debug.Log(ES3.Load(keyToDel).ToString() + " is now being deleted");
            ES3.DeleteKey(keyToDel);
            

        }

        // And if they key does not exist:
        else {

            Debug.Log("No key under term " + keyToDel + "has been saved yet");

        }

    }

    // * LOADING SECTION:
    [Command]
    public void LoadAll() {

        // * In here, load the existing states and such you want to load. DEFAULT VALUES are if no key/value pairs are found.
        GameState loadedGameStateAsEnum = ES3.Load<GameState>("gamestateAsEnum", GameState.DEFAULTED);

        // ? If self-created enums cannot be loaded, try <string> version.
        string loadedGameState = ES3.Load<string>("gamestate", "defaultValue");

        // * Loading player location:
        if (ES3.KeyExists("playerLocation")) {

            string loadedPlayerLocation = ES3.Load("playerLocation").ToString(); // No default needed, because "playerLocation" Key was checked to exist first.

        }

    }

    // * LOADING PLAYER FIELDS SUBSECTION:
    public void LoadPlayerFields() {

        if (ES3.KeyExists("pFieldMoveSpeed")) {

            float loadedMoveSpeed = ES3.Load<float>("pFieldMoveSpeed", 0.0f);

        }

        if (ES3.KeyExists("pFieldPlayerLocation")) { // Needed b/c no default case here (Transform).

            Transform loadedMoveSpeed = ES3.Load<Transform>("pFieldPlayerLocation");

        }

        if (ES3.KeyExists("pFieldHorizontalInput")) {

            float loadedMoveSpeed = ES3.Load<float>("pFieldHorizontalInput", 0.0f);

        }

        if (ES3.KeyExists("pFieldVerticalInput")) {

            float loadedMoveSpeed = ES3.Load<float>("pFieldVerticalInput", 0.0f);

        }
        
        if (ES3.KeyExists("pFieldMoveDirection")) { // Needed again because no default case here again (VECTOR3).

            Vector3 loadedMoveSpeed = ES3.Load<Vector3>("pFieldMoveDirection");

        }

        if (ES3.KeyExists("pFieldInventory")) { // Needed again because no default case here again (VECTOR3).

            List<GameObject> loadedInventory = ES3.Load<List<GameObject>>("pFieldInventory");

        }

        if (ES3.KeyExists("piFieldDialogueSysList")) {
            List<PixelCrushers.DialogueSystem.Conversation> loadedDialogueSystemList = ES3.Load<List<PixelCrushers.DialogueSystem.Conversation>>("piFieldDialogueSysList");
        }

        // * Jump field additions....
        if (ES3.KeyExists("pFieldJumpV3")) {

            Vector3 loadedMoveSpeed = ES3.Load<Vector3>("pFieldJumpV3");

        }
        if (ES3.KeyExists("pFieldJumpHeight")) {

            float loadedMoveSpeed = ES3.Load<float>("pFieldJumpV3", 0.0f);

        }
        if (ES3.KeyExists("pFieldGrounded")) {

            bool loadedMoveSpeed = ES3.Load<bool>("pFieldJumpV3", false); // Default grounded as false...

        }

        
    }

}


// **For gamestates to come:
public enum GameState
{
    GAMEOVER,
    PLAYING,
    WON,
    DEFAULTED
}

