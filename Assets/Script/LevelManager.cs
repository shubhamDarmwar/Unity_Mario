using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
	public float respawnDelay;
	private PlayerController gamePlayer;
    private Vector3 level1StartPoint = new Vector3(-20.38f, 1.0f, 0.0f);
    private Vector3 level2StartPoint = new Vector3(-8.38f, 2.0f, 0.0f);
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

    public void changeLevel(int level) {
        Vector3 startPoint = new Vector3(-20.38f, 1.0f, 0.0f);
        if (level == 1){
                startPoint = level1StartPoint;
            } else if (level == 2) {
                startPoint = level2StartPoint;
            }
        
        gamePlayer.respawnPoint = startPoint;
        gamePlayer.transform.position = startPoint;
        SceneManager.LoadScene(level);
    }
}



















