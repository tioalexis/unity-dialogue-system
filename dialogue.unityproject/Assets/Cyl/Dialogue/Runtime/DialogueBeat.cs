using System;
using UnityEngine;

namespace Cyl.Dialogue
{
    /// <summary>
    /// Represents a single beat of dialogue.
    /// A beat is a single line of dialogue spoken by a single speaker.
    /// </summary>
    [Serializable]
    public struct DialogueBeat
    {
        [Tooltip("The name of the speaker.")]
        [SerializeField] private string speakerName;
        
        [Tooltip("The text of the dialogue.")]
        [SerializeField, TextArea] private string dialogueText;
        
        /// <summary>
        /// The name of the speaker.
        /// </summary>
        public string SpeakerName => speakerName;
        
        /// <summary>
        /// The text of the dialogue.
        /// </summary>
        public string DialogueText => dialogueText;
    }
}