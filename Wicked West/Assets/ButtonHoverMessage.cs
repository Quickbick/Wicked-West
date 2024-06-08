using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonHoverMessage : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] public float messageDisplayTime = 3f; // Adjust this to change how long the message displays

    public void OnPointerEnter(PointerEventData eventData)
    {
        // Display the message when the mouse enters the button
        MessageManager.Instance.DisplayMessage("Enter a custom API Token from your OpenAI Account if you own one already", messageDisplayTime);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        // You can optionally implement logic for when the mouse exits the button
    }
}
