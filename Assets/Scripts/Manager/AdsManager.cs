using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;
using System;

public class AdsManager : Singleton<AdsManager>, IUnityAdsListener
{

    private string gameId = "4519619";
    private string rewarded = "Rewarded_Android";
    private string _interstitial = "Interstitial_Android";

    private Action OnRewardedSucced;

    private bool _showAd;
    private float _adCount;

    [SerializeField] float _adTime = 300;


    protected override void Awake()
    {
        if(Instance != null) 
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);

            base.Awake();

            Advertisement.Initialize(gameId);
            Advertisement.AddListener(this);
            print("AWAKEEEEEEEEEEEEEEEEEEEEEE");
            ResetAdCount();
        }
        
       
    }

    private void Update() 
    {
        if(!_showAd && Time.time >= _adCount)
        {
            _showAd = true;
            print("AHORA");
        }
    }

    public void PlayRewardedAd(Action OnSuccess)
    {
        OnRewardedSucced += OnSuccess;
        if(Advertisement.IsReady(rewarded))
        {
            Advertisement.Show(rewarded);
            ResetAdCount();
        }
        else
        {
           // print("Rewarded is not ready!");
        }
    }

     public void ResetAdCount()
    {
        _showAd = false;
        _adCount = Time.time + _adTime;
        print("RESETTTTTTTTTTTTTTTTTT");
    }


    public void PlayAd()
    {
        if(_showAd)
        {
            if(Advertisement.IsReady(_interstitial))
            {
                Advertisement.Show(_interstitial);
            }
            ResetAdCount();
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
