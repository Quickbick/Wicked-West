// Copyright (c) Pixel Crushers. All rights reserved.

#if USE_OPENAI

using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace PixelCrushers.DialogueSystem.OpenAIAddon
{

    /// <summary>
    /// Base panel type for panels that use OpenAI's text generation.
    /// </summary>
    public abstract class TextGenerationPanel : BasePanel
    {

        private const float DefaultTemperature = 0.2f;
        private const float DefaultTopP = 1;
        private const float DefaultFrequencyPenalty = 0;
        private const float DefaultPresencePenalty = 0;
        private const int BottomTokenRange = 16;
        private const int DefaultMaxTokens = 1024;

        protected TextModelName ModelName { get; private set; } = TextModelName.GPT3_5_Turbo_16K;
        protected Model Model { get; private set; } = Model.GPT3_5_Turbo_16K;
        protected bool IsChatModel => Model.ModelType == ModelType.Chat;
        protected float Temperature { get; private set; } = DefaultTemperature;
        protected float TopP { get; private set; } = DefaultTopP;
        protected float FrequencyPenalty { get; private set; } = DefaultFrequencyPenalty;
        protected float PresencePenalty { get; private set; } = DefaultPresencePenalty;
        protected int TopTokenRange => (Model != null) ? Model.MaxTokens : DefaultMaxTokens;
        protected int MaxTokens { get; private set; } = DefaultMaxTokens;
        protected string AssistantPrompt { get; set; } = string.Empty;
        protected string ResultText { get; set; } = string.Empty;
        protected List<ChatMessage> Messages { get; private set; } = new List<ChatMessage>();
        protected bool IsDialogueEntry => asset == null;
        protected bool NeedsRepaint { get; set; }

        private GUIContent ModelLabel = new GUIContent("Model", "Text generation model to use.\n- GPT_4: Best for most cases but limited availability and higher cost than GPT_3_5.\n- GPT_4_32K: GPT-4 with larger context window for longer responses, but more expensive.\n- GPT_3_5_Turbo: Best for most cases. Similar performance to Davinci at 10% of the price.\nGPT_3_5_Turbo_16K: Same as GPT_3_5_Turbo but with 4x the context.\n- Davinci: Powerful model but slow. Good at complex intent and cause & effect.\n- Curie: Second most powerful model. Good at language translation.\n- Babbage: Second fastest model. Good at classification.\n- Ada: Smallest, fastest, and least expensive model, but results may be poor.");
        private GUIContent TemperatureLabel = new GUIContent("Temperature", "Randomness, where 0=predictable, 1=very random.");
        private GUIContent TopPLabel = new GUIContent("Top P", "Alternative to sampling with temperature, called nucleus sampling, where the model considers the results of the tokens with top_p probability mass. So 0.1 means only the tokens comprising the top 10% probability mass are considered.\nWe generally recommend altering this or temperature but not both.");
        private GUIContent FrequencyPenaltyLabel = new GUIContent("Frequency Penalty", "Number between -2.0 and 2.0. Positive values penalize new tokens based on their existing frequency in the text so far, decreasing the model's likelihood to repeat the same line verbatim.");
        private GUIContent PresencePenaltyLabel = new GUIContent("Presence Penalty", "Positive values penalize new tokens based on whether they appear in the text so far, increasing the model's likelihood to talk about new topics.");
        private GUIContent MaxTokensLabel = new GUIContent("Max Tokens", "Max tokens to spend on request. Fewer tokens will result in shorter responses.");
        private GUIContent AssistantPromptLabel = new GUIContent("Assistant Prompt", "(Optional) Additional instructions to guide text generation.");

        public TextGenerationPanel(string apiKey, DialogueDatabase database, Asset asset, DialogueEntry entry, Field field)
            : base(apiKey, database, asset, entry, field)
        { }

        public void DrawModelSettings()
        {
            EditorGUI.BeginChangeCheck();
            ModelName = (TextModelName)EditorGUILayout.EnumPopup(ModelLabel, ModelName);
            if (EditorGUI.EndChangeCheck()) Model = OpenAI.NameToModel(ModelName);
            Temperature = EditorGUILayout.Slider(TemperatureLabel, Temperature, 0, 1);
            TopP = EditorGUILayout.Slider(TopPLabel, TopP, 0, 1);
            FrequencyPenalty = EditorGUILayout.Slider(FrequencyPenaltyLabel, FrequencyPenalty, -2, 2);
            PresencePenalty = EditorGUILayout.Slider(PresencePenaltyLabel, PresencePenalty, -2, 2);
            MaxTokens = EditorGUILayout.IntSlider(MaxTokensLabel, MaxTokens, BottomTokenRange, TopTokenRange);
        }

        protected void SetModelByName(TextModelName newModelName)
        {
            ModelName = newModelName;
            Model = OpenAI.NameToModel(newModelName);
        }

        protected virtual void DrawAssistantPrompt()
        {
            AssistantPrompt = EditorGUILayout.TextField(AssistantPromptLabel, AssistantPrompt);
        }

        protected virtual void SetResultText(string text)
        {
            if (LogDebug) Debug.Log($"Received from OpenAI: {text}");
            ResultText = text;
            IsAwaitingReply = false;
            if (NeedsRepaint) Repaint();
            NeedsRepaint = false;
        }

        protected void SubmitPrompt(string userPrompt, string assistantPrompt, string progressTitle = "Contacting OpenAI",
            float progress = 0.5f, bool debug = true, bool repaint = true)
        {
            var prompt = string.IsNullOrEmpty(assistantPrompt) ? userPrompt : $"{userPrompt} {assistantPrompt}";
            if (debug) Debug.Log($"Sending to OpenAI: {prompt}");
            NeedsRepaint = repaint;
            LogDebug = debug;
            try
            {
                IsAwaitingReply = true;
                if (IsChatModel)
                {
                    Messages.Clear();
                    if (!string.IsNullOrEmpty(userPrompt)) Messages.Add(new ChatMessage("user", userPrompt));
                    if (!string.IsNullOrEmpty(assistantPrompt)) Messages.Add(new ChatMessage("assistant", assistantPrompt));
                    OpenAI.SubmitChatAsync(apiKey, Model, 
                        Temperature, TopP,
                        FrequencyPenalty, PresencePenalty,
                        MaxTokens, Messages, SetResultText);
                }
                else
                {
                    OpenAI.SubmitCompletionAsync(apiKey, Model, 
                        Temperature, TopP,
                        FrequencyPenalty, PresencePenalty,
                        MaxTokens, prompt, SetResultText);
                }
            }
            catch (System.Exception)
            {
                IsAwaitingReply = false;
            }
        }

        protected void SubmitEdit(string prompt, string progressTitle = "Revising Text",
            float progress = 0.5f, bool debug = true, bool repaint = true)
        {
            if (debug) Debug.Log($"Sending to OpenAI: {prompt}");
            NeedsRepaint = repaint;
            LogDebug = debug;
            try
            {
                IsAwaitingReply = true;
                if (IsChatModel)
                {
                    Messages.Add(new ChatMessage("user", prompt));
                    OpenAI.SubmitChatAsync(apiKey, Model, 
                        Temperature, TopP,
                        FrequencyPenalty, PresencePenalty,
                        MaxTokens, Messages, SetResultText);
                }
                else
                {
                    OpenAI.SubmitEditAsync(apiKey, Model, 
                        Temperature, TopP,
                        FrequencyPenalty, PresencePenalty,
                        MaxTokens, ResultText, prompt, SetResultText);
                }
            }
            catch (System.Exception)
            {
                IsAwaitingReply = false;
            }
        }

    }
}

#endif
