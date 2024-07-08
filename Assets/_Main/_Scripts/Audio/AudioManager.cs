using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using static UnityEngine.Rendering.DebugUI;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    [SerializeField] private AudioMixerGroup musicMixerGroup;
    [SerializeField] private AudioMixerGroup soundEffectsMixterGruop;
    [SerializeField] private Sound[] sounds;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            //DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(gameObject);
        }

        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.audioClip;
            s.source.loop = s.isLoop;
            s.source.volume = s.volume;

            switch (s.audioType)
            {
                case Sound.AudioTypes.soundEffect:
                    s.source.outputAudioMixerGroup = soundEffectsMixterGruop;
                    break;
                case Sound.AudioTypes.music:
                    s.source.outputAudioMixerGroup = musicMixerGroup;
                    break;
            }

            if (s.playOnAwake)
                s.source.Play();
        }
    }

    private void Start()
    {
        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            Stop("MusicGameplay");
            PlayMusic("MusicMainMenu");
        }
        else
        {
            Stop("MusicMainMenu");
            PlayMusic("MusicGameplay");
        }

        AudioOptionManager.instance.OnSoundEffectsSliderValueChange(PlayerPrefs.GetFloat("soundVolume", 1f));
        AudioOptionManager.instance.OnMusicSliderValueChange(PlayerPrefs.GetFloat("musicaVolume", 1f));
    }

    public void PlayMusic(string clipName, bool playIfPlaying = false)
    {
        Sound s = Array.Find(sounds, dummySound => dummySound.clipName == clipName);
        if (s == null)
        {
            //Debug.LogError($"Sound: {clipName} does NOT exist!");
            return;
        }
        if (!s.source.isPlaying || playIfPlaying)
        {
            s.source.Play();
            s.source.playOnAwake = true;
            s.source.loop = true;
        }
    }

    public void StopAll()
    {
        foreach (Sound s in sounds)
        {
            s.source.Stop();
        }
    }
    public void Play(string clipName, bool playIfPlaying = false, bool canLoop = false)
    {
        Sound s = Array.Find(sounds, dummySound => dummySound.clipName == clipName);

        ///por favor no pongas mas debug error gracias
        if (!s.source.isPlaying || playIfPlaying == false) 
        {
            s.source.Play();
            s.source.loop = canLoop;
        }
       
    }

    public void Stop(string clipName)
    {
        Sound s = Array.Find(sounds, dummySound => dummySound.clipName == clipName);
        s.source.loop = false;
        if (s == null)
        {
            //Debug.LogError($"Sound: {clipName} does NOT exist!");
            return;
        }
        if (!s.source.isPlaying)
        {        
            s.source.Stop();
        }
    }

    public void UpdateMixerVolumeMusic()
    {
        float volumeMusic = Mathf.Log10(AudioOptionManager.MusicVolume) * 20;
        musicMixerGroup.audioMixer.SetFloat("Music Volume", volumeMusic);
        PlayerPrefs.SetFloat("musicaVolume", AudioOptionManager.MusicVolume);
    }

    public void UpdateMixerVolumeSound()
    {
        float soundMusic = Mathf.Log10(AudioOptionManager.SoundEffectsVolume) * 20;
        soundEffectsMixterGruop.audioMixer.SetFloat("Sound Effect Volume", soundMusic);
        PlayerPrefs.SetFloat("soundVolume", AudioOptionManager.SoundEffectsVolume);
    }
}
