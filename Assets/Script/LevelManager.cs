using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using GoogleMobileAds.Api;
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
	private InterstitialAd interstitial;

    public int currentLevel = 0;

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);
        gamePlayer = FindObjectOfType<PlayerController>(); 

        // Initialize the Google Mobile Ads SDK.
        MobileAds.Initialize(initStatus => { });

        RequestInterstitial();
        
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
    	showAdd();
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
    	showAdd();
    	transition.SetTrigger("LevelEnd");
        StartCoroutine(loadLevelCoroutine(index));
    	// SceneManager.LoadScene(index);
     //    transition.SetTrigger("LevelStart");
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
		Debug.Log("Load " + index.ToString());
        yield return new WaitForSeconds(transitionTime);
        //Change level

        Vector3 startPoint = level1StartPoint;
        gamePlayer.respawnPoint = startPoint;
        gamePlayer.transform.position = startPoint;
        SceneManager.LoadScene(index);
        transition.SetTrigger("LevelStart");
    }

    private void savePlayer() {
        SaveSystem.savePlayer(currentLevel, gamePlayer.score);
    }




	private void RequestInterstitial()
	{
	    #if UNITY_ANDROID
	        string adUnitId = "ca-app-pub-5428825449568419~5474031146"; 
	        //"ca-app-pub-3940256099942544/1033173712";//
	    // #elif UNITY_IPHONE
	    //     string adUnitId = "ca-app-pub-3940256099942544/4411468910";
	    #else
	        string adUnitId = "unexpected_platform";
	    #endif

	    // Initialize an InterstitialAd.
	    this.interstitial = new InterstitialAd(adUnitId);

	    // Create an empty ad request.
	    AdRequest request = new AdRequest.Builder().Build();
	    // Load the interstitial with the request.
	    this.interstitial.LoadAd(request);
	    Debug.Log("RequestInterstitial ---- ");
	}

	private void showAdd()
	{
		Debug.Log("showAdd ---- ");
	  	if (this.interstitial.IsLoaded()) {

	    	this.interstitial.Show();
	  	} else {
	  		Debug.Log("showAdd ---- not present");
	  	}
	RequestInterstitial();

	}
}



















