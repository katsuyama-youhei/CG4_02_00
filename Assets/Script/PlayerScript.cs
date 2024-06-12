using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerScript : MonoBehaviour
{
    public Rigidbody rb;
    public float moveSpeed = 1.0f;
    public float jumpSpeed = 1.0f;

    // ray—p
    float distance = 0.72f;
    private bool isCollisionBlock = true;

    // SE—p
    private AudioSource audioSource;
    // Start is called before the first frame update

    public GameObject bombParticle;

    public Animator animator;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        transform.rotation = Quaternion.Euler(0, 90, 0);
    }

    // Update is called once per frame
    void Update()
    {

        // ƒŒƒC‚ð‰º•ûŒü‚É”ò‚Î‚·
        Vector3 rayPosition = transform.position+ new Vector3(0.0f,0.7f,0.0f);
        Ray ray = new Ray(rayPosition, Vector3.down);
       // Debug.DrawRay(rayPosition, Vector3.down * distance, Color.red);
        isCollisionBlock = Physics.Raycast(ray, distance);

        if (GoalScript.isGameClear == false)
        {
            if (isCollisionBlock)
            {
                Jump();
                Debug.Log("Hit");
            }
            else
            {
                animator.SetBool("jump", false);
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

        if (move < 0)
        {
            transform.rotation = Quaternion.Euler(0, -90, 0);
            animator.SetBool("mode", true);
        }
        else if (move > 0)
        {
            transform.rotation = Quaternion.Euler(0, 90, 0);
            animator.SetBool("mode", true);
        }
        else
        {
            animator.SetBool("mode", false);
        }

        v.x = moveSpeed * move;
        rb.velocity = v;
    }

    void Jump()
    {
        Vector3 v = rb.velocity;

        if (Input.GetAxis("Jump") != 0)
        {
            v.y = jumpSpeed;
            animator.SetBool("jump", true);
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
            Instantiate(bombParticle, transform.position, Quaternion.identity);
        }
    }

}
