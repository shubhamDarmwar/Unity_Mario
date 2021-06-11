using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;

public enum GameControlsChild {
    pauseMenuView,
    gameControlsView,
    adCheckpointView,
    addStoneView,
    noInternetView
}

public enum AdRequestReason {
    addStone,
    loadCheckpoint
}

public class GameControls : MonoBehaviour
{
    public GameObject pauseMenuView;
    public GameObject gameControlsView;
    public GameObject adCheckpointView;
    public GameObject addStoneView;
    public GameObject noInternetView;
    public GameObject stone;
    public Button soundButton;

    private bool mute = false;
    private static LevelManager levelManager;
    private AdRequestReason adRequestReason; 
    private GameControlsChild oldView;

    // private string adCheckpointText =  "Continue from last checkpoint?";
    // private string noInternetText = "Please Turn On Internet !!!";
    
    void Start() {
        showChildView(GameControlsChild.gameControlsView);
        levelManager = FindObjectOfType<LevelManager>();
        GoogleAds.adFineshedDelegate = adFinshedPlaying;

        // GoogleAds.loadAdsIfNotLoaded();
    }

    void FixedUpdate() {
        // if(Time.time % 5f == 0) {
            // GoogleAds.loadAdsIfNotLoaded();
        // }
    }

    void showAdsView(bool isTrue) {
        showChildView(GameControlsChild.adCheckpointView);
    }

    public void showChildView(GameControlsChild child) {
        gameControlsView.SetActive(child == GameControlsChild.gameControlsView);
        pauseMenuView.SetActive(child == GameControlsChild.pauseMenuView);
        adCheckpointView.SetActive(child == GameControlsChild.adCheckpointView);
        addStoneView.SetActive(child == GameControlsChild.addStoneView);
        noInternetView.SetActive(child == GameControlsChild.noInternetView);
        pauseGame(child != GameControlsChild.gameControlsView);
        if (child != GameControlsChild.noInternetView) {
            oldView = child;
        }
    }

///////////////////////////////////////////////////////////////////////////////////////////////////////

    // Game control methods

    public void pauseClicked() {
        showChildView(GameControlsChild.pauseMenuView);
    }

    public void soundButtonClicked() {
    	if (mute) {
    		GameObject.FindGameObjectsWithTag("AudioSource")[0].GetComponent<AudioSource>().volume = 1;
    		mute = false;
            soundButton.GetComponentsInChildren<TMPro.TextMeshProUGUI>()[0].text = "Sound: ON";
    	} else {
    		GameObject.FindGameObjectsWithTag("AudioSource")[0].GetComponent<AudioSource>().volume = 0;
    		mute = true;
            soundButton.GetComponentsInChildren<TMPro.TextMeshProUGUI>()[0].text = "Sound: OFF";
    	}
    	
    }

    public void pauseGame(bool isTrue) {
        if (isTrue) {
            Time.timeScale = 0;
        } else {
            Time.timeScale = 1;
        }
    }

    public void mainMenuClicked() {
        pauseGame(false);
        levelManager.loadScene(0);
    }

    public void restartButtonClicked() {
        showChildView(GameControlsChild.gameControlsView);
        levelManager.loadScene(levelManager.currentLevel);
    }

    public void resumeButtonClicked() {
        showChildView(GameControlsChild.gameControlsView);
    }

    public void showAddStoneButtonClicked() {
        showChildView(GameControlsChild.addStoneView);
    }
///////////////////////////////////////////////////////////////////////////////////////////////////////
    // No internet connection view method
    public void noInternetViewCloseClicked() {
        showChildView(oldView);
    }

///////////////////////////////////////////////////////////////////////////////////////////////////////
    //// Add stones Ads view methods
    public void addStoneContinueClicked() {
        if (GoogleAds.isInterstitialAdLoaded()) {
            showChildView(GameControlsChild.gameControlsView);
            GoogleAds.showRewardedAd();
            adRequestReason = AdRequestReason.addStone;
        } else {
            showChildView(GameControlsChild.noInternetView);
        }
        
    }

    public void addStoneCancelClicked() {
        showChildView(GameControlsChild.gameControlsView);
    }

    public void fireButtonClicked() {
        PlayerController player = GameObject.FindObjectOfType<PlayerController>();
        if (player != null) {
            if(player.stones > 0) {
                Throw(player.transform.position, player.transform.localScale.x);
            } else {
                showChildView(GameControlsChild.addStoneView);
            }
        }
    }
///////////////////////////////////////////////////////////////////////////////////////////////////////
    // Checkpoint Ads methods
    public void adRestartLevel() {
        showChildView(GameControlsChild.gameControlsView);
        levelManager.restartLevel();
    }

    public void adResumeFromLastCheckpoint() {
        // showChildView(GameControlsChild.gameControlsView);
        if (GoogleAds.isInterstitialAdLoaded()) {
            GoogleAds.showRewardedAd();
            adRequestReason = AdRequestReason.loadCheckpoint;
        } else {
            showChildView(GameControlsChild.noInternetView);
        }
        
    }

    public void adFinshedPlaying(bool rewarded) {
        if (adRequestReason == AdRequestReason.loadCheckpoint) {
            if (!rewarded) {
                levelManager.restartLevel();
            }
        } 
        else if (adRequestReason == AdRequestReason.addStone) {
            if(rewarded) {
                PlayerController player = GameObject.FindObjectOfType<PlayerController>();
                if (player != null) {
                    player.stones += 3;
                }
            }
        }
        showChildView(GameControlsChild.gameControlsView);
    }

///////////////////////////////////////////////////////////////////////////////////////////////////////
    //////   Throw Stone method

    public void Throw(Vector3 location, float direction)
     {
        if(GameObject.FindGameObjectWithTag("PlayerTag").GetComponent<PlayerController>().stones < 1 ) {
            return;
        }
        GameObject.FindGameObjectWithTag("PlayerTag").GetComponent<PlayerController>().stones -= 1;
        GameObject newStone = Instantiate(stone);
        newStone.GetComponent<Stone>().thrown = true;

        Rigidbody2D rigidBody = newStone.GetComponent<Rigidbody2D>() ;
        float dir = 1;
        if (direction < 0) {
            newStone.transform.position = new Vector3(location.x - 1,location.y + 0.5f, location.z);
            dir = -1;
        } else {
            newStone.transform.position = new Vector3(location.x + 1,location.y + 0.5f, location.z);
            dir = 1;
        }
        rigidBody.velocity = new Vector2(dir * 10f, 0);
     }
///////////////////////////////////////////////////////////////////////////////////////////////////////
}
