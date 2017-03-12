using UnityEngine;

public class DragableController : MonoBehaviour
{
    public Vector2 Offset;

    // Use this for initialization
    private void Start ()
    {

    }

    private void Update()
    {
        Vector2 cursorPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition + new Vector3(Offset.x, Offset.y)); //getting cursor position
        transform.position = cursorPosition;
    }
}
