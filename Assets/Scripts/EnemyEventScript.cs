using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyEventScript : MonoBehaviour {

    public AudioClip clipStep0, clipStep1, clipStep2, clipStep3;
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
        int whichStep = Random.Range(0, 4);
        switch (whichStep)
        {
            case 0:
                audioSource.PlayOneShot(clipStep0);
                break;
            case 1:
                audioSource.PlayOneShot(clipStep1);
                break;
            case 2:
                audioSource.PlayOneShot(clipStep2);
                break;
            case 3:
                audioSource.PlayOneShot(clipStep3);
                break;
        }
    }

    public void damageGate()
    {
        gateScript.hitGate();
        audioSource.volume = hitVolume;
        audioSource.PlayOneShot(clipHit);
    }
}
