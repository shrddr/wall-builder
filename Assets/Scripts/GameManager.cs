using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    
    private BoardManager _boardManager;
    private PlayerManager _playerManager;
    private ControlsManager _controlsManager;
    private Text _scoreText;
    private int _score;

    // Awake is always called before any Start functions
    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);

        _boardManager = GetComponent<BoardManager>();
        _playerManager = GetComponent<PlayerManager>();
        _controlsManager = GetComponent<ControlsManager>();
    }

    private void OnLevelLoaded(Scene scene, LoadSceneMode mode)
    {
        InitGame();
    }

    private void InitGame()
    {
        _scoreText = GameObject.Find("ScoreText").GetComponent<Text>();
        _scoreText.text = "Score: " + _score;
        _controlsManager.Initialize();
        _boardManager.SetupScene();
        _playerManager.SetupPlayers();        
    }

    public void GameOver()
    {
        _score++;
        SceneManager.LoadScene(0);
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnLevelLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnLevelLoaded;
    }
}
