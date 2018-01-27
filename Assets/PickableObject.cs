using UnityEngine;

public class PickableObject : MonoBehaviour
{

    public enum ObjectType
    {
        iron,
        pickaxe
    }

    [SerializeField] public ObjectType objectType = ObjectType.iron;

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
