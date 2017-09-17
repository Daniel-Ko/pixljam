using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSprite : MonoBehaviour {

    public Sprite[] sprites;
    public string resourceName;
    private SpriteRenderer spriteRenderer;

    // Use this for initialization
    void Start () {
		if(resourceName != "")
        {
            sprites = Resources.LoadAll<Sprite>(resourceName);
        }
        spriteRenderer.sprite = sprites[0];
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
