// * Usings Section:
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// * Message on Code Comments in this File:
// ? INSTALL + APPLY the plugin 'Better Comments' by iguissouma to see color-differentiation between comments dependent on header sybmols (*, ?, !, etc.)
// ! This isn't mandatory, just a note about file comments, but it is quick for anyone to add (<1 minute).

// * This script was created and shown to be put on a gameobject that has a stationary collider.
// It could work the other way around, but basically just don't put the collider on the moving object and things will go smooth.
// Thus, its triggered here with this class, and the OpenDoor.cs component has been deactivated on the other gameobject.
// !~ note: your other script, OpenDoor.cs, works just fine and this script ended up needed no altering....
public class DoorColliderScript : MonoBehaviour {

   [SerializeField] 
   private Animator myAnimationController;

   private void OnTriggerEnter(Collider other) {

        Debug.Log("OnTriggerEnter with: " + other);

        if(other.CompareTag("Player")) {

            myAnimationController.SetBool("triggeredOpen", true);

        }

   }

   private void OnTriggerExit(Collider other) {

        Debug.Log("OnTriggerExit with: " + other);

        if(other.CompareTag("Player")) {

            myAnimationController.SetBool("triggeredOpen", false);

        }

   }

}
