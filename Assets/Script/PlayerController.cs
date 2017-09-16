using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
	public float playerSpeed;
	public float flyingSpeed;
	public float airSpeedMult = .3f;

	private Animator anim;
	private int totalKiwis;

	//ground stuff
	public Transform groundCheck;
	public float groundCheckRadius;
	public LayerMask whatIsGround;
	private bool grounded;
	private bool doubleJumped;

	//Kiwi and bird interaction fields
	private bool isCarryingKiwi;

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator>();
	}

	//runs every n times a frame
	void FixedUpdate()
	{
		grounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);
		if (grounded) anim.SetBool("Jumping", false);

		//anim.SetFloat("falling", GetComponent<Rigidbody2D>().velocity.y);

	}

	// Update is called once per frame
	void Update () {
		float forceY = 0f;
		float forceX = 0f;
		float absValueX = GetComponent<Rigidbody2D>().velocity.x;
		float absValueY = GetComponent<Rigidbody2D>().velocity.y;
		if (grounded) 
			doubleJumped = false;

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
	public void feedKiwi(int kiwiWeight) {
		totalKiwis += kiwiWeight;
	}

	public void carryingCheck(bool carry) {
		isCarryingKiwi = carry;
	}

}