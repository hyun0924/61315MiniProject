using System.Collections;
using System.Collections.Generic;
using GoogleMobileAds.Api;
using UnityEngine;

public class GoogleAds : MonoBehaviour
{
    // These ad units are configured to always serve test ads.
#if UNITY_ANDROID
    // private string _adUnitId = "ca-app-pub-3940256099942544/6300978111"; // test
    private string _adUnitId = "ca-app-pub-1115139103674761/3227403689";
#elif UNITY_IPHONE
    private string _adUnitId = "ca-app-pub-3940256099942544/2934735716";
#else
    private string _adUnitId = "unused";
#endif

    BannerView _bannerView;

    private void Start()
    {
        _bannerView = null;
        MobileAds.Initialize((InitializationStatus initStatus) =>
        {
            LoadBannerView();
        });


    }
    private void CreateBannerView()
    {
        Debug.Log("Creating banner view");

        // If we already have a banner, destroy the old one.
        if (_bannerView != null)
        {
            DestroyBannerView();
        }

        AdSize adSize = AdSize.GetCurrentOrientationAnchoredAdaptiveBannerAdSizeWithWidth(AdSize.FullWidth);

        // Create a 320x50 banner at top of the screen
        _bannerView = new BannerView(_adUnitId, adSize, AdPosition.Bottom);
    }

    private void LoadBannerView()
    {

        // create an instance of a banner view first.
        if (_bannerView == null)
        {
            CreateBannerView();
        }

        // create our request used to load the ad.
        AdRequest adRequest = new AdRequest.Builder().Build();

        // // send the request to load the ad.
        // Debug.Log("Loading banner ad.");
        _bannerView.LoadAd(adRequest);
    }

    public void DestroyBannerView()
    {
        if (_bannerView != null)
        {
            Debug.Log("Destroying banner view.");
            _bannerView.Destroy();
            _bannerView = null;
        }
    }
}