using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
using UnityEngine;

public class UIMANAGER : MonoBehaviour
{
    [Header("Buttons")]
    [SerializeField] private Button startGameButton;
    [SerializeField] private Button restartLevelButton;

    [Header("Panels")]

    [SerializeField] private GameObject inGamePanel;
    [SerializeField] private GameObject startGamePanel;
    [SerializeField] private GameObject wonGamePanel;
    [SerializeField] private GameObject gameOverPanel;
    [Header("TextFields")]
    [SerializeField] private TMP_Text score;
    [SerializeField] private TMP_Text level;
    [SerializeField] private TMP_Text lives;
    [SerializeField] private TMP_Text countdownText;

    private const string ScoreT = "SCORE: ";
    private const string LivesT = "LIVES: ";
    private const string LevelT = "LEVEL: ";
    
    WaitForSeconds wait1Seconds = new WaitForSeconds(1);
    private void Start()
    {
        SubscribeToEvents();
        
        startGameButton.onClick.AddListener(StartClicked);
        restartLevelButton.onClick.AddListener(RestartClicked);
    }
    
    private void OnDestroy()
    {
        UnSubscribeToEvents();
        
        startGameButton.onClick.RemoveAllListeners();
        restartLevelButton.onClick.RemoveAllListeners();
    }

    private void SubscribeToEvents()
    {
        EventManager.StartListening(Constants.LEVEL_MODIFIED, UpdateLevel);
        EventManager.StartListening(Constants.LIVES_MODIFIED, UpdateLives);
        EventManager.StartListening(Constants.SCORE_MODIFIED, UpdateScore);
        EventManager.StartListening(Constants.GAMEOVER, GameOver);
        EventManager.StartListening(Constants.RESTART_LEVEL, RestartLevel);
        EventManager.StartListening(Constants.WON_GAME, WonGame);
        EventManager.StartListening(Constants.NEW_BALL_COUNTDOWN, DoCountdown);
            ;
    }

    private void UnSubscribeToEvents()
    {
        EventManager.StopListening(Constants.LEVEL_MODIFIED, UpdateScore);
        EventManager.StopListening(Constants.LIVES_MODIFIED, UpdateLives);
        EventManager.StopListening(Constants.SCORE_MODIFIED, UpdateScore);
        EventManager.StopListening(Constants.GAMEOVER, GameOver);
        EventManager.StopListening(Constants.RESTART_LEVEL, RestartLevel);
        EventManager.StopListening(Constants.WON_GAME, WonGame);
        EventManager.StopListening(Constants.NEW_BALL_COUNTDOWN, DoCountdown);
    }
    

    private void StartClicked()
    {
        Debug.Log("START CLICKED");
        startGamePanel.SetActive(false);
        Debug.Log("START CLICKED2");

        Dictionary<string, object> temp = new Dictionary<string, object>();
        EventManager.TriggerEvent(Constants.START_GAME,temp);
        Debug.Log("START CLICKED3");

        DoCountdown();
    }
    
    private void DoCountdown(Dictionary<string, object> obj = null)
    {
        
        Debug.Log("DoCountdown");
        StartCoroutine(StartCountdown());
    }
    
    private void RestartClicked()
    {
        EventManager.TriggerEvent(Constants.RESTART_LEVEL);
    }

    private void WonGame(Dictionary<string, object> obj)
    {
        wonGamePanel.SetActive(true);
    }

    private void RestartLevel(Dictionary<string, object> obj)
    {
        gameOverPanel.SetActive(false);
    }

    private void GameOver(Dictionary<string, object> obj)
    {
        gameOverPanel.SetActive(true);
    }

    private void UpdateLevel(Dictionary<string, object> obj)
    {
        level.text = LevelT + (int) obj[Constants.LEVEL];
    }

    private void UpdateScore(Dictionary<string, object> obj)
    {
        score.text = ScoreT + (int) obj[Constants.POINTS];
    }   
    private void UpdateLives(Dictionary<string, object> obj)
    {
        lives.text = LivesT + (int) obj[Constants.LIVES];
    }

    IEnumerator StartCountdown()
    {
        Debug.Log("DoCountdown");

        countdownText.gameObject.SetActive(true);
        countdownText.text = "3";
        yield return wait1Seconds;
        countdownText.text = "2";
        yield return wait1Seconds;
        countdownText.text = "1";
        yield return wait1Seconds;
        countdownText.gameObject.SetActive(false);
        EventManager.TriggerEvent(Constants.NEW_BALL);
    }
}
