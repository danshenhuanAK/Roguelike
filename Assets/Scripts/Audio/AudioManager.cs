using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : Singleton<AudioManager>
{
    public AudioMixer audioMixer;
    public AudioMixerGroup soundMixer;

    public Vector2 mainValueRange;
    public Vector2 musicValueRange;
    public Vector2 soundValueRange;

    public List<AudioData> musicAudios, soundAudios;

    public AudioSource musicSource;
    private AudioSource[] soundSources;
    private AudioSource[] attackedSources;

    public GameObject soundSourceObject;
    public GameObject attackedObject;
    public int attackedSourceNum;

    protected override void Awake()
    {
        base.Awake();

        soundSources = new AudioSource[soundAudios.Count];
        for (int i = 0; i < soundAudios.Count; i++)
        {
            soundSources[i] = soundSourceObject.AddComponent<AudioSource>();
            soundSources[i].outputAudioMixerGroup = soundMixer;
        }

        if(attackedSourceNum != 0)
        {
            attackedSources = new AudioSource[attackedSourceNum];
            for (int i = 0; i < attackedSourceNum; i++)
            {
                attackedSources[i] = attackedObject.AddComponent<AudioSource>();
                attackedSources[i].outputAudioMixerGroup = soundMixer;
            }
        }
    }

    private void Start()
    {
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
        float mainValue;

        if (value != 0)
        {
            mainValue = mainValueRange.x - (mainValueRange.x - mainValueRange.y) * (1 - value);
        }
        else
        {
            mainValue = -80f;
        }

        audioMixer.SetFloat("Main", mainValue);
    }

    public void UpdateMusicVolume(float value)
    {
        float musicValue;

        if (value != 0)
        {
            musicValue = musicValueRange.x - (musicValueRange.x - musicValueRange.y) * (1 - value);
        }
        else
        {
            musicValue = -80f;
        }

        audioMixer.SetFloat("Music", musicValue);
    }

    public void UpdateSoundVolume(float value)
    {
        float soundValue;

        if (value != 0)
        {
            soundValue = soundValueRange.x - (soundValueRange.x - soundValueRange.y) * (1 - value);
        }
        else
        {
            soundValue = -80f;
        }

        for (int i = 0; i < soundSources.Length; i++)
        {
            audioMixer.SetFloat("Sound", soundValue);
        }

        if(attackedSourceNum != 0)
        {
            for (int i = 0; i < attackedSources.Length; i++)
            {
                audioMixer.SetFloat("Sound", soundValue);
            }
        }
    }
}
