using UnityEngine;
using System.Collections;
using GoogleMobileAds.Api;
using UnityEngine.Advertisements;
using System;
using UnityEngine.UI;
public class NativeAds3 : MonoBehaviour
{
    [Header("Native Ads")]
    public GameObject adNativePanel;
    public RawImage adIcon;
    public RawImage adChoices;
    public Text adHeadline;
    public Text adCallToAction;
    public Text adAdvertiser;
    public NativeAd nativeAd;
    private bool nativeAdLoaded;
    bool requestOnce = false;

    [Header("Alternative Ads")]
    public GameObject localAd;

    [Header("Native Ad ID")]
    public string NativeID;

    public void RequestNativeAd()
    {
        AdLoader adLoader = new AdLoader.Builder(NativeID)
            .ForNativeAd()
            .Build();
        adLoader.OnNativeAdLoaded += this.HandleNativeAdLoaded;
        adLoader.OnAdFailedToLoad += this.HandleNativeAdFailedToLoad;
        adLoader.LoadAd(new AdRequest.Builder().Build());

    }
    // Update is called once per frame
    void Update()
    {
        try { 
        if (AdsManager.Instance.sdkInitializes)
        {
            if (!requestOnce)
            {
                RequestNativeAd();
                requestOnce = true;
            }
        }
        }
        catch { }


        if (this.nativeAdLoaded)
        {
            this.nativeAdLoaded = false;

            Texture2D iconTexture = this.nativeAd.GetIconTexture();
            Texture2D iconAdChoices = this.nativeAd.GetAdChoicesLogoTexture();
            string headline = this.nativeAd.GetHeadlineText();
            string cta = this.nativeAd.GetCallToActionText();
            string advertiser = this.nativeAd.GetAdvertiserText();            

            adIcon.texture = iconTexture;
            adChoices.texture = iconAdChoices;
            adHeadline.text = headline;
            adAdvertiser.text = advertiser;
            adCallToAction.text = cta;

            nativeAd.RegisterIconImageGameObject(adIcon.gameObject);
            nativeAd.RegisterAdChoicesLogoGameObject(adChoices.gameObject);
            nativeAd.RegisterHeadlineTextGameObject(adHeadline.gameObject);
            nativeAd.RegisterCallToActionGameObject(adCallToAction.gameObject);
            nativeAd.RegisterAdvertiserTextGameObject(adAdvertiser.gameObject);

            adNativePanel.SetActive(true);

        }
    }

    private void HandleNativeAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
    {
        //Debug.Log("Native ad failed to load: " + args.Message);

        try
        {
            Debug.Log("Native ad failed to load: ");
            localAd.GetComponent<Image>().raycastTarget = true;
            localAd.SetActive(true);
        }
        catch { }
    }

    private void HandleNativeAdLoaded(object sender, NativeAdEventArgs args)
    {
        try
        {
            Debug.Log("Native ad loaded.");
            this.nativeAd = args.nativeAd;
            this.nativeAdLoaded = true;
            localAd.GetComponent<Image>().raycastTarget = false;
            localAd.SetActive(false);
        }
        catch { }
    }
}
