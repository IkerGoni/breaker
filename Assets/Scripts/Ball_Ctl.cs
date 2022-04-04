using System.Collections.Generic;
using UnityEngine;

public class Ball_Ctl : MonoBehaviour
{
    public static int ballPower = 1;
    public float MinY_velocity;
    [SerializeField] private Rigidbody2D _rigidbody2D;

    public float InitForce;
    // Start is called before the first frame update
    void OnEnable()
    {
        SetStartValues();
    }

    private void FixedUpdate ()
    {
        ForceMinYVelocity();
    }

    private void SetStartValues()
    {
        transform.position = Vector3.zero;
        _rigidbody2D.velocity = Vector2.zero;
        _rigidbody2D.AddForce(new Vector2(-1,-1)*InitForce);
    }
    
    void ForceMinYVelocity()
    {
        if (_rigidbody2D.velocity.y > 0 && _rigidbody2D.velocity.y < MinY_velocity)
            _rigidbody2D.velocity = new Vector2(_rigidbody2D.velocity.x,MinY_velocity);
        else if (_rigidbody2D.velocity.y < 0 && _rigidbody2D.velocity.y > -MinY_velocity)
            _rigidbody2D.velocity = new Vector2(_rigidbody2D.velocity.x, -MinY_velocity);
    }

    void OnCollisionEnter2D(Collision2D collider)
    {
        if (collider.gameObject.CompareTag(Constants.TAG_PLAYER))
        {
            float offset = collider.transform.position.x - transform.position.x;
            ModifyDirection(offset);
        }
        else if (collider.gameObject.CompareTag(Constants.TAG_DEAD_AREA))
        {
            Dictionary<string, object> eventData = new Dictionary<string, object>();
            eventData.Add(Constants.GAMEOBJECT, this.gameObject);
            EventManager.TriggerEvent(Constants.BALL_DESTROYED, eventData);
        }
    }

    void ModifyDirection(float offSetToPaddle)
    {
        if (offSetToPaddle > 0.15f)
            _rigidbody2D.velocity = new Vector2(-Mathf.Abs(_rigidbody2D.velocity.x)*1.1f, _rigidbody2D.velocity.y);
        else if(offSetToPaddle < -0.15f)
            _rigidbody2D.velocity = new Vector2(Mathf.Abs(_rigidbody2D.velocity.x)*1.1f, _rigidbody2D.velocity.y);
    }
}
