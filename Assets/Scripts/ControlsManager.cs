using System;
using Assets.Scripts;
using UnityEngine;

public class ControlsManager : MonoBehaviour
{
    private Player _activePlayer;
    private bool _keypressEnded = true;
    private PlayerManager _playerManager;
    private GameObject _dragableObject;

    public GameObject HorizontalDragable;
    public GameObject VerticalDragable;


    public bool HorizontalWallDrag { get; private set; }
    public bool VerticalWallDrag { get; private set; }

    public void Initialize()
    {
        _playerManager = GetComponent<PlayerManager>();
    }

    public void SetActivePlayer(Player player)
    {
        _activePlayer = player;
    }

    public void StartDrag(WallType wallType)
    {
        switch (wallType)
        {
            case WallType.Vertical:
                DragVertical();
                break;
            case WallType.Horizontal:
                DragHorizontal();
                break;
            default:
                throw new ArgumentOutOfRangeException("wallType", wallType, null);
        }
    }

    private void DragHorizontal()
    {
        HorizontalWallDrag = true;
        var mousePos = Input.mousePosition;
        var objectPos = Camera.main.ScreenToWorldPoint(mousePos);
        _dragableObject = Instantiate(HorizontalDragable, objectPos, Quaternion.identity);
    }

    private void DragVertical()
    {
        VerticalWallDrag = true;
        var mousePos = Input.mousePosition;
        var objectPos = Camera.main.ScreenToWorldPoint(mousePos);
        _dragableObject = Instantiate(VerticalDragable, objectPos, Quaternion.identity);
    }

    // Use this for initialization
    private void Start()
    {
    }

    // Update is called once per frame
    private void Update()
    {
        TryStopDrag();

        var h = (int) Input.GetAxisRaw("Horizontal");
        var v = (int) Input.GetAxisRaw("Vertical");

        if (h == 0 && v == 0)
        {
            _keypressEnded = true;
            return;
        }

        if (!_keypressEnded)
            return;

        if (h != 0)
            v = 0;

        _keypressEnded = false;

        if (_activePlayer.AttemptMove(h, v))
            _playerManager.EndPlayerTurn();
    }

    private void TryStopDrag()
    {
        if (Input.GetMouseButtonUp(0) && (HorizontalWallDrag || VerticalWallDrag))
        {
            HorizontalWallDrag = false;
            VerticalWallDrag = false;
            Destroy(_dragableObject);
        }
    }
}
