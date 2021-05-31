using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ladybird : MonoBehaviour
{

    // void OnTriggerEnter2D(Collider2D other) {
    // 	if (other.tag == "Stone") {
    // 		transform.localScale = new Vector2(1f,0.5f);
    // 	}

    // }
    public AudioController audioController;
    
    void Start() {
        audioController = FindObjectOfType<AudioController>();
    }

    void OnCollisionEnter2D(Collision2D other) {
    	if (other.gameObject.tag == "Stone") {
    		GetComponent<Collider2D>().isTrigger = true;
    		AutoMove movingObj = gameObject.GetComponent<AutoMove>();
    		movingObj.canMove = false;
    		transform.localScale = new Vector2(0.5f,0.1f);
    		transform.position = new Vector3(transform.position.x,transform.position.y - 0.4f, transform.position.z);
            audioController.playClip(Clip.ghostDied);
    	}
    	
    	
    }
}
