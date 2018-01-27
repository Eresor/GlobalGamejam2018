﻿using Managers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackScript : MonoBehaviour
{
    public float radius;
    public float attackDelay = 1f;

    private PlayerController playerController;
    private float delayCounter = 1f;

    private bool XButtonPressed = false;
    private bool XButtonPressedLast = false;
    private bool BlastUpdateButtonPressed = false;


    // Use this for initialization
    void Start ()
    {
        playerController = GetComponentInParent<PlayerController>();

    }

    // Update is called once per frame
    void Update()
    {
        if (delayCounter > 0)
        {
            delayCounter -= Time.deltaTime;
        }
        else
        {
            XButtonPressed = InputManager.GetPlayerButtonDown(playerController.player, InputManager.Buttons.X);
        }
    }
    void FixedUpdate()
    {
        
        if (XButtonPressed )//&& !BlastUpdateButtonPressed && delayCounter <= 0) // && trzyma miecz //TODO
            if(!BlastUpdateButtonPressed && delayCounter <= 0)
        {

            //dodaj animację //TODO

            Ray ray = new Ray(transform.position, transform.position);
            RaycastHit[] hitTable = Physics.SphereCastAll(ray,radius);

            foreach (RaycastHit hit in hitTable)
            {
                if (hit.collider != null &&
                    hit.collider.transform != null &&
                    hit.collider.transform.parent != null
                    )
                {
                    GameObject collider = hit.collider.transform.parent.gameObject;
                    if (collider.name.Equals("Skeleton_LightSoldier"))
                    {
                        GameObject enemy = collider.transform.parent.gameObject;
                        enemy.GetComponent<EnemyController>().onHit();
                    }
                }
            }

            XButtonPressed = false;
            delayCounter = attackDelay;
        }

        BlastUpdateButtonPressed = XButtonPressed;
    }
}