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

    public void StopDrag()
    {
        HorizontalWallDrag = false;
        VerticalWallDrag = false;
        Destroy(_dragableObject);
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
        _dragableObject = Instantiate(HorizontalDragable, Input.mousePosition, Quaternion.identity);
    }

    private void DragVertical()
    {
        VerticalWallDrag = false;
        _dragableObject = Instantiate(VerticalDragable, Input.mousePosition, Quaternion.identity);
    }

    // Use this for initialization
    private void Start()
    {
    }

    // Update is called once per frame
    private void Update()
    {
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
}
