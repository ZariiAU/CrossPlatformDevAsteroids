using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsUI : MonoBehaviour
{
    public Settings settings;

    public Slider musicVolume;
    public Slider soundFxVolume;
    public Slider dialogueVolume;

    public bool isNotMuted;

    private void Start() 
    {
        musicVolume.value = settings.musicVolume;
        soundFxVolume.value = settings.soundFxVolume;
        dialogueVolume.value = settings.dialogueVolume;

        musicVolume.onValueChanged.AddListener(OnMusicVolumeChanged);
        soundFxVolume.onValueChanged.AddListener(OnMusicVolumeChanged);
        dialogueVolume.onValueChanged.AddListener(OnMusicVolumeChanged);
    }

    public void Toggle() 
    { 
        gameObject.SetActive(!gameObject.activeSelf);
    }

    public void Mute()
    {
        if (isNotMuted)
        {
            isNotMuted = false;

            settings.musicVolume = 0;
            settings.soundFxVolume = 0;
            settings.dialogueVolume = 0;
        }
        else if (!isNotMuted)
        {
            isNotMuted = true;

            settings.musicVolume = 1;
            settings.soundFxVolume = 1;
            settings.dialogueVolume = 1;
        }
    }

    public void OnMusicVolumeChanged(float volume)
    {
        settings.musicVolume = volume;
    }
}
