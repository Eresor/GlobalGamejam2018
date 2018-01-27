using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Managers;
using UnityEngine;
using UnityEngine.Experimental.UIElements;

public class PlayerPickingScript : MonoBehaviour
{
    private List<Collider> TriggerList = new List<Collider>();


    private PlayerController playerController;
    private GameObject collidingObject;
    private GameObject holdingObject;

    private GameObject holdingSpot;
    private void Start()
    {
        playerController = GetComponentInParent<PlayerController>();
        holdingSpot = gameObject.transform.parent.Find("HoldingSpot").gameObject;
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
            buttonPressed = false;
        }else if (buttonPressed && isHolding)
        {
            Drop();
            buttonPressed = false;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (!TriggerList.Contains(other))
        {
            if (other.CompareTag("PickableObject") || other.CompareTag("DropPlace") || other.CompareTag("UsableObject"))
            {
                //add the object to the list
                TriggerList.Add(other);
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (TriggerList.Contains(other))
        {
            if (other.CompareTag("PickableObject") || other.CompareTag("DropPlace") || other.CompareTag("UsableObject"))
            {
                //add the object to the list
                TriggerList.Remove(other);
            }
        }
    }


    private void Pick()
    {
        var getObject = TriggerList.FirstOrDefault(x => x.CompareTag("PickableObject"));
        if (getObject == null)
            return;

        TriggerList.Remove(getObject);
        isHolding = true;

        getObject.transform.position = holdingSpot.transform.position;
        getObject.GetComponent<Collider>().enabled = false;
        getObject.transform.SetParent(transform.parent);
        holdingObject = getObject.gameObject;
        Debug.Log("pick");
    }

    private void Drop()
    {
        var getObject = TriggerList.FirstOrDefault(x => x.CompareTag("DropPlace"));
        if (getObject == null)
            return;

        Debug.Log("drop");
        TriggerList.Remove(getObject);

        if (getObject.GetComponent<DropPlaceScript>().holdingObject != null)
        {
            return;
        }


        isHolding = false;
        getObject.GetComponent<DropPlaceScript>().holdingObject = holdingObject;

        holdingObject.transform.position = getObject.GetComponent<DropPlaceScript>().holdingSpot.transform.position;

        holdingObject.transform.parent = getObject.transform;

        holdingObject.GetComponent<Collider>().enabled = true;
        holdingObject = null;
    }
}
