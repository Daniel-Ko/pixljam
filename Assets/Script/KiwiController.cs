using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KiwiController : MonoBehaviour {
    public int totalWeight;

    private PlayerController script = null;
    private bool beingCarried;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
      
    }

    /*  void OnTriggerEnter2D(Collider2D other)
      {
          //Make sure we are just connecting with Bird
          if (other.gameObject.tag == "Bird")
          {
              if (other.GetComponent<PlayerController>() == null) return;
              PlayerController script = other.GetComponent<PlayerController>();
              this.script = script;
              script.carryingCheck(true);
              beingCarried = true;
          }
      }
      void OnTriggerExit2D(Collider2D other)
      {
          //Make sure its the bird
          if (other.gameObject.tag == "Bird")
          {
              if (this.script != null)
              {
                  this.script.carryingCheck(false);
                  beingCarried = false;
              }
          }
      }*/

    public void feedKiwi(int kiwiWeight)
    {
        totalWeight += kiwiWeight;
    }
}
