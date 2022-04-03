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
    private int _currentPlayerLevel;
    private int _playerScore;
    private int _playerLives = 3;
    private int _ballsInPlay = 1;

    private void Start()
    {
        LevelManager.Instance.StartLevel(levels[0], brickDatas[0]);
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
    }
    private void UnSubscribeToEvents()
    {
        EventManager.StopListening(Constants.BRICK_DESTROYED, BrickDestroyed);
        EventManager.StopListening(Constants.BALL_DESTROYED, BallDestroyed);
    }
    
    private void BrickDestroyed(Dictionary<string, object> obj)
    {
        _playerScore += (int)obj[Constants.POINTS];
    }
    
    private void BallDestroyed(Dictionary<string, object> obj)
    {
        _ballsInPlay--;
        if (_ballsInPlay <= 0) //remove live restart level or gameover
            LoseLive();
    }

    private void LoseLive()
    {
        _playerLives--;
        if (_playerLives == 0)
            GameOver();
        //else
        //    NewBall();
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
        if(Input.GetKeyDown(KeyCode.O))
            NewBall();
    }
}
