using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KiwiController : MonoBehaviour {
    public int totalWeight;

    private PlayerController script = null;
    private bool beingCarried;

    private bool bounceMode = false;

    public Sprite normalSprite;
    public Sprite eatingSprite;
    public int eatingDelay;
  
    //Main players script
    private  PlayerController player_script;

    //for switching between player and kiwis ground checks

    public Transform groundCheck;
    public float groundCheckRadius;
    public LayerMask whatIsGround;
    public bool kiwi_grounded;
    // Use this for initialization
    void Start () {
        player_script = GameObject.Find("player").GetComponent <PlayerController> ();
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKey(KeyCode.Z)) {
            totalWeight = 10;
        }
    }

    void FixedUpdate() {
        kiwi_grounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);
        if (totalWeight >= 10) {
            bounceMode = true;
        }
        if (totalWeight < 5) {
            bounceMode = false;
            //kiwi's Material now 0 bounce
            //TODO
        }
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "Platform")
        {
            if (bounceMode)
            {
                Vector2 dir = (Vector2)transform.position;
                Vector2 bounceDir = new Vector2(dir.x, dir.y * 250);
                //gameObject.GetComponent<Rigidbody2D>().AddForce(bounceDir);
                player_script.GetComponent<Rigidbody2D>().AddForce(bounceDir);
                totalWeight = 0;
                //Renderer rend = GetComponent<Renderer>();
                //rend.material = (Material)Resources.Load("Materials/KiwiMaterial.physicsMaterial2D");
            }
        }
    }

    public void RenderEatingSprite() {
        GetComponent<SpriteRenderer>().sprite = eatingSprite;
    }

    public void RenderNormalSprite()
    {
        GetComponent<SpriteRenderer>().sprite = normalSprite;
    }

    public void feedKiwi(int kiwiWeight)
    {
        totalWeight += kiwiWeight;
    }

    public bool isBouncy() {
        return bounceMode;
    }
}
