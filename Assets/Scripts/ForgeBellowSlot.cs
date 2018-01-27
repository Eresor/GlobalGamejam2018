using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForgeBellowSlot : WorkSlot
{

    public LoadableObjectScript OreLoad;

    public override void OnSuccess()
    {
        GetComponent<Animator>().SetBool("DoWork",true);
    }

    public override void OnFail()
    {
        GetComponent<Animator>().SetBool("DoWork", false);

    }
}
