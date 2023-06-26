using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public List<AudioClip> tracks; // List of music tracks
    private AudioSource audioSource; // Audio source component
    private int currentTrack = 0; // Current track index

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        PlayNextTrack(); // Play the first track
    }

    public void OnButtonPress()
    {
        currentTrack++;
        if (currentTrack >= tracks.Count) // If we've passed the last track, stop the music
        {
            audioSource.Stop();
            currentTrack = -1; // Reset to -1, so the next press will play the first track
        }
        else
        {
            PlayNextTrack();
        }
    }

    private void PlayNextTrack()
    {
        audioSource.clip = tracks[currentTrack];
        audioSource.Play();
    }
}
