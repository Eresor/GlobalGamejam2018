using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pickaxeAudioScript : MonoBehaviour {
    
    private AudioSource audioSource;
    public AudioClip clip;

    // Use this for initialization
    void Start () {
        audioSource = GetComponent<AudioSource>();
    }
	
	// Update is called once per frame
	void pickaxeHit () {
        audioSource.PlayOneShot(clip);
    }
}
