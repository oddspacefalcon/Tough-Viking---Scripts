/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Monetization;

public class UnityAdsScript : MonoBehaviour {

    public static UnityAdsScript instance;

    string gameId = "3060651";
    bool testMode = false;

    private string BannerID = "BannerAdStart";
    private string VideoID = "video";
    private string RewardedVideoID = "rewardedVideo";

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        Monetization.Initialize(gameId, testMode);
    }

    //anropas i Player script 
    public void ShowVideoAd()
    {

        if (Monetization.IsReady(VideoID))
        {
            // hitta musiken och pausa under adden
            AudioSource audio = GameObject.Find("Music").GetComponent<AudioSource>();
            audio.Pause();

            ShowAdPlacementContent ad = null;
            ad = Monetization.GetPlacementContent(VideoID) as ShowAdPlacementContent;

            if (ad != null)
                ad.Show(AdFinished);  
        }
    }

    // sätt igång musiken igen när ad har slutat
    void AdFinished(ShowResult result)
    {
        if (result == ShowResult.Finished)
        {
            AudioSource audio = GameObject.Find("Music").GetComponent<AudioSource>();
            audio.UnPause();
            
        }
        if (result == ShowResult.Skipped)
        {
            AudioSource audio = GameObject.Find("Music").GetComponent<AudioSource>();
            audio.UnPause();
        }
    }

    /*
    public void ShowBannerAd()
    {
        if (Monetization.IsReady(BannerID))
        {
            ShowAdPlacementContent ad = null;
            ad = Monetization.GetPlacementContent(BannerID) as ShowAdPlacementContent;

            if (ad != null)
                ad.Show();
        }
    }*/


