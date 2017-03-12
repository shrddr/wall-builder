using UnityEngine;
using UnityEngine.EventSystems;

public class DragableController : MonoBehaviour, IPointerUpHandler
{
    private ControlsManager _controlsManager;
    private Vector3 startMousePos;
    private Vector3 startPos;

    public void OnPointerUp(PointerEventData eventData)
    {
        _controlsManager.StopDrag();
    }

    // Use this for initialization
    private void Start ()
    {
        _controlsManager = GameObject.Find("GameManager").GetComponent<ControlsManager>();
        startPos = transform.position;
        startMousePos = Input.mousePosition;
    }
	
	// Update is called once per frame
	private void Update ()
    {
        Vector3 currentPos = Input.mousePosition;
        Vector3 diff = currentPos - startMousePos;
        Vector3 pos = startPos + diff;
        transform.position = pos;
    }
}
