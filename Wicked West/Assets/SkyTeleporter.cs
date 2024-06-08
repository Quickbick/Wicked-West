using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using QFSW.QC;

// * Basically PositionalSpawnInScript but with command for it to try and make it happen in-game from dev console... if this doesn't work its some script i don't know about affecting this class.
// ! ⁺˚⋆｡°✩₊ John made this script in an attempt to get the player to go to the right location on spawn, but it actually hasn't been working like that....
// !  ⁺˚⋆｡°✩₊ basically, I'm starting to think that possibly some other script is sending it to somewhere or initializing start positions,
// !  ⁺˚⋆｡°✩₊ so instead I think I'm going to ask nate to put in a default spawn location script and show that.... if that can be done, I can just replace the Vector3 values of 
// !  ⁺˚⋆｡°✩₊ position, Quaternion values of Rotation, and Vector3 values of scale.... otherwise idk I've tried things but things are sus....
// ? [UPDATE] works when I put on the Player Prefab class... now to save/load things.

public class SkyTeleporter : MonoBehaviour {

    [Header("What scene is it? {ASSIGN  HERE}")] // ! "Town", or "Mine"
    [SerializeField] public string sceneExistance;
    // ! Height at which the GameObject will be teleported to in the sky.
    [Header("Teleport Parameters")]
    [SerializeField] public float skyHeight = 80f; // * Specify the height in the Inspector.

    // * Target X position in the sky.
    [SerializeField] public float targetX = 0f; // * Specify the X position in the Inspector.

    // * Target Z position in the sky.
    [SerializeField] public float targetZ = 0f; // * Specify the Z position in the Inspector.
    [SerializeField] public float rotationX;
    [SerializeField] public float rotationY;
    [SerializeField] public float rotationZ;

    [Header("Time in seconds in-place at spawn to override other scripts I don't know about")]
    [SerializeField] public float timeSecStay = 1f;
    

    private bool isFirstSecond = true;

    // ? The [Command] attribute allows this method to be invoked remotely using QFSW.QC;
    // * It teleports the GameObject to the specified position in the sky.
    [Command]
    public void T_TeleportToSky() {

        // ! Calculate the teleport position using the specified target X, target Z, and skyHeight.
        Vector3 teleportPosition = new Vector3(targetX, skyHeight, targetZ);

        // ! Teleport the GameObject to the calculated position in the sky.
        // Debug.Log("X: " + targetX);
        // Debug.Log("Y: " + skyHeight);
        // Debug.Log("Z: " + targetZ);
        transform.GetChild(0).transform.position = teleportPosition;

        // Set the rotation of the child GameObject (Euler angles)
        transform.GetChild(0).transform.eulerAngles = new Vector3(rotationX, rotationY, rotationZ);

    }

    private void Start() {

        // ! UNCOMMENT THIS LINE BELOW TO FIX if LOCATION SAVES ARE DOUBLES FROM ES3 ERROR STUFF:
        // FIXSAVEDLOC();

        if (sceneExistance == "Town") {

            // * POSITION:
            if (ES3.KeyExists("nppFieldTargetX") && ES3.KeyExists("nppFieldTargetZ") && ES3.KeyExists("nppFieldTargetSKY")) {

                skyHeight = ES3.Load<float>("nppFieldTargetSKY") + 0.04f; // * Add a little at end to hopefully stop it from falling through the void.
                targetX = ES3.Load<float>("nppFieldTargetX");
                targetZ = ES3.Load<float>("nppFieldTargetZ");
                Debug.Log("[c:] X&Y&Z found- setting location on spawn to SAVED location.");

            } else { // * If first run though, set XYZ to wherever the parent gameobject is located in the editor.

                // ? May need updating from grabbing parents to childs again.
                Debug.Log("[c:] X&Y&Z not found- setting location on spawn to default.");
                
                skyHeight = transform.GetChild(0).transform.position.y;
                targetX = transform.GetChild(0).transform.position.x;
                targetZ = transform.GetChild(0).transform.position.z;
                

            }

            // * ROTATION:
            if (ES3.KeyExists("nppFieldRotationX") && ES3.KeyExists("nppFieldRotationY") && ES3.KeyExists("nppFieldRotationZ")) {

                Debug.Log("Saved Y Rotation: " + ES3.Load<float>("nppFieldRotationY"));

                // Load the rotation values (Euler angles)
                rotationX = ES3.Load<float>("nppFieldRotationX");
                rotationY = ES3.Load<float>("nppFieldRotationY");
                rotationZ = ES3.Load<float>("nppFieldRotationZ");

                Debug.Log("X: " + rotationX);
                Debug.Log("Y: " + rotationY);
                Debug.Log("Z: " + rotationZ);

                Debug.Log("[c:] Rotation found- setting rotation on spawn to SAVED rotation.");

            } else {

                // If rotation data doesn't exist, use the default rotation (no changes needed here).
                Debug.Log("[c:] Rotation not found- using default rotation.");

            }

            // * Teleport the GameObject to the specified position when it starts. Otherwise, it just defaults.....
            // !OLD: T_TeleportToSky();
            // * new:
            StartCoroutine(InitialTeleport(timeSecStay));

        }

        else {

            Debug.Log("Nothing here, because " + sceneExistance + " scene detected... [For now]");

        }
        

    }

    private IEnumerator InitialTeleport(float timeSecStay)
    {
        float startTime = Time.time;

        // * Retry teleporting for the first timeSecStay second(s).
        while (Time.time - startTime < timeSecStay)
        {
            T_TeleportToSky();
            // Debug.Log("Teleporting in place off spawn");
            yield return null;
        }

        Debug.Log("Done teleporting");

        // After the first seconds, set isFirstSecond to false
        isFirstSecond = false;

    }

    // ! WHEN BROKEN: 
    public void FIXSAVEDLOC() {

        ES3.Save("nppFieldTargetX", targetX); // was skyTeleporterScript
        ES3.Save("nppFieldTargetSKY", skyHeight);
        ES3.Save("nppFieldTargetZ", targetZ); // * Sky is Y/height.

    }

}
