using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerProgress
{
    public int level;
    public int health;
    public float[] position;

    public PlayerProgress (int level) 
    {
    	Debug.Log("Saved level = " + level.ToString());
    	if (this.level < level) {
    		this.level = level;
    	}
    	
    	position = new float[3];
    	// position[0] = player.transform.position.x;
    	// position[1] = player.transform.position.y;
    	// position[2] = player.transform.position.z;
    }

    public PlayerProgress () {
    	level = 1;
    	position = new float[3];
    	position[0] = 0;
    	position[1] = 0;
    	position[2] = 0;	
    }
}
