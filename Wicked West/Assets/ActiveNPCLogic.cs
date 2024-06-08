using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using QFSW.QC;
using PixelCrushers.DialogueSystem;
using System.Linq;

// * Message on Code Comments in this File:
// ? INSTALL + APPLY the plugin 'Better Comments' by iguissouma to see color-differentiation between comments dependent on header sybmols (*, ?, !, etc.)

// ! ⁺˚⋆｡°✩₊ This script is designed to handle the state management of non-player characters (NPCs) or interactive objects, and includes functionality to pair each GameObject with a state and switch between states.

// * For management of the state of a list of GameObjects, allowing them to be in ACTIVE, HIDDEN, or DEAD states.
public class ActiveNPCLogic : MonoBehaviour
{
    // * Defines the possible states for GameObjects.
    public enum GameObjectState {
        ACTIVE,
        HIDDEN,
        DEAD
    }

    public bool cycleBeingNight = false;

    // * Stores pairs of GameObjects and their corresponding states.
    [Header("NPC State Management")]
    [SerializeField] public List<GameObjectStatePair> gameObjectStatePairs;

    // * Represents a pair of a GameObject and its current state.
    [System.Serializable]
    public class GameObjectStatePair {
        [SerializeField] public GameObject gameObject;
        [SerializeField] public GameObjectState state;
    }

    // * Sets the initial states of GameObjects on script start.
    private void Start() {
        SetInitialStates();
    }

    private void Update() {
        // * If detects it turns to night, hide all:
        if (GameManager.Instance.isNighttime != cycleBeingNight) {

            Debug.Log("[D] Go to night cycle detected.");
            cycleBeingNight = true;

            // * Then update all NPCs as hidden:
            foreach (var pair in gameObjectStatePairs) {
                if (pair.state != GameObjectState.DEAD)
                {
                    SetGameObjectState(pair.gameObject, GameObjectState.HIDDEN);
                }
            }

        }

        // * If back to day:
        if (cycleBeingNight == true && !GameManager.Instance.isNighttime) {

            Debug.Log("[D] Back to day cycle detected.");
            cycleBeingNight = false;

            SetInitialStates();
            GameManager.Instance.objComplete = false; // need to reset the objective

        }

    }

    // * Sets the initial states of GameObjects based on the provided list.
    // * This function should be called during the initialization of the script.
    private void SetInitialStates() {
        foreach (var pair in gameObjectStatePairs)
        {
            SetGameObjectState(pair.gameObject, pair.state);
        }
    }

    // ! Changes the state of a GameObject to the specified state.
    // * This function allows dynamic state changes during runtime.
    public void SetGameObjectState(GameObject gameObject, GameObjectState newState) {

        // ? Add additional logic or future possible animations here.

        // * Update the state of the specified GameObject.
        switch (newState) {
            case GameObjectState.ACTIVE:
                ActivateGameObject(gameObject); 
                break;

            case GameObjectState.HIDDEN:
                HideGameObject(gameObject);
                break;

            case GameObjectState.DEAD:
                KillGameObject(gameObject);
                break;
        }

    }

    // * Activates a GameObject, making it visible and interactive.
    private void ActivateGameObject(GameObject gameObject) {
        gameObject.SetActive(true);
    }

    // * Hides a GameObject, making it invisible and non-interactive.
    private void HideGameObject(GameObject gameObject) {
        
        if (gameObject) {

            gameObject.SetActive(false); // ? Will this patch it?

        }

        else {

            Debug.Log("gameObject found to be null when trying to hide NPC, that's fine though I hope.");

        }
        
    }

    // * Kills a GameObject, marking it as inactive or destroyed.
    private void KillGameObject(GameObject gameObject) {

        // * For simplicity, this function deactivates the GameObject.
        gameObject.SetActive(false);
        // ! Future maybe gameObject.Destroy();

    }

    //resolves all the dialogue changes when killing NPCs
    public void KillNPC(int NPCnum){
        switch (NPCnum){
            case 0: //Hunter
                DialogueLua.SetVariable("Hunter_general", " ");
                DialogueLua.SetVariable("Pastor_Hunter", " ");
                //kill NPC after these calls
                gameObjectStatePairs[0].state = GameObjectState.DEAD;
                //set item to active
                ES3.Save("WasHunterDeadF", true); // * Save a field.
                break;
            case 1: //Miner
                DialogueLua.SetVariable("Miner_general", " ");
                gameObjectStatePairs[1].state = GameObjectState.DEAD;
                if (!GameManager.Instance.Inventory.Contains(GameManager.Instance.dynamite.GetComponent<Item>()))
                    GameManager.Instance.dynamite.SetActive(true);
                ES3.Save("WasMinerDeadF", true);
                break;
            case 2: //Salesman
                DialogueLua.SetVariable("Salesman_general", " ");
                DialogueLua.SetVariable("Pastor_Salesman", " ");
                gameObjectStatePairs[2].state = GameObjectState.DEAD;
                if (!GameManager.Instance.Inventory.Contains(GameManager.Instance.vial.GetComponent<Item>()))
                    GameManager.Instance.vial.SetActive(true);
                ES3.Save("WasSalesmanDeadF", true);
                break;
            case 3: //Smith
                DialogueLua.SetVariable("Smith_general", " ");
                DialogueLua.SetVariable("Researcher_Smith", " ");
                gameObjectStatePairs[3].state = GameObjectState.DEAD;
                if (!GameManager.Instance.Inventory.Contains(GameManager.Instance.metal.GetComponent<Item>()))
                    GameManager.Instance.metal.SetActive(true);
                ES3.Save("WasSmithDeadF", true);
                break;
            case 4: //Bartender
                DialogueLua.SetVariable("Bartender_general", " ");
                gameObjectStatePairs[4].state = GameObjectState.DEAD;
                if (!GameManager.Instance.Inventory.Contains(GameManager.Instance.beer.GetComponent<Item>()))
                    GameManager.Instance.beer.SetActive(true);
                ES3.Save("WasBartenderDeadF", true);
                break;
            case 5: //Pastor
                DialogueLua.SetVariable("Pastor_general", " ");
                DialogueLua.SetVariable("Hunter_Pastor", " ");
                gameObjectStatePairs[5].state = GameObjectState.DEAD;
                if (!GameManager.Instance.Inventory.Contains(GameManager.Instance.holywater.GetComponent<Item>()))
                    GameManager.Instance.holywater.SetActive(true);
                ES3.Save("WasPastorDeadF", true);
                break;
            case 6: //Sheriff
                DialogueLua.SetVariable("Sheriff_general", " ");
                DialogueLua.SetVariable("Bartender_Sheriff", " ");
                gameObjectStatePairs[6].state = GameObjectState.DEAD;
                if (!GameManager.Instance.Inventory.Contains(GameManager.Instance.key.GetComponent<Item>()))
                    GameManager.Instance.key.SetActive(true);
                ES3.Save("WasSheriffDeadF", true);
                break;
            case 7: //Researcher
                DialogueLua.SetVariable("Researcher_general", " ");
                DialogueLua.SetVariable("Smith_Researcher", " ");
                gameObjectStatePairs[7].state = GameObjectState.DEAD;
                if (!GameManager.Instance.Inventory.Contains(GameManager.Instance.camera.GetComponent<Item>()))
                    GameManager.Instance.camera.SetActive(true);
                ES3.Save("WasResearcherDeadF", true);
                break;
        }
    }

}
