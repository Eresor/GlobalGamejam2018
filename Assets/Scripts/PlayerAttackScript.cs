using Managers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackScript : MonoBehaviour
{
    public float radius = 1f;
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

        if (XButtonPressed)
            // && gameObject.GetComponentInParent<PlayerPickingScript>().holdingObject.GetComponent<PickableObject>().objectType ==
            //PickableObject.ObjectType.sword)
            if (!BlastUpdateButtonPressed &&
                delayCounter <= 0)
            {

                //dodaj animację //TODO

                StartCoroutine(CycylicAttack());


                XButtonPressed = false;
                delayCounter = attackDelay;
            }

        BlastUpdateButtonPressed = XButtonPressed;
    }

    private IEnumerator CycylicAttack()
    {
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit[] hitTable = Physics.RaycastAll(ray, radius);

        HashSet<GameObject> hitSkeletons = new HashSet<GameObject>();
        Quaternion q = Quaternion.AngleAxis(1, Vector3.up);
        Vector3 d = transform.forward;

        for (int i = 0; i < 360; i++)
        {
            yield return new WaitForSeconds(0.002777778f);


            d = q * d;
            Debug.DrawRay(transform.position, d, Color.green, 5.0f);

            ray.direction=d;

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
                        bool dontHit = false;
                        foreach (GameObject skeleton in hitSkeletons)
                        {
                            if (skeleton == enemy) dontHit = true;
                        }
                        if (!dontHit) enemy.GetComponent<EnemyController>().onHit();
                        hitSkeletons.Add(enemy);
                    }
                }
            }
        }
    }
}
