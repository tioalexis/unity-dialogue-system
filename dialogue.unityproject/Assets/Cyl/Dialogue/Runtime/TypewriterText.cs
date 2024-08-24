using System.Collections;
using TMPro;
using UnityEngine;

namespace Cyl.Dialogue
{
    /// <summary>
    /// Simple typewriter effect for TextMeshPro text.
    /// Each character of the text is revealed one by one at a constant rate.
    /// The speed of the effect can be sped up by calling the Skip method.
    /// </summary>
    [RequireComponent(typeof(TMP_Text))]
    public class TypewriterText : MonoBehaviour
    {
        [Tooltip("The time in seconds between each character being revealed.")]
        [SerializeField] private float realtimeSecondsPerCharacter = 0.1f;
        
        [Tooltip("When the effect is sped up, the time between characters is divided by this factor.")]
        [SerializeField] private float speedUpFactor = 2f;

        [Tooltip("Whether the effect should start playing when the object is enabled.")]
        [SerializeField] private bool playOnStart;
        
        /// <summary>
        /// The text component that the typewriter effect is applied to.
        /// </summary>
        private TMP_Text _text;
        
        /// <summary>
        /// The currently active coroutine that is revealing the text.
        /// </summary>
        private Coroutine _activeCoroutine;
        
        /// <summary>
        /// The WaitForSecondsRealtime object that is used to wait between characters.
        /// We cache this object to avoid creating a new one every time we need to wait.
        /// </summary>
        private WaitForSecondsRealtime _delayBetweenCharacters;
        
        /// <summary>
        /// The WaitForSecondsRealtime object that is used to wait between characters when the effect is sped up.
        /// We cache this object to avoid creating a new one every time we need to wait.
        /// </summary>
        private WaitForSecondsRealtime _delayBetweenCharactesSpedUp;
        
        /// <summary>
        /// Whether the effect is currently sped up.
        /// </summary>
        private bool _spedUp;
        
        /// <summary>
        /// Whether the effect is currently playing.
        /// </summary>
        public bool IsPlaying => _activeCoroutine != null;
        
        /// <summary>
        /// Initializes the typewriter effect.
        /// </summary>
        private void Awake()
        {
            _text = GetComponent<TMP_Text>();
            _delayBetweenCharacters = new WaitForSecondsRealtime(realtimeSecondsPerCharacter);
            _delayBetweenCharactesSpedUp = new WaitForSecondsRealtime(realtimeSecondsPerCharacter / speedUpFactor);
        }

        /// <summary>
        /// Starts the typewriter effect if the playOnStart flag is set.
        /// </summary>
        private void Start()
        {
            if (playOnStart)
            {
                Play();
            }
        }

        /// <summary>
        /// Plays the typewriter effect on the given text.
        /// </summary>
        /// <param name="text">The text to apply the typewriter effect to.</param>
        public void Play(string text)
        {
            _text.text = text;
            
            Play();
        }
        
        /// <summary>
        /// Plays the typewriter effect on the text of the attached TextMeshPro component.
        /// </summary>
        public void Play()
        {
            if (_activeCoroutine != null)
            {
                StopCoroutine(_activeCoroutine);
                _activeCoroutine = null;
            }
            
            _spedUp = false;
            _activeCoroutine = StartCoroutine(PlayCoroutine());
        }

        /// <summary>
        /// Speeds up the typewriter effect.
        /// </summary>
        public void Skip()
        {
            _spedUp = true;
        }

        /// <summary>
        /// Coroutine that reveals the text one character at a time.
        /// </summary>
        private IEnumerator PlayCoroutine()
        {
            int totalVisibleCharacters = _text.text.Length;
            _text.maxVisibleCharacters = 0;
            
            for (int i = 0; i < totalVisibleCharacters; i++)
            {
                _text.maxVisibleCharacters = i + 1;

                if (!_spedUp)
                {
                    yield return _delayBetweenCharacters;
                }
                else
                {
                    yield return _delayBetweenCharactesSpedUp;
                }
            }

            _activeCoroutine = null;
        }
    }
}