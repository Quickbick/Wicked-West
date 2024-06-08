using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BorderErrorMessage : MonoBehaviour
{
    public GameObject gameBoundaryMessage;

    private void OnTriggerEnter(Collider other) {

        //Debug.Log("You're stuck in this town");
        gameBoundaryMessage.SetActive(true);
    }

    private void OnTriggerExit(Collider other) {

        //Debug.Log("Back in town");
        gameBoundaryMessage.SetActive(false);
    }
}
