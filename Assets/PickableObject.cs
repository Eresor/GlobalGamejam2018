using UnityEngine;

public class PickableObject : MonoBehaviour
{

    public enum ObjectType
    {
        steel,
        pickaxe,
        ironOre,
        wood,
        sword,
        coal,
        any,
        axe,
    }


    public bool alreadyUsed = false;

    [SerializeField] public ObjectType objectType = ObjectType.steel;

    public AudioClip clip;
    private AudioSource audioSource;

    // Use this for initialization
    void Start () {
        audioSource = GetComponent<AudioSource>();

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Pick()
    {
        audioSource.PlayOneShot(clip);
    }

    public void Drop()
    {
        audioSource.PlayOneShot(clip);
    }
}
