using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinScript : MonoBehaviour
{

	public int score = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other) {
    	// Debug.Log("Triggered");
    	Destroy(gameObject);
    	score = score + 1;
    	Debug.Log(score);
    }
}
