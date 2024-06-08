using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ! ⁺˚⋆｡°✩₊ John made this script in an attempt to get the player to go to the right location on spawn, but it actually hasn't been working like that....
// !  ⁺˚⋆｡°✩₊ basically, I'm starting to think that possibly some other script is sending it to somewhere or initializing start positions,
// !  ⁺˚⋆｡°✩₊ so instead I think I'm going to ask nate to put in a default spawn location script and show that.... if that can be done, I can just replace the Vector3 values of 
// !  ⁺˚⋆｡°✩₊ position, Quaternion values of Rotation, and Vector3 values of scale.... otherwise idk I've tried things but things are sus....

public class PositionalSpawnInScript : MonoBehaviour
{
    private Transform targetTransform;

    // ! Wasn't working..... idk why. So we're commenting it out
    /*
    private void Start()
    {
        // Get the target transform (in this case, the player's transform)
        targetTransform = transform;

        // Delay the execution by 0.02 seconds to ensure no other script overrode the values
        StartCoroutine(UpdateTransformValuesDelayed());
    }

    private IEnumerator UpdateTransformValuesDelayed()
    {
        yield return new WaitForSeconds(0.02f);

        // Check if the saved positions exist in ES3
        if (ES3.KeyExists("pFieldPlayerPosition") &&
            ES3.KeyExists("pFieldPlayerRotation") &&
            ES3.KeyExists("pFieldPlayerScale"))
        {
            // Load the saved position, rotation, and scale
            Vector3 savedPosition = ES3.Load<Vector3>("pFieldPlayerPosition");
            Quaternion savedRotation = ES3.Load<Quaternion>("pFieldPlayerRotation");
            Vector3 savedScale = ES3.Load<Vector3>("pFieldPlayerScale");

            // Set the target transform's position, rotation, and scale
            targetTransform.position = savedPosition;
            targetTransform.rotation = savedRotation;
            targetTransform.localScale = savedScale;

            Debug.Log("Restored position, rotation, and scale from saved data.");
        }
        else
        {
            Debug.LogWarning("Saved transform data not found. Using default values.");
        }
    }
    */

}
