using UnityEngine;

public class PickableObject : MonoBehaviour
{

    public enum ObjectType
    {
        steel,
        pickaxe,
        ironOre,
        wood,
        //coal
    }


    public bool alreadyUsed = false;

    [SerializeField] public ObjectType objectType = ObjectType.steel;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void Pick()
    {
        
    }
}
