using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour {

    public Transform target;
    public NavMeshAgent agent;
    public Animator anim;

    private bool isHit;
    private bool isDead;
    private bool isAttacking;
    private float countDown;
    private float deleteTime;

    // Use this for initialization
    void Start ()
    {
        isHit = false;
        isDead = false;
        countDown = 0;
        deleteTime = 4f;
    }

    public void onHit()
    {
        isHit = true;
        anim.SetBool("getDamage", isHit);
    }

    public void onDeath()
    {
        isDead = true;
    }

    public void startAttacking()
    {
        isAttacking = true;
    }

    // Update is called once per frame
    void Update () {
        ///TEST
        //if (Input.GetKeyDown(KeyCode.A)) onHit();
        //if (Input.GetKeyDown(KeyCode.S)) onDeath();
        //if (Input.GetKeyDown(KeyCode.Q)) onAttack();

        anim.SetBool("getDamage", isHit);
        if (isDead)
        {
            agent.isStopped = true;
            agent.acceleration = 100;
            anim.SetBool("die", true);
            deleteTime -= Time.deltaTime;
            if (deleteTime < 0)
                Destroy(gameObject);
        }
        else
        {
            if (isHit)
            {
                countDown = 0.9f;
                anim.SetBool("canRun", false);
            }
            else if (isAttacking)
            {
                anim.SetBool("attack", isAttacking);
            }

            if (countDown > 0)
            {
                agent.isStopped = true;
                agent.acceleration = 100;
                countDown -= Time.deltaTime;
                isHit = false;
            }
            else
            {
                anim.SetBool("canRun", true);
                agent.isStopped = false;
                agent.acceleration = 8;
                agent.SetDestination(target.position);
                anim.SetBool("getDamage", isHit);
            }
        }
        

        agent.updateRotation = true;
        agent.updatePosition = true;
        anim.SetBool("attack", isAttacking);
        anim.SetFloat("movementSpeed", agent.velocity.magnitude / agent.speed);
    }
}
