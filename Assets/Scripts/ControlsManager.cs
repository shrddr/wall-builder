using UnityEngine;

public class ControlsManager : MonoBehaviour
{
    private Player _activePlayer;
    private bool _keypressEnded = true;
    private PlayerManager _playerManager;

    public void Initialize()
    {
        _playerManager = GetComponent<PlayerManager>();
    }

    public void SetActivePlayer(Player player)
    {
        _activePlayer = player;
    }

	// Use this for initialization
    private void Start ()
    {
		
	}

    // Update is called once per frame
    private void Update()
    {
        var h = (int)Input.GetAxisRaw("Horizontal");
        var v = (int)Input.GetAxisRaw("Vertical");

        if (h == 0 && v == 0)
        {
            _keypressEnded = true;
            return;
        }

        if(!_keypressEnded)
            return;
        
        if (h != 0)
            v = 0;

        _keypressEnded = false;

        if(_activePlayer.AttemptMove(h, v))  
            _playerManager.EndPlayerTurn();
    }
}
