using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

// * UI Class so that instance  of gamemanager does not rely on text display inspector assigns.
public class ClocksUIActivators : MonoBehaviour
{

    [Header("Day Cycle")]
    [SerializeField] private DayCycleIncrementer dayCycleIncrementer;
    [SerializeField] private TextMeshProUGUI dayClockText;

    [Header("Night Cycle")]
    [SerializeField] private NightCycleIncrementer nightCycleIncrementer;
    [SerializeField] private TextMeshProUGUI nightClockText;


    // * Was Start(), but now Awake... hoping to catch others using this method in activators going forwards.
    private void Awake()
    {

        // ? Hopefully this could work?:
        dayCycleIncrementer = GameManager.Instance.transform.Find("Timer").GetComponent<DayCycleIncrementer>();
        nightCycleIncrementer = GameManager.Instance.transform.Find("NightTimer").GetComponent<NightCycleIncrementer>();

        // ! Check that the references are assigned in the inspector first
        if (dayCycleIncrementer == null || dayClockText == null ||
            nightCycleIncrementer == null || nightClockText == null)
        {
            Debug.LogError("ClocksUIActivators: References not assigned properly. Please check the inspector.");
            return;
        }

        // * Assign clock text references to day and night cycle scripts
        dayCycleIncrementer.SetClockText(dayClockText);
        nightCycleIncrementer.SetClockText(nightClockText);

    }

    private void Update() {

        if (GameManager.Instance.isNighttime) {

            // GameManager.Instance.transform.Find("NightTimer").gameObject.SetActive(true);
            // GameManager.Instance.transform.Find("Timer").gameObject.SetActive(false);
            dayClockText.gameObject.SetActive(false);
            nightClockText.gameObject.SetActive(true);
            // Debug.Log("night detect");

        }

        else {

            // GameManager.Instance.transform.Find("NightTimer").gameObject.SetActive(false);
            // GameManager.Instance.transform.Find("Timer").gameObject.SetActive(true);
            // Debug.Log("day detect");
            dayClockText.gameObject.SetActive(true);
            nightClockText.gameObject.SetActive(false);

        }

    }

}
