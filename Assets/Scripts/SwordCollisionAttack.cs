using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordCollisionAttack : MonoBehaviour
{
    void OnCollisionEnter(Collision col)
    {
        var other = col.collider;
        if (!other.transform.parent || !other.transform.parent.parent)
            return;

        EnemyController enemy = other.transform.parent.parent.GetComponent<EnemyController>();

        if (!enemy)
            return;

        enemy.onHit();
    }

}
