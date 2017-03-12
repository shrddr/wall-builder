using UnityEngine;

public class Player : MonoBehaviour
{
    public LayerMask BlockingLayer;

    private Rigidbody2D _rb;

    // Use this for initialization
    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    public void Move(int xDir, int yDir)
    {
        Vector2 start = transform.position;
        Vector2 end = start + new Vector2(xDir, yDir).normalized;

        RaycastHit2D hit = Physics2D.Linecast(start, end, BlockingLayer);

        if (hit.transform == null)
        {
            _rb.MovePosition(end);
            CheckIfGameOver((int)end.y);
        }     
    }

    private void CheckIfGameOver(int newY)
    {
        if (newY == 4)
            GameManager.instance.GameOver();
    }
}
