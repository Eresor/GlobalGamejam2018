using System.Collections;
using System.Collections.Generic;
using ProgressBar;
using UnityEngine;

public class Wood : MonoBehaviour
{
    public float progress = 0.00f;

    public GameObject progressBar;
    public GameObject visibleObject;
    public GameObject woodIcon;
    public GameObject axeIcon;

    public GameObject spawnedWood = null;

    float timer= 0.0f;

    public void AddProgress()
    {
        if (timer >= 1)
        {
            var getRandomProcess = Random.Range(5, 15);
            progress += getRandomProcess;
            timer = 0;
        }

    }

    public Animator AxeMineAnimator;
    public Transform NewRocksSpawnTransform;
    public PickableObject.ObjectType MiningSlotType;

    public void SpawnWood()
    {
        var newStone = Instantiate(PrefabsProvider.Instance.WoodPrefab, NewRocksSpawnTransform);
        var randPos = 5 * UnityEngine.Random.onUnitSphere;
        randPos.y = 0;
        newStone.transform.localPosition = randPos;
        newStone.SetActive(true);
    }


    public void OnSuccess()
    {
        var newStone = Instantiate(
            MiningSlotType == PickableObject.ObjectType.ironOre
                ? PrefabsProvider.Instance.IronPrefab
                : PrefabsProvider.Instance.CoalPrefab, NewRocksSpawnTransform);
        var randPos = 20 * UnityEngine.Random.onUnitSphere;
        randPos.y = 0;
        newStone.transform.localPosition = randPos;
    }

    // Update is called once per frame
    void Update () {

        if (timer < 5)
        {
            timer += Time.deltaTime;
        }

	    
      //  Debug.Log(timer);

	    if (progress == 0.0f )
	    {
	        visibleObject.SetActive(false);
	        axeIcon.SetActive(true);
	    }
	    else if(progress > 0)
	    {
            visibleObject.SetActive(true);
	        axeIcon.SetActive(false);
        }
	    else if (progress >= 100 && timer > 4)
	    {
	        visibleObject.SetActive(false);
	        axeIcon.SetActive(true);
	    }


        progressBar.GetComponent<ProgressRadialBehaviour>().Value = progress;

	}
}
