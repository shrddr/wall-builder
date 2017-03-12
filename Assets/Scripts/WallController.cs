using Assets.Scripts;
using UnityEngine;

public class WallController : MonoBehaviour
{
    private ControlsManager _controlsManager;
    private Collider2D _thisCollider;
    private SpriteRenderer _spriteRenderer;

    public Sprite WallActive;
    public Sprite WallPlaceholder;
    public Sprite WallSelected;

    public WallType WallType { get; set; }
    public bool IsActive { get; set; }

    // Use this for initialization
    private void Start ()
    {
        _controlsManager = GameObject.Find("GameManager").GetComponent<ControlsManager>();
        _thisCollider = GetComponent<Collider2D>();
        _thisCollider.enabled = true;
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _spriteRenderer.enabled = false;
    }

    public void ShowPlaceholder()
    {
        _spriteRenderer.enabled = true;
        _spriteRenderer.sprite = WallPlaceholder;
    }

    public void HidePlaceholder()
    {
        _spriteRenderer.enabled = false;        
    }

    private void OnMouseEnter()
    {
        if(!IsActive)
        {
            if (WallType == WallType.Horizontal && _controlsManager.HorizontalWallDrag)
            {
                _spriteRenderer.enabled = true;
                _spriteRenderer.sprite = WallSelected;
            }
            else if (WallType == WallType.Vertical && _controlsManager.VerticalWallDrag)
            {
                _spriteRenderer.enabled = true;
                _spriteRenderer.sprite = WallSelected;
            }
            else
            {
                _spriteRenderer.enabled = false;
            }
        }
    }

    private void OnMouseExit()
    {
        if (!IsActive)
        {
            if (WallType == WallType.Horizontal && _controlsManager.HorizontalWallDrag)
            {
                _spriteRenderer.enabled = true;
                _spriteRenderer.sprite = WallPlaceholder;
            }
            else if (WallType == WallType.Vertical && _controlsManager.VerticalWallDrag)
            {
                _spriteRenderer.enabled = true;
                _spriteRenderer.sprite = WallPlaceholder;
            }
            else
            {
                _spriteRenderer.enabled = false;
            }
        }
    }

    // Update is called once per frame
    private void Update ()
    {
		
	}
}
