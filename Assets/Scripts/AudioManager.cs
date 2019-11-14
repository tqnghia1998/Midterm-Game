using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;    

public class AudioManager : MonoBehaviour
{
    public AudioMixer theMixer;

    // Start is called before the first frame update
    void Start()
    {
        // Restore audio state
        if (PlayerPrefs.HasKey("MasterVol"))
        {
            theMixer.SetFloat("MasterVol", PlayerPrefs.GetFloat("MasterVol"));
        }

        if (PlayerPrefs.HasKey("MusicVol"))
        {
            theMixer.SetFloat("MusicVol", PlayerPrefs.GetFloat("MusicVol"));
        }

        if (PlayerPrefs.HasKey("SfxVol"))
        {
            theMixer.SetFloat("SfxVol", PlayerPrefs.GetFloat("SfxVol"));
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
