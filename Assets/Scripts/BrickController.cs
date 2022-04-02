using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BrickController : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _renderer;
    private int _hitsLeft = 1;
    [SerializeField] private TMP_Text _hitsText;

    public void SetUp(BrickLevelData data)
    {
        _hitsLeft = data.Level;
        _hitsText.text = _hitsLeft.ToString();
        _renderer.color = data.Color;
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject.CompareTag(Constants.TAG_BALL))
            ProccessHit();
    }

    private void ProccessHit()
    {
        _hitsLeft--;    
        if(_hitsLeft<=0)
            Destroy(this.gameObject);
        else
            _hitsText.text = _hitsLeft.ToString();
        
    }
}
