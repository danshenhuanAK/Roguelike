using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : Singleton<AudioManager>
{
    public List<AudioData> musicAudios, soundAudios;

    public AudioSource musicSource;
    public GameObject soundSourceObject;
    public GameObject attackedObject;
    public int attackedSourceNum;

    private float audioVolume;
    private float musicVolume;
    private float soundVolume;

    private AudioSource[] soundSources;
    private AudioSource[] attackedSources;

    protected override void Awake()
    {
        base.Awake();

        soundSources = new AudioSource[soundAudios.Count];
        for (int i = 0; i < soundAudios.Count; i++)
        {
            soundSources[i] = soundSourceObject.AddComponent<AudioSource>();
        }

        if(attackedSourceNum != 0)
        {
            attackedSources = new AudioSource[attackedSourceNum];
            for (int i = 0; i < attackedSourceNum; i++)
            {
                attackedSources[i] = attackedObject.AddComponent<AudioSource>();
            }
        }

        UpdateMusicVolume(PlayerPrefs.GetFloat("MusicVolume", 0.5f));
        UpdateSoundVolume(PlayerPrefs.GetFloat("SoundVolume", 1));
        UpdateAllAudioVolume(PlayerPrefs.GetFloat("AudioVolume", 1));
    }

    public void PlayMusic(string name)
    {
        AudioData playAudio = musicAudios.Find(audio => audio.name == name);

        if(playAudio == null)
        {
            Debug.LogWarning("没有找到" + name + "声音");
        }
        else
        {
            musicSource.clip = playAudio.clip;
            musicSource.Play();
        }
    }

    public int PlaySound(string name)
    {
        AudioData playAudio = soundAudios.Find(audio => audio.name == name);

        if (playAudio == null)
        {
            Debug.LogWarning("没有找到" + name + "声音");
            return -1;
        }

        for (int i = 0; i < soundSources.Length; i++)
        {
            if (!soundSources[i].isPlaying)
            {
                soundSources[i].clip = playAudio.clip;
                soundSources[i].Play();
                return i;
            }
        }

        Debug.LogWarning("没有空闲的音频播放器");
        return -1;
    }

    public void PlayAttackedSound(AudioClip clip)
    {
        for (int i = 0; i < attackedSources.Length; i++)
        {
            if (!attackedSources[i].isPlaying)
            {
                attackedSources[i].clip = clip;
                attackedSources[i].Play();
            }
        }
    }

    public void StopSound(int id)
    {
        soundSources[id].Stop();
    }

    public void UpdateAllAudioVolume(float value)
    {
        UpdateMusicVolume(value);
        UpdateSoundVolume(value);
    }

    public void UpdateMusicVolume(float value)
    {
        musicSource.volume = value;
    }

    public void UpdateSoundVolume(float value)
    {
        for(int i = 0; i < soundSources.Length; i++)
        {
            soundSources[i].volume = value;
        }

        if(attackedSourceNum != 0)
        {
            for (int i = 0; i < attackedSources.Length; i++)
            {
                attackedSources[i].volume = value;
            }
        }
    }
}
