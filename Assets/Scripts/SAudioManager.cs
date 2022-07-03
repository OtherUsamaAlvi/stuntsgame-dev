using UnityEngine.Audio;
using System;
using UnityEngine;

public class SAudioManager : MonoBehaviour
{
    public Sounds[] sounds;

    public static SAudioManager instance;
    //AudioManager

    void Awake()
    {

        if (instance == null)
            instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);

        foreach (Sounds s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
            //s.source.outputAudioMixerGroup = s.mixer;
        }
    }

    void Start()
    {
        //Play("Theme");
    }

    public void Play(string name)
    {
        Sounds s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found");
            return;
        }
        if (s.source==null)
        {
            Debug.LogWarning("Source is null");
            Debug.LogWarning("Source: "+ s + " Sound: " + name + " not found");

        }
        if (s.source)
        {
            if (!s.source.isPlaying)
            {
                s.source.Play();
            }
        }
       
    }

    public void ModifySoundParams(string name, float volume, bool loop)
    {

        Sounds s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found");
            return;
        }

        s.source.volume = volume;
        s.source.loop = loop;
        s.source.Play();
    }


    public void Stop(string name)
    {
        Sounds s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found");
            return;
        }

        if (s.source)
        {
            if (s.source.isPlaying)
            {
                s.source.Stop();
            }
        }
    }
}
