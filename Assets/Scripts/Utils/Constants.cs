using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Constants
{

    //TAGs
    public const string TAG_BALL = "Ball";
    public const string TAG_PLAYER = "Player";
    public const string TAG_DEAD_AREA = "DeadArea";
    
    // Events

    public const string BRICK_DESTROYED = "BrickDestroyed";
    public const string BALL_DESTROYED = "BallDestroyed";
    public const string GAMEOVER = "GameOver";
    public const string LEVEL_MODIFIED = "LevelModified";
    public const string LIVES_MODIFIED = "LivesModified";
    public static string SCORE_MODIFIED = "ScoreModified";
    public static string LEVEL_COMPLETED = "LevelCompleted";
    public static string RESTART_LEVEL = "RestartLevel";
    public static string WON_GAME = "GameWon";
    public static string START_GAME = "StartGame";
    public static string NEW_BALL = "NewBall";
    public static string NEW_BALL_COUNTDOWN = "NewBallCountdown";
    public static string ACTIVATEPOWERUP = "ActivatePowerUp";
    public static string DROPPOWERUP = "DropPowerup";
    public static string LIVE_LOST = "LiveLost";


    //Event params
    public const string POINTS = "Points";
    public const string GAMEOBJECT = "GameOject";
    public static string LEVEL = "Level";
    public static string LIVES = "Lives";
    public static string POWERUPTYPE = "PowerupType";
    public static string POSITION = "Position";
}
