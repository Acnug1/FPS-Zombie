﻿using UnityEngine;
using AppodealAds.Unity.Api; // подключаем простанство имен для работы с Appodeal
using AppodealAds.Unity.Common; // подключаем простанство имен для реализации интерфейсов Appodeal и использования методов обратного вызова
using UnityEngine.Events;

public class AdSettings : MonoBehaviour, IInterstitialAdListener, IRewardedVideoAdListener, INonSkippableVideoAdListener, IBannerAdListener // реализуем интерфейсы для каждого типа рекламы
{
    private const string AppKey = "edb00737917865780dc750a812c46ee1bfd289002f0b1820"; // ключ приложения является константой, чтобы мы не могли его переопределять https://app.appodeal.com/apps

    public event UnityAction<double, string> OnRewarded;

    private void Start()
    {
        int adTypes = Appodeal.INTERSTITIAL | Appodeal.REWARDED_VIDEO | Appodeal.NON_SKIPPABLE_VIDEO | Appodeal.BANNER_BOTTOM; // задаем типы рекламы, которые будут использоваться в приложении (мы можем их комбинировать с помощью побитового или "|")
        Appodeal.initialize(AppKey, adTypes, true); // инициализируем Appodeal (параметры: ключ приложения; типы рекламы; согласие на обработку персональных данных)

        Appodeal.setInterstitialCallbacks(this); // установка методов обратного вызова (в параметрах передаем слушателя каждого из интерфейсов, т.е. текущий класс. Он содержит в себе Callbacks интерфейсов)
        Appodeal.setRewardedVideoCallbacks(this);
        Appodeal.setNonSkippableVideoCallbacks(this);
        Appodeal.setBannerCallbacks(this);
    }

    public void ShowInterstitial() // показ полноэкранной рекламы
    {
        // проверка на то, что реклама данного типа загружена в целом - Appodeal.isLoaded(), принимает в себя тип рекламы и Appodeal.canShow(), может принимать как тип рекламы, так и название Placement и этот метод будет подчиняться правилам указанным в Placement
        if (Appodeal.canShow(Appodeal.INTERSTITIAL) && !Appodeal.isPrecache(Appodeal.INTERSTITIAL)) // если реклама готова к показу и не является isPrecache (дешевая или даже бесплатная реклама, которая может быть загружена моментально)
            Appodeal.show(Appodeal.INTERSTITIAL); // запускаем показ рекламы, указывая тип рекламы в параметрах
    }

    public void ShowRewardedVideo() // показ рекламы с наградой за просмотр
    {
        if (Appodeal.canShow(Appodeal.REWARDED_VIDEO, "Reward") && !Appodeal.isPrecache(Appodeal.REWARDED_VIDEO)) // если реклама готова к показу и не является isPrecache (дешевая или даже бесплатная реклама, которая может быть загружена моментально)
            Appodeal.show(Appodeal.REWARDED_VIDEO, "Reward"); // запускаем показ рекламы, указывая тип рекламы в параметрах 
        // (вторым параметром также можно указать название placement https://app.appodeal.com/v2/placements?app_id=698614. Тогда реклама данного типа будет показываться с зависимости от настроек, которые мы укажем в placement)
    }

    public void ShowNonSkippableVideo() // показ непропускаемого видео с рекламой
    {
        if (Appodeal.canShow(Appodeal.NON_SKIPPABLE_VIDEO) && !Appodeal.isPrecache(Appodeal.NON_SKIPPABLE_VIDEO)) // если реклама готова к показу и не является isPrecache (дешевая или даже бесплатная реклама, которая может быть загружена моментально)
            Appodeal.show(Appodeal.NON_SKIPPABLE_VIDEO); // запускаем показ рекламы, указывая тип рекламы в параметрах
    }

    public void ShowBanner() // показ баннера с дешевой рекламой
    {
        Appodeal.show(Appodeal.BANNER_BOTTOM); // показываем баннер снизу
    }

    public void HideBanner() // скрыть баннер с рекламой
    {
        Appodeal.hide(Appodeal.BANNER_BOTTOM); // скрываем баннер снизу
    }

    // методы обратного вызова содержащиеся в интерфейсах
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

    public void onInterstitialShown() // после показа рекламы вызовется данный метод
    {
        Debug.Log("onInterstitialShown");
        // и в нем будет записана логика для перехода на следующий уровень, например, SceneManager.LoadScene(1);
        // реклама может может быть не показана, так что нужно добавить условие для перехода на следующую сцену также в методах onInterstitialShowFailed() и onInterstitialFailedToLoad()
        // или предусмотреть дополнительные проверки в методе вызова рекламы
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

    public void onRewardedVideoFinished(double amount, string name) // принимает в себя количество и название валюты, которая начислится после просмотра видео
    {
        Debug.Log($"onRewardedVideoFinished. Amount: {amount}, name: {name}");
        OnRewarded?.Invoke(amount, name);
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
}
