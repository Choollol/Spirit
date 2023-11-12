using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderUtil : MonoBehaviour
{
    [SerializeField] SliderData data;

    private Slider slider;
    private void Awake()
    {
        slider = GetComponent<Slider>();

        slider.onValueChanged.AddListener(SliderValueUpdate);
    }
    private void OnEnable()
    {
        EventMessenger.StartListening(data.setSliderValueEventName, SetSliderValue);
    }
    private void OnDisable()
    {
        EventMessenger.StopListening(data.setSliderValueEventName, SetSliderValue);
    }
    private void SetSliderValue()
    {
        if (slider == null)
        {
            slider = GetComponent<Slider>();
        }
        slider.value = PrimitiveMessenger.floats[data.sliderValueName];
    }
    private void SliderValueUpdate(float sliderValue)
    {
        PrimitiveMessenger.floats[data.sliderValueName] = slider.value;
        EventMessenger.TriggerEvent(data.eventToTriggerOnValueChange);
    }
}
