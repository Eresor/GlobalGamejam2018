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

    public override bool CheckAdditionalWorkConditions(Collider other)
    {
        return OutputType == PickableObject.ObjectType.pickaxe
            ? WoodResources.objects.Count >= 2
            : WoodResources.objects.Count >= 1 && SteelResources.objects.Count >= 2;
    }

    public override void OnSuccess()
    {
        if (OutputType == PickableObject.ObjectType.pickaxe)
        {
            var wood = WoodResources.objects[0];
            WoodResources.objects.RemoveAt(0);
            Destroy(wood.gameObject);

            wood = WoodResources.objects[0];
            WoodResources.objects.RemoveAt(0);
            Destroy(wood.gameObject);

            Instantiate(PrefabsProvider.Instance.PickaxePrefab, OutputPlace.holdingSpot.transform);
        }
        else if (false)
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
        }
    }
}
