using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabsProvider : MonoBehaviour
{
    private static PrefabsProvider instance;

    public static PrefabsProvider Instance
    {
        get { return instance ?? (instance = FindObjectOfType<PrefabsProvider>()); }
    }

    public GameObject IronPrefab;
    public GameObject CoalPrefab;
    public GameObject SteelPrefab;
    public GameObject PickaxePrefab;
    public GameObject SwordPrefab;

}
