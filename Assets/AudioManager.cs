using UnityEngine.Audio;
using UnityEngine;
using System;

public class AudioManager : MonoBehaviour
{
    public AudioMixer audioMixer;
    public AudioMixerGroup audioMixerGroup;

    public Sound[] sounds;

    public static AudioManager Instance;


    // Start is called before the first frame update
    void Awake()
    {
        // singleton pattern
        if (Instance == null)
        {
            DontDestroyOnLoad(gameObject);
            Instance = this;
            foreach (Sound s in sounds)
            {
                s.source = gameObject.AddComponent<AudioSource>();
                s.source.clip = s.clip;
                s.source.volume = s.volume;
                s.source.pitch = s.pitch;
                s.source.loop = s.loop;
                s.source.outputAudioMixerGroup = audioMixerGroup;
            }
        }
        else
        {
            DestroyImmediate(gameObject);
        }

    }

    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound " + name + " not found!!");
            return;
        }
        s.source.Play();
    }

    public void Stop(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound " + name + " not found!!");
            return;
        }
        s.source.Stop();
    }

    public bool isPlaying(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound " + name + " not found!!");
            return false;
        }
        return s.source.isPlaying;
    }

    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("Volume", volume);
    }
}
