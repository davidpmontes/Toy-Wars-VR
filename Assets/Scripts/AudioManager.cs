using UnityEngine.Audio;
using UnityEngine;
using System.Collections.Generic;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    public static AudioManager Instance { get { return instance; } }

    public Sound[] sounds;

    private Dictionary<string, Sound> soundsDict;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        DontDestroyOnLoad(gameObject);

        soundsDict = new Dictionary<string, Sound>();

        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
            soundsDict.Add(s.name, s);
        }
    }

    public void Play(string name)
    {
        if (soundsDict[name].source.isPlaying)
            return;
        else
            soundsDict[name].source.Play();
    }

    public void PlayOverlapping(string name)
    {
        soundsDict[name].source.PlayOneShot(soundsDict[name].source.clip);
    }

    public void Stop(string name)
    {
        soundsDict[name].source.Stop();
    }
}

