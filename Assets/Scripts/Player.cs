using UnityEngine;

public class Player : MonoBehaviour
{
    public LayerMask BlockingLayer;

    private Rigidbody2D _rb;

    // Use this for initialization
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        var h = (int)(Input.GetAxisRaw("Horizontal"));
        var v = (int)(Input.GetAxisRaw("Vertical"));
        if (h != 0)
            v = 0;
        if (h != 0 || v != 0)
            Move(h, v);
    }

    void Move(int xDir, int yDir)
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