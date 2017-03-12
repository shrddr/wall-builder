using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    private ControlsManager _controlsManager;
    private Player _player1;
    private Player _player2;
    public RuntimeAnimatorController Player1Animator;
    public RuntimeAnimatorController Player2Animator;
    public GameObject Player;

    public void SetupPlayers()
    {
        _controlsManager = GetComponent<ControlsManager>();

        var player1Object = Instantiate(Player, new Vector3(0, 0, 0), Quaternion.identity);
        var animator1 = player1Object.GetComponent<Animator>();
        animator1.runtimeAnimatorController = Player1Animator;
        _player1 = player1Object.GetComponent<Player>();

        var player2Object = Instantiate(Player, new Vector3(2, 4, 0), Quaternion.identity);
        var animator2 = player2Object.GetComponent<Animator>();
        animator2.runtimeAnimatorController = Player2Animator;
        _player2 = player2Object.GetComponent<Player>();

        _controlsManager.SetActivePlayer(_player1);
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
