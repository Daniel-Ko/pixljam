using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPMeter : MonoBehaviour {

	private PlayerController player;
	private int maxHP;
	public Texture2D bgTexture;
	public Texture2D hpBar;
	public int iconWidth = 32;

	// Position of the HP on the screen.
	public Vector2 hpOffset = new Vector2(10,10);

	// Use this for initialization
	void Start () {
		player = GameObject.FindObjectOfType<PlayerController> ();
		maxHP = player.HP;
	}

	void OnGUI(){
		float percent = Mathf.Clamp01((float)player.HP / (float) maxHP);

		if (!player) {
			percent = 0;
		}

		DrawMeter (hpOffset.x, hpOffset.y, hpBar, bgTexture, percent);
	}

	void DrawMeter(float x, float y, Texture2D texture, Texture2D background, float percent){
		var bgW = background.width;
		var bgH = background.height;

		GUI.DrawTexture (new Rect (x, y, bgW, bgH), background);
		var newWidth = ((bgW - iconWidth) * percent) + iconWidth;
		GUI.BeginGroup (new Rect (x, y, newWidth, bgH));
		GUI.DrawTexture (new Rect (0, 0, bgW, bgH), texture);
		GUI.EndGroup ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
