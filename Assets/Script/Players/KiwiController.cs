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
    public float eatingDelay;
    private Animator anim;
    public Sprite carriedSprite;
    public Sprite carriedSprite2;
    public Sprite carriedSprite3;
    public Sprite carriedSprite4;
    public Sprite carriedSprite5;
    private SpriteRenderer spriteRenderer;

    //Main players script
    private  PlayerController player_script;

    //for switching between player and kiwis ground checks

    public Transform groundCheck;
    public float groundCheckRadius;
    public LayerMask whatIsGround;
    public bool kiwi_grounded;
    // Use this for initialization
    void Start () {
        player_script = GameObject.Find("Bird").GetComponent <PlayerController> ();
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>(); // we are accessing the SpriteRenderer that is attached to the Gameobject
        if (spriteRenderer.sprite == null) // if the sprite on spriteRenderer is null then
            spriteRenderer.sprite = normalSprite; // set the sprite to sprite1
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKey(KeyCode.Z)) {
            totalWeight = 10;
        }

        if (beingCarried) {
            Debug.Log("CARRIED");
            switch (totalWeight) {
                case 0:
                    RenderNormalSprite();
                    break;
                case 1:
                    spriteRenderer.sprite = carriedSprite;
                    break;
                case 2:
                    spriteRenderer.sprite = carriedSprite2;
                    break;
                case 3:
                    spriteRenderer.sprite = carriedSprite3;
                    break;
                case 4:
                    spriteRenderer.sprite = carriedSprite4;
                    break;
                case 5:
                    spriteRenderer.sprite = carriedSprite5;
                    break;

            }
        } else {
            RenderNormalSprite();
        }
    }

    void FixedUpdate() {
        updateIsBeingCarriedField();
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
        float max_bounce_y = 450;
        float min_bounce_y = 100;
        if (coll.gameObject.tag == "Platform")
        {

            if (bounceMode)
            {
                BounceAnimation();
                //anim.Play("Bouncing");
                //WaitForSeconds(eatingDelay);
                Vector2 dir = (Vector2)transform.position;
                Vector2 bounceDir = new Vector2(dir.x, dir.y * 150);
                if (bounceDir.y > max_bounce_y) bounceDir.y = max_bounce_y;
                if (bounceDir.y < min_bounce_y) bounceDir.y = min_bounce_y;
                player_script.gameObject.GetComponent<Rigidbody2D>().AddForce(bounceDir);
                totalWeight = 0;
                //Renderer rend = GetComponent<Renderer>();
                //rend.material = (Material)Resources.Load("Materials/KiwiMaterial.physicsMaterial2D");
            }
        }
    }

    public void BounceAnimation() {
        StartCoroutine("BounceKiwiAnimationCo");
    }

    public IEnumerator BounceKiwiAnimationCo() {
        anim.Play("Bouncing");
        yield return new WaitForSeconds(eatingDelay);
    }

    public void RenderEatingSprite() {
        spriteRenderer.sprite = eatingSprite;
    }

    public void RenderNormalSprite()
    {
        spriteRenderer.sprite = normalSprite;
    }

    public void feedKiwi(int kiwiWeight)
    {
        totalWeight += kiwiWeight;
    }

    public bool isBouncy() {
        return bounceMode;
    }

    private void updateIsBeingCarriedField() {
        beingCarried = player_script.carriedObject != null ? true:false;
        //anim.SetBool("carried")
        anim.SetBool("carried", beingCarried);
    }
}
