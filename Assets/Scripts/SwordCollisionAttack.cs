using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SwordCollisionAttack : MonoBehaviour
{
    public AudioClip clip;
    private AudioSource audioSource;
    public int Durability = 100;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
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

        audioSource.PlayOneShot(clip);

        if (Durability>0)
            return;

        GetComponentInParent<PlayerPickingScript>().DestroyPick();

    }

}
