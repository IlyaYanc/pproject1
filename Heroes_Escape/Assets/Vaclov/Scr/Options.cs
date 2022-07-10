using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class Options : MonoBehaviour
{
    float VM=1, VE=1;
    [SerializeField] private AudioMixerGroup Mixer;
    [SerializeField] private Slider MS, ES;
    public void LoadData(OptionsSavingData _data)
    {
        VM = _data.VM;
        VE = _data.VE;
        Mixer.audioMixer.SetFloat("MusicVolume", Mathf.Lerp(-50, 2, _data.VM));
        Mixer.audioMixer.SetFloat("EffectsVolume", Mathf.Lerp(-50, 0, _data.VE));
        MS.value = VM;
        ES.value = VE;
    }

    public OptionsSavingData SaveData()
    {
        //OptionsSavingData data = new OptionsSavingData(VM, VE);
        OptionsSavingData data = new OptionsSavingData(MS.value, ES.value);
        return data;
    }

    public void Exit()
    {
        gameObject.SetActive(false);
    }
    public void VolumeM(float volume)
    {
        Mixer.audioMixer.SetFloat("MusicVolume", Mathf.Lerp(-40, 2, volume));
        VM = volume;
        if (volume == 0) Mixer.audioMixer.SetFloat("MusicVolume", -80);
    }
    public void VolumeE(float volume)
    {
        Mixer.audioMixer.SetFloat("EffectsVolume", Mathf.Lerp(-40, 0, volume));
        VE = volume;
        if (volume == 0) Mixer.audioMixer.SetFloat("EffectsVolume" , - 80);
    }
    public void Volue(bool volume)
    {
        if (volume)
            Mixer.audioMixer.SetFloat("Volume", 0);
        if (!volume)
            Mixer.audioMixer.SetFloat("Volume", -80);
    }
}
public class OptionsSavingData
{
    public float VM;
    public float VE;

    public OptionsSavingData(float _VM, float _VE)
    {
        VM = _VM;
        VE = _VE;
    }
}
