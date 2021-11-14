using UnityEngine;
using AppodealAds.Unity.Api;
using AppodealAds.Unity.Common;
using UnityEngine.Events;

[RequireComponent(typeof(AudioSource))]

public class AdSettings : MonoBehaviour, IInterstitialAdListener, IRewardedVideoAdListener, INonSkippableVideoAdListener, IBannerAdListener, IMrecAdListener
{
    [SerializeField] private AudioClip _clickSound;
    [SerializeField] private AudioClip _errorSound;

    private const string AppKey = "edb00737917865780dc750a812c46ee1bfd289002f0b1820";
    private const string Reward = "Reward";
    private const string Default = "Default";

    private AudioSource _audioSource;

    public event UnityAction<double, string> OnRewarded;

    private void Start()
    {
        int adTypes = Appodeal.INTERSTITIAL | Appodeal.REWARDED_VIDEO | Appodeal.NON_SKIPPABLE_VIDEO | Appodeal.BANNER_BOTTOM | Appodeal.MREC;
        Appodeal.initialize(AppKey, adTypes, true);

        Appodeal.setInterstitialCallbacks(this);
        Appodeal.setRewardedVideoCallbacks(this);
        Appodeal.setNonSkippableVideoCallbacks(this);
        Appodeal.setBannerCallbacks(this);
        Appodeal.setMrecCallbacks(this);

        _audioSource = GetComponent<AudioSource>();
    }

    public void ShowInterstitial()
    {
        if (Appodeal.canShow(Appodeal.INTERSTITIAL) && !Appodeal.isPrecache(Appodeal.INTERSTITIAL))
        {
            PlayClickSound();
            Appodeal.show(Appodeal.INTERSTITIAL);
        }
        else
            PlayErrorSound();
    }

    public void ShowRewardedVideo()
    {
        if (Appodeal.canShow(Appodeal.REWARDED_VIDEO, Reward) && !Appodeal.isPrecache(Appodeal.REWARDED_VIDEO))
        {
            PlayClickSound();
            Appodeal.show(Appodeal.REWARDED_VIDEO, Reward);
        }
        else
            PlayErrorSound();
    }

    public void ShowNonSkippableVideo()
    {
        if (Appodeal.canShow(Appodeal.NON_SKIPPABLE_VIDEO) && !Appodeal.isPrecache(Appodeal.NON_SKIPPABLE_VIDEO))
        {
            PlayClickSound();
            Appodeal.show(Appodeal.NON_SKIPPABLE_VIDEO);
        }
        else
            PlayErrorSound();
    }

    public void ShowBanner()
    {
        Appodeal.show(Appodeal.BANNER_BOTTOM);
    }

    public void HideBanner()
    {
        Appodeal.hide(Appodeal.BANNER_BOTTOM);
    }

    public void ShowMrecVideo()
    {
        int yAxis = Screen.currentResolution.height - Screen.currentResolution.height / 10;
        int xGravity = Appodeal.BANNER_HORIZONTAL_CENTER;

        if (Appodeal.canShow(Appodeal.MREC) && !Appodeal.isPrecache(Appodeal.MREC))
            Appodeal.showMrecView(yAxis, xGravity, Default);
    }

    public void HideMrec()
    {
        Appodeal.hideMrecView();
    }

    #region InterstitialCallbacks
    public void onInterstitialLoaded(bool isPrecache)
    {
        Debug.Log($"onInterstitialLoaded. IsPrecache: {isPrecache}");
    }

    public void onInterstitialFailedToLoad()
    {
        Debug.Log("onInterstitialFaidedToLoad");
    }

    public void onInterstitialShowFailed()
    {
        Debug.Log("onInterstitialShowFailed");
    }

    public void onInterstitialShown()
    {
        Debug.Log("onInterstitialShown");
    }

    public void onInterstitialClosed()
    {
        Debug.Log("onInterstitialClosed");
    }

    public void onInterstitialClicked()
    {
        Debug.Log("onInterstitialClicked");
    }

    public void onInterstitialExpired()
    {
        Debug.Log("onInterstitialExpired");
    }
    #endregion

    #region RewardedVideoCallbacks
    public void onRewardedVideoLoaded(bool precache)
    {
        Debug.Log($"onRewardedVideoLoaded. IsPrecache: {precache}");
    }

    public void onRewardedVideoFailedToLoad()
    {
        Debug.Log("onRewardedVideoFailedToLoad");
    }

    public void onRewardedVideoShowFailed()
    {
        Debug.Log("onRewardedVideoShowFailed");
    }

    public void onRewardedVideoShown()
    {
        Debug.Log("onRewardedVideoShown");
    }

    public void onRewardedVideoFinished(double amount, string currencyName)
    {
        Debug.Log($"onRewardedVideoFinished. Amount: {amount}, currency name: {currencyName}");
        OnRewarded?.Invoke(amount, currencyName);
    }

    public void onRewardedVideoClosed(bool finished)
    {
        Debug.Log($"onRewardedVideoClosed. IsFinished: {finished}");
    }

    public void onRewardedVideoExpired()
    {
        Debug.Log("onRewardedVideoExpired");
    }

    public void onRewardedVideoClicked()
    {
        Debug.Log("onRewardedVideoClicked");
    }
    #endregion

    #region NonSkippableVideoCallbacks
    public void onNonSkippableVideoLoaded(bool isPrecache)
    {
        Debug.Log($"onNonSkippableVideoLoaded. IsPrecache: {isPrecache}");
    }

    public void onNonSkippableVideoFailedToLoad()
    {
        Debug.Log("onNonSkippableVideoFailedToLoad");
    }

    public void onNonSkippableVideoShowFailed()
    {
        Debug.Log("onNonSkippableVideoShowFailed");
    }

    public void onNonSkippableVideoShown()
    {
        Debug.Log("onNonSkippableVideoShown");
    }

    public void onNonSkippableVideoFinished()
    {
        Debug.Log("onNonSkippableVideoFinished");
    }

    public void onNonSkippableVideoClosed(bool finished)
    {
        Debug.Log($"onNonSkippableVideoClosed. IsFinished: {finished}");
    }

    public void onNonSkippableVideoExpired()
    {
        Debug.Log("onNonSkippableVideoExpired");
    }
    #endregion

    #region BannerCallbacks
    public void onBannerLoaded(int height, bool isPrecache)
    {
        Debug.Log($"onBannerLoaded. Height: {height}, isPrecache: {isPrecache}");
    }

    public void onBannerFailedToLoad()
    {
        Debug.Log("onBannerFailedToLoad");
    }

    public void onBannerShown()
    {
        Debug.Log("onBannerShown");
    }

    public void onBannerClicked()
    {
        Debug.Log("onBannerClicked");
    }

    public void onBannerExpired()
    {
        Debug.Log("onBannerExpired");
    }
    #endregion

    #region MrecCallbacks
    public void onMrecLoaded(bool isPrecache)
    {
        Debug.Log($"onMrecLoaded. IsPrecache: {isPrecache}");
    }

    public void onMrecFailedToLoad()
    {
        Debug.Log($"onMrecFailedToLoad");
    }

    public void onMrecShown()
    {
        Debug.Log($"onMrecShown");
    }

    public void onMrecClicked()
    {
        Debug.Log($"onMrecClicked");
    }

    public void onMrecExpired()
    {
        Debug.Log($"onMrecExpired");
    }
    #endregion

    private void PlayClickSound()
    {
        _audioSource.PlayOneShot(_clickSound, Settings.Volume);
    }

    private void PlayErrorSound()
    {
        _audioSource.PlayOneShot(_errorSound, Settings.Volume);
    }
}
