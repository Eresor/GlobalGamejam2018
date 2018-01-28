using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class gateScript : MonoBehaviour {

    public static Transform gatePos;
    public GameObject gameUI;
    public GameObject gameOverCanvas;
    public GameObject generator;
    public Slider HpSlider;
    public GameObject blur;
    public GameObject[] players;


    private static Slider slider;

    public static float gateHP = 100;
    public static float startHp = 100;

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
        if (gateHP < 0)
        {
            //START ANIMATION AND END GAME;
            gameUI.SetActive(false);
            gameOverCanvas.SetActive(true);
            blur.SetActive(true);
            for (int i = 0; i < 3; i++)
            {
                players[i].SetActive(false);
            }
            Time.timeScale = 0;
            gateHP = startHp;
        }

    }
    public static void hitGate()
    {
        gateHP -= 1f;
        slider.value = gateHP;
    }
}
