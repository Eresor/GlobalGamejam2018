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

	// Update is called once per frame
	void Update () {

	    if (progress == 0.00f && spawnedWood == null)
	    {
	        visibleObject.SetActive(false);
	        axeIcon.SetActive(true);
	        woodIcon.SetActive(false);
	    }
	    else if(progress >= 100f)
	    {
	        progressBar.GetComponent<ProgressRadialBehaviour>().Value = 0;
            visibleObject.SetActive(false);
	        axeIcon.SetActive(false);
	        woodIcon.SetActive(true);
        }
        else 
	    {
	        visibleObject.SetActive(true);
	        axeIcon.SetActive(false);
	        woodIcon.SetActive(false);
        }

	    progressBar.GetComponent<ProgressRadialBehaviour>().Value = progress;

	}
}
