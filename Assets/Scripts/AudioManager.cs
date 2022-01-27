using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private static AudioManager _instance;

    public static AudioManager Instance
    {
        get
        {
            if (!_instance)
            {
                _instance = FindObjectOfType<AudioManager>();
            }

            return _instance;
        }
    }

    public List<Sound> audioList;

    void Awake()
    {
        DontDestroyOnLoad(gameObject);

        foreach (Sound s in audioList)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
    }

    void Start()
    {
        // Play("Menu");
    }

    public void Play(string name)
    {
        Sound s = audioList.Find(sound => sound.name == name);
        s.source.Play();
    }

}

[System.Serializable]
public class Sound
{
    public string name;
    public AudioClip clip;

    [Range(0f, 1f)]
    public float volume;
    [Range(.1f, 3f)]
    public float pitch = 1f;

    public bool loop;

    [HideInInspector]
    public AudioSource source;

    public Sound()
    {
        volume = 1;
        pitch = 1;
    }
}
