using UnityEngine;

public class WallController : MonoBehaviour
{
    private ControlsManager _controlsManager;
    private Collider2D _thisCollider;

    public Sprite WallActive;
    public Sprite WallPlaceholder;
    public Sprite WallSelected;

    private void OnMouseEnter()
    {
        print(gameObject.name);
    }

    // Use this for initialization
    private void Start ()
    {
		_controlsManager = GameObject.Find("GameManager").GetComponent<ControlsManager>();
        _thisCollider = GetComponent<Collider2D>();
        _thisCollider.enabled = false;
    }
	
	// Update is called once per frame
    private void Update ()
    {
		
	}
}
