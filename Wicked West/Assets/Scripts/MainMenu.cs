using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


// * Message on Code Comments in this File:
// ? INSTALL + APPLY the plugin 'Better Comments' by iguissouma to see color-differentiation between comments dependent on header sybmols (*, ?, !, etc.)
// ! This isn't mandatory, just a note about file comments, but it is quick for anyone to add (<1 minute).

public class MainMenu : MonoBehaviour {

    // * Fields:
    public Color loadToColor = Color.black;
    public string sceneToLoad; // ! Updated to send to new scene with fades for game state handling with the game state handler in its own scene..
    public float speedOfFade = 4.75f;
    public string webpageURL = "https://forms.gle/6bH5RB2c8GNZ58e87";

    // * When play button pressed:
    // Function to change to the "Scene_with_Models" scene
    public void ChangeToSceneWithModels() {

        // * Faded into new scene:
        Initiate.Fade("Scene_with_Models", loadToColor, speedOfFade);

        // Old:
        // SceneManager.LoadScene("Scene_with_Models");
    
    }

    public void ChangeToAPIScene() {

        // * Faded into new scene:
        Initiate.Fade("EnterAPIKeyScene", loadToColor, speedOfFade);
    
    }

    public void ChangeToLoadStateScene() {

        // * Faded into new scene:
        Initiate.Fade("LoadGameStateScene", loadToColor, speedOfFade);
    
    }


    public void OpenExternalWebpage() // ! NEW External Call for feedback form.
    {
        Debug.Log("Opening external webpage");
        Application.OpenURL(webpageURL);
    }


}

// ? Notes about Fades:
/*
from anywhere in the game call "Initiate.Fade(sceneName,loadToColor,speed);"
where sceneName is the name of scene that you want to load of type string,
loadToColor is the color you want to fade away with of type Color and
speed is of type float variable which decides the speed of transition.
*/
