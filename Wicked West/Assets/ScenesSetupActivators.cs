using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScenesSetupActivators : MonoBehaviour
{
    [Header("Awake Scene")]
    [SerializeField] private string currentScene;

    // * Awake because activator for GMI scene handling ensurance of (G)UI elements.
    void Awake() {



    }

}
