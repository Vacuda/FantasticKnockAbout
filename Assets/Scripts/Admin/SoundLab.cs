using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundLab : MonoBehaviour
{
    AudioSource a_source;

    public AudioClip[] clip_array;

    private void Start()
    {
        a_source = gameObject.AddComponent<AudioSource>();  
    }

    public void PlaySound_Death()
    {
        a_source.PlayOneShot(clip_array[0]);
    }

    public void PlaySound_Fanfare()
    {
        a_source.PlayOneShot(clip_array[1]);
    }

    public void PlaySound_Hit()
    {
        a_source.PlayOneShot(clip_array[2], Random.Range(0.1f, 0.5f));
    }

    public void PlaySound_Pause()
    {
        a_source.PlayOneShot(clip_array[3]);
    }

    public void PlaySound_Grab()
    {
        a_source.PlayOneShot(clip_array[4]);
    }

    public void PlaySound_Release()
    {
        a_source.PlayOneShot(clip_array[5]);
    }

    public void PlaySound_GameOver()
    {
        a_source.PlayOneShot(clip_array[6]);
    }
}
