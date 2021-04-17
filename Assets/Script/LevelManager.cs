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

	private PlayerController gamePlayer;
    private Vector3 level1StartPoint = new Vector3(0f, 0f, 0.0f);
    private Vector3 level2StartPoint = new Vector3(0f, 0f, 0.0f);
    private Vector3 level3StartPoint = new Vector3(0f, 0f, 0.0f);
    private Text levelText;
    private int currentLevel = 0;

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

    IEnumerator changeLevelCoroutine() {
        //Play animation
        
        //Wait 
        yield return new WaitForSeconds(transitionTime);
        //Change level

        currentLevel = SceneManager.GetActiveScene().buildIndex + 1;
        Vector3 startPoint = level1StartPoint;
        gamePlayer.respawnPoint = startPoint;
        gamePlayer.transform.position = startPoint;
        SceneManager.LoadScene(currentLevel);
        transition.SetTrigger("LevelStart");
    }
    public void changeLevel() {
    	transition.SetTrigger("LevelEnd");
        StartCoroutine(changeLevelCoroutine());
    }
}



















