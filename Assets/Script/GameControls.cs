using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameControls : MonoBehaviour
{
    private bool paused = false;
    public void pauseClicked() {
    	if(paused) {
    		Time.timeScale = 1;
    		paused = false;
    		} else {
    			Time.timeScale = 0;
    			paused = true;
    		}
    	
    }
}
