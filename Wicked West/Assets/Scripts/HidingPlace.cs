using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HidingPlace : MonoBehaviour
{
    public GameObject hidingCamera;
    public GameObject playerCamera;

    private void Start(){
        hidingCamera.SetActive(false);
    }

    public void SwitchToCamera(){
        playerCamera.SetActive(false);
        hidingCamera.SetActive(true);
    }

    public void SwitchOffCamera(){
        playerCamera.SetActive(true);
        hidingCamera.SetActive(false);
    }
}
