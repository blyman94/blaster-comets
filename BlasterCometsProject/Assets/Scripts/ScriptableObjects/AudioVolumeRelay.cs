using UnityEngine;
using UnityEngine.Audio;

/// <summary>
/// Relays changes in UI volume sliders to the main audio mixer. Based on the
/// method described by Bodix in the following StackOverflow forum:
/// 
/// https://stackoverflow.com/questions/46529147/how-to-set-a-mixers-volume-to-a-sliders-volume-in-unity
/// </summary>
[CreateAssetMenu]
public class AudioVolumeRelay : ScriptableObject
{
    /// <summary>
    /// The game's main audio mixer.
    /// </summary>
    [Tooltip("The game's main audio mixer.")]
    [SerializeField] private AudioMixer mainAudioMixer;

    /// <summary>
	/// Changes main volume based on slider in game.
	/// </summary>
	/// <param name="newVolume">Float value of new volume.</param>
	public void RelayMainVolumeChange(float newVolume)
    {
        mainAudioMixer.SetFloat("mainVol", Mathf.Log10(newVolume) * 20);
    }

    /// <summary>
    /// Changes music volume based on slider in game.
    /// </summary>
    /// <param name="newVolume">Float value of new volume.</param>
    public void RelayMusicVolumeChange(float newVolume)
    {
        mainAudioMixer.SetFloat("musicVol", Mathf.Log10(newVolume) * 20);
    }

    /// <summary>
    /// Changes SFX volume based on slider in game.
    /// </summary>
    /// <param name="newVolume">Float value of new volume.</param>
    public void RelaySFXVolumeChange(float newVolume)
    {
        mainAudioMixer.SetFloat("sfxVol", Mathf.Log10(newVolume) * 20);
    }
}
