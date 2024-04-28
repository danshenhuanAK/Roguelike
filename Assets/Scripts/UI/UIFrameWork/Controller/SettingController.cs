using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SettingController : MonoBehaviour
{
    private AudioManager audioManager;
    private UIPanelManager uIPanelManager;
    private FightProgressAttributeManager attributeManager;

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

    private void Awake()
    {
        audioManager = AudioManager.Instance;
        uIPanelManager = UIPanelManager.Instance;
        attributeManager = FightProgressAttributeManager.Instance;
    }

    private void OnEnable()
    {
        mainValue = PlayerPrefs.GetFloat("AudioVolume", 1);
        musicValue = PlayerPrefs.GetFloat("MusicVolume", 0.5f);
        soundValue = PlayerPrefs.GetFloat("SoundVolume", 1);

        mainValueText.text = ((int)(mainValue * 100)).ToString();
        musicValueText.text = ((int)(musicValue * 100)).ToString();
        soundValueText.text = ((int)(soundValue * 100)).ToString();
        if (PlayerPrefs.GetInt("isDamagerNumber") == 1)
        {
            damageNumber.isOn = true;
        }
        else
        {
            damageNumber.isOn = false;
        }


        main.value = mainValue;
        music.value = musicValue;
        sound.value = soundValue;
    }

    public void ChangeSlider()
    {
        mainValueText.text = ((int)(main.value * 100)).ToString();
        musicValueText.text = ((int)(music.value * 100)).ToString();
        soundValueText.text = ((int)(sound.value * 100)).ToString();
    }

    /// <summary>
    /// 保存设置
    /// </summary>
    public void SaveSetting()
    {
        if(musicValue != music.value)
        {
            audioManager.UpdateMusicVolume(music.value);

            PlayerPrefs.SetFloat("MusicVolume", music.value);
        }

        if(soundValue != sound.value)
        {
            audioManager.UpdateSoundVolume(sound.value);

            PlayerPrefs.SetFloat("SoundVolume", sound.value);
        }

        if(mainValue != main.value)
        {
            audioManager.UpdateAllAudioVolume(main.value);
        }
        

        PlayerPrefs.SetFloat("AudioVolume", main.value);

        attributeManager.isCreatDamageUi = damageNumber.isOn;

        if(damageNumber.isOn == true)
        {
            PlayerPrefs.SetInt("isDamagerNumber", 1);
        }
        else
        {
            PlayerPrefs.SetInt("isDamagerNumber", 0);
        }
        
        audioManager.PlaySound(audioClipName[Random.Range(0, audioClipName.Length)]);
        uIPanelManager.PopPanel();
    }

    /// <summary>
    /// 取消设置
    /// </summary>
    public void CancelSetting()
    {
        mainValueText.text = ((int)(mainValue * 100)).ToString();
        musicValueText.text = ((int)(musicValue * 100)).ToString();
        soundValueText.text = ((int)(soundValue * 100)).ToString();
        damageNumber.isOn = attributeManager.isCreatDamageUi;
        audioManager.PlaySound(audioClipName[Random.Range(0, audioClipName.Length)]);
        uIPanelManager.PopPanel();
    }
}
