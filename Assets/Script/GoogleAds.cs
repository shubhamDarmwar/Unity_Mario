using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;

public class GoogleAds : MonoBehaviour
{
	private static InterstitialAd interstitial;

	public static void initialize() {
		// Initialize the Google Mobile Ads SDK.
        MobileAds.Initialize(initStatus => { });
	}

    public static void requestInterstitial()
	{
	    #if UNITY_ANDROID
	        string adUnitId = "ca-app-pub-5428825449568419/3580465161"; 
	        //"ca-app-pub-3940256099942544/1033173712";//
	        //app id : ca-app-pub-5428825449568419~5474031146
	    // #elif UNITY_IPHONE
	    //     string adUnitId = "ca-app-pub-3940256099942544/4411468910";
	    #else
	        string adUnitId = "unexpected_platform";
	    #endif

	    // Initialize an InterstitialAd.
	    interstitial = new InterstitialAd(adUnitId);

	    // Create an empty ad request.
	    AdRequest request = new AdRequest.Builder().Build();
	    // Load the interstitial with the request.
	    interstitial.LoadAd(request);
	    Debug.Log("RequestInterstitial ---- ");
	}

	public static void showAdd()
	{
		Debug.Log("showAdd ---- ");
	  	if (interstitial.IsLoaded()) {

	    	interstitial.Show();
	  	} else {
	  		Debug.Log("showAdd ---- not present");
	  	}
		requestInterstitial();

	}



}
