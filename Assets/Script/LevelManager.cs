using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
	public float respawnDelay;
	private PlayerController gamePlayer;
    private Vector3 level1StartPoint = new Vector3(-20.38f, 1.0f, 0.0f);
    private Vector3 level2StartPoint = new Vector3(-8.38f, 2.0f, 0.0f);
    private Vector3 level3StartPoint = new Vector3(0f, 0f, 0.0f);
    private Text levelText;
    private int currentLevel = 0;
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);
        gamePlayer = FindObjectOfType<PlayerController>();
        levelText = GameObject.FindGameObjectsWithTag("LevelText")[0].GetComponent<Text>(); 
    }

    void FixedUpdate() {
        if (levelText == null) {
            levelText = GameObject.FindGameObjectsWithTag("LevelText")[0].GetComponent<Text>(); 
        }
        levelText.text = "Level: " + (currentLevel + 1).ToString();
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

    public void changeLevel() {
        currentLevel = currentLevel + 1;
        Vector3 startPoint = new Vector3(-20.38f, 1.0f, 0.0f);
        if (currentLevel == 1){
                startPoint = level1StartPoint;
            } else if (currentLevel == 2) {
                startPoint = level2StartPoint;
            } else if (currentLevel == 3) {
                startPoint = level3StartPoint;
            }
        
        gamePlayer.respawnPoint = startPoint;
        gamePlayer.transform.position = startPoint;
        SceneManager.LoadScene(currentLevel);
        // print("level = ");
        // print(level.ToString());
        // levelText.text = "Level: " + level.ToString();
    }
}



















