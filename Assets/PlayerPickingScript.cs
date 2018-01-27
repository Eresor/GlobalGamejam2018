using System.Collections;
using System.Collections.Generic;
using Managers;
using UnityEngine;
using UnityEngine.Experimental.UIElements;

public class PlayerPickingScript : MonoBehaviour
{

    enum state
    {
        notColliding,
        collidingWithPickable,
        collidingWithDrop,
    }


    private PlayerController playerController;
    private state currentState = state.notColliding;

    private void Start()
    {
        playerController = GetComponentInParent<PlayerController>();
    }

    private bool buttonPressed = false;
    private bool isHolding = false;

    void Update()
    {
        buttonPressed = InputManager.GetPlayerButton(playerController.player, InputManager.Buttons.B);
    }

    void FixedUpdate()
    {
        if (buttonPressed && !isHolding)
        {
            Pick();
        }else if (buttonPressed && !isHolding)
        {
            Drop();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PickableObject"))
        {
            Debug.Log("collide");
            currentState = state.collidingWithPickable;
        }else if (other.CompareTag("DropPlace"))
        {
            
        }
    }

    void OnTriggerExit(Collider other)
    {
        Debug.Log("notCollide");
        currentState = state.notColliding;
    }





    private void Pick()
    {
        if (currentState == state.collidingWithPickable)
        {
            Debug.Log("Player" + playerController.player + " picking");
        }
    }

    private void Drop()
    {
        
    }
}
