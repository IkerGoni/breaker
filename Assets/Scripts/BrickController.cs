using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BrickController : MonoBehaviour
{    
    [SerializeField] private TMP_Text _hitsText;
    [SerializeField] private SpriteRenderer _renderer;
    
    private int _hitsLeft = 1;
    private int _points;
    private float _powerupProb;

    public void SetUp(BrickLevelData data)
    {
        _hitsLeft = data.Level;
        _hitsText.text = _hitsLeft.ToString();
        _renderer.color = data.Color;
        _powerupProb = data.powerUpProb;
        _points = data.Points;
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject.CompareTag(Constants.TAG_BALL))
            ProccessHit();
    }

    private void ProccessHit()
    {
        _hitsLeft -= Ball_Ctl.ballPower;
        
        if (_hitsLeft <= 0)
            HandleBrickDestroy();
        else
            _hitsText.text = _hitsLeft.ToString();
    }

    private void HandleBrickDestroy()
    {
        if (Random.Range(1, 100) < _powerupProb)
        {
            Dictionary<string, object> eventData = new Dictionary<string, object>();
            eventData.Add(Constants.POSITION, this.transform.position);
            EventManager.TriggerEvent(Constants.DROPPOWERUP, eventData);
        }

        Dictionary<string, object> eventData1 = new Dictionary<string, object>();
        eventData1.Add(Constants.POINTS, _points);
        eventData1.Add(Constants.GAMEOBJECT, this.gameObject);
        EventManager.TriggerEvent(Constants.BRICK_DESTROYED, eventData1);
    }
}
