using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour {
    [SerializeField] public List<PlatformScript> Platforms = new List<PlatformScript>();
    [SerializeField] public GameObject[] PlatformPrefabs; 
    [SerializeField] public PlayerController Player;

    [SerializeField] public TextMeshProUGUI HighScoresText;
    [SerializeField] public TextMeshProUGUI CurrentScore;
    [SerializeField] public TextMeshProUGUI GameScore;
    [SerializeField] public TextMeshProUGUI StartText;
    [SerializeField] public TextMeshProUGUI StartTimerText;

    private float startTimer = 3f;
    private float starting = 0f;

    public enum GameState {
        HOMESCREEN, STARTING, PLAYING,
    }

    private GameState gameState = GameState.HOMESCREEN;

    public GameState State {
        get { return gameState; }
    }

    private float score;
    private int highScore;
    private int lastScore;

    private void Awake() {
        gameState = GameState.HOMESCREEN;
    }

    private void OnEnable() {
        Player.gameObject.SetActive(false);
        
        HighScoresText.gameObject.SetActive(true);
        CurrentScore.gameObject.SetActive(true);
        GameScore.gameObject.SetActive(false);
        StartText.gameObject.SetActive(true);
        StartTimerText.gameObject.SetActive(false);
        
        change_platform_state(false);
    }

    private void Update() {
        if (Input.GetKey(KeyCode.Space)) {
            AddScore(2 * Time.deltaTime);
        }
        
        switch (gameState) {
            case GameState.HOMESCREEN:
                GameStateHomescreen();
                break;
            case GameState.STARTING:
                GameStateStarting();
                break;
            case GameState.PLAYING:
                GameStatePlaying();
                break;
            default:
                //agggggghhhhhhh
                break;
        }
    }

    private void GameStateHomescreen() {
        if (Input.anyKeyDown) {
            HighScoresText.gameObject.SetActive(false);
            CurrentScore.gameObject.SetActive(false);
            GameScore.gameObject.SetActive(true);
            StartText.gameObject.SetActive(false);
            StartTimerText.gameObject.SetActive(true);
            StartTimerText.text = "0";
            
            gameState = GameState.STARTING;
            starting = startTimer;

            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
    }

    private void GameStateStarting() {
        starting -= Time.deltaTime;
        StartTimerText.text = "Starting in: " + starting.ToString("F0");
        GameScore.text = "Current score: " + score.ToString("F0");
        if (starting < 0) {
            gameState = GameState.PLAYING;
            StartTimerText.gameObject.SetActive(false);
            
            Player.gameObject.SetActive(true);
            change_platform_state(true);
            
        }
    }

    private void GameStatePlaying() {
        if (Input.GetKeyDown(KeyCode.Y)) {
            Dye();
        }      
    }

    public void AddScore(float amount = 1) {
        score += amount;
    }

    public void Dye() {
        lastScore = (int)score;
        if (score > highScore) {
            highScore = (int)score;
        }
        score = 0;

        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        gameState = GameState.HOMESCREEN;
        
        HighScoresText.gameObject.SetActive(true);
        CurrentScore.gameObject.SetActive(true);
        GameScore.gameObject.SetActive(false);
        StartText.gameObject.SetActive(true);
        StartTimerText.gameObject.SetActive(false);
        
        Player.gameObject.SetActive(false);
        change_platform_state(false);
    }

    void change_platform_state(bool state) {
        for (var i = 0; i < Platforms.Count; ++i) {
            if (Platforms[i] == null) {
                Platforms.RemoveAt(i);
            }
            else {
                Platforms[i].gameObject.SetActive(state);
            }
        }
    }
}
