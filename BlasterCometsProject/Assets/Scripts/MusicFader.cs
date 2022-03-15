using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Fades different tracks as the player's score increases to make the game 
/// sound progressively more epic.
/// </summary>
public class MusicFader : MonoBehaviour
{
    /// <summary>
    /// The game's settings.
    /// </summary>
    [Tooltip("The game's settings.")]
    [SerializeField] private Settings settings;

    /// <summary>
    /// AudioSources containing the tracks to be faded.
    /// </summary>
    [Header("General")]
    [Tooltip("AudioSources containing the tracks to be faded.")]
    [SerializeField] private List<AudioSource> audioSources;

    /// <summary>
    /// IntVariable representing the player's current score.
    /// </summary>
    [Tooltip("IntVariable representing the player's current score.")]
    [SerializeField] private IntVariable playerScore;

    /// <summary>
    /// Current audiosource being played fully.
    /// </summary>
    private int currentAudioIndex = 0;

    /// <summary>
    /// Score the player must reach to fade in next level of music.
    /// </summary>
    private int targetScore;

    /// <summary>
    /// Timer for music fadeout.
    /// </summary>
    private float fadeOutTimer = 0;

    /// <summary>
    /// Elapsed time for music fadeout.
    /// </summary>
    private float elapsedTime = 0;

    #region MonoBehaviour Methods
    private void OnEnable()
    {
        playerScore.Updated += UpdateMusicFade;
    }
    private void Start()
    {
        targetScore = settings.GameParameters.PointsPerNewSong;
    }
    private void Update()
    {
        FadeInOriginalSource();
    }
    private void OnDisable()
    {
        playerScore.Updated -= UpdateMusicFade;
    }
    #endregion

    /// <summary>
    /// Begins fading in the original track.
    /// </summary>
    public void Reset()
    {
        fadeOutTimer = settings.GameParameters.MusicResetTime;
        elapsedTime = 0;
    }

    /// <summary>
    /// Fades in the original audio source while fading out all additional 
    /// audio sources.
    /// </summary>
    private void FadeInOriginalSource()
    {
        fadeOutTimer -= Time.deltaTime;
        if (fadeOutTimer > 0)
        {
            elapsedTime += Time.deltaTime;
            float test = 1 - 
                (elapsedTime / settings.GameParameters.MusicResetTime);
            for (int i = 1; i < audioSources.Count; i++)
            {
                if (audioSources[i].volume >= test)
                {
                    audioSources[i].volume = Mathf.Lerp(1, 0, 
                        elapsedTime / settings.GameParameters.MusicResetTime);
                }
            }

            float test2 = elapsedTime / settings.GameParameters.MusicResetTime;
            if (audioSources[0].volume <= test2)
            {
                audioSources[0].volume = Mathf.Lerp(0, 1, 
                    elapsedTime / settings.GameParameters.MusicResetTime);
            }
        }
    }

    /// <summary>
    /// Updates the music mix levels based on the player's current score.
    /// </summary>
    private void UpdateMusicFade()
    {
        if (currentAudioIndex < audioSources.Count - 1)
        {
            if (currentAudioIndex == 0)
            {
                audioSources[currentAudioIndex + 1].volume =
                    (float)playerScore.Value /
                    settings.GameParameters.PointsPerNewSong;
                if (playerScore.Value >= targetScore)
                {
                    currentAudioIndex += 1;
                    SetNextTargetScore();
                }
            }
            if (currentAudioIndex >= 1)
            {
                float adjustment =
                    playerScore.Value -
                    (settings.GameParameters.PointsPerNewSong *
                    currentAudioIndex);

                audioSources[currentAudioIndex - 1].volume =
                    1 - (adjustment / settings.GameParameters.PointsPerNewSong);
                audioSources[currentAudioIndex + 1].volume =
                    adjustment / settings.GameParameters.PointsPerNewSong;

                if (playerScore.Value >= targetScore)
                {
                    currentAudioIndex += 1;
                    SetNextTargetScore();
                }
            }
        }
    }

    /// <summary>
    /// Increments the next target score at which the next track will be at 
    /// full volume.
    /// </summary>
    private void SetNextTargetScore()
    {
        if (playerScore.Value > targetScore)
        {
            targetScore += settings.GameParameters.PointsPerNewSong;
        }
    }
}
