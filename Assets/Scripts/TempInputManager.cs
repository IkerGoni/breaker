using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempInputManager : MonoBehaviour
{
    public Transform player;

    public float MoveSpeed = 1f;
    // Start is called before the first frame update

    // Update is called once per frame
    void FixedUpdate()
    {
        if(Input.GetKey(KeyCode.D))
            player.Translate(Vector3.right*MoveSpeed*Time.deltaTime);
        else if(Input.GetKey(KeyCode.A))
                player.Translate(Vector3.left*MoveSpeed*Time.deltaTime);
    }
}
