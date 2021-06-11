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
    private GameControls gameControls;
    public bool checkpointReached = false;
    public int currentLevel = 0;
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);
        gamePlayer = FindObjectOfType<PlayerController>(); 
        GoogleAds.initialize();
        
    }

    void FixedUpdate() {
        if (gameControls == null){
            gameControls = FindObjectOfType<GameControls>();
        }

        if (GameObject.FindGameObjectsWithTag("LevelText").Length > 0){
            GameObject.FindGameObjectsWithTag("LevelText")[0].GetComponent<TMPro.TextMeshProUGUI>().text = "Level: " + SceneManager.GetActiveScene().buildIndex.ToString();
        }
    }
        

    public void restartLevel() {
        gamePlayer.resetPlayer();

        loadScene(currentLevel);
    }

    public void respawn() {
        GoogleAds.loadAdsIfNotLoaded();
    	StartCoroutine("respawnCoroutine");
    }

    public IEnumerator respawnCoroutine() {
    	gamePlayer.gameObject.SetActive(false);
    	yield return new WaitForSeconds(respawnDelay);
    	gamePlayer.transform.position =  gamePlayer.respawnPoint;
    	gamePlayer.gameObject.SetActive(true);
        if (checkpointReached && GoogleAds.isInterstitialAdLoaded()) {
            gameControls.showChildView(GameControlsChild.adCheckpointView);
            gameControls.pauseGame(true);
        } else {
            restartLevel();
        }
    } 


public void pauseGame(bool isTrue) {
        if (isTrue) {
            Time.timeScale = 0;
        } else {
            Time.timeScale = 1;
        }
    }

	public void startGame() {
		transition.SetTrigger("Initialize");
        StartCoroutine(changeLevelCoroutine());
	}
    public void changeLevel() {
        checkpointReached = false;
        Debug.Log("checkpointReached set to false 1");
    	GoogleAds.showinterstitialAdd();
    	transition.SetTrigger("LevelEnd");
        StartCoroutine(changeLevelCoroutine());
    }

    IEnumerator changeLevelCoroutine() {
        //Play animation
        currentLevel = SceneManager.GetActiveScene().buildIndex + 1;
        levelLoadingLabel.text = "LEVEL " + currentLevel.ToString();
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

    public void loadScene(int index) {
        GoogleAds.loadAdsIfNotLoaded();
    	checkpointReached = false;
    	transition.SetTrigger("LevelEnd");
        StartCoroutine(loadLevelCoroutine(index));
    }

	IEnumerator loadLevelCoroutine(int index) {
        //Play animation

        currentLevel = index;
        if (index == 0 || index == 5) {
                levelLoadingLabel.text = "Loading...";
            } else {
                levelLoadingLabel.text = "LEVEL " + currentLevel.ToString();
            }
        
        savePlayer();
        //Wait 
        yield return new WaitForSeconds(transitionTime);
        //Change level

        Vector3 startPoint = level1StartPoint;
        gamePlayer.respawnPoint = startPoint;
        gamePlayer.transform.position = startPoint;
        SceneManager.LoadScene(index);
        transition.SetTrigger("LevelStart");
        checkpointReached = false;
    }

    private void savePlayer() {
        SaveSystem.savePlayer(currentLevel, gamePlayer.score);
    }
}





