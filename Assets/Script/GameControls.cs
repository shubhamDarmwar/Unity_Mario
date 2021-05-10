using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameControls : MonoBehaviour
{
    public GameObject pauseMenuView;
    public GameObject gameControlsView;
    public GameObject stone;
    private bool paused = false;
    private bool mute = false;
    private static LevelManager levelManager;


    void Start() {
        gameControlsView.SetActive(true);
        pauseMenuView.SetActive(false);
        levelManager = FindObjectOfType<LevelManager>();
    }

    public void pauseClicked() {
    	Time.timeScale = 0;
        gameControlsView.SetActive(false);
        pauseMenuView.SetActive(true);
    }

    public void soundButtonClicked() {
    	if (mute) {
    		GameObject.FindGameObjectsWithTag("AudioSource")[0].GetComponent<AudioSource>().volume = 1;
    		mute = false;
    	} else {
    		GameObject.FindGameObjectsWithTag("AudioSource")[0].GetComponent<AudioSource>().volume = 0;
    		mute = true;
    	}
    	
    }

    public void mainMenuClicked() {
        Time.timeScale = 1;
        levelManager.loadScene(0);
    }

    public void restartButtonClicked() {
        Time.timeScale = 1;
        gameControlsView.SetActive(true);
        pauseMenuView.SetActive(false);
        levelManager.loadScene(levelManager.currentLevel);
    }

    public void resumeButtonClicked() {
        Time.timeScale = 1;
        gameControlsView.SetActive(true);
        pauseMenuView.SetActive(false);
    }

    public void fireButtonClicked() {
        GameObject player = GameObject.FindGameObjectWithTag("PlayerTag");
        if (player != null) {

            Throw(player.transform.position, player.transform.localScale.x);
        }
    }

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
}
