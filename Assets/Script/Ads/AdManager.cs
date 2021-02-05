using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class AdManager : MonoBehaviour, IUnityAdsListener
{
    private static AdManager instance;

    public static AdManager Instance 
    {
        get
        {
            if (instance != null)
                return instance;
            else
                return null;
        }
    }

    public string GameID
    {
        get
        {
#if UNITY_ANDROID
            return "3996929";
#endif
        }
    }

    public const string BANNER_AD = "BannerAd";
    public const string REWARD_AD = "rewardedVideo";
    public const string INTERSTITIAL_AD = "video";

    private bool test = true;

    public EventHandler<AdEventArgs> OnAdDone;

    private void Awake()
    {
        instance = this;
        Advertisement.Initialize(GameID, test);
    }

    private void Start()
    {
        Advertisement.AddListener(this);
    }


    public void ShowInterstitialAD()
    {
        
        if (Advertisement.IsReady(INTERSTITIAL_AD))
        {   
            Advertisement.Show(INTERSTITIAL_AD);
            Debug.Log("Interstitial Ad is playing");
        }
        else
        {
            Debug.Log("Interstitial Ad is not ready");
        }
        
    }

    public void ShowBannerAd()
    {
        /*
        if(Advertisement.isInitialized)
        {
            Advertisement.Banner.SetPosition(BannerPosition.TOP_CENTER);
            Advertisement.Show(BANNER_AD);
            Debug.Log($"{BANNER_AD} is Played");
        }//*/
        StartCoroutine(BannerAdRoutine());
    }

    public void HideBannerAd()
    {
        if (Advertisement.Banner.isLoaded)
        {
            Advertisement.Banner.Hide();
        }
    }

    IEnumerator BannerAdRoutine()
    {
        while (!Advertisement.isInitialized)
        {
            yield return new WaitForSeconds(0.5f);
        }
        Advertisement.Banner.SetPosition(BannerPosition.TOP_CENTER);
        Advertisement.Show(BANNER_AD);
        Debug.Log($"{BANNER_AD} is Played");
    }

    public void ShowRewardAd()
    {
        if (Advertisement.IsReady(REWARD_AD))
        {
            Advertisement.Show(REWARD_AD);
        }
        else
        {
            Debug.Log("Reward Ad is not ready");
        }
    }

    public void OnUnityAdsReady(string placementId)
    {
        Debug.Log($"{placementId} is now ready");
    }

    public void OnUnityAdsDidError(string message)
    {
        Debug.Log($"Error Encountered : {message}");
    }

    public void OnUnityAdsDidStart(string placementId)
    {
        Debug.Log($"{placementId} is now starting");
    }

    public void OnUnityAdsDidFinish(string placementId, ShowResult showResult)
    {
        if (OnAdDone != null)
        {
            AdEventArgs args = new AdEventArgs(placementId, showResult);
            OnAdDone(this, args);
        }
    }
}
