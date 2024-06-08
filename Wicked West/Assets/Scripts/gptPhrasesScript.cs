using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gptPhrasesScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // * CODES STUFFS !
    // ....
    
}

// ! This class is not needed, delete whenever wanted.
// * Pasted below is code from the demo of NpcFriend scene, which won't run here because of inspector-
// * variables needed, but can be used to see an example if wanted. remove this whenever we move on.

/*

// * Usings:
using System.Collections;
using System.Collections.Generic;
using BitSplash.AI.GPT;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

// ** Asset namespace:
namespace BitSplash.AI.GPT.Extras
{
    public class SetupConversationScript : MonoBehaviour
    {
        public TMP_InputField QuestionField;
        public TMP_Text AnswerField;
        public Button SubmitButton;

        public string NpcDirection = "Answer as a member of a Western Town.";
        public string[] Facts;
        public bool TrackConversation = false;
        public int MaximumTokens = 500;
        [Range(0f, 1f)]
        public float Temperature = 0f;
        ChatGPTConversation Conversation;

        void Start()
        {
            SetUpConversation();
        }

        void SetUpConversation()
        {
            Conversation = ChatGPTConversation.Start(this)
                .MaximumLength(MaximumTokens)
                .SaveHistory(TrackConversation)
                .System(string.Join("\n", Facts) + "\n" + NpcDirection);
            Conversation.Temperature = Temperature;
        }

        public void SendClicked()
        {
            Conversation.Say(QuestionField.text);
            SubmitButton.interactable = false;
        }

        void OnConversationResponse(string text)
        {
            AnswerField.text = text;
            SubmitButton.interactable = true;
        }

        void OnConversationError(string text)
        {
            Debug.Log("Error : " + text);
            Conversation.RestartConversation();
            AnswerField.text = "Sorry , I Got confused for a moment, what did you just ask ?";
            SubmitButton.interactable = true;
        }

        private void OnValidate()
        {
            SetUpConversation();
        }

    }

}

*/