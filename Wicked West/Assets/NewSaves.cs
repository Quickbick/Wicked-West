using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PixelCrushers;
using QFSW.QC;

// * Implementation of using the asset Easy Save 3 from a script. It can also be used from PlayMaker.
// Assembly definitions at bottom of page: [https://docs.moodkie.com/easy-save-3/getting-started/]

public class NewSaves : MonoBehaviour {

    [Header("Instances (probably won't need to touch)")]
    [SerializeField] public GameManager gm; // GameManager a special case since an instance. If saving instance stuff, follow this example.

    [Header("GameObjects (add to as needed)")]
    [SerializeField] public GameObject npcStateHandlerGO; // Grab the GameObject from inspector.
    private ActiveNPCLogic activeNPCLogicScript; // Will hold the gameobject's specific script. Really just kept private to keep from appearing in inspector.
    

    // todo: HI ALL- ADD YOUR GAMEOBJECTS, SCRIPTS REFERENCE PAIRINGS HERE, LIKE ABOVE:
    // ** ....
    // ** ....



    private void Start() {
        gm = GameManager.Instance; // GameManager unique case b/c instance.

        if (npcStateHandlerGO) { // Validate that the GameObject is not null, then assign the script.
            activeNPCLogicScript = npcStateHandlerGO.GetComponent<ActiveNPCLogic>();
        }

        // todo: ADD VALIDATIONS FOR GAMEOBJECT EXISTING AND SCRIPT PAIRING HERE, LIKE ABOVE:
        // ** ....
        // ** ....

        if (ES3.KeyExists("GameSavedLoadingSequence") && (gm.justLoggedOn)) {
            bool canStart = (ES3.Load<bool>("GameSavedLoadingSequence")); // Specify primitive type in loads
            if (canStart != false) {
                
                LoadGMStats();
                LoadDeadNPCs(); // Re-kills NPCs helper function.
                LoadItems();
                
                // todo: CALL ALL LOAD FUNCTION HELPERS CREATED HERE, LIKE ABOVE:
                // ** ....
                // ** ....

                // ! ----------------------------------------
                // ! Keep this part here after ALL load calls:
                // ES3.Save("GameSavedLoadingSequence", false);
                gm.justLoggedOn = false;
                // ! ----------------------------------------

            }

            if (gm.justLoggedOn) {

                gm.justLoggedOn = false;

            }
        }


    }

    private void Update() {
        
    }

    public void LoadGMStats() {
        // Nate, I think you can grab inventory loads from gm like this.
        gm.days = ES3.Load<int>("WhatIsCurrentDay");
        gm.nights = ES3.Load<int>("WhatIsCurrentNight");
        gm.Health = ES3.Load<float>("WhatIsCurrentHealth");
        
        // todo: Nate, you can add inventory here since inv is in GameManager instnace, or make your own helper.
        // ?? ....
        // ?? ....
    }

    public void LoadDeadNPCs() {

        Debug.Log("SCRIPT FOUND?" + activeNPCLogicScript);
        // Load primitive variables. These should be saved specifically in the other scripts where triggers happen.
        // In the other script, use [ES3.Save("WasHunterDead", true);], renaming your key to something specific and using primitive types.
        if (ES3.KeyExists("WasHunterDead")) {
            if (ES3.Load<bool>("WasHunterDead") == true) {
                activeNPCLogicScript.KillNPC(0);
            }
        }
        if (ES3.KeyExists("WasMinerDead")) {
            if (ES3.Load<bool>("WasMinerDead") == true) {
                activeNPCLogicScript.KillNPC(1);
            }
        }
        if (ES3.KeyExists("WasSalesmanDead")) {
            if (ES3.Load<bool>("WasSalesmanDead") == true) {
                activeNPCLogicScript.KillNPC(2);
            }
        }
        if (ES3.KeyExists("WasSmithDead")) {
            if (ES3.Load<bool>("WasSmithDead") == true) {
                activeNPCLogicScript.KillNPC(3);
            }
        }
        if (ES3.KeyExists("WasBartenderDead")) {
            if (ES3.Load<bool>("WasBartenderDead") == true) {
                activeNPCLogicScript.KillNPC(4);
            }
        }
        if (ES3.KeyExists("WasPastorDead")) {
            if (ES3.Load<bool>("WasPastorDead") == true) {
                activeNPCLogicScript.KillNPC(5);
            }
        }
        if (ES3.KeyExists("WasSheriffDead")) {
            if (ES3.Load<bool>("WasSheriffDead") == true) {
                activeNPCLogicScript.KillNPC(6);
            }
        }
        if (ES3.KeyExists("WasResearcherDead")) {
            if (ES3.Load<bool>("WasResearcherDead") == true) {
                activeNPCLogicScript.KillNPC(7);
            }
        }
    }

    public void LoadItems() {


        // Load primitive variables. These should be saved specifically in the other scripts where triggers happen.
        // In the other script, use [ES3.Save("WasHunterDead", true);], renaming your key to something specific and using primitive types.
        if (ES3.KeyExists("WasFound0")) {
            if (ES3.Load<bool>("WasFound0") == true) {
                GameManager.Instance.GiveItemSolo(GameManager.Instance.beer);
            }
        }
        if (ES3.KeyExists("WasFound1")) {
            if (ES3.Load<bool>("WasFound1") == true) {
                GameManager.Instance.GiveItemSolo(GameManager.Instance.camera);
            }
        }
        if (ES3.KeyExists("WasFound2")) {
            if (ES3.Load<bool>("WasFound2") == true) {
                GameManager.Instance.GiveItemSolo(GameManager.Instance.dynamite);
            }
        }
        if (ES3.KeyExists("WasFound3")) {
            if (ES3.Load<bool>("WasFound3") == true) {
                GameManager.Instance.GiveItemSolo(GameManager.Instance.holywater);
            }
        }
        if (ES3.KeyExists("WasFound4")) {
            if (ES3.Load<bool>("WasFound4") == true) {
                GameManager.Instance.GiveItemSolo(GameManager.Instance.key);
            }
        }
        if (ES3.KeyExists("WasFound5")) {
            if (ES3.Load<bool>("WasFound5") == true) {
                GameManager.Instance.GiveItemSolo(GameManager.Instance.metal);
            }
        }
        if (ES3.KeyExists("WasFound6")) {
            if (ES3.Load<bool>("WasFound6") == true) {
                GameManager.Instance.GiveItemSolo(GameManager.Instance.vial);
            }
        }
        /* FOR EITHER SLOT IN FUTURE
        if (ES3.KeyExists("WasFound7")) {
            if (ES3.Load<bool>("WasFound7") == true) {
                GiveItemSolo(GameManager.Instance.beer);
            }
        }
        */
    }
}
