using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public delegate void MoverPickUp_start_method();

public class MoverTilePickup_start : MonoBehaviour
{
public MoverPickUp_start_method moverTilePickup_start_method;

    void OnTriggerEnter2D(Collider2D other) {
      Debug.Log("Collided end");
    if (other.gameObject.tag == "PlayerTag") {
      moverTilePickup_start_method();
    }
        
  }
}