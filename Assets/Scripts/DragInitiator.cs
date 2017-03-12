using Assets.Scripts;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragInitiator : MonoBehaviour, IPointerDownHandler
{
    private ControlsManager _controlsManager;

    public WallType WallType;

    public void OnPointerDown(PointerEventData ped)
    {
        Debug.LogWarning("Pointer down");
        _controlsManager.StartDrag(WallType);
    }

    // Use this for initialization
    private void Start()
    {
        _controlsManager = GameObject.Find("GameManager").GetComponent<ControlsManager>();
    }

    // Update is called once per frame
    private void Update()
    {

    }
}
