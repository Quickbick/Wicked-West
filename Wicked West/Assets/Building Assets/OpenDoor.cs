using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoor : MonoBehaviour
{
   [SerializeField] private Animator myAnimationController;

   private void OnTriggerEnter(Collider other) {

        if(other.CompareTag("Player")) {
            myAnimationController.SetBool("triggeredOpen", true);
        }
   }

   private void OnTriggerExit(Collider other) {

        if(other.CompareTag("Player")) {
            myAnimationController.SetBool("triggeredOpen", false);
        }
   }

}
