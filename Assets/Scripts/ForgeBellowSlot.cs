using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForgeBellowSlot : WorkSlot
{

    public LoadableObjectScript OreLoad;
    public DropPlaceScript IronOutputLoad;
    public Animator BellowAnimator;
    private int bump;

    public override void OnSuccess(int id)
    {
        base.OnSuccess(id);
        BellowAnimator.SetBool("DoWork", true);

        ++bump;
        if(bump < 4)
            return;

        bump = 0;

        if(OreLoad.objects.Count == 0)
            return;

        var ore = OreLoad.objects[0];
        OreLoad.objects.RemoveAt(0);
        Destroy(ore.gameObject);

        var steel = Instantiate(PrefabsProvider.Instance.SteelPrefab);
        //IronOutputLoad.objects.Add(steel);
        IronOutputLoad.holdingObject = steel;
        steel.transform.SetParent(IronOutputLoad.transform);
        //steel.transform.localPosition = IronOutputLoad.holdingSpots[IronOutputLoad.objects.Count].transform.localPosition;
        steel.transform.localPosition = IronOutputLoad.holdingSpot.transform.localPosition;
        steel.GetComponent<PickableObject>().alreadyUsed = false;
        QuickTimeEventManager.StopQuickTimeEventForPlayer(id);
    }

    public override void OnFail(int id)
    {
        base.OnSuccess(id);
        BellowAnimator.SetBool("DoWork", false);

    }
}
