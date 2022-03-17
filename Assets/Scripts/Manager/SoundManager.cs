using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : Singleton<SoundManager>
{
    [SerializeField] private AudioSource[] _audioSources;

    private AudioSource _current;
    public void MakeSound(AudioConfig audio)
    {
        _current = GetFreeAudioSource();

        _current.clip = audio.Clip;
        _current.volume = audio.Volume;
        _current.pitch = audio.Pitch;
        _current.Play();
    }


    private AudioSource GetFreeAudioSource()
    {
        foreach(AudioSource audio in _audioSources)
        {
            if(!audio.isPlaying) return audio;
        }
        
        return _audioSources[0];
    }
}
