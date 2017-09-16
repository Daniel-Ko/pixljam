﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    private int HP = 10;
    public float playerSpeed;
    public float flyingSpeed;
    public float airSpeedMult = .3f;

    private Animator anim;
    private int totalWeight;

    //ground stuff
    public Transform groundCheck;
    public float groundCheckRadius;
    public LayerMask whatIsGround;
    private bool grounded;

    // An object need to closer than that distance to be picked up.
    public float pickUpDist = 1f;
    private Transform carriedObject = null;
    public LayerMask pickupLayer;

    private Rigidbody2D playersRigidbody;
    // Use this for initialization
    void Start () {
        anim = GetComponent<Animator>();
        playersRigidbody = GetComponent<Rigidbody2D>();
    }

    //runs every n times a frame
    void FixedUpdate()
    {
        grounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);
        //if (grounded) anim.SetBool("Jumping", false);
        //anim.SetFloat("falling", GetComponent<Rigidbody2D>().velocity.y);

        //update RigidedBody2D as totalWeight increase
        updateMassOfThisBasedOnCarried();

    }
    // Update is called once per frame
    void Update () {
        float forceY = 0f;
        float forceX = 0f;
        float absValueX = GetComponent<Rigidbody2D>().velocity.x;
        float absValueY = GetComponent<Rigidbody2D>().velocity.y;
        
        //PICKING UP KIWI
        if (Input.GetKey(KeyCode.P))
        {
            if (carriedObject != null)//holding something already drop it
            {
                drop();
            }
            else {// Nothing in hand, we check if something is around and pick it up
                pickUp();
            }
        }

        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            if (absValueX < playerSpeed)
            {
                forceX = grounded ? playerSpeed: (playerSpeed*airSpeedMult);
            }
            transform.localScale = new Vector3(2, 2, 1);
        }
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            if (absValueX < playerSpeed)
            {
                forceX = grounded ? -playerSpeed : (-playerSpeed * airSpeedMult);
            }
            transform.localScale = new Vector3(-2, 2, 1);
        }

        //Crouching
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {

        }

        //JUMPING
        if (Input.GetKey(KeyCode.Space)  )
        {
            if (absValueY < flyingSpeed)
            {
                forceY = flyingSpeed;
                //anim.SetBool("Jumping", true);
                //GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, flyingSpeed);
                //if we have ^^ then the mass wont effect the jump therefore might work better to do fourceY=..
            }
        }
        GetComponent<Rigidbody2D>().AddForce(new Vector2(forceX,forceY));
        anim.SetFloat("speed", Mathf.Abs(GetComponent<Rigidbody2D>().velocity.x));
    }

    /*
       called by collectable kiwifruit 
       increments the kiwis weight by the weight of the kiwi     
    */

    public void hurtPlayer(int damage) {
        Debug.Log("Ouch");
        HP -= damage;
    }

	public int weight() {
        return totalWeight;
    }

    private void updateMassOfThisBasedOnCarried() {
        if (carriedObject != null)
        {
            KiwiController script = carriedObject.gameObject.GetComponent<KiwiController>();
            float weightOfKiwi = script.totalWeight;
            GetComponent<Rigidbody2D>().mass = (weightOfKiwi * 0.15f);
        }
    }

    /*Methods involving interacting with Kiwi*/

    private void drop()
    {
        Debug.Log(carriedObject.gameObject.GetComponent<Rigidbody2D>());
        carriedObject.gameObject.AddComponent<Rigidbody2D>();
        Debug.Log(carriedObject.gameObject.GetComponent<Rigidbody2D>());
        carriedObject.parent = null; // Unparenting
        carriedObject.gameObject.AddComponent(typeof(Rigidbody)); // Gravity and co
        carriedObject = null; // Hands are free again
    }

    /* The player check there is a collision with its bounding box and another 
        the other has to have pickupLayer in this case its the kiwi
         
    */
    private void pickUp()
    {
        bool canPickUp = Physics2D.OverlapCircle(transform.position, pickUpDist, pickupLayer);
        Collider2D c = Physics2D.OverlapCircle(transform.position, pickUpDist, pickupLayer);

        if (canPickUp)
            carriedObject = c.transform;

        if (carriedObject != null)
        {
            //Set the box in front of character
            Destroy(carriedObject.GetComponent<Rigidbody2D>());
            carriedObject.parent = transform;
            carriedObject.localPosition = new Vector3(0, -0.6f, 1f); // Might need to change that
        }
    }

}