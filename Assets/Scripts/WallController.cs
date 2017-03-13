using Assets.Scripts;
using UnityEngine;

public class WallController : MonoBehaviour
{
    private ControlsManager _controlsManager;
    private Collider2D _thisCollider;
    private SpriteRenderer _spriteRenderer;

    public LayerMask BlockingLayer;
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

    public void Hightlight()
    {
        _spriteRenderer.enabled = true;
        _spriteRenderer.sprite = WallSelected;
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
                Vector2 start = transform.position;
                var endDirNormalized = new Vector2(0, 1);
                Vector2 end = start + endDirNormalized;

                _thisCollider.enabled = false;
                var hitWallNextThis = Physics2D.Linecast(start, end, BlockingLayer);
                _thisCollider.enabled = true;
                if (hitWallNextThis.transform != null && !TargetIsActive(hitWallNextThis))
                {
                    hitWallNextThis.transform.gameObject.GetComponent<WallController>().Hightlight();
                }

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

    private bool TargetIsActive(RaycastHit2D raycastHit2D)
    {
        var target = raycastHit2D.collider.gameObject.GetComponent<WallController>();

        return target == null || target.IsActive;
    }

    // Update is called once per frame
    private void Update ()
    {
		
	}
}
