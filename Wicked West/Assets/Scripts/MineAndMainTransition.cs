using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using QFSW.QC;

public class MineAndMainTransition : MonoBehaviour {

    // public GameObject player;
    public bool in_mine;
    public GameManager manager;

    void Start()
    {
        manager = GameManager.Instance;
    }

    private void OnTriggerEnter(Collider other) {

        Debug.Log("OnTriggerEnter with: " + other);

        if(other.CompareTag("Player")) {

            if(in_mine == true) {
                manager.hasEnteredMine = true;
                exitMine();

            }

            if(in_mine == false) {
                enterMine();
            }

        }
    }

    private void enterMine() {
        // load the mine scene
        SceneManager.LoadScene("mine_interior");

    }

    private void exitMine() {
        // load the main scene
        SceneManager.LoadScene("Scene_with_Models");
    }

}