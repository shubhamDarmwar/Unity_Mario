using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public delegate void MoverPickUp_end_method();

public class MoverTilePickup_end : MonoBehaviour
{
    public MoverPickUp_end_method moverTilePickup_end_method;

    void OnTriggerEnter2D(Collider2D other) {
      Debug.Log("Collided start");
    if (other.gameObject.tag == "PlayerTag") {
      Debug.Log("Collided start");
        moverTilePickup_end_method();
    }
    
        
  }
}
