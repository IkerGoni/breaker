using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BrickController : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _renderer;
    private int _hitsLeft = 1;
    private int _points;
    [SerializeField] private TMP_Text _hitsText;

    public void SetUp(BrickLevelData data)
    {
        _hitsLeft = data.Level;
        _hitsText.text = _hitsLeft.ToString();
        _renderer.color = data.Color;
        _points = data.Points;
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject.CompareTag(Constants.TAG_BALL))
            ProccessHit();
    }

    private void ProccessHit()
    {
        _hitsLeft--;
        if (_hitsLeft <= 0)
            HandleBrickDestroy();
        else
            _hitsText.text = _hitsLeft.ToString();
    }

    private void HandleBrickDestroy()
    {
        Dictionary<string, object> eventData = new Dictionary<string, object>();
        eventData.Add(Constants.POINTS, _points);
        EventManager.TriggerEvent(Constants.BRICK_DESTROYED, eventData);
        
        //TODO use pooling
        Destroy(this.gameObject);
    }
}
