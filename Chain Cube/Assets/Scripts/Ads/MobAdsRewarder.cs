
using UnityEngine;
using TMPro;
using GoogleMobileAds.Api;
public class MobAdsRewarder : MonoBehaviour
{
    [SerializeField] private GameManager _gameManager;
    [SerializeField] private TMP_Text _text;
    private RewardedAd _rewardedAd;
    private const string REWARDER_UNIT_ID = "ca-app-pub-3940256099942544/1712485313";

    public int ShootCount { get; set; }

    private void Start()
    {
        ShootCount = 1;
    }

    private void Update()
    {
        if (ShootCount % 12 == 0)
        { 
            OnEnable();
            ShowRewardedAd();
            ShootCount++;
        }
    }

    void OnEnable()
    {
        _rewardedAd = new RewardedAd(REWARDER_UNIT_ID);
        AdRequest adRequest = new AdRequest.Builder().Build();
        _rewardedAd.LoadAd(adRequest);
        _rewardedAd.OnUserEarnedReward += HandleUserEarnedReward;
    }

    void OnDisable()
    {
        _rewardedAd.OnUserEarnedReward -= HandleUserEarnedReward;
    }

    public void ShowRewardedAd()
    {
        if (_rewardedAd.IsLoaded())
        {
            _rewardedAd.Show();
        }
    }

    public void HandleUserEarnedReward(object sender, Reward args)
    {
        if (_gameManager != null)
        {
            _gameManager.ScoreCount += 1000;
            _text.text = "Score: " + _gameManager.ScoreCount;
        }
        else
            OnDisable();
    }

}
