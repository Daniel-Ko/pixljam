using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KiwiController : MonoBehaviour {
    public int totalWeight;

    private PlayerController script = null;
    private bool beingCarried;

    private bool bounceMode = false;

  
    //Main players script
    private  PlayerController player_script;
    // Use this for initialization
    void Start () {
        player_script = GameObject.Find("player").GetComponent <PlayerController> ();
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKey(KeyCode.Z)) {
            Debug.Log("WORKED");
            totalWeight = 10;
        }
    }

    void FixedUpdate() {
       
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
            Debug.Log("Here y'all");
            Debug.Log(bounceMode);
            if (bounceMode)
            {
                Vector2 dir = (Vector2)transform.position;
                Vector2 bounceDir = new Vector2(dir.x, dir.y * 1000);
                //gameObject.GetComponent<Rigidbody2D>().AddForce(bounceDir);
                player_script.GetComponent<Rigidbody2D>().AddForce(bounceDir);
                totalWeight = 0;
                //Renderer rend = GetComponent<Renderer>();
                //rend.material = (Material)Resources.Load("Materials/KiwiMaterial.physicsMaterial2D");
            }
        }

    }

    public void feedKiwi(int kiwiWeight)
    {
        totalWeight += kiwiWeight;
    }

    public bool isBouncy() {
        return bounceMode;
    }
}
