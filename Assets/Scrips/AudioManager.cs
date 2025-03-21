using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instace;
    public AudioSource BackGroundMusic;
    public Slider MusicVolume;
    public Toggle MusicToggel;

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
        if (MusicToggel == null)
            MusicToggel = FindObjectOfType<Toggle>();

        if (MusicVolume == null)
            MusicVolume = FindObjectOfType<Slider>();

        float saveVolume = PlayerPrefs.GetFloat("Volume", 1);
        bool isMusicOn = PlayerPrefs.GetInt("MusicOn",1) == 1;

        BackGroundMusic.volume = saveVolume;
        BackGroundMusic.mute = !isMusicOn;
        if (MusicVolume != null)
        {
            if (MusicVolume) MusicVolume.value = saveVolume;
            if (MusicVolume) MusicVolume.onValueChanged.AddListener(SetVolume);
        }

        if (MusicToggel != null)
        {
            if (MusicToggel) MusicToggel.isOn = isMusicOn;
            if (MusicToggel) MusicToggel.onValueChanged.AddListener(SetMusicState);
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
