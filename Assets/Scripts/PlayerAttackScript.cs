using Managers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackScript : MonoBehaviour
{
    public float radius;

    private PlayerController playerController;

    private bool XButtonPressed = false;
    private bool XButtonPressedLast = false;
    private bool BlastUpdateButtonPressed = false;


    // Use this for initialization
    void Start ()
    {
        playerController = GetComponentInParent<PlayerController>();

    }
	
	// Update is called once per frame
	void Update () {
        XButtonPressed = InputManager.GetPlayerButtonDown(playerController.player, InputManager.Buttons.X);
    }
    void FixedUpdate()
    {
        
        if (XButtonPressed && !BlastUpdateButtonPressed) // && trzyma miecz
        {

            //dodaj animację

            Ray ray = new Ray(transform.position, transform.position);
            RaycastHit[] hitTable = Physics.SphereCastAll(ray,radius);

            foreach (RaycastHit hit in hitTable)
            {
                GameObject collider = hit.collider.transform.parent.gameObject;
                if (collider.name.Equals("Skeleton_LightSoldier"))
                {
                    GameObject enemy = collider.transform.parent.gameObject;
                    enemy.GetComponent<EnemyController>().onHit();
                }
            }

            BlastUpdateButtonPressed = XButtonPressed;
            XButtonPressed = false;

        }
        
    }
}
