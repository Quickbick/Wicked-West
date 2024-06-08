// * Usings:
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; // Import the TextMeshPro namespace
using QFSW.QC;

// * For ES3 Clearing:
using ES3Internal;

// * Message on Code Comments in this File:
// ? INSTALL + APPLY the plugin 'Better Comments' by iguissouma to see color-differentiation between comments dependent on header sybmols (*, ?, !, etc.)

public class LoadGameStateManager : MonoBehaviour {

    // * Properties section:
    // Get TextMeshPro GameObject references:
    public GameObject txt_ID_GO;
    public GameObject txt_DayNights_GO;
    public GameObject txt_Timeelapsed_GO;
    [SerializeField] public GameObject continuePanelParent_GO;
    public string txt_ID_TEXT = "[Player #2531]";
    public string txt_DayNights_TEXT = "Day: 1";
    public string txt_Timeelapsed_TEXT = "Time: 0h2m";
    [SerializeField] public GameObject mmScriptGO;
    private MainMenu mmScriptItself;

    // Start is called before the first frame update
    void Start() {

        // * Apply GameObjects for text
        txt_ID_GO = continuePanelParent_GO.transform.GetChild(2).gameObject;
        txt_DayNights_GO = continuePanelParent_GO.transform.GetChild(3).gameObject;
        txt_Timeelapsed_GO = continuePanelParent_GO.transform.GetChild(4).gameObject;
        mmScriptItself = mmScriptGO.GetComponent<MainMenu>();

    }

    // * Update is called once per frame and will be used here to show updated game state live in Load Game State scene.
    void Update() {
        
        // ? Actually I think we can use defaultValue override....
        // if (ES3.KeyExists("gmFieldDays")) {
        // txt_ID_TEXT = ES3.Load<string>("gmFieldID", "Player").ToString();
        if  (ES3.KeyExists("gmFieldID")) { // ! Basically, this will be like the player's name or something like that if that ends up being desired......

            txt_ID_TEXT = ES3.Load<string>("gmFieldID").ToString();

        }

        else {

            txt_ID_TEXT = "Player";

        }
        
        // txt_DayNights_TEXT = ES3.Load<int>("gmFieldDays", 0).ToString();
        if  (ES3.KeyExists("gmFieldDays")) {

            txt_DayNights_TEXT = "Day " + ES3.Load<int>("gmFieldDays"); // * took out toString()

        }

        else {

            txt_DayNights_TEXT = "Day: 0";

        }
        
        // ToDo: Add timeelapsed once referenceed or refactored DayCycleIncrememnter fields:
        // txt_Timeelapsed_TEXT = ES3.Load<int>("gmFieldDays", 0).ToString();

        if (ES3.KeyExists("gmFieldTimeElapsedInSec")) {

            // txt_Timeelapsed_TEXT = "Time: " + ES3.Load<int>("gmFieldTimeElapsedInSec").ToString();
            int totalSeconds = ES3.Load<int>("gmFieldTimeElapsedInSec");
            int hours = totalSeconds / 3600;
            int minutes = (totalSeconds % 3600) / 60;
            int seconds = totalSeconds % 60;
            txt_Timeelapsed_TEXT = "Total Playtime: " + hours.ToString("D2") + "h:" + minutes.ToString("D2") + "m:" + seconds.ToString("D2") + "s";

        }

        else {

            txt_Timeelapsed_TEXT = "Total playtime: XhXm";

        }

        // * Put TMP updates in Update so clearing of data can be shown in realtime and be updated when it happens (if).
        txt_ID_GO.GetComponent<TextMeshProUGUI>().text = txt_ID_TEXT;
        txt_DayNights_GO.GetComponent<TextMeshProUGUI>().text = txt_DayNights_TEXT;
        txt_Timeelapsed_GO.GetComponent<TextMeshProUGUI>().text = txt_Timeelapsed_TEXT;

    }

    [Command]
    public void NewGamePressed() {

        DeleteAllKeysAndVals();

        // * Wait a second, hopefully then the file/key deletions are done, then goes into 
        StartCoroutine(WaitForCoroutine(0.08f, "SceneWModels"));

    }

    [Command]
    public void EnterCustomAPIPressed() {

        // * Wait a second, hopefully then the file/key deletions are done, then goes into 
        StartCoroutine(WaitForCoroutine(0.08f, "CustomAPIScene"));

    }

    [Command]
    public void TestMsgInstanceSpeak1() {

        MessageManager.Instance.DisplayMessage("Enter a custom API Token from your OpenAI Account if you own one already", 3f);

    }

    [Command]
    public void TestMsgInstanceSpeak2(float displayTime) {

        MessageManager.Instance.DisplayMessage("Your message here", displayTime);

    }

    [Command]
    public void TestMsgInstanceSpeak3(string InstanceMessageString, float displayTime) {

        MessageManager.Instance.DisplayMessage(InstanceMessageString, displayTime);

    }

    private IEnumerator WaitForCoroutine(float secondsTime, string desiredScene) {
        // Wait for approximately howevermany seconds
        yield return new WaitForSeconds(secondsTime);

        // * The code after the switch case will execute after the delay.
        switch (desiredScene) {

            case "SceneWModels":
                mmScriptItself.ChangeToSceneWithModels(); // * which begins the game.
                break;

                case "CustomAPIScene":
                mmScriptItself.ChangeToAPIScene(); // Doesn't start game, just custom API scene, which already has a return from it.
                break;

        }

    }

    [Command]
    public void DeleteAllKeysAndVals() {

        // *old: string filePath = "Wicked West/SaveFile.es3";
        // ?New attempt:
        string filePath = "SaveFile.es3";

        // ES3.DeleteFile("Wicked West/SaveFile.es3");
        // Debug.Log("Wicked West/SaveFile.es3 Cleared! (deleted)");

        if (ES3.FileExists(filePath))
        {
            ES3.DeleteFile(filePath);
            Debug.Log(filePath + " Cleared! (deleted)");
        }
        else
        {
            Debug.Log("[DNE!] " + filePath + " does not exist.");
        }

    }

}
