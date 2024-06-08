using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mine_light : MonoBehaviour
{
    //private GameManager gameManager; // Reference to the GameManager
    
    public GameObject light; // light shining on mine in the scene
    
    // Start is called before the first frame update
    void Start()
    {
        // Light should start turned off
        light.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        // Check to see if inventory is full
        if (GameManager.Instance.Inventory.Count == 7) {
            GameManager.Instance.hasAllItems = true;
        }

        if (GameManager.Instance.hasAllItems == true) {
            // Turn the spotlight on the mine on
            light.SetActive(true);
        }
        
    }
}
