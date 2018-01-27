using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuickTimeEventTest : MonoBehaviour {

	// Use this for initialization
	void Start () {

        //Invoke("Test",1);

	}

    void Test()
    {
        QuickTimeEventManager.StartQucikTimeEventForPlayer(0,transform.position,()=>Debug.Log("Success"),()=>Debug.Log("Fail"));

    }

    // Update is called once per frame
    void Update () {
		
	}
}
