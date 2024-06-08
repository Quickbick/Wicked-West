using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// using BitSplash;

namespace BitSplash.AI.GPT
{


    public class RateGPT : MonoBehaviour
    {

        // Defining public item enum, just for name recog.
        public enum Item
        {
            None,
            Beer,
            Camera,
            Dynamite,
            HolyWater,
            Key,
            Metal,
            Vial
        }

        public string phraseInput;

        [Header("Input Specification Settings")]
        [SerializeField] public string prePhrase;
        [SerializeField] public string examples;
        [SerializeField] public float confidencePoint = 0.85f;

        // Start is called before the first frame update
        void Start()
        {

            examples = " For example, If the asking question was 'can I have a beer' and the response to that was 'coming right up!', then that would be 1. If the response was 'you're too young', or 'great day we're having' or something like that, it should be 0. If no question was asked by the player initially, i.e. \"\" is all thats given in this prompt, definitly the confidencec should be 0, as an NPC shouldn't give items on greetings.";

            // var convo = ChatGPTConversation.RateGPT(this); // * Starts conversation
            // string phraseInput = 
            // convo.Say(prePhrase + "Return me a float from 0 to 1 which represents your confidence in the following quote being something spoken when someone is giving some sort of item to a player in a game: " + phraseInput);
        }

        // Update is called once per frame
        void Update()
        {
            
        }

        void OnConversationResponse(string text) {

            Debug.Log("response from RateGPT: " + text);
            // GameManager.Instance.RecieveConfidence(text); // ! OLD

            if (ParseRatingPossible(text)) {

                float parsedRating = ParseAndGetParsedRating(text);

                if ((parsedRating >= confidencePoint) && (parsedRating <= 1.0f)) { // Current Threshold

                    Debug.Log(parsedRating + " <- parsed confidence rating from 1-0, current threshold to spawn tiem is: " + confidencePoint.ToString());
                    ActivateItem((GameManager.Instance.givingItemGM));

                }

            }

        }
        
        public bool ParseRatingPossible(string text) {
            // Attempt to parse the string to float
            float result;
            return float.TryParse(text, out result); // If successful, return true; otherwise, return false
        }

        public float ParseAndGetParsedRating(string text) {
            float parsedRating = 0f;
            if (float.TryParse(text, out parsedRating)) {
                // If parsing succeeds, parsedRating will contain the parsed value
                // You can perform additional processing here if needed
            } else {
                // If parsing fails, you may handle the error or return a default value
                Debug.LogWarning("Failed to parse rating from text: " + text);
            }
            return parsedRating;
        }


        void OnConversationError(string text) {

            Debug.Log("ERROR: " + text);
            // GameManager.Instance.RecieveError("ERROR: " + text); // ! Old, also.

        }

        public void ActivateItem(string itemA) {

            // GameManager.Instance.beer.SetActive(true); // * <-- replicating this 
            // Activate the appropriate GameObject based on the item
            switch (itemA) {

                case "Beer":
                    GameManager.Instance.beer.SetActive(true);
                    Debug.Log("Successfully made beer item appear now, I believe...");
                    break;
                case "Camera":
                    GameManager.Instance.camera.SetActive(true); // ? Was cameraObj..?
                    break;
                case "Dynamite":
                    GameManager.Instance.dynamite.SetActive(true);
                    break;
                case "HolyWater":
                    GameManager.Instance.holywater.SetActive(true);
                    break;
                case "Key":
                    GameManager.Instance.key.SetActive(true);
                    break;
                case "Metal":
                    GameManager.Instance.metal.SetActive(true);
                    break;
                case "Vial":
                    GameManager.Instance.vial.SetActive(true);
                    break;
                default:
                    // If the item is None or unrecognized, do nothing
                    Debug.Log("Hmmm.... Item not recognized.....");
                    break;

            }

        }

        public void StartRateConversationGiveItem() {

            var convo = ChatGPTConversation.RateGPT(this); // * Starts conversation
            // string phraseInput = 
            convo.Say(prePhrase + " Return me a float from 0 to 1 which represents your confidence in the following quote being something spoken when someone is giving some sort of item to a player in a game: " + phraseInput);

        }

        // * best one, used the most:
        public void SRC_GiveItem_CustomPhrase(string customPhrase, Item givingItem) {

            phraseInput = customPhrase;
            string questionAskedPhrase = " Note: the question that was asked by the player was: \"";

            // * Trying to set last asked Q in runtime:
            if (GameManager.Instance) {

                questionAskedPhrase += GameManager.Instance.lastQuestionAskedByPlayer;
                questionAskedPhrase += "\". ";

            } else { 

                Debug.LogError("GameManager instance not found- issue!"); 
                questionAskedPhrase = "";
                
            }
            

            Debug.Log(phraseInput);
            GameManager.Instance.givingItemGM = givingItem.ToString();

            var convo = ChatGPTConversation.RateGPT(this); // * Starts conversation
            // string phraseInput = 

            Debug.Log("Checking if meets threshold on :: " + (prePhrase + questionAskedPhrase + "Return me a float from 0 to 1 which represents your confidence that following quote is being spoken while someone is giving a" + givingItem.ToString() + "to the person they are speaking to: " + phraseInput));
            convo.Say(prePhrase + questionAskedPhrase + "Return me a float from 0 to 1 which represents your confidence that following quote is being spoken while someone is giving a" + givingItem.ToString() + "to the person they are speaking to: " + phraseInput + examples);

        }

        public void StartRateConversationTakeItem() {

            var convo = ChatGPTConversation.RateGPT(this); // * Starts conversation
            // string phraseInput = 
            convo.Say(prePhrase + " Return me a float from 0 to 1 which represents your confidence in the following quote being something spoken when someone is taking some sort of item from a player in a game: " + phraseInput);

        }

        /*
        public void RateConversationSay() {

            convo

        }
        */

        public void DemoExampleUsage() {

            GameManager.Instance.RecieveConfidence("Here, I am giving you an item now. It is a rock.");
            GameManager.Instance.ReturnedConfYet = false; // * then, turns to false and waits.
            StartCoroutine(WaitForConfirmation());

        }

        private IEnumerator WaitForConfirmation()
        {
            // Wait until returnedConfYet is true
            while (!GameManager.Instance.ReturnedConfYet)
            {
                yield return null; // Wait for the next frame
            }

            // Do something after confirmation
            Debug.Log("Confirmation received. Printing...");
            Debug.Log("Confidence: " + GameManager.Instance.Confidence);

            // ! Reset returnedConfYet back to false
            GameManager.Instance.ReturnedConfYet = false;

        }

        public void confidencePointLOW() {confidencePoint = 0.15f;}
        public void confidencePointMED() {confidencePoint = 0.45f;}
        public void confidencePointHIGH() {confidencePoint = 0.85f;}

    }

}
