using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MainSettingController : MonoBehaviour
{
    private AudioManager audioManager;

    public GameObject panel;

    public string[] audioClipName;

    public Slider main;
    public TMP_Text mainValueText;
    private float mainValue;

    public Slider music;
    public TMP_Text musicValueText;
    private float musicValue;

    public Slider sound;
    public TMP_Text soundValueText;
    private float soundValue;

    public Toggle damageNumber;
    private bool beforeToggleIsOn;

    private void Awake()
    {
        audioManager = AudioManager.Instance;

        beforeToggleIsOn = damageNumber.isOn;
    }

    private void OnEnable()
    {
        mainValue = PlayerPrefs.GetFloat("AudioVolume", 1);
        musicValue = PlayerPrefs.GetFloat("MusicVolume", 0.5f);
        soundValue = PlayerPrefs.GetFloat("SoundVolume", 1);

        mainValueText.text = ((int)(mainValue * 100)).ToString();
        musicValueText.text = ((int)(musicValue * 100)).ToString();
        soundValueText.text = ((int)(soundValue * 100)).ToString();
        if(PlayerPrefs.GetInt("isDamagerNumber") == 1)
        {
            damageNumber.isOn = true;
        }
        else
        {
            damageNumber.isOn = false;
        }
        //damageNumber.isOn = beforeToggleIsOn;

        main.value = mainValue;
        music.value = musicValue;
        sound.value = soundValue;
    }

    private void Update()
    {
        audioManager.UpdateMusicVolume(music.value);
        audioManager.UpdateSoundVolume(sound.value);
        audioManager.UpdateAllAudioVolume(main.value);
    }

    public void ChangeSlider()
    {
        mainValueText.text = ((int)(main.value * 100)).ToString();
        musicValueText.text = ((int)(music.value * 100)).ToString();
        soundValueText.text = ((int)(sound.value * 100)).ToString();
    }

    public void OnEndDrag()
    {
        audioManager.PlaySound("WindBlade");
    }

    public void SaveChoose()
    {
        if (musicValue != music.value)
        {
            audioManager.UpdateMusicVolume(music.value);

            PlayerPrefs.SetFloat("MusicVolume", music.value);
        }

        if (soundValue != sound.value)
        {
            audioManager.UpdateSoundVolume(sound.value);

            PlayerPrefs.SetFloat("SoundVolume", sound.value);
        }

        audioManager.UpdateAllAudioVolume(main.value);

        PlayerPrefs.SetFloat("AudioVolume", main.value);

        if (damageNumber.isOn == true)
        {
            PlayerPrefs.SetInt("isDamagerNumber", 1);
        }
        else
        {
            PlayerPrefs.SetInt("isDamagerNumber", 0);
        }
        beforeToggleIsOn = damageNumber.isOn;

        audioManager.PlaySound(audioClipName[Random.Range(0, audioClipName.Length)]);

        panel.SetActive(false);
    }

    public void CancelChoose()
    {
        mainValueText.text = ((int)(mainValue * 100)).ToString();
        musicValueText.text = ((int)(musicValue * 100)).ToString();
        soundValueText.text = ((int)(soundValue * 100)).ToString();

        audioManager.UpdateMusicVolume(musicValue);
        audioManager.UpdateSoundVolume(soundValue);
        audioManager.UpdateAllAudioVolume(mainValue);

        damageNumber.isOn = beforeToggleIsOn;
        audioManager.PlaySound(audioClipName[Random.Range(0, audioClipName.Length)]);

        panel.SetActive(false);
    }
}
