/*using System.Collections;
using UnityEngine;
using UnityEngine.Advertisements;

public class BannerAds : MonoBehaviour
{

    public string bannerPlacement = "BannerAdStart";
    public bool testMode = false;

#if UNITY_IOS
    public const string gameID = "1234567";
#elif UNITY_ANDROID
    public const string gameID = "3060651";
#elif UNITY_EDITOR
    public const string gameID = "1111111";
#endif

    void Start()
    {
        Advertisement.Initialize(gameID, testMode);
        StartCoroutine(ShowBannerWhenReady());
    }

    IEnumerator ShowBannerWhenReady()
    {
        while (!Advertisement.IsReady("BannerAdStart"))
        {
            yield return new WaitForSeconds(0.5f);
        }
        Advertisement.Banner.Show(bannerPlacement);
    }
}*/