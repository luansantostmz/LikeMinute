using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    static AudioSource _audioSource;

    void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    public static void Play(AudioClip clip)
    {
        _audioSource.PlayOneShot(clip);
    }
}

[System.Serializable]
public class AudioClipPool
{
    public List<AudioClip> clips = new List<AudioClip>();

    int clipCount;

    public AudioClip GetClip()
    {
        AudioClip clip = clips[clipCount];

        clipCount++;

        if (clipCount >= clips.Count)
            clipCount = 0;

        return clip;
    }
}