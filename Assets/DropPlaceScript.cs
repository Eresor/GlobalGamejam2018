using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropPlaceScript : MonoBehaviour
{
    public GameObject holdingObject = null;
    public GameObject holdingSpot;


    private void Start()
    {
        holdingSpot = gameObject.transform.Find("HoldingSpot").gameObject;
    }

}
