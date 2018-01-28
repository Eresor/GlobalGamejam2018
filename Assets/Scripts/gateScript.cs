using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class gateScript : MonoBehaviour {

    public static Transform gatePos;

    public Slider HpSlider;

    private static Slider slider;

    public static float gateHP = 100;

	// Use this for initialization
	void Start () {
        gatePos = transform;
	    HpSlider.maxValue = gateHP;
	    HpSlider.minValue = 0;
	    HpSlider.value = gateHP;
	    slider = HpSlider;
	}

    void Update ()
    {
        //if (gateHP < 0)
            //START ANIMATION AND END GAME;
    }
    public static void hitGate()
    {
        gateHP -= 1f;
        slider.value = gateHP;
    }
}
