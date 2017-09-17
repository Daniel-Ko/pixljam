using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    public int HP = 10;
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
    //extra ground
    private Transform initGroundCheck;

    // An object need to closer than that distance to be picked up.
    public float pickUpDist = 1f;
    public Transform carriedObject = null;
    public LayerMask pickupLayer;

    private Rigidbody2D playersRigidbody;
    // Use this for initialization
    void Start () {
        anim = GetComponent<Animator>();
        playersRigidbody = GetComponent<Rigidbody2D>();
        initGroundCheck = groundCheck;
    }

    //runs every n times a frame
    void FixedUpdate()
    {
        grounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);
        //if carrying change ground check to the kiwi
        if (carriedObject != null && carriedObject.tag == "Kiwi")
        {
            KiwiController kiwiScript = carriedObject.GetComponent<KiwiController>();
            grounded = kiwiScript.kiwi_grounded;
        }
        else {
            groundCheck = initGroundCheck;
        }
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
                
            }
            else {// Nothing in hand, we check if something is around and pick it up
                pickUp();
            }
        }
        if (Input.GetKey(KeyCode.O)) {
            if (carriedObject != null)//holding something already drop it
            {
                drop();
            }
        }

        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            if (absValueX < playerSpeed)
            {
                forceX = grounded ? playerSpeed: (playerSpeed*airSpeedMult);
            }
        }
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            if (absValueX < playerSpeed)
            {
                forceX = grounded ? -playerSpeed : (-playerSpeed * airSpeedMult);
            }
        }


        //JUMPING
        if (Input.GetKey(KeyCode.Space) && grounded )
        {
            if (absValueY < flyingSpeed)
            {
                //forceY = flyingSpeed;
                //anim.SetBool("Jumping", true);
                GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, flyingSpeed);
                //if we have ^^ then the mass wont effect the jump therefore might work better to do fourceY=..
            }
        }

		if (totalWeight >= 8) {
			// Show the tired bird.
			anim.SetInteger ("AnimState", 1);
		} else {
			// Show the usual bird.
			anim.SetInteger ("AnimState", 0);
		}

        GetComponent<Rigidbody2D>().AddForce(new Vector2(forceX,forceY));
        //anim.SetFloat("speed", Mathf.Abs(GetComponent<Rigidbody2D>().velocity.x));
    }

    /*
       called by collectable kiwifruit 
       increments the kiwis weight by the weight of the kiwi     
    */

    public void hurtPlayer(int damage) {
        HP -= damage;
    }

	public int weight() {
        return totalWeight;
    }

    private void updateMassOfThisBasedOnCarried() {
        if (carriedObject != null)
        {
            KiwiController script = carriedObject.gameObject.GetComponent<KiwiController>();
            int weightOfKiwi = script.totalWeight;
            float newWeightOfPlayer = updateMassBasedOnNumKiwis(weightOfKiwi);
            
            if (newWeightOfPlayer > 0 && newWeightOfPlayer < 20)
            {
                GetComponent<Rigidbody2D>().mass = newWeightOfPlayer;
            }
            totalWeight = weightOfKiwi;
        }
    }

    /*Methods involving interacting with Kiwi*/

    private void drop()
    {
        carriedObject.gameObject.AddComponent<Rigidbody2D>();
        carriedObject.GetComponent<Rigidbody2D>().freezeRotation = true; 
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

        float locationOfPickup = GetComponent<CircleCollider2D>().bounds.size.y;
        Debug.Log(locationOfPickup);
        if (canPickUp)
            carriedObject = c.transform;
        
        if (carriedObject != null)
        {
            
            //Set the box in front of character
            Destroy(carriedObject.GetComponent<Rigidbody2D>());
            carriedObject.parent = transform;
            carriedObject.localPosition = new Vector3(0f, -locationOfPickup*15f, 1f); // Might need to change that
        }
    }


    private float updateMassBasedOnNumKiwis(int numberOfKiwisEaten) {
        switch (numberOfKiwisEaten)
            {
                case 1:
                    return 1.03f;
                case 2:
                     return 1.06f;
                case 3:
                    return 1.09f;
                case 4:
                    return 1.12f;
                case 5:
                    return 1.15f;
                case 6:
                    return 1.18f;
                case 7:
                    return 1.21f;
                case 8:
                    return 1.24f;
                 case 9:
                    return 1.27f;
                case 10:
                    return 1.30f;
            default:
                return 1f;
        }
    }
}