using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PowerUp_Ctl : MonoBehaviour
{
    private PowerupSO data;
    [SerializeField] private TMP_Text nameText;
    [SerializeField] private Rigidbody2D _rigidbody2D;
    
    public void Setup(PowerupSO powerUpData)
    {
        data = powerUpData;
        nameText.text = powerUpData.DisplayName;
        _rigidbody2D.velocity = new Vector2(0, -data.moveSpeed);
    }
    
    void OnCollisionEnter2D(Collision2D collider)
    {
        if (collider.gameObject.CompareTag(Constants.TAG_PLAYER))
        {
            Dictionary<string, object> eventData = new Dictionary<string, object>();
            eventData.Add(Constants.POWERUPTYPE, this.data.Type);
            eventData.Add(Constants.GAMEOBJECT, this.gameObject);

            EventManager.TriggerEvent(Constants.ACTIVATEPOWERUP, eventData);
        }
        else if (collider.gameObject.CompareTag(Constants.TAG_DEAD_AREA))
        {
            Dictionary<string, object> eventData = new Dictionary<string, object>();
            eventData.Add(Constants.GAMEOBJECT, this.gameObject);
            EventManager.TriggerEvent(Constants.POWERUPLOST, eventData);
        }
    }
}
