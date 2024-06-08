using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine;
using TMPro; // Import the TextMeshPro namespace

public class UIDisplay : MonoBehaviour {

    public TextMeshProUGUI daysText; // Reference to the TextMeshProUGUI element in the Inspector

    // * Update UI Display of day.
    private void Update() {

        if (!GameManager.Instance._isDevMode) {

            // Update the TextMeshProUGUI element with the 'days' value from the GameManager
            if (GameManager.Instance.isNighttime) {

                daysText.text = "Night: " + GameManager.Instance.nights.ToString();

            }

            else {

                daysText.text = "Day: " + GameManager.Instance.days.ToString();

            }
            

        }

        else {

            // Update the TextMeshProUGUI element with the 'days' value from the GameManager in // ! dev mode.
            if (GameManager.Instance.isNighttime) {

                daysText.text = "[DEV] Night: " + GameManager.Instance.nights.ToString();

            }

            else {

                daysText.text = "[DEV] Day: " + GameManager.Instance.days.ToString();

            }

        }

    }


}
