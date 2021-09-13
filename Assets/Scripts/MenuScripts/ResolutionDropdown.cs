using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ResolutionDropdown : MonoBehaviour
{
    [Header("Video Settings")]
    public TMPro.TMP_Dropdown resolutionDropdown;
    public TMPro.TMP_Dropdown displayModeDropdown;
    public TMPro.TMP_Dropdown qualityDropdown;
    public Slider brightnessSlider;

    [Header("Audio Settings")]
    public Slider sfxVolumeSlider;
    public Slider musicVolumeSlider;
    public Slider dialogueVolumeSlider;

    [Header("Gameplay Settings")]
    public Slider mouseSensitivity;

    Resolution[] resolutions; // List of possible resolutions
    FullScreenMode currentScreenMode;
    
    // Start is called before the first frame update
    void Start()
    {
        SetupResolutionDropdown();
        SetupScreenDropdown();
        SetupQualityDropdown();
        SetupBrightnessSlider();
    }

    void SetupResolutionDropdown()
    {
        resolutions = Screen.resolutions; // Populate the list with Unity's screen res

        int index = 0;

        resolutionDropdown.ClearOptions();

        for (int i = 0; i < resolutions.Length; i++)
        {
            Debug.Log(resolutions[i].ToString()); // 
            resolutionDropdown.options.Add(new TMP_Dropdown.OptionData(resolutions[i].ToString())); // Add all the options from resolutions to the dropdown options

            if (resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height)
            {
                index = i;
            }
        }

        resolutionDropdown.value = index;

        resolutionDropdown.onValueChanged.AddListener(i => Screen.SetResolution(resolutions[i].width, resolutions[i].height, currentScreenMode));

    }

    void SetupScreenDropdown()
    {
        int index = 0;

        displayModeDropdown.ClearOptions();

        // Add all fullscreen modes to the dropdown as options
        displayModeDropdown.options.Add(new TMP_Dropdown.OptionData(FullScreenMode.ExclusiveFullScreen.ToString()));
        displayModeDropdown.options.Add(new TMP_Dropdown.OptionData(FullScreenMode.FullScreenWindow.ToString()));
        displayModeDropdown.options.Add(new TMP_Dropdown.OptionData(FullScreenMode.MaximizedWindow.ToString()));
        displayModeDropdown.options.Add(new TMP_Dropdown.OptionData(FullScreenMode.Windowed.ToString()));

        // Set the dropdown value to current fullscreen mode
        for (int i = 0; i < displayModeDropdown.options.Count; i++) 
        {
            if (Screen.fullScreenMode.ToString() == displayModeDropdown.options[i].text)
            {
                index = i;
            }
        }

        // Set the value
        displayModeDropdown.value = index;
        currentScreenMode = (FullScreenMode)index;

        displayModeDropdown.onValueChanged.AddListener(i => Screen.SetResolution(Screen.width, Screen.height, (FullScreenMode)i));
    }

    void SetupQualityDropdown()
    {
        int index = 0;

        qualityDropdown.ClearOptions();

        for (int i = 0; i < QualitySettings.names.Length; i++)
        {
            qualityDropdown.options.Add(new TMP_Dropdown.OptionData(QualitySettings.names[i]));

            if (QualitySettings.GetQualityLevel().ToString() == qualityDropdown.options[qualityDropdown.value].ToString())
            {
                index = i;
            }

        }
        qualityDropdown.value = index;

        qualityDropdown.onValueChanged.AddListener(i => QualitySettings.SetQualityLevel(i));
    }

    void SetupBrightnessSlider()
    {
        brightnessSlider.value = Screen.brightness;
        brightnessSlider.onValueChanged.AddListener(i => Screen.brightness = brightnessSlider.value);
    }
}
