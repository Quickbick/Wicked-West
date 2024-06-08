// * Usings:
// These using directives specify the namespaces containing necessary classes and functionality.
using System.Collections;         // Namespace for basic collection types.
using System.Collections.Generic; // Namespace for more advanced collection types.
using UnityEngine;                // Namespace for Unity engine functionality.
using UnityEngine.UI;             // Namespace for Unity UI components.

// * Message on Code Comments in this File:
// ? INSTALL + APPLY the plugin 'Better Comments' by iguissouma to see color-differentiation between comments dependent on header symbols (*, ?, !, etc.)

public class ClickablePanel : MonoBehaviour
{
    // * Private field to hold a reference to the Button component.
    private Button panelButton;

    private void Start()
    {
        // * Find the Button component in the children of the Panel.
        panelButton = GetComponentInChildren<Button>();

        if (panelButton != null)
        {
            // Attach a click event listener to the Button component.
            panelButton.onClick.AddListener(OnPanelClick);
        }
        else
        {
            // Log an error message if no Button component is found in the children of the Panel.
            Debug.LogError("No Button component found in children of the Panel.");
        }
    }

    private void OnPanelClick()
    {
        // This function will be called when the Panel is clicked.
        // Log a message to the Unity console.
        Debug.Log("Panel Clicked! You can replace this message with your own logic.");
    }
}
