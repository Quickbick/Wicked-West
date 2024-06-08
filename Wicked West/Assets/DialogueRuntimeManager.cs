using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using QFSW.QC;

// * Message on Code Comments in this File:
// ? INSTALL + APPLY the plugin 'Better Comments' by iguissouma to see color-differentiation between comments dependent on header sybmols (*, ?, !, etc.)


namespace PixelCrushers.DialogueSystem.OpenAIAddon {

    public class DialogueRuntimeManager : MonoBehaviour {
        
        [SerializeField] public GameObject dialogueRMGO;
        public RuntimeAIConversationSettings dialogueManagerScript;
        
        // Start is called before the first frame update
        private void Start() {

            // * Initialize dialogueManagerScript
            dialogueManagerScript = dialogueRMGO.GetComponent<RuntimeAIConversationSettings>();

            // * Set up API key to load in, if custom entered and found, and do nothing if not:
            if (ES3.KeyExists("customUserAPIKey")) {

                Debug.Log("CustomAPI key found: " + ES3.Load("customUserAPIKey").ToString());

                // ? We'll see if this breaks it, or if it can update in runtime, if found.
                API_updateAPIKey(ES3.Load("customUserAPIKey").ToString());

            }

            else { Debug.Log("CustomAPI key NOT found..."); }
            
        }

        // Update is called once per frame
        private void Update() {
            
        }

        // * Update the API key in runtime:
        [Command]
        public void API_updateAPIKey(string newAPI) {

            Debug.Log("Attempted to use new API key: " + newAPI);
            dialogueManagerScript.APIKey = newAPI;

            // ! If can reach here, probably successful in runtime.
            Debug.Log("If this was reached, dialogueManagerScript's APIKey has been updated to: " + newAPI);

        }

        // * Update the API temperature in runtime:
        [Command]
        public void API_updateTemperature(float newTemp) {

            Debug.Log("Attempted to set a new temperature for ChatGPT: " + newTemp);
            dialogueManagerScript.Temperature = newTemp;

        }

        // * Update the API temperature in runtime:
        [Command]
        public void API_updateMaxtokens(int newMax) {

            Debug.Log("Attempted to set a new max tokens for ChatGPT: " + newMax);
            dialogueManagerScript.MaxTokens = newMax;

        }

    }

}
