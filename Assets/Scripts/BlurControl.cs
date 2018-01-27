using UnityEngine;
using System.Collections;
using Managers;

public class BlurControl : MonoBehaviour {
	
	float value; 
	
	// Use this for initialization
	void Start () {
		value = 2.4f;
		transform.GetComponent<Renderer>().material.SetFloat("_blurSizeXY",value);
	}

    void UpdateBlur(float value)
    {
        transform.GetComponent<Renderer>().material.SetFloat("_blurSizeXY", value);
    }

}
