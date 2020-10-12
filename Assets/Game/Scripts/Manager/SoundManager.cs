using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioSource mainSource;

    public void SetMusicVolume(float volume)
    {
        mainSource.volume = volume;
    }
}
