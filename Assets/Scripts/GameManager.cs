using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public LevelsData levelsData;
    public CarData carData;
    public RCC_Settings RCCSettingsAsset;
    
    public GameObject mainSpawnPoint;
    private GameObject nosParticles;
    [SerializeField]
    private bool usingNOS;
    RCC_SceneManager rccsceneManager;
    [SerializeField]
    int index;
    public GameObject []cars;

    SAudioManager audioManager;

    private void Awake()
    {


        

        index = MainMenuChangeCar.index;
        
        if(index==0)
        {
            index = 8;
        }



        if(AdsManager.Instance)
            AdsManager.Instance.ShowBanner();
    }
    private void OnEnable()
    {
        
        
        if(index>=1&&index<=7)
        {
            GameObject gameObject = Instantiate(cars[index-1], mainSpawnPoint.transform.position, mainSpawnPoint.transform.rotation);
        }
        else
        {
            GameObject gameObject = Instantiate(cars[0], mainSpawnPoint.transform.position, mainSpawnPoint.transform.rotation);
            //GameObject.Instantiate(Resources.Load("Car1"), mainSpawnPoint.transform.position, mainSpawnPoint.transform.rotation);
        }

        RCC.RegisterPlayerVehicle(GameObject.FindObjectOfType<RCC_CarControllerV3>());
        rccsceneManager = GameObject.FindObjectOfType<RCC_SceneManager>();
        //rccsceneManager.activePlayerVehicle.KillOrStartEngine();

        //rccsceneManager.activePlayerVehicle.StartEngine();
        nosParticles = GameObject.FindGameObjectWithTag("NosParticles");
    }
    private void Start()
    {
        
        Time.timeScale = 1;

        audioManager = FindObjectOfType<SAudioManager>();
        if (audioManager)
        {
            audioManager.Stop("MenuMusic");
            audioManager.Play("Theme");
        }
        if (levelsData.GetControllSettings() == 0)
        {
            RCCSettingsAsset.mobileController = RCC_Settings.MobileController.TouchScreen;
        }
        if (levelsData.GetControllSettings() == 1)
        {
            RCCSettingsAsset.mobileController = RCC_Settings.MobileController.Gyro;
        }
        if (levelsData.GetControllSettings() == 2)
        {
            RCCSettingsAsset.mobileController = RCC_Settings.MobileController.SteeringWheel;
        }
    }
    private void Update()
    {
        if (rccsceneManager.activePlayerVehicle)
            usingNOS = rccsceneManager.activePlayerVehicle.usingNOS;
        if (nosParticles)
        {
            if (rccsceneManager.activePlayerVehicle)
            {
                if (rccsceneManager.activePlayerVehicle.usingNOS)
                {
                    nosParticles.SetActive(true);
                }
                else
                {
                    nosParticles.SetActive(false);
                }
            }
        }
        else
        {
            nosParticles = GameObject.FindGameObjectWithTag("NosParticles");
        }
    }
}
