using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerOpenMenu : MonoBehaviour
{
    public GameObject gamePauseCanvasObject;
    public GameObject gameInventoryCanvasObject;
    public GameObject gameJournalCanvasObject;

    private bool InventoryOpen;
    private bool JournalOpen;
    private GameObject exit;

    // Start is called before the first frame update
    void Start()
    {
        /*//canvases active at start so they can be found
        gamePauseCanvasObject = GameObject.Find("Panel");
        gameInventoryCanvasObject = GameObject.Find("Inventory");

        //canvases deactivated bc they should not be open
        gamePauseCanvasObject.SetActive(false);
        gamePauseCanvasObject.SetActive(false);*/
    }

    // Update is called once per frame
    void Update()
    {
        //pause menu management
        if (Input.GetKeyDown(KeyCode.Escape)) {
            //checks for already open menus
            if (GameManager.Instance.isPaused) { 
                this.Unpause();
            }
            //when no open menus
            else {
                this.Pause();
            }
        }
        //inventory menu management
        else if (Input.GetKeyDown(KeyCode.I)){
            //checks for already open menus
            if (GameManager.Instance.isPaused && InventoryOpen) { 
                gameInventoryCanvasObject.SetActive(false);
                GameManager.Instance.Pause();
                this.InventoryOpen = false;
            }
            //when no open menus
            else if (!GameManager.Instance.isPaused) {
                gameInventoryCanvasObject.SetActive(true);
                GameManager.Instance.Pause();
                this.InventoryOpen = true;
                GameManager.Instance.InventoryPressedFromManager(gameInventoryCanvasObject);
            }
        }
        //journal menu management
        else if(Input.GetKeyDown(KeyCode.J)){
            //checks for already open menus
            if (GameManager.Instance.isPaused && JournalOpen) {
                //Close Journal
                gameJournalCanvasObject.SetActive(false);
                GameManager.Instance.Pause();
                this.JournalOpen = false;
            }
            //when no open menus
            else if (!GameManager.Instance.isPaused){
                //Open Journal
                gameJournalCanvasObject.SetActive(true);
                GameManager.Instance.Pause();
                this.JournalOpen = true;
            }
        }

    }

    //made seperate functions to accomodate UI button

    //opens pause menu and pauses game
    public void Pause(){
        gamePauseCanvasObject.SetActive(true);
        GameManager.Instance.Pause();
    }

    //closes pause menu or any other open menus
    public void Unpause(){
        gamePauseCanvasObject.SetActive(false);
        GameManager.Instance.Pause();
        //Close Other Things
        gameInventoryCanvasObject.SetActive(false);
        this.InventoryOpen = false;
        gameJournalCanvasObject.SetActive(false);
        this.JournalOpen = false;
        exit = GameObject.Find("Exit Button");
        if (exit){
            exit.GetComponent<Button>().onClick.Invoke();
            GameManager.Instance.Pause();
            exit = null;
        }
    }

}
