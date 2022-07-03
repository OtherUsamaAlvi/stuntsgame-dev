using UnityEngine;
using System.Collections;
using GoogleMobileAds.Api;
using UnityEngine.Advertisements;
using System;
using UnityEngine.UI;
public class AdsManager : MonoBehaviour
{
    // @copyrights reserved by junaidghani

    // Admob

    [Header("Admob Ads")]
    public AdPosition BannerPosition;


    [SerializeField] private string BannerID = null, InterstitialID = null, RewardedVideoID = null, AppID = null;


    public static bool initializeOnce = true;
    public static int counter_UnityAdmob;
    //private static string extraMessage;

    //CheckReward
    private RewardedAd rewardBasedVideo;
    private static AdsManager _instance;
    InterstitialAd interstitial;

    BannerView bannerView = null;
    BannerView rectBannerView = null;

    public static bool BannerAtLeft;
    private bool showAdmobInsterstitial = false;
    //  private bool showAdmobInsterstitialVideo = false;


    // Unity

    string myPlacementId = "rewardedVideo";
    //[HideInInspector] public bool videoready;


    public enum RewardType
    {
        Store_Coin_Reward,
        Level_Complete_Reward,
        Gun_Reward,
        Skin_Reward
    }
    public RewardType rewardType;

    /*[Header("Native Ads")]
    public GameObject adNativePanel;
    public RawImage adIcon;
    public RawImage adChoices;
    public Text adHeadline;
    public Text adCallToAction;
    public Text adAdvertiser;
    public NativeAd nativeAd;
    private bool nativeAdLoaded;
    */
    public static AdsManager Instance;

    public AdSize rectBannerSize;
    public bool sdkInitializes = false;

    void Awake()
    {
        try
        {
            if (_instance == null)
            {

                _instance = this;
                Instance = _instance;

                if (!PlayerPrefs.HasKey("Native Ad"))
                    PlayerPrefs.SetInt("Native Ad", 1);
                else
                    PlayerPrefs.SetInt("Native Ad", 1);               

                DontDestroyOnLoad(_instance.gameObject);


            }           
        }
        catch
        {


        }



    }

    void Start()
    {
        try
        {
            // extraMessage = null;
            Time.timeScale = 1;

            if (initializeOnce)
            {
                initializeOnce = false;


                intializeAdmobsdk(AppID);

                initAdmobBanner(BannerID);
                initAdmobInterstitial(InterstitialID);
                
                RequestRewardBasedVideo(RewardedVideoID);               

                //RequestNativeAd();                
            }
        }
        catch
        {


        }


    }

    //  ****************************** Admob ***************************************************
    public void intializeAdmobsdk(string appid)
    {
        try
        {
            //.SetMaxAdContentRating(MaxAdContentRating.G)
            RequestConfiguration requestConfiguration = new RequestConfiguration.Builder()
            .SetTagForChildDirectedTreatment(TagForChildDirectedTreatment.True)
            .build();
            MobileAds.SetRequestConfiguration(requestConfiguration);

            MobileAds.Initialize(initStatus => 
            {
                sdkInitializes = true;
            });

        }
        catch
        {


        }

    }
    public void initAdmobBanner(string admobID)
    {
        try
        {
#if UNITY_ANDROID

            rectBannerSize = new AdSize(200, 200);

            bannerView = new BannerView(admobID, AdSize.Banner, BannerPosition);

            rectBannerView = new BannerView(admobID, rectBannerSize, AdPosition.BottomLeft);

            AdRequest request3 = new AdRequest.Builder().Build();
            bannerView.LoadAd(request3);
            bannerView.Hide();
            // for rect banner
            rectBannerView.LoadAd(request3);
            rectBannerView.Hide();
#endif

        }
        catch
        {


        }

    }

   /* public void initAdmobRectBanner(string admobID)
    {
        try
        {
#if UNITY_ANDROID

            rectBannerView = new BannerView(admobID, AdSize.MediumRectangle, AdPosition.BottomLeft);

            AdRequest request3 = new AdRequest.Builder().Build();

            rectBannerView.LoadAd(request3);
            rectBannerView.Hide();
#endif
        }
        catch
        {
        }
    }*/

    public void ShowBanner()
    {
        try
        {
#if UNITY_ANDROID
            
            bannerView.Show();

#endif
        }
        catch
        {


        }


    }
    public void ShowRectangularBanner()
    {
#if !UNITY_EDITOR
        rectBannerView.Show();
#endif
    }

    void HideAdmobInterstitial()
    {
        try
        {
#if UNITY_ANDROID

            showAdmobInsterstitial = false;


#endif
        }
        catch
        {


        }

    }

    public void HideBanner()
    {
        try
        {
#if UNITY_ANDROID

            bannerView.Hide();

#endif
        }
        catch
        {


        }


    }

    public void HideRectBanner()
    {
#if !UNITY_EDITOR
        rectBannerView.Hide();
#endif
    }

    string rewardedVideoID;
    void RequestRewardBasedVideo(string rewardedID)
    {
        //CheckReward
        
        try
        {
#if UNITY_ANDROID

            rewardedVideoID = rewardedID;
            rewardBasedVideo = new RewardedAd(rewardedVideoID);

            // RewardBasedVideoAd is a singleton, so handlers should only be registered once.
            rewardBasedVideo.OnAdLoaded += HandleRewardBasedVideoLoaded;
            rewardBasedVideo.OnAdFailedToLoad += HandleRewardBasedVideoFailedToLoad;
            rewardBasedVideo.OnAdOpening += HandleRewardBasedVideoOpened;
            rewardBasedVideo.OnAdFailedToShow += HandleRewardedAdFailedToShow;
            rewardBasedVideo.OnUserEarnedReward += HandleRewardBasedVideoRewarded;
            rewardBasedVideo.OnAdClosed += HandleRewardBasedVideoClosed;
            rewardBasedVideo.LoadAd(createAdRequest());

#endif
        }
        catch
        {


        }
        
    }
    private AdRequest createAdRequest()
    {

#if UNITY_ANDROID

        return new AdRequest.Builder().Build();


#endif
    }
    public void ShowRewardBasedVideo()
    {
        //CheckReward
        
        try
        {
#if UNITY_ANDROID

            if (rewardBasedVideo.IsLoaded())
            {
                rewardBasedVideo.Show();
                //PlaceYourRewardHere or in a callback at the bottom
            }
            else
            {
                RequestRewardBasedVideo(rewardedVideoID);
                print("Reward based video ad is not ready yet.");
            }


#endif
        }
        catch
        {


        }
        
    }

    public void initAdmobInterstitial(string InterstitialID)
    {
        try
        {
#if UNITY_ANDROID

            interstitial = new InterstitialAd(InterstitialID);
            AdRequest request = new AdRequest.Builder().Build();
            interstitial.LoadAd(request);
            interstitial.OnAdClosed += admobClosed;
            interstitial.OnAdLoaded += delegate (object sender, EventArgs args)
            {
                if (showAdmobInsterstitial)
                {
                    showAdmobInsterstitial = false;
                    interstitial.Show();
                }
            };


#endif
        }
        catch
        {


        }

    }

    void requestInterstitial()
    {
        try
        {
#if UNITY_ANDROID

            AdRequest request = new AdRequest.Builder().Build();
            interstitial.LoadAd(request);


#endif
        }
        catch
        {


        }

    }

    void admobClosed(object sender, EventArgs args)
    {
        try
        {
#if UNITY_ANDROID

            print("interstitial closed.");
            requestInterstitial();


#endif
        }
        catch
        {


        }

        // Handle the ad loaded event.
    }

    public void ShowInterstitail()
    {
        try
        {
#if !UNITY_EDITOR
        
      
            if (interstitial.IsLoaded() == false)
            {
                showAdmobInsterstitial = true;

                requestInterstitial();
            }
            else
                interstitial.Show();
       
         
#endif
        }
        catch
        {


        }

    }

    

    //  ******************************  priority ads ***************************************************

    public void showUnityAdmobInter()
    {
        try
        {           
#if !UNITY_EDITOR

            if (interstitial.IsLoaded())
            {
                interstitial.Show();
            }
            else
            {
                requestInterstitial();
            }
    
#endif
        }
        catch
        {

        }

    }

    public void ShowunityadmobRewardVideo(RewardType reward)
    {
        try
        {

#if !UNITY_EDITOR
        
            rewardType = reward;
            
            if (rewardBasedVideo.IsLoaded())
            {
                rewardBasedVideo.Show();
                print("Reward based video ad is ready");
            }
            else
            {
                RequestRewardBasedVideo(rewardedVideoID);
                print("Reward based video ad is not ready yet.");
            }
   
#endif
        }
        catch
        {


        }
      
    }

    //***************************** admob rewarded events *****************************************************************************
    public void HandleRewardBasedVideoLoaded(object sender, EventArgs args)
    {
        try
        {
            Debug.Log("HandleRewardBasedVideoLoaded event received.");

        }
        catch
        {


        }

    }

    public void HandleRewardBasedVideoFailedToLoad(object sender, AdFailedToLoadEventArgs args)
    {
        //CheckReward
        try
        {
            MonoBehaviour.print("HandleRewardedAdFailedToLoad event received with message: "+ args.LoadAdError.GetMessage());
        }
        catch
        {
        
        }
         
    }

    public void HandleRewardBasedVideoOpened(object sender, EventArgs args)
    {
        try
        {
            Debug.Log("HandleRewardBasedVideoOpened event received");

        }
        catch
        {


        }

    }

    public void HandleRewardedAdFailedToShow(object sender, AdErrorEventArgs args)
    {
        MonoBehaviour.print(
            "HandleRewardedAdFailedToShow event received with message: "
                             + args.AdError.GetMessage());
    }
    public void HandleRewardBasedVideoClosed(object sender, EventArgs args)
    {
        try
        {
            Debug.Log("HandleRewardBasedVideoClosed event received");
            RequestRewardBasedVideo(rewardedVideoID);
        }
        catch
        {


        }

    }

    public void HandleRewardBasedVideoRewarded(object sender, Reward args)
    {
        try
        {
            string type = args.Type;
            double amount = args.Amount;
            Debug.Log("HandleRewardBasedVideoRewarded event received for " + amount.ToString() + " " +
                type);
            //PlaceYourRewardHere or in a isloaded condition where admobreward.show() function shows
            //some

            switch (rewardType)
            {
                case RewardType.Store_Coin_Reward:

                    /*dev-junaid-v2.5.0b41 */


                    //FindObjectOfType<MenuHandler>().RewardCoins();

                    break;
                case RewardType.Level_Complete_Reward:


                    //FindObjectOfType<GameCompleteManager>().OpenPanel(GameCompleteManager.GameStatus.gotReward);


                    break;
                case RewardType.Gun_Reward:

                    //FindObjectOfType<Cut_Scene_Manager>().Get_Reward_Gun();


                    break;
                case RewardType.Skin_Reward:

                    //FindObjectOfType<Cut_Scene_Manager>().Get_Reward_Skin();


                    break;
                default:
                    break;
            }

        }
        catch
        {


        }


    }


    #region Native Ads
  /*  public void RequestNativeAd()
    {
        AdLoader adLoader = new AdLoader.Builder(NativeID)
            .ForNativeAd()
            .Build();
        adLoader.OnNativeAdLoaded += this.HandleNativeAdLoaded;
        adLoader.OnAdFailedToLoad += this.HandleNativeAdFailedToLoad;
        adLoader.LoadAd(new AdRequest.Builder().Build());
        
    }

    void Update()
    {
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
            
            if (PlayerPrefs.GetInt("Native Ad") == 1)
                PlayerPrefs.SetInt("Native Ad", 0);
            
        }
    }

    private void HandleNativeAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
    {
        //Debug.Log("Native ad failed to load: " + args.Message);
        Debug.Log("Native ad failed to load: ");
    }

    private void HandleNativeAdLoaded(object sender, NativeAdEventArgs args)
    {
        Debug.Log("Native ad loaded.");
        this.nativeAd = args.nativeAd;        
        this.nativeAdLoaded = true;        
    }
  */
    #endregion

}
