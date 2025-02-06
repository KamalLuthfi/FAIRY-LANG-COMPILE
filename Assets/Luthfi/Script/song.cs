using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MP3Player : MonoBehaviour
{
    public AudioSource audioSource;
    public TextMeshProUGUI songTitleText;
    public TextMeshProUGUI artistNameText;
    public Image albumCoverImage;
    public Slider progressSlider; // Slider for audio progress
    public Slider volumeSlider;   // Slider for audio volume

    private int currentSongIndex = 0;

    // Assign audio clips, song titles, and album covers in the inspector
    public AudioClip[] audioClips;
    public string[] songTitles;
    public string[] artistNames;
    public Sprite[] albumCovers;

    void Start()
    {

        audioSource = GetComponent<AudioSource>();
        songTitleText.text = songTitles[currentSongIndex];
        artistNameText.text = artistNames[currentSongIndex];
        albumCoverImage.sprite = albumCovers[currentSongIndex]; // Set initial album cover
        PlaySong();
        progressSlider.maxValue = audioSource.clip.length; // Set maximum value of progress slider to audio length
        volumeSlider.value = audioSource.volume; // Set initial volume value

    }

    void Update()
    {
        // Update progress slider value based on audio playback position
        progressSlider.value = audioSource.time;
    }

    public void PlaySong()
    {
        audioSource.clip = audioClips[currentSongIndex];
        audioSource.Play();

    }

    public void PauseSong()
    {
        audioSource.Pause();

    }

    public void StopSong()
    {
        audioSource.Stop();
    }

    public void PlayNextSong()
    {
        currentSongIndex = (currentSongIndex + 1) % audioClips.Length;
        songTitleText.text = songTitles[currentSongIndex];
        artistNameText.text = artistNames[currentSongIndex];
        albumCoverImage.sprite = albumCovers[currentSongIndex];
        ResetSliders(); // Reset sliders when next song is played
        PlaySong();
        progressSlider.maxValue = audioSource.clip.length; // Set maximum value of progress slider to audio length
        volumeSlider.value = audioSource.volume; // Set initial volume value
    }

    private void ResetSliders()
    {
        // Reset progress slider to zero
        progressSlider.value = 0f;

        // Reset volume slider to default volume level
        volumeSlider.value = audioSource.volume;
    }

    public void PlayPreviousSong()
    {
        currentSongIndex = (currentSongIndex - 1 + audioClips.Length) % audioClips.Length;
        songTitleText.text = songTitles[currentSongIndex];
        artistNameText.text = artistNames[currentSongIndex];
        albumCoverImage.sprite = albumCovers[currentSongIndex];
        ResetSliders(); // Reset sliders when next song is played
        PlaySong();
        progressSlider.maxValue = audioSource.clip.length; // Set maximum value of progress slider to audio length
        volumeSlider.value = audioSource.volume; // Set initial volume value
    }

    public void OnProgressSliderValueChanged()
    {
        // Set audio playback position based on slider value
        audioSource.time = progressSlider.value;
    }

    public void OnVolumeSliderValueChanged()
    {
        // Set audio volume based on slider value
        audioSource.volume = volumeSlider.value;
    }


}