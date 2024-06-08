using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractCollider : MonoBehaviour
{
    public UnityEngine.UI.Image crosshair;
    public Sprite DefaultCrosshair;
    public Sprite NPCCrosshair;
    public Sprite ObjectCrosshair;
    public Sprite HidingCrosshair;
    public Sprite DestructibleCrosshair;

    public GameObject interactTextBG;
    public GameObject talkText;
    public GameObject hideText;
    public GameObject altarText;
    public GameObject sleepText;
    public GameObject pickUpText;

    public Collider NotedObject = null;

    public void ResetCrosshair()
    {
        crosshair.sprite = DefaultCrosshair;
        this.NotedObject = null;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        interactTextBG.SetActive(false);
        talkText.SetActive(false);
        hideText.SetActive(false);
        altarText.SetActive(false);
        sleepText.SetActive(false);
        pickUpText.SetActive(false);
    }

    private void OnTriggerEnter(Collider other) 
    {
        switch (other.tag)
        {
            case "NPC":
                this.crosshair.sprite = NPCCrosshair;
                this.NotedObject = other;
                interactTextBG.SetActive(true);
                talkText.SetActive(true);
                break;
            case "Object":
                this.crosshair.sprite = ObjectCrosshair;
                this.NotedObject = other;
                interactTextBG.SetActive(true);
                pickUpText.SetActive(true);
                break;
            case "Hiding Place":
                this.crosshair.sprite = HidingCrosshair;
                this.NotedObject = other;
                interactTextBG.SetActive(true);
                hideText.SetActive(true);
                break;
            case "Destructible":
                this.crosshair.sprite = DestructibleCrosshair;
                this.NotedObject = other;
                break;
            case "Altar":
                this.crosshair.sprite = ObjectCrosshair;
                this.NotedObject = other;
                interactTextBG.SetActive(true);
                altarText.SetActive(true);
                break;

            // * Sleeping in beds:
            case "Bed Place":
                this.crosshair.sprite = HidingCrosshair;
                this.NotedObject = other;
                interactTextBG.SetActive(true);
                sleepText.SetActive(true);
                break;

            default:
                break;
        } 
    }

    private void OnTriggerExit(Collider other){
        ResetCrosshair();
    }
}
