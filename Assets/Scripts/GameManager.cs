using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;
    
    private BoardManager _boardManager;
    private Text _scoreText;
    private int _score;

    // Awake is always called before any Start functions
    void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);

        _boardManager = GetComponent<BoardManager>();
    }

    void OnLevelLoaded(Scene scene, LoadSceneMode mode)
    {
        InitGame();
    }

    void InitGame()
    {
        _scoreText = GameObject.Find("ScoreText").GetComponent<Text>();
        _scoreText.text = "Score: " + _score;
        _boardManager.SetupScene();
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