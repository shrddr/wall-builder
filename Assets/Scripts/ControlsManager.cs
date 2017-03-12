using UnityEngine;

public class ControlsManager : MonoBehaviour
{
    private Player _activePlayer;
    private bool _keypressEnded = true;

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

        if (h > 0) h = 1;
        if (h < 0) h = -1;
        if (v > 0) v = 1;
        if (v < 0) v = -1;

        _activePlayer.Move(h, v);       
    }
}
