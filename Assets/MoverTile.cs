using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class MoverTile : MonoBehaviour
{


public MoverTilePickup_start pickup_start_moverTile;
public MoverTilePickup_end pickup_end_moverTile;

	public float speed = 1f;
	public int distance = 10;

	private Vector3 originalPosition;
	private bool moveLeft = false;
  private bool startTravel = false;

    // Start is called before the first frame update
    void Start()
    {
        originalPosition = transform.position;
        pickup_start_moverTile.moverTilePickup_start_method = OnPickUp_start;
        pickup_end_moverTile.moverTilePickup_end_method = OnPickUp_end;
    }

    // Update is called once per frame

    void Update()
    {

      if (startTravel) {
        if(transform.position.x - originalPosition.x > distance){
          moveLeft = true;
        } else if (originalPosition.x > transform.position.x) {
          moveLeft = false;
        }

        float direction = 1;
        if (moveLeft) {
          direction = -1;
        }

        transform.Translate(Vector3.right * Time.deltaTime * speed * direction);
      }
    }

    void OnCollisionEnter2D(Collision2D other) {
    if (other.gameObject.tag == "PlayerTag") {
        startTravel = true;
    }
  }


  public void OnPickUp_start() {
    Debug.Log("OnPickUp_start");
    startTravel = false;
    transform.position = originalPosition;
  }

  public void OnPickUp_end() {
   Debug.Log("OnPickUp_end"); 
   startTravel = false;
   transform.position = new Vector3(originalPosition.x + distance, originalPosition.y, originalPosition.z);
  }

}
