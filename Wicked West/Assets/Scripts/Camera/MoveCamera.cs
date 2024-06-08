using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
    Attaching a camera directly to a player object is notedly buggy in Unity, so this was the recomended method online for fixing that issue.
*/

public class MoveCamera : MonoBehaviour
{
    
    public Transform cameraPosition;

    // Update is called once per frame
    private void Update()
    {
        transform.position = cameraPosition.position; 
    }
}
