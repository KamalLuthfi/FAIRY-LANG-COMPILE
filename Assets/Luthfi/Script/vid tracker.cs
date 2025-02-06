using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using UnityEngine.EventSystems;

public class VideoTrack : MonoBehaviour, IPointerDownHandler, IPointerUpHandler

{
    //1. Declare the variables
    public VideoPlayer video;
    Slider tracking;
    bool controller = false;

    public Slider volumeSlider;
    public AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {

        //2. Ensure volume slider reflects the initial volume
        volumeSlider.value = audioSource.volume;

        //3. Start the tracking function
        tracking = GetComponent<Slider>();

    }

    //4. Clickable controller (drag)
    public void OnPointerDown(PointerEventData a)
    {
        controller = true;

    }

    //5. Clickable controller (release)
    public void OnPointerUp(PointerEventData a)
    {
        //Track the position of the video
        float frame = (float)tracking.value * (float)video.frameCount;
        video.frame = (long)frame;
        controller = false;

    }

    // Update is called once per frame
    void Update()
    {
        //6. Control the tracker or slider
        if (!controller)
        {
            //Trace the tracker throughout the video
            tracking.value = (float)video.frame / (float)video.frameCount;
        }

    }

    //7. Volume Slider Control
    public void volume()
    {
        audioSource.volume = volumeSlider.value;
    }

}
