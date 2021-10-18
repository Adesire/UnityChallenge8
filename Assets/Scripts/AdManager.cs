using System;
using System.Collections;
using System.Collections.Generic;
using GoogleMobileAds.Api;
using UnityEngine;

public class AdManager : MonoBehaviour
{
    private BannerView _bannerAd;
    private InterstitialAd _interstitial;
    private RewardedInterstitialAd _rewardBasedVideoAd;

    public static AdManager instance;
    private bool _isRewarded = false;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        MobileAds.Initialize(status => { });
        this.RequestBanner();
        RequestRewardAd();
    }

    // Update is called once per frame
    void Update()
    {
        if (_isRewarded)
        {
            _isRewarded = false;
            //Unlock Character
        }
    }

    private AdRequest CreateAdRequest()
    {
        return new AdRequest.Builder().Build();
    }

    private void RequestBanner()
    {
        string adUnitId = "ca-app-pub-3940256099942544/6300978111";
        this._bannerAd = new BannerView(adUnitId, AdSize.SmartBanner, AdPosition.Bottom);
        this._bannerAd.LoadAd(CreateAdRequest());
    }

    public void RequestInterstitial()
    {
        string adUnitId = "ca-app-pub-3940256099942544/1033173712";
        if (this._interstitial != null)
            this._interstitial.Destroy();

        this._interstitial = new InterstitialAd(adUnitId);
        this._interstitial.LoadAd(CreateAdRequest());
    }

    private void RequestRewardAd()
    {
        string adUnitId = "ca-app-pub-3940256099942544/5354046379";
        RewardedInterstitialAd.LoadAd(adUnitId, CreateAdRequest(),
            (ad, args) => 
                AdLoadCallback(ad, args.LoadAdError
                .GetMessage()));
    }

    public void ShowInterstitial()
    {
        if (this._interstitial.IsLoaded())
            _interstitial.Show();
        else
        {
            Debug.Log("Interstitial Ad is not ready yet");
        }
    }

    private void AdLoadCallback(RewardedInterstitialAd ad, string error)
    {
        if (error == null)
        {
            _rewardBasedVideoAd = ad;
        }
    }

    public void ShowRewardedInterstitialAd()
    {
        if (_rewardBasedVideoAd != null)
        {
            _rewardBasedVideoAd.Show(userEarnedRewardCallback);
        }
    }

    private void userEarnedRewardCallback(Reward reward)
    {
        // TODO: Reward the user.
        _isRewarded = true;
    }
}