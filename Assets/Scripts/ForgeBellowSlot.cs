using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForgeBellowSlot : WorkSlot
{

    public LoadableObjectScript OreLoad;
    public LoadableObjectScript IronOutputLoad;
    public Animator BellowAnimator;
    public override void OnSuccess()
    {
        BellowAnimator.SetBool("DoWork",true);

        if(OreLoad.objects.Count == 0)
            return;

        var ore = OreLoad.objects[0];
        OreLoad.objects.RemoveAt(0);
        Destroy(ore.gameObject);

        var steel = Instantiate(PrefabsProvider.Instance.SteelPrefab);
        IronOutputLoad.objects.Add(steel);
        steel.transform.SetParent(IronOutputLoad.transform);
        steel.transform.localPosition = IronOutputLoad.holdingSpots[IronOutputLoad.objects.Count].transform.localPosition;
        steel.GetComponent<PickableObject>().alreadyUsed = false;
    }

    public override void OnFail()
    {
        BellowAnimator.SetBool("DoWork", false);

    }
}
