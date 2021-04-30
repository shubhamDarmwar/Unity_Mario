using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ladybird : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // void OnTriggerEnter2D(Collider2D other) {
    // 	if (other.tag == "Stone") {
    // 		transform.localScale = new Vector2(1f,0.5f);
    // 	}

    // }
    void OnCollisionEnter2D(Collision2D other) {
    	if (other.gameObject.tag == "Stone") {
    		transform.localScale = new Vector2(1f,0.5f);
    		Debug.Log("stone");
    	} else {
    		Debug.Log("No stone");
    	}
    }
}
