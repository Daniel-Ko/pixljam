using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KiwiController : MonoBehaviour {
    public int totalWeight;

    private PlayerController script = null;
    private bool beingCarried;

    private bool bounceMode = false;
    private bool bouncing = false;

    public Sprite normalSprite;
    public Sprite normalSprite2;
    public Sprite normalSprite3;
    public Sprite normalSprite4;
    public Sprite normalSprite5;
    public float eatingDelay;
    private Animator anim;
    public Sprite carriedSprite;
    public Sprite carriedSprite2;
    public Sprite carriedSprite3;
    public Sprite carriedSprite4;
    public Sprite carriedSprite5;
    public Sprite eatingSprite;
    public Sprite eatingSprite2;
    public Sprite eatingSprite3;
    public Sprite eatingSprite4;
    public Sprite eatingSprite5;
    private bool eating = false;
    //public Sprite[] sprites;
    //public string resourceName;
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
    }

    private void LateUpdate() {
        if (beingCarried && !bouncing) {
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
                default:
                    spriteRenderer.sprite = carriedSprite5;
                    break;
            }
        }
        /*
        if (eating){
            RenderEatingSprite();
        }
        */
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
                bouncing = true;
                Vector2 dir = (Vector2)transform.position;
                Vector2 bounceDir = new Vector2(dir.x, dir.y * 150);
                if (bounceDir.y > max_bounce_y) bounceDir.y = max_bounce_y;
                if (bounceDir.y < min_bounce_y) bounceDir.y = min_bounce_y;
                player_script.gameObject.GetComponent<Rigidbody2D>().AddForce(bounceDir);
                totalWeight = 0;
                //Renderer rend = GetComponent<Renderer>();
                //rend.material = (Material)Resources.Load("Materials/KiwiMaterial.physicsMaterial2D");
            } else
            {
                bouncing = false;
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
        //spriteRenderer.sprite = eatingSprite;
        switch (totalWeight)
        {
            case 0:
                spriteRenderer.sprite = eatingSprite;
                break;
            case 1:
                spriteRenderer.sprite = eatingSprite2;
                break;
            case 2:
                spriteRenderer.sprite = eatingSprite3;
                break;
            case 3:
                spriteRenderer.sprite = eatingSprite4;
                break;
            case 4:
                spriteRenderer.sprite = eatingSprite5;
                break;
            default:
                spriteRenderer.sprite = eatingSprite5;
                break;
        }
    }

    public void RenderNormalSprite() {
        //spriteRenderer.sprite = normalSprite;

        switch (totalWeight)
        {
            case 0:
                spriteRenderer.sprite = normalSprite;
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
            default:
                spriteRenderer.sprite = carriedSprite5;
                break;
        }
    }

    public void feedKiwi(int kiwiWeight)
    {
        totalWeight += kiwiWeight;
    }

    public bool isBouncy() {
        return bounceMode;
    }

    public void setEating(bool b)
    {
        eating = b;
    }

    private void updateIsBeingCarriedField() {
        beingCarried = player_script.carriedObject != null ? true:false;
        anim.SetBool("carried", beingCarried);
    }
}
