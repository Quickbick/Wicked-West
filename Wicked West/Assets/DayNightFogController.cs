using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayNightFogController : MonoBehaviour
{

    public GameObject nightFog;
    public GameObject dayFog;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameManager.Instance.isNighttime)
        {           
            if (!GameManager.Instance.inBuilding) {
                // Update fog so the correct particle effect is displayed for day
                dayFog.SetActive(true);
                nightFog.SetActive(false);
            }
        }
        else
        {
            if (!GameManager.Instance.inBuilding) {
                // Update fog so the correct particle effect is displayed for night
                nightFog.SetActive(true);
                dayFog.SetActive(false);
            }
        }
    }
}
