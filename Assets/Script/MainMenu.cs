﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public GameObject mainMenuView;
    public GameObject levelMenuView;
	public Button[] levelButtons;
    private static LevelManager levelManager;
    public GameObject player;
    public bool addLoaded = false;

	void Start() {
		levelManager = FindObjectOfType<LevelManager>();
        mainMenuView.SetActive(true);
        levelMenuView.SetActive(false);

        //Add listeners to button
        for (int i = 0; i < levelButtons.Length; i++)
        {
            int closureIndex = i ; // Prevents the closure problem
            levelButtons[closureIndex].onClick.AddListener( () => levelButtonClicked( closureIndex ) );
        }
        if (GameObject.FindGameObjectWithTag("PlayerTag") == null) {
            Instantiate(player);
        }
        

	}

void FixedUpdate() {
    for(int i = 0; i < levelButtons.Length; i++) {
        levelButtons[i].GetComponentsInChildren<TMPro.TextMeshProUGUI>()[0].text = "Level " + (i + 1).ToString();
    }
    
}
    public void playGame() {
        mainMenuView.SetActive(false);
        levelMenuView.SetActive(true);

        PlayerProgress data = SaveSystem.loadPlayer();
        // int level = data.level;
        int level = 6;
        // Debug.Log("Unlocked level" + level.ToString());
        for(int i = 0; i < levelButtons.Length; i++) {
            if (i < level) {
                levelButtons[i].interactable = true;
                } else {
                    levelButtons[i].interactable = false;
                }
        }
    }

    public void quitGame() {
    	Application.Quit();
    }

    public void levelButtonClicked(int buttonIndex) {
        levelManager.loadScene(buttonIndex + 1);
    }

    public void levelMenuBackButtonClicked() {
        mainMenuView.SetActive(true);
        levelMenuView.SetActive(false);
    }

    public void resetProgressClicked() {
        SaveSystem.resetData();
    }

    
}
