using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManagerScript : MonoBehaviour
{
    public static GameManagerScript instance;

    public PlayerController playerController;
    public PlayerView playerView;

    private int score = 0;

    [SerializeField] private GameObject PreparePanel, MainGamePanel, FinishGamePanel;
    [SerializeField] private Text scoreTextMainGame,scoreTextGameOver,bestScoreText;
    [SerializeField] private GameObject medalSilver, medalGold, newBestPanel;
    public enum GameState
    {
        Prepare,
        MainGame,
        FinishGame,
    }
    private GameState _currentGameState;
    public GameState CurrentGameState
    {
        get { return _currentGameState; }
        set
        {
            switch (value)
            {
                case GameState.Prepare:
                    break;
                case GameState.MainGame:
                    break;
                case GameState.FinishGame:
                    break;
                default:
                    break;
            }
            _currentGameState = value;
        }
    }
    private void Awake()
    {
       // PlayerPrefs.SetInt("bestScore", 0);
        instance = this;
        _currentGameState = GameState.Prepare;
    }
    void Start()
    {
        PlayerPrefs.GetInt("bestScore", 0);
        playerController = new PlayerController(playerView);
    }
   
    void Update()
    {
        playerController.BirdWingAnimation();
        playerController.CheckEverytime();
        playerController.UpdatePlayerStartPos();

        switch (CurrentGameState)
        {
            case GameState.Prepare:
                PrepareGame();
                break;

            case GameState.MainGame:
               
                GamePlay();
                break;

            case GameState.FinishGame:
                GameOver();
                break;
            default:
                break;
        }

    }
    public void increaseScore(int score)
    {
        this.score += score;
    }
    IEnumerator GameOverShow()
    {
        yield return new WaitForSeconds(1f);
        
        PreparePanel.SetActive(false);
        FinishGamePanel.SetActive(true);
        MainGamePanel.SetActive(false);
        if (score == PlayerPrefs.GetInt("bestScore",0) && score != 0)
        {
            medalSilver.SetActive(false);
            medalGold.SetActive(true);
            newBestPanel.SetActive(true);
        }
        else
        {
            medalSilver.SetActive(true);
            medalGold.SetActive(false);
            newBestPanel.SetActive(false);
        }
        scoreTextGameOver.text = score.ToString();
        bestScoreText.text = PlayerPrefs.GetInt("bestScore", 0).ToString();
    }
    private void GameOver()
    {
        StartCoroutine("GameOverShow");
    }
    private void GamePlay()
    {
        PreparePanel.SetActive(false);
        FinishGamePanel.SetActive(false);
        MainGamePanel.SetActive(true);
        scoreTextMainGame.text = score.ToString();
        if (score > PlayerPrefs.GetInt("bestScore",0))
        {
            PlayerPrefs.SetInt("bestScore", score);
        }
    }
    private void PrepareGame()
    {
        score = 0;
        PreparePanel.SetActive(true);
        FinishGamePanel.SetActive(false);
        MainGamePanel.SetActive(false);
    }
    public void StartGame()
    {
        _currentGameState = GameState.MainGame;
        SoundManager.instance.PlaySound(SoundManager.instance.pageSound, 1f);

    }
    public void RestartGame()
    {
        StopCoroutine("GameOverShow");
        _currentGameState = GameState.Prepare;
        SoundManager.instance.PlaySound(SoundManager.instance.pageSound, 1f);


    }
}
