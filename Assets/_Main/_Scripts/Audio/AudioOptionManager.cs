using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class AudioOptionManager : MonoBehaviour
{
    public static AudioOptionManager instance;

    public static float MusicVolume { get; set; }
    public static float SoundEffectsVolume { get; set; }

    [SerializeField] private TextMeshProUGUI musicSliderText;
    [SerializeField] private TextMeshProUGUI soundEffectSliderText;

    [SerializeField] private Slider musicVolumeSlider;
    [SerializeField] private Slider soundVolumeSlider;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void OnMusicSliderValueChange(float value)
    {
        if (musicVolumeSlider == null) return;
        MusicVolume = value;
        musicVolumeSlider.value = value;    
        musicSliderText.text = ((int)(value * 100)).ToString();
        AudioManager.instance.UpdateMixerVolumeMusic();
    }

    public void OnSoundEffectsSliderValueChange(float value)
    {
        if (soundVolumeSlider == null) return;
        SoundEffectsVolume = value;
        soundVolumeSlider.value = value;    
        soundEffectSliderText.text = ((int)(value * 100)).ToString();
        AudioManager.instance.UpdateMixerVolumeSound();
    }

    public void GetAudio()
    {
        OnMusicSliderValueChange(PlayerPrefs.GetFloat("musicaVolume"));
        OnSoundEffectsSliderValueChange(PlayerPrefs.GetFloat("soundVolume"));
    }
}
