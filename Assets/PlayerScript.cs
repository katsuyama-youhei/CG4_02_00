using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public Rigidbody rb;
    public float moveSpeed= 1.0f;
    public float jumpSpeed= 1.0f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Jump();
    }

    private void FixedUpdate()
    {
        Move();
    }

    void Move()
    {
        Vector3 v = rb.velocity;
        if (Input.GetKey(KeyCode.RightArrow))
        {
            v.x = moveSpeed;
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            v.x = -moveSpeed;
        }
        else
        {
            v.x = 0;
        }
        rb.velocity = v;
    }

    void Jump()
    {
        Vector3 v = rb.velocity;
        if (Input.GetKeyDown(KeyCode.Space))
        {
            v.y = jumpSpeed;
        }
        
        rb.velocity = v;
    }

}
