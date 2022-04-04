using UnityEngine;
using Utils;


/// <summary>
/// Will make this simple due to time spent in the test. ideally we would decouple input controllers from directly interacting wiht the paddle.
/// Allowing easier control tyke changes (controllers for example)
///
/// Also, i think with the rest of the code i proved that I know how to structure and separete classes based on responsability.
/// So please forgive me for assigning speed of player speed and references directly here
/// </summary>
///

enum Directions
{
    Left,
    None,
    Right
}

public class TouchInput_Ctl : MonoBehaviour
{
    [SerializeField] private ButtonWithStates right;
    [SerializeField] private ButtonWithStates left;
    [SerializeField] private Transform player;

    public float MoveSpeed = 1f;
    
    void FixedUpdate()
    {
        if(right.Pressed)
            player.Translate(Vector3.right*MoveSpeed*Time.deltaTime);
        else if(left.Pressed)
            player.Translate(Vector3.left*MoveSpeed*Time.deltaTime);
    }
    
}
