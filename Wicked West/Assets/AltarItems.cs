using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AltarItems : MonoBehaviour
{   

    public GameObject beerBottle;
    public GameObject camera;
    public GameObject dynamite;
    public GameObject holywater;
    public GameObject key;
    public GameObject metal;
    public GameObject vial;

    public void revealAltarItem(string itemName) {
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
