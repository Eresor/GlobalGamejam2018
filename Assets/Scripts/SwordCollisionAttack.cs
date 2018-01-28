using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordCollisionAttack : MonoBehaviour
{
    public int Durability = 100;
    void OnCollisionEnter(Collision col)
    {
        var other = col.collider;
        if (!other.transform.parent || !other.transform.parent.parent)
            return;

        EnemyController enemy = other.transform.parent.parent.GetComponent<EnemyController>();

        if (!enemy)
            return;

        --Durability;

        enemy.onHit();

        if(Durability>0)
            return;

        GetComponentInParent<PlayerPickingScript>().DestroyPick();

    }

}
