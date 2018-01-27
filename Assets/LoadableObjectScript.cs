using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadableObjectScript : MonoBehaviour
{

    public PickableObject.ObjectType Type;

    public List<GameObject> holdingSpots = new List<GameObject>();

    public List<GameObject> objects = new List<GameObject>();

    public int maxHold = 3;

}
