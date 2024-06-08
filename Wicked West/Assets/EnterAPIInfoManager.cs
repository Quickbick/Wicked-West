using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; // Import the TextMeshPro namespace
using QFSW.QC;
using UnityEngine.UI;
using PixelCrushers;

// * Message on Code Comments in this File:
// ? INSTALL + APPLY the plugin 'Better Comments' by iguissouma to see color-differentiation between comments dependent on header sybmols (*, ?, !, etc.)

public class EnterAPIInfoManager : MonoBehaviour {

    [SerializeField] public GameObject keyEnterInputFieldGO;
    [SerializeField] public Button enterKeyButtonGO;
    [SerializeField] public Button returnKeyButtonGO;
    [SerializeField] public bool deleteAPIKeyAndVal = false;

    // Start is called before the first frame update
    void Start() {

        // ! Inspector clearing in case API breaks between key changes.
        if (deleteAPIKeyAndVal) {

            // If found:
            if (ES3.KeyExists("customUserAPIKey")) {

                Debug.Log(ES3.Load("customUserAPIKey").ToString() + " is now being deleted");
                ES3.DeleteKey("customUserAPIKey");
                

            }

            // And if they key does not exist:
            else {

                Debug.Log("No key under term " + "customUserAPIKey" + "has been saved yet");

            }

        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PushEnterButton() {

        // ? Update the saved API key (make new save/loads) here before entering:
        // ! .......
        string newAPIKEY = keyEnterInputFieldGO.GetComponent<TMP_InputField>().text;

        // * Save as key/val pair
        ES3.Save("customUserAPIKey", newAPIKEY);
        Debug.Log("SAVED new key");


    }

    public void PushReturnButton() {

        Debug.Log("Fading back to LoadGameStateScene");
        Initiate.Fade("LoadGameStateScene", Color.white, 1.1f);

    }

}
