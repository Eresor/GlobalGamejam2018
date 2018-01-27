using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour {

    public Transform target;
    public NavMeshAgent agent;
    public Animator anim;

    public float HP = 3;

    private bool isHit;
    private bool isDead;
    private bool isAttacking;
    private float countDown;
    private float deleteTime;
    private Vector3 destination;

    public float enemyTargetWidth;

    // Use this for initialization
    void Start ()
    {
        isHit = false;
        isDead = false;
        countDown = 0;
        deleteTime = 4f;
        destination = target.position;
        destination.x += UnityEngine.Random.RandomRange(-enemyTargetWidth, enemyTargetWidth);
        destination.z -= 20;
    }

    public void onHit()
    {
        isHit = true;
        anim.SetBool("getDamage", isHit);
        HP--;
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
            //getting hit
            if (isHit)
            {
                countDown = 0.9f;
                anim.SetBool("canRun", false);
            }
            else if (isAttacking)
            {
                anim.SetBool("attack", isAttacking);
            }

            //being hit annimation and stop movement
            if (countDown > 0)
            {
                agent.isStopped = true;
                agent.acceleration = 100;
                countDown -= Time.deltaTime;
                isHit = false;
            }
            else
            //after taking dmg
            {
                anim.SetBool("canRun", true);
                agent.isStopped = false;
                agent.acceleration = 8;
                agent.SetDestination(destination);
                anim.SetBool("getDamage", isHit);
            }
        }

        //dojdzie do celu
        if (!agent.pathPending)
        {
            if (agent.remainingDistance <= agent.stoppingDistance)
            {
                if (!agent.hasPath || agent.velocity.sqrMagnitude <= 1f)
                {
                    // Done

                    agent.isStopped = true;
                    startAttacking();
                }
            }
        }

        if(agent.isStopped)
        {
            Vector3 dir = destination - transform.position;
            dir.z += 1;
            float angle = Mathf.Atan2(dir.x, dir.z) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.up);
        }

        if (HP <= 0)
        {
            onDeath();
        }

        agent.updateRotation = true;
        agent.updatePosition = true;
        anim.SetBool("attack", isAttacking);
        anim.SetFloat("movementSpeed", agent.velocity.magnitude / agent.speed);
    }
}
