using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioManager : MonoBehaviour
{
    public static AudioManager Current;
    public static AudioSource audioSource;

    private void Awake()
    {
        if (Current == null)
            Current = this;
        else
            Destroy(gameObject);

        audioSource = GetComponent<AudioSource>();
    }

    public static void PlayClip(AudioClip clip, float volume = 1, bool randomisePitch = true)
    {
        if (audioSource == null) { return; }

        if (randomisePitch)
            RandomisePitch();

        audioSource.PlayOneShot(clip, volume);
    }

    public static void RandomisePitch()
    {
        int[] pentatonicSemitones = new[] { 0, 2, 4, 7, 9 };
        int x = pentatonicSemitones[Random.Range(0, pentatonicSemitones.Length)];

        audioSource.pitch = Mathf.Pow(1.059463f, x);
    }
}
