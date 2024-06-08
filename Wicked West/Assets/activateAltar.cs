using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class activateAltar : MonoBehaviour
{
    public GameObject light; // light shining on altar in the scene
    public GameObject altar; // altar in the mine scene

    public GameObject beerBottle;
    public GameObject camera;
    public GameObject dynamite;
    public GameObject holywater;
    public GameObject key;
    public GameObject metal;
    public GameObject vial;

    private bool playAnimation;

    [SerializeField] 
    private Animator itemAnimationController;

    
    // Start is called before the first frame update
    void Start()
    {
        // Light should start turned off
        light.SetActive(false);
        // Altar should not be visible
        altar.SetActive(false);
        // Items should not be visible by default
        beerBottle.SetActive(false);
        camera.SetActive(false);
        dynamite.SetActive(false);
        holywater.SetActive(false);
        key.SetActive(false);
        metal.SetActive(false);
        vial.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        // Check to see if inventory is full
        if (GameManager.Instance.Inventory.Count == 7) {
            GameManager.Instance.hasAllItems = true;
        }

        if (GameManager.Instance.hasAllItems == true) {
            // Turn the light on the altar on
            light.SetActive(true);
            // Make the altar visible
            altar.SetActive(true);
            // set isActive Boolean to true
            playAnimation = true;
        }

        // animate the objects after they have all been placed on the altar
        if (GameManager.Instance.Inventory.Count == 0 && playAnimation == true) {
            itemAnimationController.SetBool("itemsPlaced", true);
            playAnimation = false;
        }
    }
}
