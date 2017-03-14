using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
    private ControlsManager _controlsManager;
    private BoardManager _boardManager;
    private Player _player1;
    private Player _player2;
    private GameObject _activePlayerText;

    private Player _activePlayer;
    private Player ActivePlayer
    {
        get { return _activePlayer; }
        set
        {
            _activePlayer = value;
            _activePlayerText.transform.position =  _activePlayer.transform.position;
        }
    }

    public string ActivePlayerText;
    public RuntimeAnimatorController Player1Animator;
    public RuntimeAnimatorController Player2Animator;
    public GameObject Player;
    public Vector3 Player1StartPosition;
    public Vector3 Player2StartPosition;



    public void SetupPlayers()
    {
        _activePlayerText = GameObject.Find("ActivePlayerText");
        _controlsManager = GetComponent<ControlsManager>();
        _boardManager = GetComponent<BoardManager>();

        var player1Object = Instantiate(Player, Player1StartPosition, Quaternion.identity);
        var animator1 = player1Object.GetComponent<Animator>();
        animator1.runtimeAnimatorController = Player1Animator;
        animator1.SetBool("Active", true);
        _player1 = player1Object.GetComponent<Player>();

        var player2Object = Instantiate(Player, Player2StartPosition, Quaternion.identity);
        var animator2 = player2Object.GetComponent<Animator>();
        animator2.runtimeAnimatorController = Player2Animator;
        animator2.SetBool("Active", false);
        _player2 = player2Object.GetComponent<Player>();

        ActivePlayer = _player1;
        _controlsManager.SetActivePlayer(_activePlayer);
    }

    public void EndPlayerTurn()
    {
        ActivePlayer = ActivePlayer == _player1 ? _player2 : _player1;
        _controlsManager.SetActivePlayer(ActivePlayer);

        _boardManager.UpdatePlayer(1, _player1.transform.position.x, _player1.transform.position.y);
        _boardManager.UpdatePlayer(2, _player2.transform.position.x, _player2.transform.position.y);

        _player1.Highlight(ActivePlayer == _player1);
        _player2.Highlight(ActivePlayer == _player2);

        if (ActivePlayer == _player1)
            SoundManager.instance.PlayGirl();
        else
            SoundManager.instance.PlayBoy();
    }

	// Use this for initialization
    private void Start ()
    {
		
	}
	
	// Update is called once per frame
    private void Update ()
    {
		
	}
}
