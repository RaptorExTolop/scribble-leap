using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour {
    [SerializeField] public PlatformScript []Platforms;
    [SerializeField] public GameObject []PlatformPrefabs; 
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

    private int score;
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
        
        foreach (var platform in Platforms) {
            platform.gameObject.SetActive(false);
        }
    }

    private void Update() {
        if (Input.GetKey(KeyCode.Space)) {
            AddScore(2);
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
        HighScoresText.gameObject.SetActive(true);
        CurrentScore.gameObject.SetActive(true);
        GameScore.gameObject.SetActive(false);
        StartText.gameObject.SetActive(true);
        StartTimerText.gameObject.SetActive(false);

        Debug.Log("HOMESCREEN");
        
        if (Input.anyKeyDown) {
            HighScoresText.gameObject.SetActive(false);
            CurrentScore.gameObject.SetActive(false);
            GameScore.gameObject.SetActive(true);
            StartText.gameObject.SetActive(false);
            StartTimerText.gameObject.SetActive(true);
            StartTimerText.text = "0";
            
            gameState = GameState.STARTING;
            starting = startTimer;
        }
    }

    private void GameStateStarting() {
        Debug.Log("STARTING THE GAME");
        starting -= Time.deltaTime;
        StartTimerText.text = "Starting in: " + starting.ToString("F0");
        GameScore.text = "Current score: " + score.ToString("F0");
        if (starting < 0) {
            gameState = GameState.PLAYING;
        }
    }

    private void GameStatePlaying() {
        Debug.Log("Your mum");
    }

    public void AddScore(int amount = 1) {
        score += amount;
    }

    public void Dye() {
        lastScore = score;
        if (score > highScore) {
            highScore = score;
        }
        score = 0;
    }
}
