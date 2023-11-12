using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeManager : MonoBehaviour
{
    private static VolumeManager instance;
    public static VolumeManager Instance
    {
        get { return instance; }
    }
    public static float sfxVolume { get; private set; }
    public static float bgmVolume { get; private set; }

    [SerializeField] private AudioMixer mixer;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }
    }
    private void OnEnable()
    {
        EventMessenger.StartListening("UpdateSFXVolume", UpdateSFXVolume);
        EventMessenger.StartListening("UpdateBGMVolume", UpdateBGMVolume);
        EventMessenger.StartListening("UpdateVolumeSliders", UpdateSliders);
    }
    private void OnDisable()
    {
        EventMessenger.StopListening("UpdateSFXVolume", UpdateSFXVolume);
        EventMessenger.StopListening("UpdateBGMVolume", UpdateBGMVolume);
        EventMessenger.StopListening("UpdateVolumeSliders", UpdateSliders);
    }
    public void UpdateSFXVolume()
    {
        sfxVolume = PrimitiveMessenger.floats["sfxVolume"];

        if (sfxVolume == 0)
        {
            sfxVolume = 0.001f;
        }
        UpdateMixer();
    }
    public void UpdateBGMVolume()
    {
        bgmVolume = PrimitiveMessenger.floats["bgmVolume"];

        if (bgmVolume == 0)
        {
            bgmVolume = 0.001f;
        }
        UpdateMixer();
    }
    private void UpdateMixer()
    {
        mixer.SetFloat("sfxVolume", Mathf.Log10(sfxVolume) * 20);
        mixer.SetFloat("bgmVolume", Mathf.Log10(bgmVolume) * 20);
    }
    public void UpdateSliders()
    {
        PrimitiveMessenger.floats["sfxVolume"] = sfxVolume;
        PrimitiveMessenger.floats["bgmVolume"] = bgmVolume;
        EventMessenger.TriggerEvent("SetSFXSliderValue");
        EventMessenger.TriggerEvent("SetBGMSliderValue");
    }
    public void SetVolumes(float newSFXVolume, float newBGMVolume)
    {
        sfxVolume = newSFXVolume;
        bgmVolume = newBGMVolume;

        UpdateSliders();
        UpdateMixer();
    }
}
