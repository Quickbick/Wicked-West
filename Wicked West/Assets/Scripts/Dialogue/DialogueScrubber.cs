using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PixelCrushers.DialogueSystem;
using BitSplash.AI.GPT;

public class DialogueScrubber : MonoBehaviour
{
    [Header("RateGPT")]
    [SerializeField] public GameObject RateGPTHandlerGO;
    public RateGPT RateGPTScript;
    
    private string NPCLine;
    private string Speaker;


    private void Start() {

        if (!RateGPTHandlerGO) {

            Debug.LogError("RateGPTHandler gameobject not found error.");

        }

        else {

            RateGPTScript = RateGPTHandlerGO.GetComponent<RateGPT>();

        }

    }

    void OnConversationLine(Subtitle subtitle)
    {
        NPCLine = subtitle.formattedText.text;
        Speaker = subtitle.speakerInfo.Name;
        this.CheckForTriggerAI(NPCLine);
        /*ItemManager man = GameManager.Instance.transform.Find("Item Manager").gameObject.GetComponent<ItemManager>();
        Debug.Log(man.ItemsAcquired); // The one populated on initizlization, if this line passes then ref is probably okay.
        Debug.Log(man.beer.ToString()); // If this one fails, then beer could be where the issue is. If they both pass, then */
    }

    void CheckForTriggerAI(string line){
        if (Speaker.Contains("Bartender")){
            if (GameManager.Instance.ItemsAcquired[0] == false){
                // * RateGPT v1.2: 
                RateGPTScript.SRC_GiveItem_CustomPhrase(line, RateGPT.Item.Beer);
            }
        }
        if (Speaker.Contains("Researcher")){
            if (GameManager.Instance.ItemsAcquired[1] == false){
                RateGPTScript.SRC_GiveItem_CustomPhrase(line, RateGPT.Item.Camera);
            }
        }
        if (Speaker.Contains("Miner")){
            if (GameManager.Instance.ItemsAcquired[2] == false){
                RateGPTScript.SRC_GiveItem_CustomPhrase(line, RateGPT.Item.Dynamite);
            }
        }
        if (Speaker.Contains("Pastor")){
            if (GameManager.Instance.ItemsAcquired[3] == false){
                RateGPTScript.SRC_GiveItem_CustomPhrase(line, RateGPT.Item.HolyWater);
            }
        }
        if (Speaker.Contains("Sheriff")){
            if (GameManager.Instance.ItemsAcquired[4] == false){
                RateGPTScript.SRC_GiveItem_CustomPhrase(line, RateGPT.Item.Key);
            }
        }
        if (Speaker.Contains("Smith")){
            if (GameManager.Instance.ItemsAcquired[5] == false){
                RateGPTScript.SRC_GiveItem_CustomPhrase(line, RateGPT.Item.Metal);
            }
        }
        if (Speaker.Contains("Salesman")){
            if (GameManager.Instance.ItemsAcquired[6] == false){
                RateGPTScript.SRC_GiveItem_CustomPhrase(line, RateGPT.Item.Vial);
            }
        }
    }

    //OLD CHECK FOR TRIGGER FUNCTION, KEPT AROUND IN CASE SOMETHING BREAKS
    /*void CheckForTrigger(string line){
        if (Speaker.Contains("Bartender")){
            if (GameManager.Instance.ItemsAcquired[0] == false && (line.Contains("Coming right up") || (line.Contains("take") && line.Contains("beer")))){

                // * RateGPT v1.2: 
                RateGPTScript.SRC_GiveItem_CustomPhrase(line, RateGPT.Item.Beer);
                // Disabled until I can work out more compile errors from new additions:

               
                GameManager.Instance.beer.SetActive(true);

            }
        }
        if (Speaker.Contains("Researcher")){
            if (line.Contains("take") && line.Contains("camera") && GameManager.Instance.ItemsAcquired[1] == false){
                GameManager.Instance.camera.SetActive(true);
            }
        }
        if (Speaker.Contains("Miner")){
            if (line.Contains("dynamite") && GameManager.Instance.ItemsAcquired[2] == false){
                GameManager.Instance.dynamite.SetActive(true);
            }
        }
        if (Speaker.Contains("Pastor")){
            if (line.Contains("holy water") && GameManager.Instance.ItemsAcquired[3] == false){
                GameManager.Instance.holywater.SetActive(true);
            }
        }
        if (Speaker.Contains("Sheriff")){
            if (line.Contains("take") && line.Contains("key") && GameManager.Instance.ItemsAcquired[4] == false){
                GameManager.Instance.key.SetActive(true);
            }
        }
        if (Speaker.Contains("Smith")){
            if (line.Contains("scrap metal") && GameManager.Instance.ItemsAcquired[5] == false){
                GameManager.Instance.metal.SetActive(true);
            }
        }
        if (Speaker.Contains("Salesman")){
            if (line.Contains("sold") && GameManager.Instance.ItemsAcquired[6] == false){
                GameManager.Instance.vial.SetActive(true);
            }
        }
    }*/
}
