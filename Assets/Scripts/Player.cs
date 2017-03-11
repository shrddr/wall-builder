using UnityEngine;

public class Player : MonoBehaviour
{
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
        _rb.MovePosition(end);
        CheckIfGameOver();
    }

    private void CheckIfGameOver()
    {
        if ((int)_rb.position.y == 4)
            GameManager.instance.GameOver();
    }
}