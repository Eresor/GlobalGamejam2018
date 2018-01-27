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
    public GameObject holdingObject;


    private bool BlastUpdateButtonPressed = false;
    private GameObject holdingSpot;
    private void Start()
    {
        playerController = GetComponentInParent<PlayerController>();
        holdingSpot = gameObject.transform.parent.Find("HoldingSpot").gameObject;
    }

    private bool BbuttonPressed = false;
    private bool isHolding = false;


    private bool XButtonPressed = false;
    private bool XButtonPressedLast = false;

    void Update()
    {
        BbuttonPressed = InputManager.GetPlayerButtonDown(playerController.player, InputManager.Buttons.B);
        XButtonPressed = InputManager.GetPlayerButtonDown(playerController.player, InputManager.Buttons.X);
    }

    void FixedUpdate()
    {
        if (BbuttonPressed && !BlastUpdateButtonPressed)
        {
            if (!isHolding)
            {
                Pick();
                BbuttonPressed = false;
            }
            else if (isHolding)
            {
                Drop();
                BbuttonPressed = false;
            }
        }





       BlastUpdateButtonPressed = BbuttonPressed;
       XButtonPressedLast = XButtonPressed;
    }

    void OnTriggerEnter(Collider other)
    {
        if (!TriggerList.Contains(other))
        {
            if (other.CompareTag("PickableObject") || other.CompareTag("DropPlace") || other.CompareTag("UsableObject") || other.CompareTag("LoadableObject"))
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
            if (other.CompareTag("PickableObject") || other.CompareTag("DropPlace") || other.CompareTag("UsableObject") || other.CompareTag("LoadableObject"))
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

        if (getObject.GetComponent<PickableObject>().alreadyUsed)
        {
            return;
        }


        TriggerList.Remove(getObject);
        isHolding = true;

        getObject.transform.position = holdingSpot.transform.position;
        getObject.GetComponent<Collider>().enabled = false;

        if (getObject.transform.parent)
        {
            var dropPoint = getObject.transform.parent.GetComponent<DropPlaceScript>();

            if (dropPoint)
                dropPoint.holdingObject = null;
        }

        var rb = getObject.GetComponent<Rigidbody>();
        if (rb)
            rb.isKinematic = true;

        getObject.transform.SetParent(transform);
        holdingObject = getObject.gameObject;

        Debug.Log("pick");
    }

    private void Drop()
    {
        var getObject = TriggerList.FirstOrDefault(x => x.CompareTag("DropPlace"));
        if (getObject == null)
        {
            getObject = TriggerList.FirstOrDefault(x => x.CompareTag("LoadableObject"));
            if (getObject == null)
            {
                return;
            }
            DropToLoadable();
            return;
        }

        Debug.Log("drop");
        

        if (getObject.GetComponent<DropPlaceScript>().holdingObject != null)
        {
            return;
        }


        isHolding = false;



        getObject.GetComponent<DropPlaceScript>().holdingObject = holdingObject;

        holdingObject.transform.position = getObject.GetComponent<DropPlaceScript>().holdingSpot.transform.position;
        holdingObject.transform.rotation = getObject.GetComponent<DropPlaceScript>().holdingSpot.transform.rotation;

        holdingObject.transform.parent = getObject.transform;

        holdingObject.GetComponent<Collider>().enabled = true;
        holdingObject = null;

    }


    void DropToLoadable()
    {
        var getObject = TriggerList.FirstOrDefault(x => x.CompareTag("LoadableObject"));

        if (getObject.GetComponent<LoadableObjectScript>().objects.Count >= getObject.GetComponent<LoadableObjectScript>().maxHold)
        {
            return;
        }


        isHolding = false;


        getObject.GetComponent<LoadableObjectScript>().objects.Add(holdingObject);

        holdingObject.GetComponent<PickableObject>().alreadyUsed = true;
        holdingObject.transform.position = getObject.GetComponent<LoadableObjectScript>().holdingSpots[getObject.GetComponent<LoadableObjectScript>().objects.Count-1].transform.position;

        holdingObject.transform.parent = getObject.transform;

        holdingObject.GetComponent<Collider>().enabled = true;
        holdingObject = null;



        //////////// TUTAJ WYWOLA AKCJE JEZELI BEDZIE MAX PRZEDMiTów:OOOO

    }


}
