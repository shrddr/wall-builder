using Assets.Scripts;
using UnityEngine;

public class WallController : MonoBehaviour
{
    private ControlsManager _controlsManager;
    private BoardManager _boardManager;
    private Collider2D _thisCollider;
    private SpriteRenderer _spriteRenderer;

    public LayerMask BlockingLayer;
    public Sprite WallActive;
    public Sprite WallPlaceholder;
    public Sprite WallSelected;
    public Sprite WallDisabled;

    public WallType WallType { get; set; }
    public bool IsActive { get; set; }
    public static WallPair WallsToPlace { get; set; }

    // Use this for initialization
    private void Start ()
    {
        _controlsManager = GameObject.Find("GameManager").GetComponent<ControlsManager>();
        _boardManager = GameObject.Find("GameManager").GetComponent<BoardManager>();
        _thisCollider = GetComponent<Collider2D>();
        _thisCollider.enabled = true;
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _spriteRenderer.enabled = false;
    }

    public void ActivateWall()
    {
        _spriteRenderer.enabled = true;
        _spriteRenderer.sprite = WallActive;
        IsActive = true;
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
        if (IsActive) return;

        if (WallType == WallType.Horizontal && _controlsManager.HorizontalWallDrag)
        {
            Vector2 start = transform.position;
            var endDirNormalized = new Vector2(-1, 0);
            Vector2 end = start + endDirNormalized;

            _thisCollider.enabled = false;
            var hitWallNextThis = Physics2D.Linecast(start, end, BlockingLayer);
            _thisCollider.enabled = true;

            var pairWall = hitWallNextThis.transform.gameObject;
            var x1 = transform.position.x;
            var y1 = transform.position.y;
            var x2 = pairWall.transform.position.x;
            var y2 = pairWall.transform.position.y;

            if (hitWallNextThis.transform != null &&
                !TargetIsActive(hitWallNextThis) &&
                _boardManager.CheckNewWall(false, x1, y1, x2, y2))
            {   
                pairWall.GetComponent<WallController>().Hightlight();
                _spriteRenderer.sprite = WallSelected;
                WallsToPlace = new WallPair { Wall1 = gameObject, Wall2 = pairWall };
            }
            else
            {
                _spriteRenderer.sprite = WallDisabled;
                WallsToPlace = null;
            }

            _spriteRenderer.enabled = true;
        }
        else if (WallType == WallType.Vertical && _controlsManager.VerticalWallDrag)
        {
            Vector2 start = transform.position;
            var endDirNormalized = new Vector2(0, 1);
            Vector2 end = start + endDirNormalized;

            _thisCollider.enabled = false;
            var hitWallNextThis = Physics2D.Linecast(start, end, BlockingLayer);
            _thisCollider.enabled = true;

            var pairWall = hitWallNextThis.transform.gameObject;
            var x1 = transform.position.x;
            var y1 = transform.position.y;
            var x2 = pairWall.transform.position.x;
            var y2 = pairWall.transform.position.y;

            if (hitWallNextThis.transform != null &&
                !TargetIsActive(hitWallNextThis) &&
                _boardManager.CheckNewWall(true, x1, y1, x2, y2))
            {
                pairWall.GetComponent<WallController>().Hightlight();
                _spriteRenderer.sprite = WallSelected;
                WallsToPlace = new WallPair {Wall1 = gameObject, Wall2 = pairWall};
            }
            else
            {
                _spriteRenderer.sprite = WallDisabled;
                WallsToPlace = null;
            }

            _spriteRenderer.enabled = true;               
        }
        else
        {
            _spriteRenderer.enabled = false;
        }
    }

    private void OnMouseExit()
    {
        if (WallsToPlace != null)
        {
            WallsToPlace.Wall2.GetComponent<WallController>().ShowPlaceholder();
            WallsToPlace = null;
        }
               
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
