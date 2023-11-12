using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveData
{
    public float sfxVolume;
    public float bgmVolume;

    public List<string> controlNames = new List<string>();
    public List<string> controlButtons = new List<string>();
    public List<string> controlAltButtons = new List<string>();
}
