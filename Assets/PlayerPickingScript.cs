using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using Managers;
using ProgressBar;
using UnityEngine;
using UnityEngine.Experimental.UIElements;

public class PlayerPickingScript : MonoBehaviour
{
    private List<Collider> TriggerList = new List<Collider>();

    public GameObject woodPrefab;
    public GameObject woodSpawner;

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


    private bool AButtonPressed = false;
    private bool AButtonPressedLast = false;

    void Update()
    {
        BbuttonPressed = InputManager.GetPlayerButtonDown(playerController.player, InputManager.Buttons.B);
        AButtonPressed = InputManager.GetPlayerButtonDown(playerController.player, InputManager.Buttons.A);



    }

    void FixedUpdate()
    {


        CheckCollisions();

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

        if (AButtonPressed && !AButtonPressedLast)
        {
            if (!isHolding)
            {
                Use();
                BbuttonPressed = false;
            }
        }


        



        BlastUpdateButtonPressed = BbuttonPressed;
       AButtonPressedLast = AButtonPressed;
    }

    private void CheckCollisions()
    {

        for (int i = 0; i < this.TriggerList.Count; i++)
        {
            if(!TriggerList[i])
                continue;

            var glow = TriggerList[i].gameObject.GetComponent<ObjectGlow>();

            if (glow != null)
            {
                glow.Off();
            }
        }
        TriggerList.Clear();


            var colliders = Physics.OverlapSphere(transform.position, 7 ,1<<10);



           // Debug.Log(colliders.Length);

        foreach (var other in colliders)
        {
            if(!other)
                continue;

            if (other.CompareTag("PickableObject") || other.CompareTag("DropPlace") ||
                other.CompareTag("UsableObject") || other.CompareTag("LoadableObject"))
            {
                var glow = other.gameObject.GetComponent<ObjectGlow>();

                if (glow != null)
                {
                    glow.On();
                }

                //add the object to the list
                TriggerList.Add(other);
            }
        }

    }

    private void Use()
    {
        var getObject = TriggerList.FirstOrDefault(x => x.GetComponent<Wood>() != null);
        if (getObject == null)
        {
            return;
        }
        Debug.Log("znaleziono drewno");

        var wood = getObject.GetComponent<Wood>();
        if (wood.progress < 100)
        {
            wood.progress += 10;
            AudioSource audio = GetComponentInParent<AudioSource>();
            audio.Play();

            if (wood.progress == 100)
            {
               wood.spawnedWood = Instantiate(woodSpawner, woodSpawner.transform.parent,false);
                wood.spawnedWood.SetActive(true);

            }

        }
    }



    private void Pick()
    {
        var getObject = TriggerList.FirstOrDefault(x => x.CompareTag("PickableObject"));

        Debug.Log("picK");
        if (getObject == null)
            return;

        Debug.Log("POsitive");
        if (getObject.GetComponent<PickableObject>().alreadyUsed)
        {
            return;
        }


        var glow = getObject.gameObject.GetComponent<ObjectGlow>();

        if (glow != null)
        {
            glow.Off();
        }


        var wood = TriggerList.FirstOrDefault(x => x.GetComponent<Wood>() != null);
        if (wood == null)
        {
            return;
        }
        Debug.Log("znaleziono drewno");

        wood.GetComponent<Wood>().spawnedWood = null;



        TriggerList.Remove(getObject);
        isHolding = true;

        getObject.transform.position = holdingSpot.transform.position;
        getObject.GetComponent<Collider>().enabled = false;

        if (getObject.transform.parent)
        {
            var dropPoint = getObject.transform.parent.GetComponent<DropPlaceScript>();

            if(!dropPoint && getObject.transform.parent.parent!=null)
                dropPoint = getObject.transform.parent.parent.GetComponent<DropPlaceScript>();

            if (dropPoint)
                dropPoint.holdingObject = null;
        }

        var rb = getObject.GetComponent<Rigidbody>();
        if (rb)
            rb.isKinematic = true;

        getObject.transform.SetParent(transform);
        holdingObject = getObject.gameObject;

        //Debug.Log("pick");
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

        //Debug.Log("drop");
        

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

        if(getObject.GetComponent<LoadableObjectScript>().Type!=holdingObject.GetComponent<PickableObject>().objectType)
            return;


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
