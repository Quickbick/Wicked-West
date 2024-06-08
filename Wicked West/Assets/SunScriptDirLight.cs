using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using QFSW.QC;

public class SunScriptDirLight : MonoBehaviour
{
    [Header("How bright do you want the sun to be? (0->1)")]
    [SerializeField] private float desiredIntensity;
    private Light sunLight; // Reference to the Light component attached to this GameObject

    // Start is called before the first frame update
    void Start()
    {
        // Get the Light component attached to this GameObject
        sunLight = GetComponent<Light>();

        // Check if the Light component exists
        if (sunLight == null)
        {
            Debug.LogError("Light component not found on GameObject.");
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Check if GameManager.Instance exists
        if (GameManager.Instance != null)
        {
            // Check if it is nighttime
            if (GameManager.Instance.isNighttime)
            {
                // Set the intensity to the desired intensity (zero)
                sunLight.intensity = 0f;
                ChangeIntensity(0.05f);
                
            }
            else
            {
                // Set the intensity to 0 if it's not nighttime
                sunLight.intensity = desiredIntensity;
                ChangeIntensity(1f);
            }
        }
        else
        {
            // Print an error message if GameManager.Instance is not found
            Debug.LogError("GameManager.Instance not found.");
        }
    }

    // * Function to set the intensity multiplier
    public void SetIntensityMultiplier(float intensityMultiplier)
    {
        // Set the ambient intensity multiplier
        RenderSettings.ambientIntensity = intensityMultiplier;
    }

    // ! DEV COMMAND, too.
    [Command]
    public void ChangeIntensity(float newIntense)
    {
        // Set the intensity multiplier to 1.5
        SetIntensityMultiplier(newIntense);
    }

}
