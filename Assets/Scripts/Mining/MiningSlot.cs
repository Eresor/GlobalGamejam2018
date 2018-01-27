using System.Collections;
using System.Collections.Generic;
using Managers;
using UnityEngine;

public class MiningSlot : WorkSlot
{
    public Transform NewRocksSpawnTransform;
    public PickableObject.ObjectType MiningSlotType;

    public override bool CheckAdditionalWorkConditions(Collider other)
    {
        PlayerPickingScript pick = other.GetComponentInChildren<PlayerPickingScript>();
        if (!pick)
            return false;

        PickableObject pickaxe;
        if (!pick.holdingObject || !(pickaxe = pick.holdingObject.GetComponent<PickableObject>()))
            return false;

        if (pickaxe.objectType != PickableObject.ObjectType.pickaxe)
            return false;

        return true;
    }

    public override void OnSuccess()
    {
        var newStone = Instantiate(
            MiningSlotType == PickableObject.ObjectType.ironOre
                ? PrefabsProvider.Instance.IronPrefab
                : PrefabsProvider.Instance.CoalPrefab, NewRocksSpawnTransform);
        var randPos = 20 * UnityEngine.Random.onUnitSphere;
        randPos.y = 0;
        newStone.transform.localPosition = randPos;
    }
    public override void OnFail()
    {

    }
}

//public enum MaterialType
//{
//    Iron,
//    Wood
//}