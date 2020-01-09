using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    private List<Sound> activeSounds;
    private List<Sound> pausedSounds;
    public Sound[] sounds;

    private bool audioIsGloballyPaused;

    private void Start()
    {
        activeSounds = new List<Sound>();
        pausedSounds = new List<Sound>();

        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Update()
    {
        foreach (Sound sound in activeSounds)
        {
            if (!sound.source.isPlaying && !pausedSounds.Contains(sound))
            {
                activeSounds.Remove(sound);
                Destroy(sound.source);
            }
        }
    }

    public void Play(string name, bool isLooping)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);

        s.source = gameObject.AddComponent<AudioSource>();
        s.source.clip = s.clip;
        s.source.volume = s.volume;
        s.source.pitch = s.pitch;

        s.source.Play();
        activeSounds.Add(s);

        if (isLooping) s.source.loop = true;
    }

    public void PlayIfNotPlaying(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);

        if (!GetSound(name).source.isPlaying) instance.Play(name, false);
    }

    public void Stop(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);

        activeSounds.Remove(s);

        StartCoroutine(VolumeFade(s, s.source, 0f, 0.05f));
    }

    public void Stop(string name, float fadeLength)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);

        activeSounds.Remove(s);

        if (s.source.isPlaying) StartCoroutine(VolumeFade(s, s.source, 0f, fadeLength));
    }

    public void StopAll()
    {
        foreach (Sound s in sounds)
        {
            activeSounds.Remove(s);
            if (s.source.isPlaying) StartCoroutine(VolumeFade(s, s.source, 0f, 0.05f));
        }
    }

    public void StopAll(float fadeLength)
    {
        foreach (Sound s in sounds)
        {
            activeSounds.Remove(s);
            if (s.source.isPlaying) StartCoroutine(VolumeFade(s, s.source, 0f, fadeLength));
        }
    }

    public void Pause(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);

        pausedSounds.Add(s);

        s.source.Pause();
    }

    public void UnPause(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);

        pausedSounds.Remove(s);

        s.source.UnPause();
    }

    public void PauseAll()
    {
        foreach (Sound s in sounds)
        {
            pausedSounds.Add(s);
            s.source.Pause();
        }
    }

    public void UnPauseAll()
    {
        foreach (Sound s in sounds)
        {
            pausedSounds.Remove(s);
            s.source.UnPause();
        }
    }

    private IEnumerator VolumeFade(Sound sound, AudioSource audioSource, float endVolume, float fadeLength)
    {
        var startVolume = audioSource.volume;
        var startTime = Time.time;

        while (Time.time < startTime + fadeLength)
        {
            audioSource.volume = startVolume + ((endVolume - startVolume) * ((Time.time - startTime) / fadeLength));
            yield return null;
        }

        if (endVolume == 0) audioSource.Stop();

        sound.source.volume = sound.volume;
    }

    public Sound GetSound(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        return s;
    }
}
