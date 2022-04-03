using System;
using System.Collections;
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
    private BrickData _currentBricksData;

    private int _bricksLeftInLevel;
    
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
        EventManager.StartListening(Constants.BRICK_DESTROYED, BrickDestroyed);

    }

    private void OnDestroy()
    {
        EventManager.StopListening(Constants.BRICK_DESTROYED, BrickDestroyed);

    }

    public void StartLevel(LevelSO levelData, BrickData brickData)
    {
        _bricksLeftInLevel = 0;
         _currentLevel = levelData;
         _currentBricksData = brickData;
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
                        BrickController brickController = 
                            PoolManager.GetObjectFromPool(brick, 
                                new Vector3(brickXPositions[k], brickYPositions[j], 0),
                                Quaternion.identity, _brickContainer).GetComponent<BrickController>();
                        
                        brickController.SetUp(_currentBricksData.BrickLevelsData[_currentLevel.LevelLayout[i].rows[j].row[k]-1]);
                        _bricksLeftInLevel++;
                    }
                }
            }
        }
        
    }

    private void BrickDestroyed(Dictionary<string, object> obj)
    {
        GameObject gameObject = (GameObject)obj[Constants.GAMEOBJECT];
        PoolManager.ReturnObjectToPool(brick.GetInstanceID(), gameObject);

        _bricksLeftInLevel--;
        
        if(_bricksLeftInLevel <=0)
            EventManager.TriggerEvent(Constants.LEVEL_COMPLETED);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
