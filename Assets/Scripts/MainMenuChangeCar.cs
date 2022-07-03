using System.Collections;


using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;
using UnityEngine.AddressableAssets;

public class MainMenuChangeCar : MonoBehaviour
{
    [SerializeField]
    public int testindex; 
    public static int index = 0;
    [Header("Huggy and Cars")]
    public GameObject huggy;

    public GameObject[] cars;
   // public GameObject selectLevelPanel;
   
    public GameObject carsObject;

    


    [Header("Background Images")]
    public GameObject image;
    public GameObject levelImage;
    public GameObject garageImage;

    [Header("Menus")]
    public GameObject newMainMenu;
    public GameObject garadgeMenu;
    public GameObject prompt;
    public GameObject promptNotEnoughCoins;
    public Button notEnoughCoinsContinueButton;
    public GameObject loadingScreen;
    public GameObject bottomSlider;
    public GameObject leftRightButtons;
    public GameObject settingsMenu;


    [Header("Garage Elements")]
    public TextMeshProUGUI carNameInMenu;
    public TextMeshProUGUI carPriceInMenu;
    public Slider topSpeed;
    public Slider acceleration;
    public Slider handling;
    public TextMeshProUGUI continueButtonText;

    public TextMeshProUGUI carNameOutside;
    public TextMeshProUGUI carPriceOutside;
    public TextMeshProUGUI totalCoins;


    [Header("Career Mode Elements")]
    public GameObject CareerModeMenu;
    public TextMeshProUGUI roundsCompleted;
    public TextMeshProUGUI PromptCarName;
    public TextMeshProUGUI PromptCarPrice;
    public TextMeshProUGUI promptNotEnoughCoinsCarName;
    public TextMeshProUGUI promptNotEnoughCoinsCarPrice;

    [Header("Career Mode Buttons Text")]
    public TextMeshProUGUI level1Gold;
    public TextMeshProUGUI level1Silver;
    public TextMeshProUGUI level1Bronze;


    public TextMeshProUGUI level2Gold;
    public TextMeshProUGUI level2Silver;
    public TextMeshProUGUI level2Bronze;

    public TextMeshProUGUI level3Gold;
    public TextMeshProUGUI level3Silver;
    public TextMeshProUGUI level3Bronze;

    public TextMeshProUGUI level4Gold;
    public TextMeshProUGUI level4Silver;
    public TextMeshProUGUI level4Bronze;

    public TextMeshProUGUI level5Gold;
    public TextMeshProUGUI level5Silver;
    public TextMeshProUGUI level5Bronze;

    public TextMeshProUGUI level6Gold;
    public TextMeshProUGUI level6Silver;
    public TextMeshProUGUI level6Bronze;

    public TextMeshProUGUI level7Gold;
    public TextMeshProUGUI level7Silver;
    public TextMeshProUGUI level7Bronze;

    public TextMeshProUGUI level8Gold;
    public TextMeshProUGUI level8Silver;
    public TextMeshProUGUI level8Bronze;

    public TextMeshProUGUI level9Gold;
    public TextMeshProUGUI level9Silver;
    public TextMeshProUGUI level9Bronze;

    public TextMeshProUGUI level10Gold;
    public TextMeshProUGUI level10Silver;
    public TextMeshProUGUI level10Bronze;

    [Header("Career Mode Level Names Text")]
    public TextMeshProUGUI level1Name;
    public TextMeshProUGUI level2Name;
    public TextMeshProUGUI level3Name;
    public TextMeshProUGUI level4Name;
    public TextMeshProUGUI level5Name;
    public TextMeshProUGUI level6Name;
    public TextMeshProUGUI level7Name;
    public TextMeshProUGUI level8Name;
    public TextMeshProUGUI level9Name;
    public TextMeshProUGUI level10Name;

    /// <summary>
    /// FreeMode Data
    /// </summary>
    [Header("Freemode Elements")]
    public GameObject freemodeMenu;


    /// <summary>
    /// scriptable objects data
    /// </summary>

    [Header("Scriptable Objects")]
    public CarData carData;
    public LevelsData levelsData;
   

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

    private int lastUnlockedCarIndex;

    [Header("Bottom Car Selection")]
    public Button[] btnCar;
    public Sprite[] carSprite;
    public Sprite[] carLockedSprite;

    
    private float carspeed;             // This is temprary , get this through RCC's Speed later
    private float carAcceleration;
    private float carHandeling;

    public Image touchScreenButton;
    public Image gyroButton;
    public Image steeringWheelButton;

    int coins;
    int selectedLevelindex;
    public SAudioManager audioManager;

    public Canvas connectivityPopUp;

    private LevelLoader levelLoader;
    private AddressablesInternetConnectivityManager addressablesInternetConnectivityManager;

    int setting;  // 0= Buttons , 1 = gyro , 2= Sterring WHeel

   
    void Start()
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

        lastUnlockedCarIndex = carData.GetLastUnlockedCar();
        coins = levelsData.GetCoins();
        totalCoins.text = coins.ToString();

        CarSelectionStatus();

        image.SetActive(true);
        prompt.SetActive(false);
        roundsCompleted.text = levelsData.GetRoundsCompleted().ToString();

        index = lastUnlockedCarIndex;
        
        cars[index-1].SetActive(true);



        carNameInMenu.text = carData.Get_Name(index-1);
        carNameOutside.text = carData.Get_Name(index - 1);
        carPriceOutside.text = carData.GetPrice(index - 1).ToString();
        carPriceInMenu.text = carData.GetPrice(index - 1).ToString();

        newMainMenu.SetActive(true);
        carspeed = carData.Get_Speed(index - 1);                      
        carspeed = (carspeed / 300) * 100;
        carspeed = carspeed / 100;
        topSpeed.value = carspeed;

        carAcceleration = carData.Get_Acceleration(index - 1);
        carAcceleration = (carAcceleration / 700) * 100;
        carAcceleration = carAcceleration / 100;
        acceleration.value = carAcceleration;

        carHandeling = carData.Get_Handeling(index - 1);
        carHandeling = (carHandeling / 500) * 100;
        carHandeling = carHandeling / 100;
        handling.value = carHandeling;

        if(audioManager==null)
            audioManager = FindObjectOfType<SAudioManager>();
        level1Gold.text = levelsData.GetGoldReward(0).ToString();
        level1Silver.text = levelsData.GetSilverReward(0).ToString();
        level1Bronze.text = levelsData.GetBronzeReward(0).ToString();

        level2Gold.text = levelsData.GetGoldReward(1).ToString();
        level2Silver.text = levelsData.GetSilverReward(1).ToString();
        level2Bronze.text = levelsData.GetBronzeReward(1).ToString();

        level3Gold.text = levelsData.GetGoldReward(2).ToString();
        level3Silver.text = levelsData.GetSilverReward(2).ToString();
        level3Bronze.text = levelsData.GetBronzeReward(2).ToString();

        level4Gold.text = levelsData.GetGoldReward(3).ToString();
        level4Silver.text = levelsData.GetSilverReward(3).ToString();
        level4Bronze.text = levelsData.GetBronzeReward(3).ToString();

        level5Gold.text = levelsData.GetGoldReward(4).ToString();
        level5Silver.text = levelsData.GetSilverReward(4).ToString();
        level5Bronze.text = levelsData.GetBronzeReward(4).ToString();

        level6Gold.text = levelsData.GetGoldReward(5).ToString();
        level6Silver.text = levelsData.GetSilverReward(5).ToString();
        level6Bronze.text = levelsData.GetBronzeReward(5).ToString();

        level7Gold.text = levelsData.GetGoldReward(6).ToString();
        level7Silver.text = levelsData.GetSilverReward(6).ToString();
        level7Bronze.text = levelsData.GetBronzeReward(6).ToString();

        level8Gold.text = levelsData.GetGoldReward(7).ToString();
        level8Silver.text = levelsData.GetSilverReward(7).ToString();
        level8Bronze.text = levelsData.GetBronzeReward(7).ToString();

        level9Gold.text = levelsData.GetGoldReward(8).ToString();
        level9Silver.text = levelsData.GetSilverReward(8).ToString();
        level9Bronze.text = levelsData.GetBronzeReward(8).ToString();

        level10Gold.text = levelsData.GetGoldReward(9).ToString();
        level10Silver.text = levelsData.GetSilverReward(9).ToString();
        level10Bronze.text = levelsData.GetBronzeReward(9).ToString();


        level1Name.text = levelsData.GetLevelName(0);
        level2Name.text = levelsData.GetLevelName(1);
        level3Name.text = levelsData.GetLevelName(2);
        level4Name.text = levelsData.GetLevelName(3);
        level5Name.text = levelsData.GetLevelName(4);
        level6Name.text = levelsData.GetLevelName(5);
        level7Name.text = levelsData.GetLevelName(6);
        level8Name.text = levelsData.GetLevelName(7);
        level9Name.text = levelsData.GetLevelName(8);
        level10Name.text = levelsData.GetLevelName(9);

        levelLoader = GetComponent<LevelLoader>();
        addressablesInternetConnectivityManager = GetComponent<AddressablesInternetConnectivityManager>();

    }

    private void Update()
    {
        testindex = index;
    }


    public void setIndex(int ind)
    {
        index = ind;
    }
    public void Back()
    {
        image.SetActive(true);
       
    }
   
    public void SelectCar()
    {
        image.SetActive(false);
      
    }
    public void FreeModeButton()
    {
        levelImage.SetActive(true);
        image.SetActive(false);
        newMainMenu.SetActive(false);
        freemodeMenu.SetActive(true);
        carsObject.SetActive(false);
    }
    public void FreemodeBack()
    {
        levelImage.SetActive(false);
        image.SetActive(true);
        newMainMenu.SetActive(true);
        freemodeMenu.SetActive(false);
        carsObject.SetActive(true);

    }


    public void CareerModeButton()
    {
        image.SetActive(false);
        newMainMenu.SetActive(false);
        levelImage.SetActive(true);
        CareerModeMenu.SetActive(true);
        carsObject.SetActive(false);

    }
    public void CareerModeBackButton()
    {
        CareerModeMenu.SetActive(false);
        image.SetActive(true);
        newMainMenu.SetActive(true);
        levelImage.SetActive(false);
        carsObject.SetActive(true);


    }
    public void RightButton()
    {
       
        if (index == cars.Length)
        {

            cars[0].SetActive(true);
            cars[cars.Length-1].SetActive(false);
            index = 1;
          

            
        }
        else
        {

            cars[index-1].SetActive(false);
            cars[index].SetActive(true);
            carspeed += 20;                      // Set This To RCC's Speed after If Else Condition
            index++;
            
        }

        //carspeed = RCC_SceneManager.activeplayer.topspeed;
        carNameInMenu.text = carData.Get_Name(index - 1);
        carPriceInMenu.text = carData.GetPrice(index - 1).ToString();

        carNameOutside.text = carData.Get_Name(index - 1);
        carPriceOutside.text = carData.GetPrice(index - 1).ToString();

        promptNotEnoughCoinsCarName.text = carData.Get_Name(index - 1);
        promptNotEnoughCoinsCarPrice.text = carData.GetPrice(index - 1).ToString();

        carspeed = carData.Get_Speed(index - 1);
        carspeed = (carspeed / 300) * 100;
        carspeed = carspeed / 100;
        topSpeed.value = carspeed;

        carAcceleration = carData.Get_Acceleration(index - 1);
        carAcceleration = (carAcceleration / 400) * 100;
        carAcceleration = carAcceleration / 100;
        acceleration.value = carAcceleration;

        carHandeling = carData.Get_Handeling(index - 1);
        carHandeling = (carHandeling / 200) * 100;
        carHandeling = carHandeling / 100;
        handling.value = carHandeling;

        if (carData.IsLocked(index-1))
        {
            continueButtonText.text = "Purchase";
        }
        else
        {
            continueButtonText.text = "Continue";
        }
    }
    
    public void continueButton()
    {
        
        if (continueButtonText.text == "Continue")
        {
            GarageBackButton();
        }
        else
        {
            if(coins>=carData.GetPrice(index-1))
            {
                carData.SetIsLocked(index - 1, false);
                lastUnlockedCarIndex = carData.GetLastUnlockedCar();
                coins -= carData.GetPrice(index - 1);
                continueButtonText.text = "Continue";
                levelsData.SetCoins(coins);
                totalCoins.text = levelsData.GetCoins().ToString();
                CarSelectionStatus();
                audioManager.Play("CoinPurchase");
            }
            else
            {
                promptNotEnoughCoins.SetActive(true);
                carsObject.SetActive(false);
                leftRightButtons.SetActive(false);
                bottomSlider.SetActive(false);

            }

        }
        
    }

    public void LeftButton()
    {
        if (index == 1)
        {

            cars[index - 1].SetActive(false);
            cars[cars.Length - 1].SetActive(true);
            index = cars.Length;
            
        }
        else
        {

            cars[index - 1].SetActive(false);
            cars[index - 2].SetActive(true);
            carspeed -= 20;                      // Set This To RCC's Speed after If Else Condition
            index--;
           
        }

        carNameInMenu.text = carData.Get_Name(index - 1);
        carPriceInMenu.text = carData.GetPrice(index - 1).ToString();

        carNameOutside.text = carData.Get_Name(index - 1);
        carPriceOutside.text = carData.GetPrice(index - 1).ToString();

        promptNotEnoughCoinsCarName.text = carData.Get_Name(index - 1);
        promptNotEnoughCoinsCarPrice.text = carData.GetPrice(index - 1).ToString();

        carspeed = carData.Get_Speed(index - 1);
        carspeed = (carspeed / 300) * 100;
        carspeed = carspeed / 100;
        topSpeed.value = carspeed;

        carAcceleration = carData.Get_Acceleration(index - 1);
        carAcceleration = (carAcceleration / 400) * 100;
        carAcceleration = carAcceleration / 100;
        acceleration.value = carAcceleration;

        carHandeling = carData.Get_Handeling(index - 1);
        carHandeling = (carHandeling / 200) * 100;
        carHandeling = carHandeling / 100;
        handling.value = carHandeling;



        if (carData.IsLocked(index - 1))
        {
            continueButtonText.text = "Purchase";
        }
        else
        {
            continueButtonText.text = "Continue";
        }

    }
    public void ChooseCar()
    {
        int coin;
        PlayerPrefs.SetInt("selectedIndex", 1);
        if (PlayerPrefs.HasKey("coins"))
        {
            coin = PlayerPrefs.GetInt("coins");
        }
        else
        {
            coin = 0;
        }
        if(coin==0)
        {
            if(index==1)
            {
                image.SetActive(false);
               
            }
        }    
        
        
    }

    public void Quit()
    { 

        Application.Quit();
    }
    

public void GarageBackButton()
    {
        huggy.SetActive(true);
        leftRightButtons.SetActive(true);
        bottomSlider.SetActive(true);
        garageImage.SetActive(false);
        image.SetActive(true);
        newMainMenu.SetActive(true);
        garadgeMenu.SetActive(false);
        if(carData.IsLocked(index - 1))
        {

            cars[index - 1].SetActive(false);
            index = lastUnlockedCarIndex;
            cars[index - 1].SetActive(true);
        }
        else
        {
            cars[index - 1].SetActive(false);
            cars[index - 1].SetActive(true);
        }

        prompt.SetActive(false);
        promptNotEnoughCoins.SetActive(false);
        carsObject.SetActive(true);
       
    }

    public void CarGirrage()
    {
        huggy.SetActive(false);

        if (carData.IsLocked(index - 1))
        {
            continueButtonText.text = "Purchase";
        }
        else
        {
            continueButtonText.text = "Continue";
        }

        garageImage.SetActive(true);
        image.SetActive(false);
        newMainMenu.SetActive(false);
        garadgeMenu.SetActive(true);

        carNameInMenu.text = carData.Get_Name(index - 1);
        carPriceInMenu.text = carData.GetPrice(index - 1).ToString();

        carNameOutside.text = carData.Get_Name(index - 1);
        carPriceOutside.text = carData.GetPrice(index - 1).ToString();

        promptNotEnoughCoinsCarName.text= carData.Get_Name(index - 1);
        promptNotEnoughCoinsCarPrice.text = carData.GetPrice(index - 1).ToString();

        carspeed = carData.Get_Speed(index - 1);
        carspeed = (carspeed / 300) * 100;
        carspeed = carspeed / 100;
        topSpeed.value = carspeed;

        carAcceleration = carData.Get_Acceleration(index - 1);
        carAcceleration = (carAcceleration / 400) * 100;
        carAcceleration = carAcceleration / 100;
        acceleration.value = carAcceleration;

        carHandeling = carData.Get_Handeling(index - 1);
        carHandeling = (carHandeling / 200) * 100;
        carHandeling = carHandeling / 100;
        handling.value = carHandeling;

        cars[index - 1].SetActive(false);
        cars[index - 1].SetActive(true);
    }


    public void CareerMode()
    {

        image.SetActive(true);
       
    }
    public void SideObjectsDemo()
    {
        PlayerPrefs.SetInt("selectedIndex", index);
        SceneManager.LoadScene("SideObjectsDemo");
    }
    
    public void FreeMode()
    {
        PlayerPrefs.SetInt("selectedIndex", index);

        var sceneName = "SkatePark";

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


    public void Settings()
    {
        newMainMenu.SetActive(false);
        image.SetActive(false);
        settingsMenu.SetActive(true);
        levelImage.SetActive(true);
        carsObject.SetActive(false);
        huggy.SetActive(false);

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

    public void BackSettings()
    {
        newMainMenu.SetActive(true);
        image.SetActive(true);
        settingsMenu.SetActive(false);
        levelImage.SetActive(false);
        carsObject.SetActive(true);
        huggy.SetActive(true);
    }
    public void SaveSettings()
    {
        touchScreenButton.color = new Color32(255, 255, 255, 255);
        gyroButton.color = new Color32(255, 255, 255, 255);
        steeringWheelButton.color = new Color32(255, 255, 255, 255);

        levelsData.SetcontrollsSettings(setting);
        if (levelsData.GetControllSettings() == 0)
        {
            RCCSettingsAsset.mobileController = RCC_Settings.MobileController.TouchScreen;
            touchScreenButton.color = new Color32(255, 0, 66, 255);
        }
        if (levelsData.GetControllSettings() == 1)
        {
            RCCSettingsAsset.mobileController = RCC_Settings.MobileController.Gyro;
            gyroButton.color = new Color32(255, 0, 66, 255);
        }
        if (levelsData.GetControllSettings() == 2)
        {
            RCCSettingsAsset.mobileController = RCC_Settings.MobileController.SteeringWheel;
            steeringWheelButton.color = new Color32(255, 0, 66, 255);
        }

    }

    public void Buttons()
    {
        setting = 0;
        
        
        SaveSettings();
    }

    public void Gyro()
    {
        setting = 1;
        
        
        SaveSettings();
    }

    public void SteeringWheel()
    {
        setting = 2;
        
       
        SaveSettings();
    }

    #region Level Buttons


    public void Level(int levelIndex)
    {

        if (!levelsData.careerMode[levelIndex].isLocked)
        {
            selectedLevelindex = levelIndex + 1;
            CareerModeMenu.SetActive(false);
            if (index < levelsData.GetRequiredCar(levelIndex)+1)
            {
                prompt.SetActive(true);
                PromptCarName.text = carData.Get_Name(levelsData.GetRequiredCar(levelIndex));
                PromptCarPrice.text = carData.GetPrice(levelsData.GetRequiredCar(levelIndex)).ToString();
                promptNotEnoughCoinsCarName.text = carData.Get_Name(levelsData.GetRequiredCar(levelIndex));
                promptNotEnoughCoinsCarPrice.text = carData.GetPrice(levelsData.GetRequiredCar(levelIndex)).ToString();
            }
            else
            {
                AdsManager.Instance.showUnityAdmobInter();

                audioManager.ModifySoundParams("MenuMusic", .1f, true);
                audioManager.Play("GameStartSpeedingSound");

                int stageIndex = levelIndex + 1;

                var sceneName = "Stage " + stageIndex;

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
        }
    }

    public void PromptNotEnoughCoinsContinue()
    {
        promptNotEnoughCoins.SetActive(false);
        carsObject.SetActive(true);
        leftRightButtons.SetActive(true);
        bottomSlider.SetActive(true);
    }

    public void PromptContinue()
    {
        
        if (carData.IsLocked(levelsData.GetRequiredCar(selectedLevelindex - 1)) == true)
        {

            
            if (coins >= carData.GetPrice(levelsData.GetRequiredCar(selectedLevelindex - 1)))
            {


                var sceneName = "Stage " + selectedLevelindex;

                bool showConnectivityPopUp = addressablesInternetConnectivityManager.ShowConnectivityPopUp(sceneName);

                if (showConnectivityPopUp && Application.internetReachability == NetworkReachability.NotReachable)
                {
                    connectivityPopUp.enabled = true;
                    Debug.Log("Please connect to the internet");
                }
                else
                {

                    carData.SetIsLocked(levelsData.GetRequiredCar(selectedLevelindex - 1), false);

                    lastUnlockedCarIndex = carData.GetLastUnlockedCar();
                    coins -= carData.GetPrice(levelsData.GetRequiredCar(selectedLevelindex - 1));
                    levelsData.SetCoins(coins);
                    audioManager.Play("CoinPurchase");

                    totalCoins.text = levelsData.GetCoins().ToString();
                    CarSelectionStatus();
                    index = lastUnlockedCarIndex;

                    sceneName = "Stage " + selectedLevelindex;
                    prompt.SetActive(false);

                    StartCoroutine(LoadingScreen(sceneName));
                }
            }
            else
            {
                var sceneName = "Stage " + selectedLevelindex;
                prompt.SetActive(false);
                promptNotEnoughCoins.SetActive(true);

                notEnoughCoinsContinueButton.interactable = false;
                StartCoroutine(LoadingScreen(sceneName));
            }
        }
        else
        {
            var sceneName = "Stage " + selectedLevelindex;
            prompt.SetActive(false);
            promptNotEnoughCoins.SetActive(true);
            lastUnlockedCarIndex = carData.GetLastUnlockedCar();
            index = lastUnlockedCarIndex;
            notEnoughCoinsContinueButton.interactable = false;
            StartCoroutine(LoadingScreen(sceneName));
        }
    }

    IEnumerator LoadingScreen(string sceneName)
    {
        yield return new WaitForSeconds(3f);
        promptNotEnoughCoins.SetActive(false);
        loadingScreen.SetActive(true);
        levelLoader.LoadLevel(sceneName);

    }

    public void PromptSkip()
    {
        AdsManager.Instance.showUnityAdmobInter();
        prompt.SetActive(false);
        var sceneName = "Stage " + selectedLevelindex;

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

    #endregion

    #region Car Bottom Panel Selection
    void CarSelectionStatus()
    {
        for(int i = 0; i < btnCar.Length; i++)
        {
            if (carData.carData[i].isLocked)
                btnCar[i].GetComponent<Image>().sprite = carLockedSprite[i];
            else
                btnCar[i].GetComponent<Image>().sprite = carSprite[i];
        }
    }

    public void Select_Car(int carNo)
    {        
        if (index != carNo)
        {            
            cars[index - 1].SetActive(false);
            cars[carNo - 1].SetActive(true);

            index = carNo;


            carNameInMenu.text = carData.Get_Name(index - 1);
            carPriceInMenu.text = carData.GetPrice(index - 1).ToString();

            carNameOutside.text = carData.Get_Name(index - 1);
            carPriceOutside.text = carData.GetPrice(index - 1).ToString();

            promptNotEnoughCoinsCarName.text = carData.Get_Name(index - 1);
            promptNotEnoughCoinsCarPrice.text = carData.GetPrice(index - 1).ToString();

            carspeed = carData.Get_Speed(index - 1);
            carspeed = (carspeed / 300) * 100;
            carspeed = carspeed / 100;
            topSpeed.value = carspeed;

            carAcceleration = carData.Get_Acceleration(index - 1);
            carAcceleration = (carAcceleration / 700) * 100;
            carAcceleration = carAcceleration / 100;
            acceleration.value = carAcceleration;

            carHandeling = carData.Get_Handeling(index - 1);
            carHandeling = (carHandeling / 500) * 100;
            carHandeling = carHandeling / 100;
            handling.value = carHandeling;

            if (carData.IsLocked(index - 1))
            {
                continueButtonText.text = "Purchase";
            }
            else
            {
                continueButtonText.text = "Continue";
            }
        }
    }

    #endregion
}
