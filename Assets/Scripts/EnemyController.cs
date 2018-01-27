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
    private Vector3 destination;

    // Use this for initialization
    void Start ()
    {
        isHit = false;
        isDead = false;
        countDown = 0;
        deleteTime = 4f;
        destination = target.position;
        destination.x = target.position.x + UnityEngine.Random.RandomRange(-9.0f,0.0f);
        destination.z -= 1;
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
                agent.SetDestination(destination);
                anim.SetBool("getDamage", isHit);
            }
        }

        if (!agent.pathPending)
        {
            if (agent.remainingDistance <= agent.stoppingDistance)
            {
                if (!agent.hasPath || agent.velocity.sqrMagnitude == 0f)
                {
                    // Done
                    
                    Vector3 dir = destination - transform.position;
                    dir.z += 1;
                    float angle = Mathf.Atan2(dir.x, dir.z) * Mathf.Rad2Deg;
                    transform.rotation = Quaternion.AngleAxis(angle, Vector3.up);
                    startAttacking();
                }
            }
        }

        agent.updateRotation = true;
        agent.updatePosition = true;
        anim.SetBool("attack", isAttacking);
        anim.SetFloat("movementSpeed", agent.velocity.magnitude / agent.speed);
    }
}
