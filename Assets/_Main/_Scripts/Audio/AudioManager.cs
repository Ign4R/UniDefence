using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

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
    public void StopAll()
    {
        foreach (Sound s in sounds)
        {
            s.source.Stop();
        }
    }
    public void Play(string clipName, bool playIfPlaying = false)
    {
        Sound s = Array.Find(sounds, dummySound => dummySound.clipName == clipName);
        if(s == null)
        {
            Debug.LogError($"Sound: {clipName} does NOT exist!");
            return;
        }
        if (!s.source.isPlaying || playIfPlaying)
        {
            s.source.Play();
        }
    }

    public void Stop(string clipName)
    {
        Sound s = Array.Find(sounds, dummySound => dummySound.clipName == clipName);
        if(s == null)
        {
            Debug.LogError($"Sound: {clipName} does NOT exist!");
            return;
        }
        s.source.Stop();
    }

    public void UpdateMixerVolume()
    {
        musicMixerGroup.audioMixer.SetFloat("Music Volume", Mathf.Log10(AudioOptionManager.MusicVolume) * 20);
        soundEffectsMixterGruop.audioMixer.SetFloat("Sound Effect Volume", Mathf.Log10(AudioOptionManager.SoundEffectsVolume) * 20);
    }
}
