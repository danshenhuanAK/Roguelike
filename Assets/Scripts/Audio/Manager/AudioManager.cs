using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : Singleton<AudioManager>
{
    public List<AudioData> musicAudios, soundAudios;

    public AudioSource musicSource, soundSource;

    public void PlayAudio(string name)
    {
        AudioData playAudio = musicAudios.Find(audio => audio.name == name);

        if(playAudio == null)
        {
            Debug.LogWarning("√ª”–’“µΩ" + name + "…˘“Ù");
        }
        else
        {
            musicSource.clip = playAudio.clip;
            musicSource.Play();
        }
    }    
}
