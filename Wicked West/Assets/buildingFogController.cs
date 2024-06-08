using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class buildingFogController : MonoBehaviour {

    private GameManager gameManager; // Reference to the GameManager
    
    public GameObject nightFog; // attached to the night fog object in the scene (located under player prefab)
    public GameObject dayFog; // attached to the day fog object in the scene (located under player prefab)

    private void Start()
    {
        // Get a reference to the GameManager
        gameManager = GameManager.Instance;
    }

    private void OnTriggerEnter(Collider other) {

        Debug.Log("OnTriggerEnter with: " + other);

        if(other.CompareTag("Player")) {

            // let the game know the player has entered a building
            // needs to be updated otherwise NightCycleIncrementer script turns thw fog back on
            gameManager.inBuilding = true;

            // check to see if it is day or night by checking which fog is active
            if (nightFog.activeSelf == true) {

                Debug.Log("Entered Building: Deactivate Night Fog");
                // its night
                // turn night fog off
                nightFog.SetActive(false);
            } else if (dayFog.activeSelf == true) {
                
                Debug.Log("Entered Building: Deactivate Day Fog");
                // its day time
                // turn day fog off
                dayFog.SetActive(false);
            }

        }

    }

    private void OnTriggerExit(Collider other) {

        Debug.Log("OnTriggerExit with: " + other);

        if(other.CompareTag("Player")) {

            // let the game know the player has left a building
            // needs to be updated so NightCycleIncrementer script turns the fog back on
            gameManager.inBuilding = false;

            // check to see if it is day or night
            if (gameManager.isNighttime) {

                Debug.Log("Left Building: Reactivate Night Fog");
                // its night
                // turn night fog back on
                nightFog.SetActive(true);
            } else {

                Debug.Log("Left Building: Reactivate Day Fog");
                // its day time
                // turn day fog back on
                dayFog.SetActive(true);
            }

        }

    }

}
