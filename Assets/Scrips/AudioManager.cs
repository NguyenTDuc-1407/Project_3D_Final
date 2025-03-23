using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instace;
    public AudioSource BackGroundMusic;
    public Slider MusicVolume;
    public Toggle MusicToggle;
    public GameObject SettingMenu;
    private void Awake()
    {
        if (Instace == null)
        {
            Instace = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }
    void Start()
    {
        if (SettingMenu == null)
            SettingMenu = GameObject.Find("Setting Menu");

        if (MusicToggle == null)
            MusicToggle = GameObject.Find("MusicState")?.GetComponent<Toggle>();

        if (MusicVolume == null)
            MusicVolume = GameObject.Find("Audio")?.GetComponent<Slider>();
        
        if (BackGroundMusic == null)
            BackGroundMusic = GameObject.Find("BackGroundMusic")?.GetComponent<AudioSource>();

        SettingMenu.SetActive(false);
        float saveVolume = PlayerPrefs.GetFloat("Volume", 1);
        bool isMusicOn = PlayerPrefs.GetInt("MusicOn",1) == 1;

        BackGroundMusic.volume = saveVolume;
        BackGroundMusic.mute = !isMusicOn;
        if (MusicVolume != null)
        {
            if (MusicVolume) MusicVolume.value = saveVolume;
            if (MusicVolume) MusicVolume.onValueChanged.AddListener(SetVolume);
        }

        if (MusicToggle != null)
        {
            if (MusicToggle) MusicToggle.isOn = isMusicOn;
            if (MusicToggle) MusicToggle.onValueChanged.AddListener(SetMusicState);
        }
    }

    void SetVolume(float volume)
    {
        BackGroundMusic.volume = volume;
        PlayerPrefs.SetFloat("Volume", volume);
        PlayerPrefs.Save();
    }
    void SetMusicState(bool isOn)
    {
        BackGroundMusic.mute = !isOn;
        PlayerPrefs.SetInt("MusicOn", isOn ? 1 : 0);
        PlayerPrefs.Save();
    }

}
