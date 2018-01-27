using UnityEngine;

public class PickableObject : MonoBehaviour
{

    enum ObjectType
    {
        iron
    }

    [SerializeField] private ObjectType objectType = ObjectType.iron;

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
