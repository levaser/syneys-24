using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class VolumeSetting : MonoBehaviour
{
    [SerializeField] private Slider mainClipVolumeBar;
    [SerializeField] private Slider fXClipVolumeBar;
    [SerializeField] private AudioMixer myMixer;


    private void Start()
    {
        if (PlayerPrefs.HasKey("MainThemes"))
            LoadMainClipVolume();
        else                        
            SetMainClipVolume();

        if (PlayerPrefs.HasKey("SFX"))
            LoadFXClipVolume();
        else
            SetFXClipVolume();
    }
    public void SetMainClipVolume()
    {
        float volume = mainClipVolumeBar.value;
        myMixer.SetFloat("MainThemes", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("MainThemes", volume);
    }

    private void LoadMainClipVolume()
    {
        mainClipVolumeBar.value = PlayerPrefs.GetFloat("MainThemes");
        SetMainClipVolume();    
    }

    public void SetFXClipVolume()
    {
        float volume = fXClipVolumeBar.value;
        myMixer.SetFloat("SFX", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("SFX", volume);
    }

    private void LoadFXClipVolume()
    {
        fXClipVolumeBar.value = PlayerPrefs.GetFloat("SFX");
        SetFXClipVolume();
    }
}
