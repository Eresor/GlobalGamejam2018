using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructableObject : PickableObject
{

    public int Durability { get; protected set; }

    public void Use()
    {
        --Durability;
    }

}
