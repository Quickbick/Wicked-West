using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using QFSW.QC;

public class GameManagerButtonHandler : MonoBehaviour {

    // * new [01/16/24] Fields added for audio/return stuff:
    [SerializeField] public GameObject volumePanel;

    // * Essentially, OnClick() stuff and more in the inspector can't grab instances, which the
    // * gamemanager is, so use this or other things to reference onclicks when desired to workaround.
    // * Example for all:
    public void CallGameManagerFunction()
    {
        // Call the GameManager.Instance function here
        // ! [WOULD BE]: GameManager.Instance.YourFunctionName();
    }

    // * refactored Onclick() helper sections:
    public void PausePressed() {
        // Pause something...
        GameManager.Instance.Pause();
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

    }

    public void UnpausePressed() {
        // UnPause something...
        GameManager.Instance.Pause();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void QuitPressed() {

        GameManager.Instance.gameStateRn = CurrentGameState.Quit;

        // * Now, exit:
        // Inside the function, we use preprocessor directives (#if UNITY_EDITOR) to handle two different cases:
        // If you're running the application inside the Unity Editor, it will stop playing the current scene using 
        // ...UnityEditor.EditorApplication.isPlaying.
        // If you're running a standalone build (e.g., a compiled executable), it will quit the application using Application.Quit().
        #if UNITY_EDITOR
                // If running in Unity Editor, stop playing the scene.
                UnityEditor.EditorApplication.isPlaying = false;
        #else
                // If running a standalone build, quit the application.
                Application.Quit();
        #endif

    }

    public void MainMenuPressed(){

        // * Fields:
        Color loadToColor = Color.black;
        string sceneToLoad = "MainMenuScene";
        float speedOfFade = 3.25f;

        // Upause first:
        // this.UnpausePressed();

        // ? Or possibly just:
        GameManager.Instance.TogglePause(); // now to set  Time.TimeScale to 1 for fading to occur. Maybe after below line though.

        // * Faded into main menu scene:
        Initiate.Fade(sceneToLoad, loadToColor, speedOfFade); // Hopefully goes well when unpaused.

    }

    // * [Script Newly added 01/02/24] ⁺˚⋆｡°✩₊ Note reach out to John if anythings broken with these changes and I'll try and fix it.

    [Command] // ! [01/16/24]: For devconsole for now, will discuss with group how we want this to be openable (keys maybe too?) in a future meeting....
    public void OpenVolumesMenu() {

        volumePanel.SetActive(true);

    }

    // * For Return Onlick();
    public void onClickReturn() {

        volumePanel.SetActive(false);

    }

}
