using Managers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackScript : MonoBehaviour
{
    public float radius = 3f;
    public float attackDelay = 1f;

    private PlayerController playerController;
    private float delayCounter = 1f;

    private bool AButtonPressed = false;
    private bool bumpAttack;
    private PickableObject sword;

    // Use this for initialization
    void Start()
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
            AButtonPressed = InputManager.GetPlayerButtonDown(playerController.player, InputManager.Buttons.A);
        }

        if (!AButtonPressed)
            return;

        sword = transform.parent.GetComponentInChildren<PickableObject>();
        if(!sword)
            return;

        if (sword.objectType != PickableObject.ObjectType.sword)
        {
            sword = null;
            return;
        }

        AButtonPressed = false;

        if(!bumpAttack)
            StopAllCoroutines();

        bumpAttack = true;
        StartCoroutine(CycylicAttack());
    }

    private IEnumerator CycylicAttack()
    {
        while (bumpAttack)
        {
            bumpAttack = false;
            Ray ray = new Ray(transform.position, transform.forward);

            HashSet<GameObject> hitSkeletons = new HashSet<GameObject>();
            float anglesPerFrame = 10f;
            Quaternion q = Quaternion.AngleAxis(anglesPerFrame, Vector3.up);
            Quaternion qsword = Quaternion.AngleAxis(anglesPerFrame, Vector3.forward);
            Vector3 d = playerController.transform.forward;

            for (float i = 0; i < 360; i += anglesPerFrame)
            {
                yield return null;
                d = q * d;
                if(sword==null)
                    yield break;    
                sword.transform.localRotation = sword.transform.localRotation * qsword;

                //Debug.DrawRay(transform.position, d * radius, Color.green, 5.0f);
                //RaycastHit[] hitTable = Physics.RaycastAll(ray, radius);
                //ray = new Ray(transform.position, transform.forward);
                //ray.direction = d;

                //foreach (RaycastHit hit in hitTable)
                //{
                //    if (hit.collider != null &&
                //        hit.collider.transform != null &&
                //        hit.collider.transform.parent != null
                //    )
                //    {
                //        GameObject collider = hit.collider.transform.parent.gameObject;
                //        if (collider.name.Equals("Skeleton_LightSoldier"))
                //        {
                //            GameObject enemy = collider.transform.parent.gameObject;
                //            bool dontHit = hitSkeletons.Contains(enemy);
                //            if (!dontHit) enemy.GetComponent<EnemyController>().onHit();
                //            hitSkeletons.Add(enemy);
                //        }
                //    }
                //}
            }
        }
    }
}
