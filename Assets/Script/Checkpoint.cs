using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
	public Sprite sprite1; // Drag your first sprite here
	public Sprite sprite2; // Drag your second sprite here
	public bool isChecked = false;
	private SpriteRenderer spriteRenderer; 

	void Start ()
	{
	    spriteRenderer = GetComponent<SpriteRenderer>(); // we are accessing the SpriteRenderer that is attached to the Gameobject
	    if (spriteRenderer.sprite == null) // if the sprite on spriteRenderer is null then
	        spriteRenderer.sprite = sprite1; // set the sprite to sprite1
	}

    void OnTriggerEnter2D(Collider2D other) {
    	isChecked = true;
    	spriteRenderer.sprite = sprite2;
    }
}
