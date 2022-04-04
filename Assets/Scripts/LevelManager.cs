using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;
    private static readonly float[] brickXPositions = {-1.95f, -1.3f, -0.65f, 0, 0.65f, 1.3f, 1.95f};
    private static readonly float[] brickYPositions = {2.375f, 2.13f, 1.885f, 1.64f, 1.395f, 1.15f,0.905f};
    [SerializeField] private GameObject brick;
    [SerializeField] private Transform _brickContainer;
    private LevelSO _currentLevel;
    private BrickDataSO _currentBricksDataSo;

    private List<GameObject> _bricksLeftInLevel = new List<GameObject>();
    
    void Awake()
    {
        if (Instance != null)
        {
            Destroy(this);
            return;
        }

        Instance = this;
    }

    void Start()
    {
        Application.targetFrameRate = 60; //Don't feel that more is necessary in this game, and will help saving battery
        
        EventManager.StartListening(Constants.BRICK_DESTROYED, BrickDestroyed);
        EventManager.StartListening(Constants.RESTART_LEVEL, RestartLevel);
    }

    private void OnDestroy()
    {
        EventManager.StopListening(Constants.BRICK_DESTROYED, BrickDestroyed);

    }

    public void StartLevel(LevelSO levelData, BrickDataSO brickDataSo)
    {
         _bricksLeftInLevel.Clear();
         _currentLevel = levelData;
         _currentBricksDataSo = brickDataSo;
         CreateLevelLayout();
    }

    private void RestartLevel(Dictionary<string, object> obj)
    {
        for (int i = 0; i < _bricksLeftInLevel.Count; i++)
        {
            PoolManager.ReturnObjectToPool(brick.GetInstanceID(),_bricksLeftInLevel[i]);
        }
        _bricksLeftInLevel.Clear();
        CreateLevelLayout();
    }
    
    private void CreateLevelLayout()
    {
        for (int i = 0; i < _currentLevel.LevelLayout.Length; i++)
        {
            for (int j = 0; j < _currentLevel.LevelLayout[i].rows.Length; j++)
            {
                for (int k = 0; k < _currentLevel.LevelLayout[i].rows[j].row.Length; k++)
                {
                    if (_currentLevel.LevelLayout[i].rows[j].row[k]!=0)
                    {
                        Brick_Ctl brickCtl = 
                            PoolManager.GetObjectFromPool(brick, 
                                new Vector3(brickXPositions[k], brickYPositions[j], 0),
                                Quaternion.identity, _brickContainer).GetComponent<Brick_Ctl>();
                        
                        brickCtl.SetUp(_currentBricksDataSo.BrickLevelsData[_currentLevel.LevelLayout[i].rows[j].row[k]-1]);
                        _bricksLeftInLevel.Add(brickCtl.gameObject);
                    }
                }
            }
        }
        
    }

    private void BrickDestroyed(Dictionary<string, object> obj)
    {
        GameObject gameObj = (GameObject)obj[Constants.GAMEOBJECT];
        PoolManager.ReturnObjectToPool(brick.GetInstanceID(), gameObj);

        _bricksLeftInLevel.Remove(gameObj);
        
        if(_bricksLeftInLevel.Count == 0)
            EventManager.TriggerEvent(Constants.LEVEL_COMPLETED);
    }
}
