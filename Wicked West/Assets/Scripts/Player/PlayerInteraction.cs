using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PixelCrushers;
using PixelCrushers.DialogueSystem;
using System;
using System.Threading;

public class PlayerInteraction : MonoBehaviour
{
    public GameObject interactCollider;
    public List<PixelCrushers.DialogueSystem.Conversation> dialogueSysList; // * Holds all dialogue systems started from things like case "NPC"

    private InteractCollider collider;
    private GameObject holdThis;
    private MeshRenderer renderer;

    // these variables only need to be assigned in mine scene
    public GameObject beerBottle;
    public GameObject camera;
    public GameObject dynamite;
    public GameObject holywater;
    public GameObject key;
    public GameObject metal;
    public GameObject vial;

    // used to control active state of necessary interact directions canvases
    public GameObject interactTextBG;
    public GameObject talkText;
    public GameObject hideText;
    public GameObject unhideText;
    public GameObject sleepText;

    // Start is called before the first frame update
    void Start()
    {
        collider = interactCollider.GetComponent<InteractCollider>();
        renderer = this.GetComponent<MeshRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E) && GameManager.Instance.isHiding && !GameManager.Instance.isPaused){
            
            Unhide();
        }
        else if (Input.GetKeyDown(KeyCode.E) && collider.NotedObject != null && !GameManager.Instance.isPaused){
            Interact();
        }
    }

    void Interact(){
        switch (collider.NotedObject.tag)
        {
            case "NPC":
                // turn off interact directions canvas for talk
                interactTextBG.SetActive(false);
                talkText.SetActive(false);

                //unlocks cursor for use in dialog menu
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                GameManager.Instance.Pause();

                //call dialogue
                collider.NotedObject.gameObject.GetComponent<PixelCrushers.DialogueSystem.DialogueSystemTrigger>().OnUse();
                
                // * Trying new to assign a savable/loadable DialogueSystem...
                // Removed before demo: 
                /*
                PixelCrushers.DialogueSystem.Conversation newDialogueSys = collider.NotedObject.gameObject.GetComponent<PixelCrushers.DialogueSystem.DialogueSystemTrigger>().OnUse();
                dialogueSysList.add(newDialogueSys); // And add to list...
                */
                break;
            case "Object":
                //add object to inventory
                Item itemToAdd = collider.NotedObject.gameObject.GetComponent<Item>();
                GameManager.Instance.Inventory.Add(itemToAdd);
                GameManager.Instance.ItemsAcquired[itemToAdd.managerValue] = true;
                //delete object in world
                collider.NotedObject.gameObject.SetActive(false);
                collider.ResetCrosshair();
                break;
            case "Hiding Place":
                // turn off interact directions canvas for hide
                interactTextBG.SetActive(false);
                hideText.SetActive(false);

                // turn on interact directions canvas for unhide
                unhideText.SetActive(true);

                GameManager.Instance.isHiding = true;
                renderer.enabled = false;
                this.holdThis = collider.NotedObject.gameObject;
                holdThis.GetComponent<HidingPlace>().SwitchToCamera();
                break;
            
            // * Sleeping:
            case "Bed Place":
                // turn off interact directions canvas for sleep
                interactTextBG.SetActive(false);
                sleepText.SetActive(false);

                if (GameManager.Instance.isNighttime) {

                    Debug.Log("You woke up at 3am, you want to look around, not sleep! [GameManager.Instance.isNighttime detected]");

                }

                else {

                    GameManager.Instance.isHiding = true;
                    renderer.enabled = false;
                    this.holdThis = collider.NotedObject.gameObject;
                    holdThis.GetComponent<HidingPlace>().SwitchToCamera();

                    // ? Call sleep here I believe:
                    GameManager.Instance.transform.Find("Timer").GetComponent<DayCycleIncrementer>().ToNight();

                }
                
                break;
            
            // * Altar:
            case "Altar":
                Debug.Log("Interacted with Altar");
                Debug.Log("Item to reveal:" + GameManager.Instance.Inventory[GameManager.Instance.Inventory.Count - 1].itemName);

                // (probably change this, otherwise altar can be activated at one item)
                // check to see if the player has added all their items to the altar
                if (GameManager.Instance.Inventory.Count == 1) {
                    // reveal the last item in the player's inventory list on the altar
                    revealAltarItem(GameManager.Instance.Inventory[GameManager.Instance.Inventory.Count - 1].itemName);
                    Thread.Sleep(3000); // Sleep for 3 seconds to play item animation

                    // -----------------------------------------------------------------------------------
                    // ADD CODE HERE FOR WIN SCREEN, BOSS FIGHT, WHATEVER WE DECIDE TO DO AFTER THIS POINT
                    // -----------------------------------------------------------------------------------
                    GameManager.Instance.bossActive = true;
                }

                // reveal the last item in the player's inventory list on the altar
                if (GameManager.Instance.Inventory[GameManager.Instance.Inventory.Count - 1]) {
                    revealAltarItem(GameManager.Instance.Inventory[GameManager.Instance.Inventory.Count - 1].itemName);
                Debug.Log("Item revealed");
                }
                
                break;

            case "Destructible":
                //break item
                break;
            default:
                break;
        } 
    }

    void Unhide(){
        // turn off interact directions canvas for unhide
        unhideText.SetActive(false);

        GameManager.Instance.isHiding = false;
        renderer.enabled = true;
        holdThis.GetComponent<HidingPlace>().SwitchOffCamera();
    }

    void revealAltarItem(string itemName) {
        switch (itemName) {
            case "Mysterious Vial":
                // set vial on the altar
                vial.SetActive(true);
                // remove the item from inventory
                GameManager.Instance.Inventory.RemoveAt(GameManager.Instance.Inventory.Count - 1);
                break;
            
            case "Metal Scrap":
                // set metal on the altar
                metal.SetActive(true);
                // remove the item from inventory
                GameManager.Instance.Inventory.RemoveAt(GameManager.Instance.Inventory.Count - 1);
                break;
            
            case "Key":
                // set key on the altar
                key.SetActive(true);
                // remove the item from inventory
                GameManager.Instance.Inventory.RemoveAt(GameManager.Instance.Inventory.Count - 1);
                break;
            
            case "Holy Water":
                // set holywater on the altar
                holywater.SetActive(true);
                // remove the item from inventory
                GameManager.Instance.Inventory.RemoveAt(GameManager.Instance.Inventory.Count - 1);
                break;
            
            case "Dynamite":
                // set dynamite on the altar
                dynamite.SetActive(true);
                // remove the item from inventory
                GameManager.Instance.Inventory.RemoveAt(GameManager.Instance.Inventory.Count - 1);
                break;
            
            case "Camera":
                // set camera on the altar
                camera.SetActive(true);
                // remove the item from inventory
                GameManager.Instance.Inventory.RemoveAt(GameManager.Instance.Inventory.Count - 1);
                break;
            
            case "Beer":
                // set beer bottle on the altar
                beerBottle.SetActive(true);
                // remove the item from inventory
                GameManager.Instance.Inventory.RemoveAt(GameManager.Instance.Inventory.Count - 1);
                break;
            
        }
    }
}
