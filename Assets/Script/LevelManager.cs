using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
	public float respawnDelay;
    public Animator transition;
    public float transitionTime = 1f;
    public TMPro.TextMeshProUGUI levelLoadingLabel;

	private PlayerController gamePlayer;
    private Vector3 level1StartPoint = new Vector3(0f, 0f, 0.0f);
    private Vector3 level2StartPoint = new Vector3(0f, 0f, 0.0f);
    private Vector3 level3StartPoint = new Vector3(0f, 0f, 0.0f);
    private Text levelText;
    public int currentLevel = 0;

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);
        gamePlayer = FindObjectOfType<PlayerController>(); 
    }

    void FixedUpdate() {
        if (GameObject.FindGameObjectsWithTag("LevelText").Length > 0){
            GameObject.FindGameObjectsWithTag("LevelText")[0].GetComponent<TMPro.TextMeshProUGUI>().text = "Level: " + SceneManager.GetActiveScene().buildIndex.ToString();
        }
        
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


	public void startGame() {
		transition.SetTrigger("Initialize");
        StartCoroutine(changeLevelCoroutine());
	}
    public void changeLevel() {
    	transition.SetTrigger("LevelEnd");
        StartCoroutine(changeLevelCoroutine());
    }

    IEnumerator changeLevelCoroutine() {
        //Play animation
        currentLevel = SceneManager.GetActiveScene().buildIndex + 1;
        levelLoadingLabel.text = "LEVEL " + currentLevel.ToString();
        Debug.Log("Current level : " + currentLevel.ToString());
        savePlayer();
        //Wait 
        yield return new WaitForSeconds(transitionTime);
        //Change level

        Vector3 startPoint = level1StartPoint;
        gamePlayer.respawnPoint = startPoint;
        gamePlayer.transform.position = startPoint;
        SceneManager.LoadScene(currentLevel);
        transition.SetTrigger("LevelStart");
    }

    private void savePlayer() {
        SaveSystem.savePlayer(currentLevel);
    }
}



















