using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCam : MonoBehaviour
{
    //mouse movement sensitivity
    public float sensX;
    public float sensY;

    //player orientation
    public Transform orientation;

    private float xRotation;
    private float yRotation;
    private float startingY;

    private void Start(){
        //locks cursor to center of screen and makes invisible
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        startingY = transform.parent.rotation.eulerAngles.y;
        yRotation = startingY;
    }

    private void Update(){
        //get mouse input
        float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * sensX;
        float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * sensY;
        
        yRotation += mouseX;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -60f, 60f); //clamps vertical movement to 60 degree angle

        //rotate camera and orientation
        transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
        orientation.rotation = Quaternion.Euler(0, yRotation, 0);
    }
}
