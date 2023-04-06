using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveSquare : MonoBehaviour
{
    public new Rigidbody2D rigidbody2D;
    public float speed;
    public KeyCode upKey;
    public KeyCode downKey;
    void Start()
    {
        speed = 2;
        rigidbody2D = GetComponent<Rigidbody2D>();
        rigidbody2D.gravityScale = 0;
    }

    void Update()
    { 
        if(Input.GetKey(upKey) && transform.position.y < 5)
        {
            rigidbody2D.velocity = Vector2.up * speed;
        }
        else if (Input.GetKey(downKey) && transform.position.y > -5)
        {
            rigidbody2D.velocity = Vector2.down * speed;
        }
        else
        {
            rigidbody2D.velocity = Vector2.zero;
        }
    }
}
