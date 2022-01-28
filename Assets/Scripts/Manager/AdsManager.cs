using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;
using System;

public class AdsManager : Singleton<AdsManager>, IUnityAdsListener
{

    string gameId = "4519619";
    string rewarded = "Rewarded_Android";

    private Action OnRewardedSucced;


    protected override void Awake()
    {
        base.Awake();
        Advertisement.Initialize(gameId);
        Advertisement.AddListener(this);
    }

    public void PlayRewardedAd(Action OnSuccess)
    {
        OnRewardedSucced += OnSuccess;
        if(Advertisement.IsReady(rewarded))
        {
            Advertisement.Show(rewarded);
        }
        else
        {
           // print("Rewarded is not ready!");
        }
    }

    public void OnUnityAdsReady(string placementId)
    {
      //  print("ADS ARE READY");
    }

    public void OnUnityAdsDidError(string message)
    {
       // print("ADS ERROR: " + message);
    }

    public void OnUnityAdsDidStart(string placementId)
    {
       // print("ADS START");
    }

    public void OnUnityAdsDidFinish(string placementId, ShowResult showResult)
    {
        if (placementId == rewarded && showResult == ShowResult.Finished)
        {
            if(OnRewardedSucced!=null) 
            {
                OnRewardedSucced.Invoke();
                OnRewardedSucced = null;
            }
        }
    }
}
