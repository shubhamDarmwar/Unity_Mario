using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stone : MonoBehaviour
{
	public bool thrown = false;
    void Start() {
    	if (thrown) {
    		StartCoroutine(destroySelf());
    	}
		
    }

    IEnumerator destroySelf() {
    	yield return new WaitForSeconds(1.5f);
    	Destroy(gameObject);

	}

}
