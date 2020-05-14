using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;
using GoogleMobileAds;
using GoogleMobileAds.Api;

public class StartRewardButton : MonoBehaviour
{
    
    private RewardBasedVideoAd rewardBasedVideo;
    private InterstitialAd interstitial;

    
    //public static StartRewardButton instance;
   /* private void Awake()
    {
       instance = this;
    }*/

    
    private void Start()
    {
#if UNITY_ANDROID
        string appId = "ca-app-pub-6404957345030488~2409666371";
#elif UNITY_IPHONE
        string appId = "ca-app-pub-6404957345030488~6749844982";
#else
            string appId = "unexpected_platform";
#endif

        // Initialize the Google Mobile Ads SDK.
        MobileAds.Initialize(appId);

        // Get singleton reward based video ad reference.
        this.rewardBasedVideo = RewardBasedVideoAd.Instance;

        // Called when an ad request has successfully loaded.
        rewardBasedVideo.OnAdLoaded += HandleRewardBasedVideoLoaded;
        // Called when an ad request failed to load.
        rewardBasedVideo.OnAdFailedToLoad += HandleRewardBasedVideoFailedToLoad;
        // Called when an ad is shown.
        rewardBasedVideo.OnAdOpening += HandleRewardBasedVideoOpened;
        // Called when the ad starts to play.
        rewardBasedVideo.OnAdStarted += HandleRewardBasedVideoStarted;
        // Called when the user should be rewarded for watching a video.
        rewardBasedVideo.OnAdRewarded += HandleRewardBasedVideoRewarded;
        // Called when the ad is closed.
        rewardBasedVideo.OnAdClosed += HandleRewardBasedVideoClosed;
        // Called when the ad click caused the user to leave the application.
        rewardBasedVideo.OnAdLeavingApplication += HandleRewardBasedVideoLeftApplication;

        this.RequestRewardBasedVideo();


        RequestInterstitial();
        RequestRewardBasedVideo();

    }


    
    //REWARD VIDEO
    private void RequestRewardBasedVideo()
    {
#if UNITY_ANDROID
        string adUnitId = "ca-app-pub-6404957345030488/5746276153";
        //string adUnitId = "ca-app-pub-3940256099942544/5224354917"; //test
#elif UNITY_IPHONE
        string adUnitId = "ca-app-pub-6404957345030488/4201193959";
#else
        string adUnitId = "unexpected_platform";
#endif

        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();
        // Load the rewarded video ad with the request.
        this.rewardBasedVideo.LoadAd(request, adUnitId);
    }

    public void HandleRewardBasedVideoLoaded(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleRewardBasedVideoLoaded event received");
    }

    public void HandleRewardBasedVideoFailedToLoad(object sender, AdFailedToLoadEventArgs args)
    {
        RequestRewardBasedVideo();
        MonoBehaviour.print(
            "HandleRewardBasedVideoFailedToLoad event received with message: "
                             + args.Message);
    }

    public void HandleRewardBasedVideoOpened(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleRewardBasedVideoOpened event received");
    }

    public void HandleRewardBasedVideoStarted(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleRewardBasedVideoStarted event received");
    }

    public void HandleRewardBasedVideoClosed(object sender, EventArgs args)
    {
        RequestRewardBasedVideo();
        MonoBehaviour.print("HandleRewardBasedVideoClosed event received");
    }

    public void HandleRewardBasedVideoRewarded(object sender, Reward args)
    {
        Counter obj = GameObject.Find("SceneCounter").GetComponent<Counter>();
        int count = obj.InstantNumberOfSceneChanges();
        if(count != 0)
            PlayerPrefs.SetInt("TotalCoin", PlayerPrefs.GetInt("TotalCoin") + (50/count + 1));
        else
            PlayerPrefs.SetInt("TotalCoin", PlayerPrefs.GetInt("TotalCoin") + 50);
    }

    public void HandleRewardBasedVideoLeftApplication(object sender, EventArgs args)
    {
        RequestRewardBasedVideo();
        MonoBehaviour.print("HandleRewardBasedVideoLeftApplication event received");
    }

    //kalla när man klickar på knapp
    public void ShowRewardAd()
    {
        if (rewardBasedVideo.IsLoaded())
        {
            rewardBasedVideo.Show();
        }
    }






    // interstitial ads.... 
    private void RequestInterstitial()
    {
#if UNITY_ANDROID
        string adUnitId = "ca-app-pub-6404957345030488/8017216276";
        //string adUnitId = "ca-app-pub-3940256099942544/1033173712"; //test
#elif UNITY_IPHONE
        string adUnitId = "ca-app-pub-6404957345030488/4967480716";
#else
        string adUnitId = "unexpected_platform";
#endif

        // Initialize an InterstitialAd.
        this.interstitial = new InterstitialAd(adUnitId);
        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();
        // Load the interstitial with the request.
        this.interstitial.LoadAd(request);
    }

    public void ShowInterstitial()
    {
        this.interstitial.Show();
        if (this.interstitial.IsLoaded())
        {
            this.interstitial.Show();
        }
    }

}