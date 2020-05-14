
/*using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Monetization;

[RequireComponent(typeof(Button))]
public class GameUnityAdsButton : MonoBehaviour
{

    public string placementId = "rewardedVideo";
    private Button adButton;

#if UNITY_IOS
   private string gameId = "3060650";
#elif UNITY_ANDROID
    private string gameId = "3060651";
#endif

    void Start()
    {
        adButton = GetComponent<Button>();
        if (adButton)
        {
            adButton.onClick.AddListener(ShowAd);
        }

        if (Monetization.isSupported)
        {
            Monetization.Initialize(gameId, true);
        }
    }

    void Update()
    {
        if (adButton)
        {
            adButton.interactable = Monetization.IsReady(placementId);
        }
    }

    public void ShowAd()
    {
        ShowAdCallbacks options = new ShowAdCallbacks();
        options.finishCallback = HandleShowResult;
        ShowAdPlacementContent ad = Monetization.GetPlacementContent(placementId) as ShowAdPlacementContent;
       

        ad.Show(options);
        //pausa musik
        AudioSource audio = GameObject.Find("Music").GetComponent<AudioSource>();
        audio.Pause();
    }

    void HandleShowResult(ShowResult result)
    {
        if (result == ShowResult.Finished)
        {
            AudioSource audio = GameObject.Find("Music").GetComponent<AudioSource>();
            audio.UnPause();
            PlayerPrefs.SetInt("TotalCoin", PlayerPrefs.GetInt("TotalCoin") + 50);
        }
        else if (result == ShowResult.Skipped)
        {
            AudioSource audio = GameObject.Find("Music").GetComponent<AudioSource>();
            audio.UnPause();
            Debug.LogWarning("The player skipped the video - DO NOT REWARD!");
        }
        else if (result == ShowResult.Failed)
        {
            AudioSource audio = GameObject.Find("Music").GetComponent<AudioSource>();
            audio.UnPause();
            Debug.LogError("Video failed to show");
        }
    }
}*/