using System;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public GameObject player;
    public GameObject ball;
    [SerializeField] private List<LevelSO> levels = new List<LevelSO>();
    [SerializeField] private List<BrickData> brickDatas = new List<BrickData>();


    //player stats
    private int _currentPlayerLevel = 1;
    private int _playerScore;
    private int _playerLives = 3;
    private int _ballsInPlay = 1;

    private void Start()
    {
        LevelManager.Instance.StartLevel(levels[_currentPlayerLevel-1], brickDatas[0]);
        SubscribeToEvents();
    }

    private void OnDestroy()
    {
        UnSubscribeToEvents();
    }
    
    private void SubscribeToEvents()
    {
        EventManager.StartListening(Constants.BRICK_DESTROYED, BrickDestroyed);
        EventManager.StartListening(Constants.BALL_DESTROYED, BallDestroyed);
        EventManager.StartListening(Constants.LEVEL_COMPLETED, EvaluateGameCompleted);
    }

    private void EvaluateGameCompleted(Dictionary<string, object> obj)
    {
        _currentPlayerLevel++;
        
        if(_currentPlayerLevel> levels.Count)
            Debug.Log("Won the game");
        else
        {
            LoadNextLevel();
        }
       
    }

    private void LoadNextLevel()
    {
        LevelManager.Instance.StartLevel(levels[_currentPlayerLevel-1], brickDatas[0]);
        
        Dictionary<string, object> eventData = new Dictionary<string, object>();
        eventData.Add(Constants.LEVEL, _currentPlayerLevel);
        EventManager.TriggerEvent(Constants.LEVEL_MODIFIED, eventData);    
    }

    private void UnSubscribeToEvents()
    {
        EventManager.StopListening(Constants.BRICK_DESTROYED, BrickDestroyed);
        EventManager.StopListening(Constants.BALL_DESTROYED, BallDestroyed);
    }
    
    private void BrickDestroyed(Dictionary<string, object> obj)
    {
        _playerScore += (int)obj[Constants.POINTS];
        Dictionary<string, object> eventData = new Dictionary<string, object>();
        eventData.Add(Constants.POINTS, _playerScore);
        EventManager.TriggerEvent(Constants.SCORE_MODIFIED, eventData);
        
    }
    
    private void BallDestroyed(Dictionary<string, object> obj)
    {
        GameObject ballObject = (GameObject)obj[Constants.GAMEOBJECT];
        PoolManager.ReturnObjectToPool(ball.GetInstanceID(), ballObject);

        LoseLive();

        _ballsInPlay--;
        if (_ballsInPlay <= 0)
        {
            Debug.Log("spawn new ball");
        } 
    }

    private void LoseLive()
    {
        _playerLives--;
        
        if (_playerLives == 0)
        {
            EventManager.TriggerEvent(Constants.GAMEOVER);

            GameOver();
        }
        else
        {
            Dictionary<string, object> eventData = new Dictionary<string, object>();
            eventData.Add(Constants.LIVES,_playerLives);
            EventManager.TriggerEvent(Constants.LIVES_MODIFIED, eventData);

        }
    }

    private void GameOver()
    {
        Debug.Log("GAMEOVER");
    }
    
    private void NewBall()
    {
        PoolManager.GetObjectFromPool(ball, Vector3.zero, Quaternion.identity, null);
        
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.O))
        {
            _ballsInPlay++;
            NewBall();
        }
    }
}
