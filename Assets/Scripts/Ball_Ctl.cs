using UnityEngine;

public class Ball_Ctl : MonoBehaviour
{

    public float MinY_velocity;
    [SerializeField] private Rigidbody2D _rigidbody2D;

    public float InitForce;
    // Start is called before the first frame update
    void Start()
    {
        _rigidbody2D.AddForce(new Vector2(-1,-1)*InitForce);
    }

    private void FixedUpdate ()
    {
        ForceMinYVelocity();
    }

    void ForceMinYVelocity()
    {
        if (_rigidbody2D.velocity.y > 0 && _rigidbody2D.velocity.y < MinY_velocity)
            _rigidbody2D.velocity = new Vector2(_rigidbody2D.velocity.x,MinY_velocity);
        else if (_rigidbody2D.velocity.y < 0 && _rigidbody2D.velocity.y > -MinY_velocity)
            _rigidbody2D.velocity = new Vector2(_rigidbody2D.velocity.x, -MinY_velocity);
    }

    // Update is called once per frame
    void OnCollisionEnter2D(Collision2D collider)
    {
        if (collider.gameObject.CompareTag(Constants.TAG_PLAYER))
        {
            float offset = collider.transform.position.x - transform.position.x;
            ModifyDirection(offset);

        }

        //float x = Random.Range(-.5f,.5f);
        //Vector2 velocity = new Vector2((transform.position.x - col.transform.position.x) + x, transform.position.y - col.transform.position.y);
        //rb.velocity = velocity.normalized * speed;
    }

    void ModifyDirection(float offSetToPaddle)
    {
        if (offSetToPaddle > 0.15f && _rigidbody2D.velocity.x > 0 || offSetToPaddle < 0.15f && _rigidbody2D.velocity.x < 0)
            _rigidbody2D.velocity = new Vector2(-_rigidbody2D.velocity.x*1.1f, _rigidbody2D.velocity.y);
    }
}
