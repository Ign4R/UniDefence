using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AudioOptionManager : MonoBehaviour
{
    public static float MusicVolume { get; set; }
    public static float SoundEffectsVolume { get; set; }

    [SerializeField] private TextMeshProUGUI musicSliderText;
    [SerializeField] private TextMeshProUGUI soundEffectSliderText;

    public void OnMusicSliderValueChange(float value)
    {
        MusicVolume = value;
        musicSliderText.text = ((int)(value * 100)).ToString();
        AudioManager.instance.UpdateMixerVolume();
    }

    public void OnSoundEffectsSliderValueChange(float value)
    {
        SoundEffectsVolume = value;
        soundEffectSliderText.text = ((int)(value * 100)).ToString();
        AudioManager.instance.UpdateMixerVolume();
    }
}
