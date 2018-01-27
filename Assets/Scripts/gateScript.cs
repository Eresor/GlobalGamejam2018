using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gateScript : MonoBehaviour {

    public static Transform gatePos;

    public static float gateHP = 20;

	// Use this for initialization
	void Start () {
        gatePos = transform;
	}

    void Update ()
    {
        if (gateHP < 0)
            Destroy(gameObject);
    }
    public static void hitGate()
    {
        gateHP -= 1f;
    }
}
