using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class Ads : MonoBehaviour
{

    string gameId = "com.lightningdrago.chintujump";
    bool testMode = true;

    void Start () {
        Advertisement.Initialize (gameId, testMode);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
