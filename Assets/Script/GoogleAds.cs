
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;
using System;
using GoogleMobileAds.Common;

public delegate void RewardedGoogleAdsFinished(bool rewarded);

public class GoogleAds : MonoBehaviour
{

private static InterstitialAd interstitial;
private static RewardedAd rewardedAd;
private static LevelManager levelManager;
private static bool earnedReward = false;

public static RewardedGoogleAdsFinished adFineshedDelegate;
public static void initialize() {
	
	MobileAds.Initialize(initStatus => { });
	// string appId = "ca-app-pub-5428825449568419~5474031146";
	string interstitialUnitId = "ca-app-pub-5428825449568419/3580465161"; 
	string rewardedAdUnitId = "ca-app-pub-5428825449568419/2234528245"; 
	// string adUnitId = "ca-app-pub-3940256099942544/1033173712";

	interstitial = new InterstitialAd(interstitialUnitId);
	rewardedAd = new RewardedAd(rewardedAdUnitId);
	addRewardAdHandles();
	addInterstitialAdHandles();
	levelManager = FindObjectOfType<LevelManager>();
	requestInterstitial();
	requestRewarded();
}

public static void addInterstitialAdHandles() {
		// Called when an ad request has successfully loaded.
	interstitial.OnAdLoaded += handleOnAdLoaded;
	// Called when an ad request failed to load.
	interstitial.OnAdFailedToLoad += handleOnAdFailedToLoad;
	// Called when an ad is shown.
	interstitial.OnAdOpening += handleOnAdOpened;
	// Called when the ad is closed.
	interstitial.OnAdClosed += handleOnAdClosed;
}
public static void requestInterstitial()
{
    
    AdRequest request = new AdRequest.Builder().Build();
    interstitial.LoadAd(request);
}

public static bool isInterstitialAdLoaded() {
    return interstitial.IsLoaded();
}

public static void showinterstitialAdd()
{
  	if (interstitial.IsLoaded()) {
    	interstitial.Show();
  	}
}

public static void handleOnAdLoaded(object sender, EventArgs args)
{
    MonoBehaviour.print("HandleAdLoaded event received");
    // FindObjectOfType<MainMenu>().addLoaded = true;
}

public static void handleOnAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
{
    // MonoBehaviour.print("HandleFailedToReceiveAd event received with message: ");
}

public static void handleOnAdOpened(object sender, EventArgs args)
{
    // MonoBehaviour.print("HandleAdOpened event received");
}

public static void handleOnAdClosed(object sender, EventArgs args)
{
    MonoBehaviour.print("HandleAdClosed event received");
    levelManager.pauseGame(false);

}

public static void handleOnAdFailedToLoad1(object sender, AdFailedToLoadEventArgs args)
{
    // print("Interstitial failed to load: ");
}



public static void requestRewarded()
{
	AdRequest request = new AdRequest.Builder().Build();
	rewardedAd.LoadAd(request);

}

public static bool isRewardedAdLoaded() {
    return rewardedAd.IsLoaded();
}

public static void showRewardedAd()
{
  	if (rewardedAd.IsLoaded()) {
    	rewardedAd.Show();
  	}
    GoogleAds.loadAdsIfNotLoaded();
}

public static void addRewardAdHandles() {
		// Called when an ad request has successfully loaded.
    rewardedAd.OnAdLoaded += handleRewardedAdLoaded;
        // Called when an ad request failed to load.
    rewardedAd.OnAdFailedToLoad += handleRewardedAdFailedToLoad1;
        // Called when an ad is shown.
    rewardedAd.OnAdOpening += handleRewardedAdOpening;
        // Called when an ad request failed to show.
    rewardedAd.OnAdFailedToShow += handleRewardedAdFailedToShow;
        // Called when the user should be rewarded for interacting with the ad.
    rewardedAd.OnUserEarnedReward += handleUserEarnedReward;
        // Called when the ad is closed.
    rewardedAd.OnAdClosed += handleRewardedAdClosed;
}



public static void handleRewardedAdLoaded(object sender, EventArgs args)
{
    MonoBehaviour.print("HandleRewardedAdLoaded event received");
}

public static void handleRewardedAdFailedToLoad1(object sender, EventArgs args)
{
    MonoBehaviour.print(
        "HandleRewardedAdFailedToLoad event received with message: ");
}

public static void handleRewardedAdOpening(object sender, EventArgs args)
{
    MonoBehaviour.print("HandleRewardedAdOpening event received");
}

public static void handleRewardedAdFailedToShow(object sender, AdErrorEventArgs args)
{
    MonoBehaviour.print(
        "HandleRewardedAdFailedToShow event received with message: ");
}

public static void handleRewardedAdClosed(object sender, EventArgs args)
{
    MonoBehaviour.print("HandleRewardedAdClosed event received");
    levelManager.pauseGame(false);
    // if (!earnedReward) {
    // 	levelManager.restartLevel();
    // }

    adFineshedDelegate(earnedReward);
    earnedReward = false;
    
}

public static void handleUserEarnedReward(object sender, Reward args)
{
    string type = args.Type;
    double amount = args.Amount;
    MonoBehaviour.print(
        "HandleRewardedAdRewarded event received for "
                    + amount.ToString() + " " + type);
    // levelManager.pauseGame(false);
    earnedReward = true;

}

public static void loadAdsIfNotLoaded() {
    if (!isRewardedAdLoaded()){
        requestRewarded();
    }
    if (!isInterstitialAdLoaded()){
        requestInterstitial();
    }
    
}


}
