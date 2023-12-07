using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioClip[] audioClips;
    [SerializeField] private float soundVolumeLevel = .5f;
    [SerializeField] private float musicVolumeLevel = 1f;

    static AudioClip[] sounds;

    static AudioSource[] audioSources;

    static float soundVolume = 1f;
    static float musicVolume = .5f;

    private void Start()
    {
        sounds = new AudioClip[audioClips.Length];
        audioSources = new AudioSource[audioClips.Length];

        soundVolume = soundVolumeLevel;
        musicVolume = musicVolumeLevel;

        for (int i = 0; i < audioClips.Length; i++)
        {
            sounds[i] = audioClips[i];

            audioSources[i] = this.AddComponent<AudioSource>();
        }

        normalMusic();
    }

    public static void quickShot()
    {
        //the first part of the sound array needs to be shooting sounds for this to work
        //can set to Random.range(0,2) to get a mix of sounds but its kinda weird
        int i = 1;

        audioSources[i].clip = sounds[i];
        audioSources[i].volume = soundVolume;
        audioSources[i].Play();
    }

    public static void beam()
    {
        audioSources[2].clip = sounds[2];
        audioSources[2].volume = soundVolume;
        audioSources[2].Play();
    }

    public static void cannon()
    {
        audioSources[3].clip = sounds[3];
        audioSources[3].volume = soundVolume;
        audioSources[3].Play();
    }

    public static void explosion()
    {
        audioSources[4].clip = sounds[4];
        audioSources[4].volume = soundVolume;
        audioSources[4].Play();
    }

    public static void damage()
    {
        audioSources[5].clip = sounds[5];
        audioSources[5].volume = soundVolume;
        audioSources[5].Play();
    }

    public static void bossMusic()
    {
        cancelMusic();
        audioSources[6].clip = sounds[6];
        audioSources[6].loop = true;
        audioSources[6].volume = musicVolume;
        audioSources[6].Play();
    }

    public static void normalMusic()
    {
        cancelMusic();
        audioSources[7].clip = sounds[7];
        audioSources[7].loop = true;
        audioSources[7].volume = musicVolume;
        audioSources[7].Play();
    }

    private static void cancelMusic()
    {
        audioSources[6].Stop();
        audioSources[7].Stop();
    }
}
