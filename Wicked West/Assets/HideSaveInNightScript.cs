using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideSaveInNightScript : MonoBehaviour
{

    public GameObject childSaveButton;

    // Start is called before the first frame update
    void Start()
    {
        childSaveButton = gameObject.transform.GetChild(0).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.isNighttime) {

            if (childSaveButton) {

                childSaveButton.SetActive(false);

            }

            
            // childSaveButton.SetActive(false);

        }

        else {

            childSaveButton.SetActive(true);


        }
    }
}
