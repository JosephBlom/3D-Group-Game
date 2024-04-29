using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class MusicManager : MonoBehaviour
{
    public AudioSource audioSource;
    public List<AudioClip> soundtracks = new List<AudioClip>();
    public int currentClip = 0;
    public Slider volumeSlider;
    public float musicVolume;

    private void Update()
    {
        if (!audioSource.isPlaying)
        {
            playNextSong();
        }
    }

    void playNextSong()
    {
        currentClip++;
        if (currentClip > soundtracks.Count -1)
        {
            currentClip = 0;
        }
        audioSource.clip = soundtracks[currentClip];
        audioSource.Play();
    }

    public void SetVolume()
    {
        audioSource.volume = volumeSlider.value;
        musicVolume = volumeSlider.value;
    }
}
