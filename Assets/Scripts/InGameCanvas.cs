using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

using UnityEngine.AddressableAssets;
using UnityEngine.Networking;
using UnityEngine.Analytics;

public class InGameCanvas : MonoBehaviour
{
    [Header("Scriptable Objects")]
    public LevelsData levelsData;
    public CarData carData;

    #region RCC Settings Instance

    private RCC_Settings RCCSettingsInstance;
    private RCC_Settings RCCSettingsAsset
    {
        get
        {
            if (RCCSettingsInstance == null)
            {
                RCCSettingsInstance = RCC_Settings.Instance;
                return RCCSettingsInstance;
            }
            return RCCSettingsInstance;
        }
    }

    #endregion

    

    [Header("Reward Score")]
    public Image medalSpriteObject;
    public Sprite[] medalSprites;
    public TextMeshProUGUI totalMoney;
    public TextMeshProUGUI roundPoints;
    

    public TextMeshProUGUI levelNameTMP;

    OnObsitcleColusions onObsitcleColusions;
    public RespawnManager respawnManager;
    public GameObject pauseMenu;
    public GameObject settingsMenu;
    public GameObject prompt;
    public GameObject promptNotEnoughCoins;
    public GameObject completedMenu;

    public TextMeshProUGUI PromptCarName;
    public TextMeshProUGUI PromptCarPrice;
    public TextMeshProUGUI promptNotEnoughCoinsCarName;
    public TextMeshProUGUI promptNotEnoughCoinsCarPrice;
    public TextMeshProUGUI promptPurchaseButtonText;
    public GameObject image;

    public GameObject inGameMenu;
    public GameObject oldCanvas;
    public GameObject finishMenu;
    public GameObject rccCanvas;
    // private GameObject speed;
    public RCC_SceneManager rCC_SceneManager;
    public TextMeshProUGUI speedText;
    public GameObject loadingScreen;
    public Canvas connectivityPopUp;

    private LevelLoader levelLoader;
    private AddressablesInternetConnectivityManager addressablesInternetConnectivityManager;
    public finishline finish;
    int sceneIndex;

    public Image touchScreenButton;
    public Image gyroButton;
    public Image steeringWheelButton;

    public SAudioManager audioManager;

    int setting;  // 0= Buttons , 1 = gyro , 2= Sterring WHeel
    int counter=0;
    bool showResult = true;
    string scenenam;
    int coins;

    int selectedLevelindex;
    public int testIndex;
    public int testRequiredcarIndex;

    
    private void Start()
    {


        if (levelsData.GetControllSettings() == 0)
        {
            Buttons();
        }
        if (levelsData.GetControllSettings() == 1)
        {
            Gyro();
        }
        if (levelsData.GetControllSettings() == 2)
        {
            SteeringWheel();
        }


        audioManager = FindObjectOfType<SAudioManager>();
        image.SetActive(false);
        
        coins = levelsData.GetCoins();

        levelLoader = GetComponent<LevelLoader>();

        
        settingsMenu.SetActive(false);
        pauseMenu.SetActive(false);
        finishMenu.SetActive(false);
        inGameMenu.SetActive(true);
        addressablesInternetConnectivityManager = GetComponent<AddressablesInternetConnectivityManager>();

        var sceneName = SceneManager.GetActiveScene().name;
        scenenam= SceneManager.GetActiveScene().name;
        if (sceneName!="SkatePark")
        {
            int.TryParse(sceneName.Split(' ')[1], out sceneIndex);
            

            levelNameTMP.text = levelsData.GetLevelName(sceneIndex - 1);
        }
        else
        {
            levelNameTMP.text = "Skate Park";
        }

        
        selectedLevelindex = sceneIndex;
        if(selectedLevelindex<10)
        {
            testRequiredcarIndex = levelsData.GetRequiredCar(selectedLevelindex);
        }
        
    }

    private void Update()
    {
        if (!onObsitcleColusions)
        {
            onObsitcleColusions = FindObjectOfType<OnObsitcleColusions>();
        }
        testIndex = MainMenuChangeCar.index;
        if(Input.GetKey(KeyCode.K))
        {
            RCC.SetController(0);
        }

    }

    private void LateUpdate()
    {
        if(rCC_SceneManager.activePlayerVehicle)
        {
            int SPEED = (int)Mathf.Round(rCC_SceneManager.activePlayerVehicle.speed);
            speedText.text = SPEED.ToString();
        }
        if (scenenam != "SkatePark")
        {
            if (finishMenu.activeSelf)
            {
                if (Time.timeScale == 1)
                    Time.timeScale = 0;
                oldCanvas.SetActive(false);
                inGameMenu.SetActive(false);
                pauseMenu.SetActive(false);
                rccCanvas.SetActive(false);
                
                if (showResult == true)
                {
                    showResult = false;
                    medalSpriteObject.sprite = medalSprites[finish.garReward()];

                    if (finish.garReward() == 0)
                    {
                        roundPoints.text = " Level Reward: " + levelsData.GetGoldReward(sceneIndex - 1).ToString() + "    Rotation Points: " + levelsData.GetRotationPoints().ToString();


                        levelsData.AddCoins(levelsData.GetGoldReward(sceneIndex - 1) + levelsData.GetRotationPoints());

                    }
                    if (finish.garReward() == 1)
                    {
                        roundPoints.text = " Level Reward: " + levelsData.GetSilverReward(sceneIndex - 1).ToString() + "    Rotation Points: " + levelsData.GetRotationPoints().ToString();

                        levelsData.AddCoins(levelsData.GetSilverReward(sceneIndex - 1) + levelsData.GetRotationPoints());
                    }
                    if (finish.garReward() == 2)
                    {
                        roundPoints.text = " Level Reward: " + levelsData.GetBronzeReward(sceneIndex - 1).ToString() + "    Rotation Points: " + levelsData.GetRotationPoints().ToString();

                        levelsData.AddCoins(levelsData.GetBronzeReward(sceneIndex - 1) + levelsData.GetRotationPoints());


                    }
                    levelsData.removeRotationPoints();
                }
                if (counter < levelsData.GetCoins())
                    counter += 5;

                totalMoney.text = "Total: " + counter.ToString();
            }
        }

    }
    public void pauseButton()
    {       
        Time.timeScale = 0;
        rccCanvas.SetActive(false);
        pauseMenu.SetActive(true);
        settingsMenu.SetActive(false);
        inGameMenu.SetActive(false);
        oldCanvas.SetActive(false);

        if (AdsManager.Instance)
        {
            AdsManager.Instance.ShowRectangularBanner();
            AdsManager.Instance.HideBanner();
        }

    }
    public void BackToMainMenu()
    {
        levelsData.removeRotationPoints();
        Time.timeScale = 1;
        if (AdsManager.Instance)
        {
            AdsManager.Instance.HideBanner();
            AdsManager.Instance.HideRectBanner();
        }

        Analytics.CustomEvent(string.Concat("Time spent on level ", SceneManager.GetActiveScene().name, ": ", Time.timeSinceLevelLoad));
        loadingScreen.SetActive(true);
        SceneManager.LoadScene("SelectCarMainMenu");
    }
   
    public void Respawn()
    {
        respawnManager.SetFallen(true);
        onObsitcleColusions.setResetTimeToZero();
        


    }
    public void RestartRound()
    {
        levelsData.removeRotationPoints();
        Time.timeScale = 1;
        if (AdsManager.Instance)
        {
            AdsManager.Instance.HideBanner();
            AdsManager.Instance.HideRectBanner();
        }

        var sceneName = SceneManager.GetActiveScene().name;
        bool showConnectivityPopUp = addressablesInternetConnectivityManager.ShowConnectivityPopUp(sceneName);

        if (showConnectivityPopUp && Application.internetReachability == NetworkReachability.NotReachable)
        {
            connectivityPopUp.enabled = true;
            Debug.Log("Please connect to the internet");
        }
        else
        {
            Analytics.CustomEvent(string.Concat("Time spent on level ", SceneManager.GetActiveScene().name, ": ", Time.timeSinceLevelLoad));

            loadingScreen.SetActive(true);
            levelLoader.LoadLevel(sceneName);
        }
    }

    public void Drive()
    {
        Time.timeScale = 1;
        pauseMenu.SetActive(false);
        settingsMenu.SetActive(false);
        inGameMenu.SetActive(true);
        oldCanvas.SetActive(true);
        rccCanvas.SetActive(true);

        if (AdsManager.Instance)
        {
            AdsManager.Instance.HideRectBanner();
            AdsManager.Instance.ShowBanner();
        }
    }


    public void PromptContinue()
    {
        var sceneName = SceneManager.GetActiveScene().name;
        int sceneIndex;
        int.TryParse(sceneName.Split(' ')[1], out sceneIndex);




        if (promptPurchaseButtonText.text == "Purchase")
        {


            if (coins >= carData.GetPrice(levelsData.GetRequiredCar(selectedLevelindex)))
            {




                bool showConnectivityPopUp = addressablesInternetConnectivityManager.ShowConnectivityPopUp(sceneName);

                if (showConnectivityPopUp && Application.internetReachability == NetworkReachability.NotReachable)
                {
                    connectivityPopUp.enabled = true;
                    Debug.Log("Please connect to the internet");
                }
                else
                {
                    carData.SetIsLocked(levelsData.GetRequiredCar(selectedLevelindex), false);

                    coins -= carData.GetPrice(levelsData.GetRequiredCar(selectedLevelindex));
                    levelsData.SetCoins(coins);
                    if (audioManager)
                        audioManager.Play("CoinPurchase");



                    MainMenuChangeCar.index = carData.GetLastUnlockedCar();

                    sceneName = "Stage " + (sceneIndex + 1);
                    prompt.SetActive(false);
                    loadingScreen.SetActive(true);
                    levelLoader.LoadLevel(sceneName);
                }
            }
            else
            {

                prompt.SetActive(false);
                
                
                
                promptNotEnoughCoins.SetActive(true);
            }
        }
        else
        {
            MainMenuChangeCar.index = carData.GetLastUnlockedCar();
            prompt.SetActive(false);
            promptNotEnoughCoins.SetActive(true);


        }
    }


    public void PromptSkip()
    {
        AdsManager.Instance.showUnityAdmobInter();

        prompt.SetActive(false);

        var sceneName = SceneManager.GetActiveScene().name;
        int sceneIndex;
        int.TryParse(sceneName.Split(' ')[1], out sceneIndex);

        sceneIndex++;
        sceneName = "Stage " + sceneIndex;

        bool showConnectivityPopUp = addressablesInternetConnectivityManager.ShowConnectivityPopUp(sceneName);

        if (showConnectivityPopUp && Application.internetReachability == NetworkReachability.NotReachable)
        {
            connectivityPopUp.enabled = true;
            Debug.Log("Please connect to the internet");
        }
        else
        {
            loadingScreen.SetActive(true);
            levelLoader.LoadLevel(sceneName);
        }
    }


    public void PromptNotEnoughCoinsContinue()
    {
        var sceneName = SceneManager.GetActiveScene().name;
        int sceneIndex;
        int.TryParse(sceneName.Split(' ')[1], out sceneIndex);

        sceneIndex++;
        sceneName = "Stage " + sceneIndex;

        promptNotEnoughCoins.SetActive(false);
        image.SetActive(false);
        loadingScreen.SetActive(true);
        levelLoader.LoadLevel(sceneName);
    }


    public void Continue()
    {
        coins = levelsData.GetCoins();
        levelsData.removeRotationPoints();
        Time.timeScale = 1;

        if (AdsManager.Instance)
        {
            AdsManager.Instance.HideBanner();
            AdsManager.Instance.HideRectBanner();
        }

        var sceneName = SceneManager.GetActiveScene().name;
        int sceneIndex;
        int.TryParse(sceneName.Split(' ')[1], out sceneIndex);
        
        sceneIndex++;
        sceneName = "Stage " + sceneIndex;

        bool showConnectivityPopUp = addressablesInternetConnectivityManager.ShowConnectivityPopUp(sceneName);

        if (showConnectivityPopUp && Application.internetReachability == NetworkReachability.NotReachable)
        {
            connectivityPopUp.enabled = true;
            Debug.Log("Please connect to the internet");
        }
        else
        {
            Debug.Log(selectedLevelindex);
            if (MainMenuChangeCar.index < levelsData.GetRequiredCar(selectedLevelindex)+1)
            {
                prompt.SetActive(true);
                finishMenu.SetActive(false);
                image.SetActive(true);
                PromptCarName.text = carData.Get_Name(levelsData.GetRequiredCar(selectedLevelindex));
                PromptCarPrice.text = carData.GetPrice(levelsData.GetRequiredCar(selectedLevelindex)).ToString();
                promptNotEnoughCoinsCarName.text = carData.Get_Name(levelsData.GetRequiredCar(selectedLevelindex));
                promptNotEnoughCoinsCarPrice.text = carData.GetPrice(levelsData.GetRequiredCar(selectedLevelindex)).ToString();
                
                if(carData.IsLocked(levelsData.GetRequiredCar(selectedLevelindex))==true)
                {
                    promptPurchaseButtonText.text = "Purchase";
                   
                }
                else
                {
                    promptPurchaseButtonText.text = "Continue";
                    promptNotEnoughCoinsCarPrice.text = "0";
                }
            }
            else
            {
                Analytics.CustomEvent(string.Concat("Time spent on level ", SceneManager.GetActiveScene().name, ": ", Time.timeSinceLevelLoad));
                loadingScreen.SetActive(true);
                levelLoader.LoadLevel(sceneName);
            }
        }
    }

    //Settings Menu
    public void SettingsBack()
    {
        Time.timeScale = 1;
        pauseMenu.SetActive(false);
        settingsMenu.SetActive(false);
        inGameMenu.SetActive(true);
        oldCanvas.SetActive(true);
        rccCanvas.SetActive(true);
        completedMenu.SetActive(true);
       
        if (AdsManager.Instance)
        {
            AdsManager.Instance.HideRectBanner();
            AdsManager.Instance.ShowBanner();
        }
    }

    public void Settings()
    {

       
        rccCanvas.SetActive(false);
        pauseMenu.SetActive(false);
        settingsMenu.SetActive(true);
        inGameMenu.SetActive(false);
        oldCanvas.SetActive(false);
        completedMenu.SetActive(false);
        Time.timeScale = 0;
        touchScreenButton.color = new Color32(255, 255, 255, 255);
        gyroButton.color = new Color32(255, 255, 255, 255);
        steeringWheelButton.color = new Color32(255, 255, 255, 255);

        if (levelsData.GetControllSettings() == 0)
        {
            touchScreenButton.color = new Color32(255, 0, 66, 255);
        }
        if (levelsData.GetControllSettings() == 1)
        {
            gyroButton.color = new Color32(255, 0, 66, 255);
        }
        if (levelsData.GetControllSettings() == 2)
        {
            steeringWheelButton.color = new Color32(255, 0, 66, 255);
        }
        
    }
    public void Save()
    {
        touchScreenButton.color = new Color32(255, 255, 255, 255);
        gyroButton.color = new Color32(255, 255, 255, 255);
        steeringWheelButton.color = new Color32(255, 255, 255, 255);

        levelsData.SetcontrollsSettings(setting);
        if(levelsData.GetControllSettings()== 0)
        {
            RCCSettingsAsset.mobileController = RCC_Settings.MobileController.TouchScreen;
            touchScreenButton.color = new Color32(255, 0, 66, 255);
        }
        if(levelsData.GetControllSettings() == 1)
        {
            RCCSettingsAsset.mobileController = RCC_Settings.MobileController.Gyro;
            gyroButton.color = new Color32(255, 0, 66, 255);
        }
        if(levelsData.GetControllSettings() == 2)
        {
            RCCSettingsAsset.mobileController = RCC_Settings.MobileController.SteeringWheel;
            steeringWheelButton.color = new Color32(255, 0, 66, 255);
        }
    }

    public void Buttons()
    {
        setting = 0;
        
        Save();
    }

    public void Gyro()
    {
        setting = 1;
        
        Save();
    }

    public void SteeringWheel()
    {
        setting = 2;
        
        Save();
    }


}