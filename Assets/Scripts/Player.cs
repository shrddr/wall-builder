using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public LayerMask BlockingLayer;
    public LayerMask PlayerLayer;

    private Rigidbody2D _rb;
    private Collider2D _thisCollider;
    private Animator _animator;

    // Use this for initialization
    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _thisCollider = GetComponent<Collider2D>();
        _animator = GetComponent<Animator>();
    }

    public void Highlight(bool active)
    {
        _animator.SetBool("Active", active);
    }

    /// <summary>
    /// Attempts to move with given vector. Returns false if movement blocked.
    /// </summary>
    /// <param name="xDir"></param>
    /// <param name="yDir"></param>
    /// <returns></returns>
    public bool AttemptMove(int xDir, int yDir)
    {
        Debug.ClearDeveloperConsole();
        Vector2 start = transform.position;
        var endDirNormalized = new Vector2(xDir, yDir).normalized;     
        Vector2 end = start + endDirNormalized;

        var hitWallNextToPlayer = Physics2D.Linecast(start, end, BlockingLayer);
        if (hitWallNextToPlayer.transform == null || !TargetIsActive(hitWallNextToPlayer))
        {
            _thisCollider.enabled = false;
            var hitPlayerNextToPlayer = Physics2D.Linecast(start, end, PlayerLayer);
            _thisCollider.enabled = true;

            if (hitPlayerNextToPlayer.transform == null)
            {
                _rb.MovePosition(end);
                return true;
            }

            var endWithJumpAheadEnemyPlayer = start + new Vector2(endDirNormalized.x * 2, endDirNormalized.y * 2);

            var hitWallNextToEnemyPlayer = Physics2D.Linecast(start, endWithJumpAheadEnemyPlayer, BlockingLayer);
            if (hitWallNextToEnemyPlayer.transform == null || !TargetIsActive(hitWallNextToEnemyPlayer))
            {
                _rb.MovePosition(endWithJumpAheadEnemyPlayer);
                return true;
            }           
        }

        return false;
    }

    private bool TargetIsActive(RaycastHit2D raycastHit2D)
    {
        var target = raycastHit2D.collider.gameObject.GetComponent<WallController>();

        return target == null || target.IsActive;
    }
}
