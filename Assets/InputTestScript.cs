using System;
using Managers;
using UnityEngine;

public class InputTestScript : MonoBehaviour {

    #region MonoBehaviour Callbacks

    // Update is called once per frame
    void Update ()
	{

	    var playersControllers = Enum.GetValues(typeof(InputManager.Player));

	    var axises = Enum.GetValues(typeof(InputManager.Axis));

        var buttons = Enum.GetValues(typeof(InputManager.Buttons));

        foreach (InputManager.Player player in playersControllers)
	    {
	        foreach (InputManager.Axis axis in axises)
	        {
	            var value = InputManager.GetPlayerAxis(player, axis);
	            if (value > 0.2 || value < -0.2)
	            {
	               // Debug.Log(string.Format("Player {0}, Axis: {1}, Value: {2}", player, axis, value));
	            }          
            }

	        foreach (InputManager.Buttons button in buttons)
	        {
	            if (InputManager.GetPlayerButton(player, button))
	            {
	              //  Debug.Log(string.Format("Player {0}, Button {1}", player, button));
                }
	        }
        }
	}

    #endregion
}
