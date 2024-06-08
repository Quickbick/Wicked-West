using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MessageManager : MonoBehaviour
{
    public static MessageManager Instance; // Singleton instance

    public GameObject messagePrefab; // Prefab of the TextMeshPro object
    public Canvas canvas; // Reference to the Canvas

    private void Awake()
    {
        // Ensure only one instance of MessageManager exists
        if (Instance == null)
        {
            Instance = this;
            // DontDestroyOnLoad(gameObject); //!Trying to adjust to keep interscene transitions working.
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Function to display a message on the screen
    public void DisplayMessage(string message, float displayTime)
    {
        // Create a new instance of the message prefab
        GameObject newMessage = Instantiate(messagePrefab, canvas.transform);

        // Set the text of the TextMeshPro object
        newMessage.GetComponent<TextMeshProUGUI>().text = message;

        // Start a coroutine to handle message display time and fade out
        StartCoroutine(DisplayMessageCoroutine(newMessage, displayTime));
    }

    // Coroutine to handle message display time and fade out
    private IEnumerator DisplayMessageCoroutine(GameObject messageObject, float displayTime)
    {
        // Wait for the specified display time
        yield return new WaitForSeconds(displayTime);

        // Fade out the message
        TextMeshProUGUI textMesh = messageObject.GetComponent<TextMeshProUGUI>();
        float fadeDuration = 1f;
        float timer = 0f;
        Color initialColor = textMesh.color;

        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            float alpha = Mathf.Lerp(1f, 0f, timer / fadeDuration);
            textMesh.color = new Color(initialColor.r, initialColor.g, initialColor.b, alpha);
            yield return null;
        }

        // Destroy the message object after fading out
        Destroy(messageObject);
    }
}
