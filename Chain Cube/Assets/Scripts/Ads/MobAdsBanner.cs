using UnityEngine;
using GoogleMobileAds.Api;
using System.Collections;

public class MobAdsBanner : MonoBehaviour
{
    private BannerView _bannerView;
    private const string BANNER_UNIT_ID = "ca-app-pub-3940256099942544/2934735716";


    private void Awake()
    {
        MobileAds.Initialize(initStatos => { });
    }

    private void OnEnable()
    {
        _bannerView = new BannerView(BANNER_UNIT_ID, AdSize.Banner, AdPosition.Bottom);
        AdRequest adRequest = new AdRequest.Builder().Build();
        _bannerView.LoadAd(adRequest);
        StartCoroutine(ShowBanner());
    }

    private IEnumerator ShowBanner()
    {
        yield return new WaitForSecondsRealtime(3.0f);
        _bannerView.Show();
    }
}
