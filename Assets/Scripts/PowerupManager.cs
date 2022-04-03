using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class PowerupManager : MonoBehaviour
{
    [SerializeField] private GameObject powerupPrefab;
    [SerializeField] private List<PowerupSO> GamePowerups = new List<PowerupSO>();
    private Dictionary<PowerType, PowerupSO> powerups = new Dictionary<PowerType, PowerupSO>();
    private void Start()
    {
        BuildPowerupDict();
        EventManager.StartListening(Constants.ACTIVATEPOWERUP, ActivatePowerup);
        EventManager.StartListening(Constants.DROPPOWERUP, DropPowerup);
    }
    
    private void OnDestroy()
    {
        EventManager.StopListening(Constants.ACTIVATEPOWERUP, ActivatePowerup);
        EventManager.StopListening(Constants.DROPPOWERUP, DropPowerup);    
    }

    private void BuildPowerupDict()
    {
        for (int i = 0; i < GamePowerups.Count; i++)
        {
            powerups.Add(GamePowerups[i].Type,GamePowerups[i]);
        }
    }

    private void DropPowerup(Dictionary<string, object> obj)
    {
        Vector3 pos = (Vector3) obj[Constants.POSITION];
        PowerupSO randomPowerup = GamePowerups[Random.Range(0, GamePowerups.Count)];
        PoolManager.GetObjectFromPool(powerupPrefab, pos, Quaternion.identity, null).
            GetComponent<PowerUp_Ctl>().Setup(randomPowerup);
    }

    private void ActivatePowerup(Dictionary<string, object> obj)
    {
        PowerType type = (PowerType) obj[Constants.POWERUPTYPE];
        PowerupSO selected = powerups[type];

        switch (selected.Type)
        {
            case PowerType.ExtraBall:
                ExtraBall();
                break;
            default:
                // code block
                break;
        }
        
        GameObject gameobj = (GameObject) obj[Constants.GAMEOBJECT];
        
        PoolManager.ReturnObjectToPool(powerupPrefab.GetInstanceID(),gameobj);

    }

    private void ExtraBall()
    {
        EventManager.TriggerEvent(Constants.NEW_BALL);
    }
}
