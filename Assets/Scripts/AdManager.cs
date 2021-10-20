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

    private RewardedAd _rewardedAd;

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
        _rewardedAd = new RewardedAd("ca-app-pub-3940256099942544/5224354917");
        _rewardedAd.OnUserEarnedReward += this.HandleUserRewarded;
        _rewardedAd.OnAdClosed += this.HandleRewardedAdClosed;
        RequestRewardAd();
    }

    // Update is called once per frame
    void Update()
    {
        if (_isRewarded)
        {
            _isRewarded = false;
            Debug.Log("The user gets a reward");
            //Unlock Character
            FindObjectOfType<CharacterSelection>().ChangeCharacter(4);
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

    public void ShowInterstitial()
    {
        if (this._interstitial.IsLoaded())
            _interstitial.Show();
        else
        {
            Debug.Log("Interstitial Ad is not ready yet");
        }
    }

    private void RequestRewardAd()
    {
        _rewardedAd.LoadAd(CreateAdRequest());
    }

    public void ShowRewardedAd()
    {
        if (_rewardedAd.IsLoaded())
        {
            _rewardedAd.Show();
        }
    }

    private void HandleRewardedAdClosed(object sender, EventArgs e)
    {
        this.RequestRewardAd();
    }

    private void HandleUserRewarded(object sender, Reward e)
    {
        _isRewarded = true;
    }
}