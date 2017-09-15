using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    public float playerSpeed;
    public float jumpHeight;


    private Animator anim;


    //ground stuff
    public Transform groundCheck;
    public float groundCheckRadius;
    public LayerMask whatIsGround;
    private bool grounded;
    private bool doubleJumped;

    // Use this for initialization
    void Start () {
        anim = GetComponent<Animator>();
    }

    //runs every n times a frame
    void FixedUpdate()
    {
        grounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);
        if (grounded) anim.SetBool("Jumping", false);

        anim.SetFloat("falling", GetComponent<Rigidbody2D>().velocity.y);

    }

    // Update is called once per frame
    void Update () {

        float absValueY = GetComponent<Rigidbody2D>().velocity.y;
        if (grounded) 
            doubleJumped = false;

        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            transform.localScale = new Vector3(2, 2, 1);
            GetComponent<Rigidbody2D>().velocity = new Vector2(playerSpeed, GetComponent<Rigidbody2D>().velocity.y);
        }
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            transform.localScale = new Vector3(-2, 2, 1);
            GetComponent<Rigidbody2D>().velocity = new Vector2(-playerSpeed, GetComponent<Rigidbody2D>().velocity.y);
        }

        //Crouching
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {

        }

        //JUMPING
        if (Input.GetKey(KeyCode.Space) && grounded)
        {
            jump();
        }
        if (Input.GetKey(KeyCode.Space) && !grounded && !doubleJumped)
        {
            jump();
            doubleJumped = true;
        }
        anim.SetFloat("speed", Mathf.Abs(GetComponent<Rigidbody2D>().velocity.x));
    }
    public void jump()
    {
        GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, jumpHeight);

        //update animation
        anim.SetBool("Jumping", true);
    }
}
