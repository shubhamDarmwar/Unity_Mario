using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
	public ParticleSystem spark;
	public ParticleSystem unused;
	public ParticleSystem blast;
    // Start is called before the first frame update
    void Start()
    {
        unused.Stop();
        blast.Stop();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter2D(Collider2D other) {
    	if (other.tag == "PlayerTag") {
    		
    		// makeBlast();
    		StartCoroutine("makeBlast");
    	}
    }
    
    // public void makeBlast() {
    	
    // }

    public IEnumerator makeBlast() {
    	spark.Stop();
    	blast.Play();
    	// gameObject.SetActive(false);
    	yield return new WaitForSeconds(1.0f);
    	// gameObject.SetActive(false);

    	// yield return new WaitForSeconds(0.4f);
    	// gameObject.SetActive(true);
    	spark.Play();
    	blast.Stop();
    	
    } 
}
