using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BellowEventScript : MonoBehaviour {


    private AudioSource audioSource;
    public AudioClip pumpDownClip, pumpUpClip;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    void PumpDown()
    {
        audioSource.PlayOneShot(pumpDownClip);
    }
    void PumpUp()
    {
        audioSource.PlayOneShot(pumpUpClip);
    }
}
