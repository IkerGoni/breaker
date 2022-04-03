using System;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public GameObject player;
    public GameObject ball;
    private List<GameObject> ballsInGame = new List<GameObject>();
    [SerializeField] private List<LevelSO> levels = new List<LevelSO>();
    [SerializeField] private List<BrickData> brickDatas = new List<BrickData>();

    [Header("Containers")] 
    [SerializeField] private Transform ballContainer;

    //player stats
    private int _currentPlayerLevel = 1;
    private int _playerScore;
    private int _playerLives = 3;
    private int _ballsInPlay = 0;

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
        EventManager.StartListening(Constants.BRICK_DESTROYED, BrickDestroyed);
        EventManager.StartListening(Constants.BALL_DESTROYED, BallDestroyed);
        EventManager.StartListening(Constants.LEVEL_COMPLETED, EvaluateGameCompleted);
        EventManager.StartListening(Constants.START_GAME, Starlevel);
        EventManager.StartListening(Constants.RESTART_LEVEL, RestartLevel);
        EventManager.StartListening(Constants.NEW_BALL, NewBall);
    }

    private void Starlevel(Dictionary<string, object> obj)
    {
        LevelManager.Instance.StartLevel(levels[_currentPlayerLevel-1], brickDatas[0]);
    }

    private void RestartLevel(Dictionary<string, object> obj)
    {
        ModifyLives(+3);
    }

    private void EvaluateGameCompleted(Dictionary<string, object> obj)
    {
        _currentPlayerLevel++;
        
        if(_currentPlayerLevel> levels.Count)
            PlayerWins();
        else
            LoadNextLevel();
    }

    private void PlayerWins()
    {
        EventManager.TriggerEvent(Constants.WON_GAME);
        for (int i = 0; i < ballsInGame.Count; i++)
        {
            PoolManager.ReturnObjectToPool(ball.GetInstanceID(),ballsInGame[i]);
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
        
        _ballsInPlay--;
        if (_ballsInPlay <= 0)
        {
            ModifyLives(-1);
        } 
    }

    private void ModifyLives(int livesToAdd)
    {
        _playerLives += livesToAdd;
        
        Dictionary<string, object> eventData = new Dictionary<string, object>();
        eventData.Add(Constants.LIVES,_playerLives);
        EventManager.TriggerEvent(Constants.LIVES_MODIFIED, eventData);
        
        if(livesToAdd == -1)
            EventManager.TriggerEvent(Constants.LIVE_LOST);

        if (_playerLives == 0)
        {
            EventManager.TriggerEvent(Constants.GAMEOVER);
            GameOver();
        }
        else
        {
            EventManager.TriggerEvent(Constants.NEW_BALL_COUNTDOWN);
        }
    }

    private void GameOver()
    {
        Debug.Log("GAMEOVER");
    }
    
    private void NewBall(Dictionary<string, object> obj = null)
    {
        _ballsInPlay++;
        ballsInGame.Add(PoolManager.GetObjectFromPool(ball, Vector3.zero, Quaternion.identity, ballContainer));
    }
}
