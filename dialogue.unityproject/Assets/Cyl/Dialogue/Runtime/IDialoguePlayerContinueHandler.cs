namespace Cyl.Dialogue
{
    /// <summary>
    /// Interface for handling dialogue player continue events.
    /// Since each game may have different requirements for when dialogue should continue,
    /// we use this interface to allow for custom implementations.
    /// </summary>
    public interface IDialoguePlayerContinueHandler
    {
        /// <summary>
        /// Whether the dialogue should continue.
        /// If the current dialogue beat is still playing, the dialogue beat will be sped up.
        /// Otherwise, the next dialogue beat will be played.
        /// </summary>
        /// <returns>True if the dialogue should continue; false otherwise.</returns>
        public bool ContinueDialogueBeat();
    }
}