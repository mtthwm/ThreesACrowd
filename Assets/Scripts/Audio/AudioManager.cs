using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;

    public static AudioManager instance;

    private HashSet<string> currentlyFading = new HashSet<string>();
    private HashSet<string> interruptFade = new HashSet<string>();

    public HashSet<string> currentlyPlaying = new();

    public void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }
        DontDestroyOnLoad(this.gameObject);
        LoadSounds();
    }

    private void LoadSounds()
    {
        foreach (Sound sound in sounds)
        {
            sound.source = this.gameObject.AddComponent<AudioSource>();
            sound.source.clip = sound.clip;
            sound.source.volume = sound.volume;
            sound.source.loop = sound.loop;
        }
    }

    public void Play(string name)
    {
        Sound sound = Array.Find(sounds, x => x.id.Equals(name));
        if (sound == null)
        {
            Debug.LogWarning("Sound: " + name + " is incorrectly configured.");
            return;
        }
        if (currentlyFading.Contains(sound.id))
        {
            interruptFade.Add(name);
        }
        currentlyPlaying.Add(name);
        sound.Play();
    }

    public void Stop(string name)
    {
        Sound sound = Array.Find(sounds, x => x.id.Equals(name));
        if (sound == null)
        {
            Debug.LogWarning("Sound: " + name + " is incorrectly configured.");
            return;
        }
        if (currentlyFading.Contains(sound.id))
        {
            interruptFade.Add(name);
        }
        currentlyPlaying.Remove(name);
        sound.Stop();
    }

    public void FadeOut(string name, float time)
    {
        Sound sound = Array.Find(sounds, x => x.id.Equals(name));
        if (sound == null)
        {
            Debug.LogWarning("Sound: " + name + " is incorrectly configured.");
            return;
        }
        StartCoroutine(DoFade(sound, time));
    }

    IEnumerator DoFade(Sound sound, float time)
    {
        currentlyFading.Add(sound.id);
        float timePerUnit = sound.source.volume / time;
        float startVolume = sound.source.volume;
        float x = 0;
        while (sound.source.volume > 0)
        {
            if (interruptFade.Contains(sound.id))
            {
                interruptFade.Remove(sound.id);
                yield break;
            }
            else
            {
                sound.source.volume = -(startVolume / time) * x + startVolume;
                x += timePerUnit;
                yield return new WaitForSeconds(timePerUnit);
            }
        }
        sound.Stop();
        sound.source.volume = startVolume;
        currentlyFading.Remove(sound.id);
    }
}
