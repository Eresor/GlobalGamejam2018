using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyEventScript : MonoBehaviour {

    public AudioClip clipStep;
    public AudioClip clipHit;
    public float hitVolume, stepVolume;
    private AudioSource audioSource;

	// Use this for initialization
	void Start () {
        audioSource = GetComponent<AudioSource>();

    }
	
	// Update is called once per frame
	void Update () {
	}

    public void FootStep()
    {
        audioSource.volume = stepVolume;
        audioSource.PlayOneShot(clipStep);
    }

    public void damageGate()
    {
        gateScript.hitGate();
        audioSource.volume = hitVolume;
        audioSource.PlayOneShot(clipHit);
    }
}
