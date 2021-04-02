using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
	public float respawnDelay;
	private PlayerController gamePlayer;

    // Start is called before the first frame update
    void Start()
    {
        gamePlayer = FindObjectOfType<PlayerController>();
    }

    public void respawn() {
    	StartCoroutine("respawnCoroutine");
    }

    public IEnumerator respawnCoroutine() {
    	gamePlayer.gameObject.SetActive(false);
    	yield return new WaitForSeconds(respawnDelay);
    	gamePlayer.transform.position =  gamePlayer.respawnPoint;
    	gamePlayer.gameObject.SetActive(true);
    } 
}



















