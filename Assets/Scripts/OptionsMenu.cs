using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class OptionsMenu : MonoBehaviour
{
    public Toggle fullScreenTog, vsyncTog;
    public ResItem[] resolutions;
    public int selectedResolution;
    public Text resolutionLabel;
    public AudioMixer theMixer;
    public Slider masterSlider, musicSlider, sfxSlider;
    public Text masterLabel, musicLabel, sfxLabel;
    public AudioSource sfxLoop;
    public AudioSource audio;

    // Start is called before the first frame update
    void Start()
    {
        // Update toggles
        fullScreenTog.isOn = Screen.fullScreen;
        if (QualitySettings.vSyncCount == 0)
        {
            vsyncTog.isOn = false;
        }
        else
        {
            vsyncTog.isOn = true;
        }

        // Find current resolution to display
        bool foundResolution = false;
        for (int i = 0; i < resolutions.Length; i++)
        {
            if (Screen.width == resolutions[i].horizontal && Screen.height == resolutions[i].vertical)
            {
                foundResolution = true;
                selectedResolution = i;
                UpdateResLabel();
            }
        }

        // Default to 1920x1080
        if (foundResolution == false)
        {
            selectedResolution = 0;
            ApplyGraphics();
        }

        // Restore audio state
        if (PlayerPrefs.HasKey("MasterVol"))
        {
            theMixer.SetFloat("MasterVol", PlayerPrefs.GetFloat("MasterVol"));
            masterSlider.value = PlayerPrefs.GetFloat("MasterVol");
        }

        if (PlayerPrefs.HasKey("MusicVol"))
        {
            theMixer.SetFloat("MusicVol", PlayerPrefs.GetFloat("MusicVol"));
            musicSlider.value = PlayerPrefs.GetFloat("MusicVol");
        }

        if (PlayerPrefs.HasKey("SfxVol"))
        {
            theMixer.SetFloat("SfxVol", PlayerPrefs.GetFloat("SfxVol"));
            sfxSlider.value = PlayerPrefs.GetFloat("SfxVol");
        }
        
        masterLabel.text = (masterSlider.value + 80).ToString();
        musicLabel.text = (musicSlider.value + 80).ToString();
        sfxLabel.text = (sfxSlider.value + 80).ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ResLeft()
    {
        if (--selectedResolution < 0)
        {
            selectedResolution = 0;
        }
        UpdateResLabel();
    }

    public void ResRight()
    {
        if (++selectedResolution > resolutions.Length - 1)
        {
            selectedResolution = resolutions.Length - 1;
        }
        UpdateResLabel();
    }

    public void UpdateResLabel()
    {
        resolutionLabel.text = resolutions[selectedResolution].horizontal.ToString()
                                + " x " + resolutions[selectedResolution].vertical.ToString();
    }

    public void ApplyGraphics()
    {
        // Apply vSync
        if (vsyncTog.isOn)
        {
            QualitySettings.vSyncCount = 1;
        }
        else
        {
            QualitySettings.vSyncCount = 0;
        }

        // Apply resolution & fullscreen
        Screen.SetResolution(resolutions[selectedResolution].horizontal, resolutions[selectedResolution].vertical, fullScreenTog.isOn);
    } 

    public void setMasterVolume()
    {
        masterLabel.text = (masterSlider.value + 80).ToString();

        theMixer.SetFloat("MasterVol", masterSlider.value);

        PlayerPrefs.SetFloat("MasterVol", masterSlider.value);
    }

    public void setMusicVolume()
    {
        musicLabel.text = (musicSlider.value + 80).ToString();

        theMixer.SetFloat("MusicVol", musicSlider.value);

        PlayerPrefs.SetFloat("MusicVol", musicSlider.value);
    }

    public void setSfxVolume()
    {
        sfxLabel.text = (sfxSlider.value + 80).ToString();

        theMixer.SetFloat("SfxVol", sfxSlider.value);

        PlayerPrefs.SetFloat("SfxVol", sfxSlider.value);
    }

    public void playSfxLoop()
    {
        sfxLoop.Play();
    }

    public void stopSfxLoop()
    {
        sfxLoop.Stop();
    }

    public void playSfx()
    {
        audio.Play();
    }
}

[System.Serializable]
public class ResItem
{
    public int horizontal, vertical;
}
