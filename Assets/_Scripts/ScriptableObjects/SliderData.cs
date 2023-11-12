using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

[CreateAssetMenu(fileName = "SliderData", menuName = "ScriptableObjects/SliderData")]
public class SliderData : ScriptableObject
{
    public string sliderValueName;
    public string eventToTriggerOnValueChange;
    public string setSliderValueEventName;
}
