﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Clip {
    jump,
    coin,
    blast,
    checkPoint,
    gameOver,
    stoneCollected,
    ghostDied
}

public class AudioController : MonoBehaviour
{
	public AudioClip coinAudioClip;
    public AudioClip blastAudioClip;
    public AudioClip jumpAudioClip;
    public AudioClip checkPointAudioClip;
    public AudioClip gameOverClip;
    public AudioClip stoneCollected;
    public AudioClip ghostDied;

	private AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
    	DontDestroyOnLoad(gameObject);
     	audioSource = GameObject.FindGameObjectsWithTag("AudioSource")[0].GetComponent<AudioSource>();   
    }

    public void playClip(Clip clip) {
    	
    	AudioClip audioClip;
    	switch(clip) {
    		case Clip.coin:
    			audioClip = coinAudioClip;
    		break;
    		case Clip.jump:
    			audioClip = jumpAudioClip;
    		break;
    		case Clip.blast:
    			audioClip = blastAudioClip;
    		break;
    		case Clip.checkPoint:
    			audioClip = checkPointAudioClip;
    		break;
            case Clip.gameOver:
                Debug.Log("Game over");
                audioClip = gameOverClip;
            break;
            case Clip.stoneCollected:
                audioClip = stoneCollected;
            break;
            case Clip.ghostDied:
                audioClip = ghostDied;
            break;
    		default:
    			audioClip = blastAudioClip;
    		break;
    	}
        if (audioClip == null) {
        	Debug.Log("Clip not found");
        }
    	StartCoroutine(playClipCoroutine(audioClip));
    }

	IEnumerator playClipCoroutine(AudioClip clip) {
		audioSource.clip = clip;
		audioSource.Play();
		yield return new WaitForSeconds(1f);
	}
    
}
