using System.Collections;
using System.Collections.Generic;
using Managers;
using UnityEngine;

public class MiningSlot : MonoBehaviour
{
    public Transform NewRocksSpawnTransform;
    public MaterialType MiningSlotType;

    void OnTriggerEnter(Collider other)
    {
        PlayerPickingScript pick = other.GetComponentInChildren<PlayerPickingScript>();
        if(!pick)
            return;

        PickableObject pickaxe;
        if(!pick.holdingObject || !(pickaxe = pick.holdingObject.GetComponent<PickableObject>()))
            return;

        if(pickaxe.objectType!=PickableObject.ObjectType.pickaxe)
            return;

        PlayerController pc = other.GetComponent<PlayerController>();

        if(!pc)
            return;

        QuickTimeEventManager.StartQuickTimeEventForPlayer((int)pc.player,transform.position,OnMiningSuccess,OnMiningFail);
    }

    void OnTriggerExit(Collider other)
    {
        QuickTimeEventManager.StopQuickTimeEventForPlayer(0);
    }

    void OnMiningSuccess()
    {
        var newStone = Instantiate(
            MiningSlotType == MaterialType.Iron
                ? PrefabsProvider.Instance.IronPrefab
                : PrefabsProvider.Instance.CoalPrefab, NewRocksSpawnTransform);
        newStone.transform.localPosition = UnityEngine.Random.onUnitSphere;
    }
    void OnMiningFail()
    {

    }
}

public enum MaterialType
{
    Iron,
    Wood
}