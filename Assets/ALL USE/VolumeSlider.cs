using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeControl : MonoBehaviour
{
    //1. Declare the variables
    public Slider volumeSlider;
    public AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        //2. Set the initial volume of the slider
        volumeSlider.value = audioSource.volume;

        //3. Update the value of the audio following the slider

        volumeSlider.onValueChanged.AddListener(OnVolumeChange);

    }

    //4. Create a new method
    void OnVolumeChange(float volume)
    {
        //Update the volume of the audio source
        audioSource.volume = volume;
    }

}
