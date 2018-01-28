using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SwordCollisionAttack : MonoBehaviour
{
    public AudioClip clip;
    private AudioSource audioSource;
    private TrailRenderer trail;
    public int Durability = 100;
    private float maxDur;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        trail = GetComponentInChildren<TrailRenderer>();
        maxDur = Durability;
        var grad = trail.colorGradient;
        grad.colorKeys = new GradientColorKey[] { new GradientColorKey(Color.green, 0f) };
        trail.colorGradient = grad;
    }
    void OnCollisionEnter(Collision col)
    {
        var other = col.collider;
        if (!other.transform.parent || !other.transform.parent.parent)
            return;

        EnemyController enemy = other.transform.parent.parent.GetComponent<EnemyController>();

        if (!enemy)
            return;

        if (enemy.HP<=0)
            return;

        --Durability;

        enemy.onHit();

        audioSource.PlayOneShot(clip);

        var grad = trail.colorGradient;
        grad.colorKeys = new GradientColorKey[] { new GradientColorKey(Color.Lerp(Color.red, Color.green, Durability / maxDur), 0f) };
        trail.colorGradient = grad;

        if (Durability>0)
            return;

        GetComponentInParent<PlayerPickingScript>().DestroyPick();
    }

}
