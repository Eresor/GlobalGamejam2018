using System.Collections;
using System.Collections.Generic;
using Managers;
using UnityEngine;

public class MenuScript : MonoBehaviour
{
    private bool startChosen = true;
    public GameObject startSword;
    public GameObject endSword;
    public GameObject tutorialx;

    private bool tutorial = false;

    private bool changed = false;
    // Use this for initialization
    void Start () {
	}

    private bool wasPressed = false;
	// Update is called once per frame
	void Update () {
	    if (InputManager.GetPlayerAxis(InputManager.Player.P1, InputManager.Axis.Vertical) == 1 &&  !startChosen && !tutorial)
	    {
	        endSword.SetActive(false);
	        startSword.SetActive(true);
            startChosen = !startChosen;
        }
	    else if (InputManager.GetPlayerAxis(InputManager.Player.P1, InputManager.Axis.Vertical) == -1 && startChosen &&  !tutorial)
        {
	        endSword.SetActive(true);
	        startSword.SetActive(false);
            startChosen = !startChosen;
        }

        var isPressed = InputManager.GetPlayerButtonDown(InputManager.Player.P1, InputManager.Buttons.A);




        if (!wasPressed && isPressed && startChosen && !tutorial)
	    {
	        tutorialx.SetActive(true);
	        tutorial = true;
	    }
	    else if (!wasPressed && isPressed && !startChosen && !tutorial)
	    {
           GameManager.Instance.Exit();

	    }else if (!wasPressed && isPressed && tutorial)
	    {
	        GameManager.Instance.LoadGame();
        }
	    wasPressed = isPressed;

	}


}
