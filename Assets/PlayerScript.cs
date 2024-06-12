using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerScript : MonoBehaviour
{
    public Rigidbody rb;
    public float moveSpeed = 1.0f;
    public float jumpSpeed = 1.0f;

    // ray�p
    float distance = 0.6f;
    private bool isCollisionBlock = true;

    // SE�p
    private AudioSource audioSource;
    // Start is called before the first frame update

    public GameObject bombParticle;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

        // ���C���������ɔ�΂�
        Vector3 rayPosition = transform.position;
        Ray ray = new Ray(rayPosition, Vector3.down);
        //Debug.DrawRay(rayPosition, Vector3.down * distance, Color.red);
        isCollisionBlock = Physics.Raycast(ray, distance);

        if (GoalScript.isGameClear == false)
        {
            if (isCollisionBlock)
            {
                Jump();
            }
        }

        /*  if (isCollisionBlock)
          {
              Debug.DrawRay(rayPosition, Vector3.down * distance, Color.red);
          }
          else
          {
              Debug.DrawRay(rayPosition, Vector3.down * distance, Color.yellow);
          }*/
    }

    private void FixedUpdate()
    {
        if (GoalScript.isGameClear == false)
        {
            Move();
        }
        else
        {
            rb.velocity = Vector3.zero;
        }

    }

    void Move()
    {
        Vector3 v = rb.velocity;
        float move = Input.GetAxis("Horizontal");
        
        v.x = moveSpeed * move;
        rb.velocity = v;
    }

    void Jump()
    {
        Vector3 v = rb.velocity;
        if (Input.GetButtonDown("Jump"))
        {
            v.y = jumpSpeed;
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            v.y = jumpSpeed;
        }

        rb.velocity = v;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "COIN")
        {
            GameManagerScript.score += 1;
            audioSource.Play();
            other.gameObject.SetActive(false);
            Instantiate(bombParticle,transform.position, Quaternion.identity);
        }
    }

}
