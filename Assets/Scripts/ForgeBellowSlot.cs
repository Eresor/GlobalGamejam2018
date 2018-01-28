using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForgeBellowSlot : WorkSlot
{

    public LoadableObjectScript OreLoad;
    public DropPlaceScript IronOutputLoad;
    public Animator BellowAnimator;
    private int bump;

    private AudioSource audioSource;
    public AudioClip pumpDownClip, pumpUpClip;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    void PumpDown()
    {
        audioSource.PlayOneShot(pumpDownClip);
    }
    void PumpUp()
    {
        audioSource.PlayOneShot(pumpUpClip);
    }

    public override void OnSuccess()
    {
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
    }

    public override void OnFail()
    {
        BellowAnimator.SetBool("DoWork", false);

    }
}
