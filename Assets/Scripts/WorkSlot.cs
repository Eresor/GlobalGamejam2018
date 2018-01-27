using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkSlot : MonoBehaviour {


    void OnTriggerEnter(Collider other)
    {

        PlayerController pc = other.GetComponent<PlayerController>();

        if (!pc && other.transform.parent)
            pc = other.transform.parent.GetComponent<PlayerController>();

        if(!pc)
            return;

        if(!CheckAdditionalWorkConditions(other))
            return;

        QuickTimeEventManager.StartQuickTimeEventForPlayer((int)pc.player, transform.position, OnSuccess, OnFail);
    }

    public virtual bool CheckAdditionalWorkConditions(Collider other)
    {
        return true;
    }

    void OnTriggerExit(Collider other)
    {
        OnFail();
        QuickTimeEventManager.StopQuickTimeEventForPlayer(0);
    }

    public virtual void OnSuccess()
    {
    }

    public virtual void OnFail()
    {
    }
}
