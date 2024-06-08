using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinePosition : MonoBehaviour
{
    public GameManager manager;
    public GameObject player;

    [SerializeField] public GameObject bedObject;
    [SerializeField] public float standStillTime = 4.5f;
    [SerializeField] public bool isThisMineScene;

    // Start is called before the first frame update
    void Start()
    {
        // get game manager object
        manager = GameManager.Instance;
    }

    // Update is called once per frame
    void Update()
    {
        // if the player has entered the mine, they should be transported to the mine entrance of the main scene upon exit from the mine scene
        if (manager.hasEnteredMine == true && (!isThisMineScene)) {

            Debug.Log("[J] Trying to tele player to mine exit");

            // move the player to the mine entrance upon scene load
            transform.position = new Vector3(-222, 2, -366);
            // debug
            Debug.Log("Exited mine: position moved");
            // change global variable back to false
            manager.hasEnteredMine = false;
            
            // * SKYBOX PATCH ATTEMPT:
            if (GameManager.Instance.isNighttime) {

                GameManager.Instance.transform.Find("Timer").GetComponent<DayCycleIncrementer>().ChangeSkyboxNEWtoNight();
                Debug.Log("Attempted mine-exit override skybox change to night");

            }
            
            else {

                GameManager.Instance.transform.Find("Timer").GetComponent<DayCycleIncrementer>().ChangeSkyboxNEWtoDay();
                Debug.Log("Attempted mine-exit override skybox change to night");

            }

        }

        if ((isThisMineScene)) { // ? Trying...
            
            // * SKYBOX PATCH ATTEMPT:
            if (GameManager.Instance.isNighttime) {

                GameManager.Instance.transform.Find("Timer").GetComponent<DayCycleIncrementer>().ChangeSkyboxNEWtoNight();
                Debug.Log("Attempted mine-exit override skybox change to night");

            }
            
            else {

                GameManager.Instance.transform.Find("Timer").GetComponent<DayCycleIncrementer>().ChangeSkyboxNEWtoDay();
                Debug.Log("Attempted mine-exit override skybox change to night");

            }

        }

    }

    // * Public for bed, sorry megan I'm hijacking quickly
    public void PlayerFallenSleepAction() {

        Debug.Log("Hmmm........ Was this hit?");

        if (bedObject) {

            // ! Didn't work: transform.position = new Vector3(-243.1f, 3.6f, -170.89f); // The position of where the bed/bedroom should go and where the player will stand can go here.
            // ! DIDNT WORK EITHER: transform.position = bedObject.transform.position; // * does.
            // * Works sometimes: transform.position = new Vector3(-243.1f, 3.6f, -170.89f); // The position of where the bed/bedroom should go and where the player will stand can go here.
            transform.position = new Vector3(bedObject.transform.position.x, (bedObject.transform.position.y + .2f), bedObject.transform.position.z); // ? Work sometimes.
            Debug.Log("SHOULD HAVE MOVED....");

        }

        else {

            Debug.LogError("Problem falling asleep - BED OBJECT IS NULL");

        }

        // if (transform.position != )
        StartCoroutine(UpdateHalting(standStillTime)); // ! Stand still for 4.5 seconds

    }

    public void PlayerMadeMorningAction() {

        if (bedObject) {

            transform.position = new Vector3(bedObject.transform.position.x, (bedObject.transform.position.y + .2f), bedObject.transform.position.z); // ? Work sometimes.
            Debug.Log("Should be in bed now.");

        }

        else {

            Debug.LogError("Problem falling asleep - BED OBJECT IS NULL");

        }

        StartCoroutine(UpdateHalting(standStillTime)); // ! Stand still

    }

    private IEnumerator UpdateHalting(float ftime) {

        yield return new WaitForSeconds(ftime);

        Debug.Log("should be called -> gameObject.GetComponent<PlayerMovement>().haltMovement = false;");
        gameObject.GetComponent<PlayerMovement>().haltMovement = false; // * Free player to move again.
        Debug.Log(gameObject.GetComponent<PlayerMovement>().haltMovement);

        yield return null; // Yielding null to update every frame

    }

}
