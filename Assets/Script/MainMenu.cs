using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
	private static LevelManager levelManager;
	void Start() {
		levelManager = FindObjectOfType<LevelManager>();
	}

    public void playGame() {
    	print("playGame");
    	// SceneManager.LoadScene(4);
    	levelManager.changeLevel();
    }

    public void quitGame() {
    	print("quitGame");
    	Application.Quit();
    }
}
