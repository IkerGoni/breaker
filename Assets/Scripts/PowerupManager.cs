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
    private List<GameObject> activePowerUpsGameObjects = new List<GameObject>();
    private void Start()
    {
        BuildPowerupDict();
        EventManager.StartListening(Constants.ACTIVATEPOWERUP, ActivatePowerup);
        EventManager.StartListening(Constants.DROPPOWERUP, DropPowerup);
        EventManager.StartListening(Constants.LIVE_LOST, RemoveAllActivePowerupObjects);
    }
    private void OnDestroy()
    {
        EventManager.StopListening(Constants.ACTIVATEPOWERUP, ActivatePowerup);
        EventManager.StopListening(Constants.DROPPOWERUP, DropPowerup);    
        EventManager.StopListening(Constants.LIVE_LOST, RemoveAllActivePowerupObjects);
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
        GameObject activePowerupObject = PoolManager.GetObjectFromPool(powerupPrefab, pos, Quaternion.identity, null);
        activePowerupObject.GetComponent<PowerUp_Ctl>().Setup(randomPowerup);
        activePowerUpsGameObjects.Add(activePowerupObject);
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
        activePowerUpsGameObjects.Remove(gameobj);
        PoolManager.ReturnObjectToPool(powerupPrefab.GetInstanceID(),gameobj);
    }
    private void RemoveAllActivePowerupObjects(Dictionary<string, object> obj)
    {
        for (int i = activePowerUpsGameObjects.Count-1; i >= 0; i--)
        {
            PoolManager.ReturnObjectToPool(powerupPrefab.GetInstanceID(),activePowerUpsGameObjects[i]);
            activePowerUpsGameObjects.RemoveAt(i);
        }
        
    }

    private void ExtraBall()
    {
        EventManager.TriggerEvent(Constants.NEW_BALL);
    }
}
