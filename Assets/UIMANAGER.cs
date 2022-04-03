using System;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
using UnityEngine;

public class UIMANAGER : MonoBehaviour
{
    [SerializeField] private GameObject inGamePanel;
    [SerializeField] private TMP_Text score;
    [SerializeField] private TMP_Text level;
    [SerializeField] private TMP_Text lives;

    private const string ScoreT = "SCORE: ";
    private const string LivesT = "LIVES: ";
    private const string LevelT = "LEVEL: ";
    
    private void Start()
    {
        SubscribeToEvents();
    }
    
    private void OnDestroy()
    {
        UnSubscribeToEvents();
    }

    private void SubscribeToEvents()
    {
        EventManager.StartListening(Constants.LEVEL_MODIFIED, UpdateLevel);
        EventManager.StartListening(Constants.LIVES_MODIFIED, UpdateLives);
        EventManager.StartListening(Constants.SCORE_MODIFIED, UpdateScore);
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

    private void UnSubscribeToEvents()
    {
        EventManager.StopListening(Constants.LEVEL_MODIFIED, UpdateScore);
    }
}
