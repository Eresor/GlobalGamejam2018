using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AnvilSlot : WorkSlot
{
    public PickableObject.ObjectType OutputType;
    public LoadableObjectScript WoodResources;
    public LoadableObjectScript SteelResources;
    public DropPlaceScript OutputPlace;

    public AudioClip clip;
    private AudioSource audioSource;
    private int anvilBump;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public override bool CheckAdditionalWorkConditions(Collider other)
    {
        return OutputType == PickableObject.ObjectType.pickaxe
            ? WoodResources.objects.Count >= 2
            : WoodResources.objects.Count >= 1 && SteelResources.objects.Count >= 2;
    }

    public override void OnSuccess()
    {
        ++anvilBump;
        if(anvilBump < 4)
            return;

        anvilBump = 0;
        if (OutputType == PickableObject.ObjectType.pickaxe)
        {
            var wood = WoodResources.objects[0];
            WoodResources.objects.RemoveAt(0);
            Destroy(wood.gameObject);

            wood = WoodResources.objects[0];
            WoodResources.objects.RemoveAt(0);
            Destroy(wood.gameObject);

            var newObj = Instantiate(PrefabsProvider.Instance.PickaxePrefab);
            OutputPlace.holdingObject = newObj;
            newObj.transform.SetParent(OutputPlace.holdingSpot.transform);
            newObj.transform.localPosition = Vector3.zero;
            newObj.transform.localEulerAngles = Vector3.zero;

        }
        else if (OutputType == PickableObject.ObjectType.sword)
        {
            var wood = WoodResources.objects[0];
            WoodResources.objects.RemoveAt(0);
            Destroy(wood.gameObject);

            var steel = SteelResources.objects[0];
            SteelResources.objects.RemoveAt(0);
            Destroy(steel.gameObject);

            steel = SteelResources.objects[0];
            SteelResources.objects.RemoveAt(0);
            Destroy(steel.gameObject);


            var newObj = Instantiate(PrefabsProvider.Instance.SwordPrefab);
            OutputPlace.holdingObject = newObj;
            newObj.transform.SetParent(OutputPlace.holdingSpot.transform);
            newObj.transform.localPosition = Vector3.zero;
            newObj.transform.localEulerAngles = Vector3.zero;
        }
        audioSource.PlayOneShot(clip);
        //QuickTimeEventManager.StopQuickTimeEventForPlayer((int) pc.player);
    }
}
