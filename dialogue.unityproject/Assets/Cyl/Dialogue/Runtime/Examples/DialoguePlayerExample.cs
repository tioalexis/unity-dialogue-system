using UnityEngine;

namespace Cyl.Dialogue.Examples
{
    public class DialoguePlayerExample : MonoBehaviour, IDialoguePlayerContinueHandler
    {
        [SerializeField] private DialogueAsset dialogueAsset;
        [SerializeField] private DialoguePlayer dialoguePlayer;
        
        private void Start()
        {
            dialoguePlayer.ContinueHandler = this;
            dialoguePlayer.Play(dialogueAsset);
        }

        public bool ContinueDialogueBeat()
        {
            return Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0) || Input.touchCount > 0;
        }
    }
}