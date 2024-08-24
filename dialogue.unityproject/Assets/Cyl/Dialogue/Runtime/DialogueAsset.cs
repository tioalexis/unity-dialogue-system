using System.Collections.Generic;
using UnityEngine;

namespace Cyl.Dialogue
{
    /// <summary>
    /// Represents a collection of dialogue beats.
    /// A single dialogue is a collection of dialogue beats that are played in sequence.
    /// </summary>
    [CreateAssetMenu(menuName = "Cyl/Dialogue/Dialogue Asset")]
    public class DialogueAsset : ScriptableObject
    {
        [Tooltip("The dialogue beats that make up this dialogue.")]
        [SerializeField] private DialogueBeat[] dialogueBeats;
        
        /// <summary>
        /// Read-only list of dialogue beats that make up this dialogue.
        /// </summary>
        public IReadOnlyList<DialogueBeat> DialogueBeats => dialogueBeats;
    }
}