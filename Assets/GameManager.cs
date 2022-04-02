using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    [SerializeField] private List<LevelSO> levels = new List<LevelSO>();
    [SerializeField] private List<BrickData> brickDatas = new List<BrickData>();


    //player stats
    private int _currentPlayerLevel;
    private int _playerScore;
    private int _playerLives = 3;
    private void Start()
    {
        LevelManager.Instance.StartLevel(levels[0],brickDatas[0]);
    }
}
