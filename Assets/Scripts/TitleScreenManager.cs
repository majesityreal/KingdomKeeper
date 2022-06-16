using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using UnityEngine.UI;

public class TitleScreenManager : MonoBehaviour
{

    public Slider volumeSlider;

    Resolution[] resolutions;

    public Dropdown resolutionDropdown;
    public Toggle fullscreenToggle;


    // Start is called before the first frame update
    void Start()
    {
        if (!AudioManager.Instance.isPlaying("TitleTheme"))
        {
            AudioManager.Instance.Play("TitleTheme");
        }

        resolutions = Screen.resolutions;
        resolutionDropdown.ClearOptions();

        // creating a List of options to add to the dropdown.
        List<string> options = new List<string>();

        // compares current screen resolution to set it to default value
        int currentResolution = 0;

        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            options.Add(option);
            if (resolutions[i].width == Screen.currentResolution.width &&
                resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolution = i;
            }
        }
        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolution;
        resolutionDropdown.RefreshShownValue();

        // sets the volume slider to the correct position on scene load
        AudioManager.Instance.audioMixer.GetFloat("Volume", out float val);
        volumeSlider.value = val;

        // resets fullscreen toggle
        fullscreenToggle.isOn = Screen.fullScreen;

    }

    public void NewGame()
    {
        SceneManager.LoadScene("CharacterSelect");

    }

    public void StartGame()
    {
        if (UIManager.Instance != null)
        {
            UIManager.Instance.gameObject.SetActive(true);
        }
    }

    public void Quit()
    {
        // anything else needed before quit goes here
        Application.Quit();
    }

    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }

    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

    public void CreditsLink(string URL)
    {
        Application.OpenURL(URL);
    }

}
