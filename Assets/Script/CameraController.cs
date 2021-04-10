using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
	public GameObject player;
	public float offset;
	public float offsetSmoothing;
	private Vector3 playerPosition;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (player == null) {
             // FindObjectOfType<PlayerController>();
            player = GameObject.FindGameObjectsWithTag("PlayerTag")[0]; 
        }
        
        playerPosition = new Vector3(player.transform.position.x, player.transform.position.y, transform.position.z);
        if (player.transform.localScale.x > 0f) {
        	playerPosition = new Vector3(playerPosition.x + offset, playerPosition.y + 2, playerPosition.z);
        } else {
        	playerPosition = new Vector3(playerPosition.x - offset, playerPosition.y, playerPosition.z);
        }

        // transform.position = playerPosition;
        //Smooth transition
        //Use delta time to consider device framerate
        transform.position = Vector3.Lerp(transform.position, playerPosition, offsetSmoothing * Time.deltaTime);
    }







}
