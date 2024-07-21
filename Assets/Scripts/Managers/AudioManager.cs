using UnityEngine;
using System.Collections.Generic;

public class AudioManager : SingletonClass<AudioManager>
{
    [System.Serializable]
    public class Sound
    {
        public string name;
        public AudioClip clip;
        [Range(0f, 1f)]
        public float volume = 1f;
        [Range(0.1f, 3f)]
        public float pitch = 1f;
        public bool loop = false;

        [HideInInspector]
        public AudioSource source;
    }

    public Sound[] sounds;

    private Dictionary<string, float> soundTimers = new Dictionary<string, float>();

    private void Awake()
    {
        if (sounds == null)
            return;

        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
    }

    public void Play(string name)
    {
        //Sound s = System.Array.Find(sounds, sound => sound.name == name);
        //if (s == null)
        //{
        //    Debug.LogWarning("Sound: " + name + " not found!");
        //    return;
        //}
        //s.source.Play();
    }

    public void PlayWithCooldown(string name, float cooldown)
    {
        if (!soundTimers.ContainsKey(name) || Time.time - soundTimers[name] >= cooldown)
        {
            Play(name);
            soundTimers[name] = Time.time;
        }
    }

    public void Stop(string name)
    {
        Sound s = System.Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }
        s.source.Stop();
    }

    public void SetVolume(string name, float volume)
    {
        Sound s = System.Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }
        s.source.volume = volume;
    }

    public void SetMasterVolume(float volume)
    {
        foreach (Sound s in sounds)
        {
            s.source.volume = s.volume * volume;
        }
    }
}