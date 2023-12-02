using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    private static AudioSource audioSource;

    private void Awake()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
    }

    public static void PlaySound(AudioClip soundClip)
    {
        audioSource.PlayOneShot(soundClip);
    }
}
