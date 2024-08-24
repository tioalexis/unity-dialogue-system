using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Cyl.Dialogue
{
    /// <summary>
    /// Receives a <see cref="DialogueAsset"/> and plays it.
    /// </summary>
    public class DialoguePlayer : MonoBehaviour
    {
        [Tooltip("The canvas that displays the dialogue.")]
        [SerializeField] private Canvas dialogueCanvas;
        
        [Tooltip("The text component that displays the name of the speaker.")]
        [SerializeField] private TMP_Text speakerNameText;
        
        [Tooltip("The text component that displays the dialogue.")]
        [SerializeField] private TypewriterText dialogueText;
        
        [Tooltip("GameObject that displays a prompt to continue to the next dialogue beat.")]
        [SerializeField] private GameObject continuePrompt;
        
        /// <summary>
        /// The current dialogue being played.
        /// </summary>
        private DialogueAsset _currentDialogue;
        
        /// <summary>
        /// The current index of the dialogue beat being played.
        /// </summary>
        private int _currentDialogueBeatIndex;
        
        /// <summary>
        /// The handler to check for when the player wants to continue to the next dialogue beat.
        /// </summary>
        public IDialoguePlayerContinueHandler ContinueHandler { get; set; }

        /// <summary>
        /// Begins playing the given <see cref="DialogueAsset"/>.
        /// </summary>
        /// <param name="dialogueAsset">The dialogue to play.</param>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="dialogueAsset"/> is <c>null</c>.</exception>
        public void Play(DialogueAsset dialogueAsset)
        {
            _currentDialogue = dialogueAsset ?? throw new ArgumentNullException(nameof(dialogueAsset));
            dialogueCanvas.enabled = true;
            StartCoroutine(PlayCoroutine());
        }

        /// <summary>
        /// Coroutine that plays the current dialogue.
        /// If the player skips the current dialogue beat, the current dialogue beat is skipped.
        /// </summary>
        /// <returns></returns>
        private IEnumerator PlayCoroutine()
        {
            IReadOnlyList<DialogueBeat> dialogueBeats = _currentDialogue.DialogueBeats;
            int numDialogueBeats = dialogueBeats.Count;
            
            for (int i = 0; i < numDialogueBeats; i++)
            {
                DialogueBeat dialogueBeat = dialogueBeats[i];
                speakerNameText.text = dialogueBeat.SpeakerName;
                dialogueText.Play(dialogueBeat.DialogueText);
                
                continuePrompt.SetActive(false);
                
                while (dialogueText.IsPlaying)
                {
                    if (ContinueHandler?.ContinueDialogueBeat() == true)
                    {
                        dialogueText.Skip();
                    }
                    
                    yield return null;
                }
                
                continuePrompt.SetActive(true);
                
                yield return new WaitUntil(() => ContinueHandler?.ContinueDialogueBeat() == true);
                
                // Wait for an extra frame so that inputs are not double-processed.
                yield return null;
            }

            dialogueCanvas.enabled = false;
        }
    }
}