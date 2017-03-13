using System;
using System.Linq;
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

        ShowPlaceholders(WallType.Horizontal);
    }

    private void DragVertical()
    {
        VerticalWallDrag = true;
        var mousePos = Input.mousePosition;
        var objectPos = Camera.main.ScreenToWorldPoint(mousePos);
        _dragableObject = Instantiate(VerticalDragable, objectPos, Quaternion.identity);

        ShowPlaceholders(WallType.Vertical);
    }

    private static void ShowPlaceholders(WallType wallType)
    {
        var walls = FindObjectsOfType<WallController>()
            .Where(wall => !wall.IsActive && wall.WallType == wallType)
            .ToList();

        foreach (var wall in walls)
        {
            wall.ShowPlaceholder();
        }
    }

    private static void HidePlaceholders()
    {
        var walls = FindObjectsOfType<WallController>()
            .Where(wall => !wall.IsActive)
            .ToList();

        foreach (var wall in walls)
        {
            wall.HidePlaceholder();
        }
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
            HidePlaceholders();

            if (WallController.WallsToPlace != null)
            {
                WallController.WallsToPlace.Wall1.GetComponent<WallController>().ActivateWall();
                WallController.WallsToPlace.Wall2.GetComponent<WallController>().ActivateWall();
                _playerManager.EndPlayerTurn();
            }
        }
    }
}
