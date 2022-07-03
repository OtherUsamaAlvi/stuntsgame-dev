using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class RespawnManager : MonoBehaviour 
{
    [HideInInspector]
    private bool fallen=false;
    
    private bool resetingCar = false;

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

    public LevelsData levelsData;
    private int carNumber;
    public bool[] passedCheckpoints;
    public GameObject[] checkPoints;
    public GameObject []spawmPoints;
    
    private GameObject[] cars;
    private GameManager gameManager; 
    private float resetTime=0;

    public bool useCustomRespawn=false;
    public bool useRccRespawn=true;
    public bool useRccCarResetWhenFliped = true;
    public GameObject rotatingCarController;

    public RCC_SceneManager rcc_SceneManager;

    int index;

    private Rigidbody rb;

    //public bool respawnbutton = false;


    private void Awake()
    {
        
        gameManager = FindObjectOfType<GameManager>();
        index = MainMenuChangeCar.index;

        if (index == 0)
        {
            index = 8;
        }
        resetTime = 0;
        passedCheckpoints = new bool[checkPoints.Length+1];
        passedCheckpoints[0] = true;
        //respawnbutton = false;
        fallen = false;
        useRccRespawn = true;
        cars = new GameObject[gameManager.cars.Length];
    }

    private void Start()
    {
        
        for (int i=0;i<gameManager.cars.Length;i++)
        {
            cars[i] = gameManager.cars[i];
        }
    }

    private void FixedUpdate()
    {

        
        if(rcc_SceneManager.activePlayerVehicle)
        {
            rb = rcc_SceneManager.activePlayerVehicle.GetComponent<Rigidbody>();
            rcc_SceneManager.activePlayerVehicle.SetresetCarWhenFliped(useRccCarResetWhenFliped);
        }
        

       
        if(fallen)
        {
            fallen = false;
            if (useCustomRespawn)
            {
                CustomRespawnLogicFallen();
            }
            if(useRccRespawn)
            {
                RCCRespawnLogicFallen();
            }
            if(rotatingCarController)
                 rotatingCarController.SetActive(false);
        }
        /*
        if(respawnbutton)
        {
            respawnbutton = false;
            if (useCustomRespawn)
            {
                CustomRespawnLogicRespawnButton();
            }
            if (useRccRespawn)
            {
                RCCRespawnLogicRespawnButton();
            }

            rotatingCarController.SetActive(false);

        }
        */
        if(useRccCarResetWhenFliped==false)
            ResetCar();
    }


    

    
    public void CustomRespawnLogicFallen()
    {
        
        fallen = false;

        
        //RCC_CustomizerExample.RepairCar();

        for (int i = 0; i < passedCheckpoints.Length; i++)
        {
            if (passedCheckpoints[i])
            {
                rcc_SceneManager.activePlayerVehicle.transform.position = spawmPoints[i].transform.position;
                rcc_SceneManager.activePlayerVehicle.transform.rotation = spawmPoints[i].transform.rotation;
                
            }
        }
        StartCoroutine(WaitToEnablePhysics());

    }




    public void CustomRespawnLogicRespawnButton()
    {
        /*
        respawnbutton = false;
        
        //RCC_CustomizerExample.RepairCar();

        for (int i = 0; i < passedCheckpoints.Length; i++)
        {
            if (passedCheckpoints[i])
            {
                rcc_SceneManager.activePlayerVehicle.transform.position = spawmPoints[i].transform.position;
                rcc_SceneManager.activePlayerVehicle.transform.rotation = spawmPoints[i].transform.rotation;
                
            }
        }
        StartCoroutine(WaitToEnablePhysics());
        */
    }


    IEnumerator WaitToEnablePhysics()
    {

        rb = rcc_SceneManager.activePlayerVehicle.GetComponent<Rigidbody>();
        rcc_SceneManager.activePlayerVehicle.repairNow = true;
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        rb.useGravity = false;
        rcc_SceneManager.activePlayerVehicle.KillEngine();
        rcc_SceneManager.activePlayerVehicle.CreateEngineCurve();
        
        yield return new WaitForSeconds(0.5f);
        rb.useGravity = true;

        rcc_SceneManager.activePlayerVehicle.StartEngine();
        
    }

    IEnumerator WaitBeforeDestroying(GameObject cars)
    {
        if(rotatingCarController)
            rotatingCarController.SetActive(false);
       
        
        yield return new WaitForSeconds(0.1f);
        GameObject.Destroy(cars);
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

    public void RCCRespawnLogicRespawnButton()           
    {
        /*
        Vector3 position= Vector3.zero;
        Quaternion rotation= new Quaternion(0, 0, 0, 0);
        respawnbutton = false;
        OnObsitcleColusions onObsitcleColusions = GameObject.FindObjectOfType<OnObsitcleColusions>();
        if(!onObsitcleColusions)
        {
            onObsitcleColusions = GameObject.FindObjectOfType<OnObsitcleColusions>();
        }
        else
        {
            carNumber = onObsitcleColusions.carNumber;
        }
        
        for (int i = 0; i < passedCheckpoints.Length; i++)
        {
            if (passedCheckpoints[i])
            {
                position = spawmPoints[i].transform.position;
                rotation = spawmPoints[i].transform.rotation;

            }
        }
        
        GameObject cars = GameObject.FindGameObjectWithTag("PlayerVehicle");

        if (!respawnbutton)
        {
            if (carNumber == 1)
            {
                GameObject.Instantiate(Resources.Load("Car1"), position, rotation);
            }
            else if(carNumber==2)
            {
                GameObject.Instantiate(Resources.Load("Car2"), position, rotation);
            }
            rcc_SceneManager.activePlayerVehicle.StartEngine();
        }
        StartCoroutine(WaitBeforeDestroying(cars));
       */


    }


    public void RCCRespawnLogicFallen()
    {
        
        Vector3 position = Vector3.zero;
        Quaternion rotation = new Quaternion(0, 0, 0, 0);
        fallen = false;


        OnObsitcleColusions onObsitcleColusions = GameObject.FindObjectOfType<OnObsitcleColusions>();
        if (!onObsitcleColusions)
        {
            onObsitcleColusions = GameObject.FindObjectOfType<OnObsitcleColusions>();
        }
        else
        {
            carNumber = onObsitcleColusions.carNumber;
        }

        for (int i = 0; i < passedCheckpoints.Length; i++)
        {
            if (passedCheckpoints[i])
            {
                position = spawmPoints[i].transform.position;
                rotation = spawmPoints[i].transform.rotation;

            }
        }

        GameObject oldcar = GameObject.FindGameObjectWithTag("PlayerVehicle");

        if (fallen == false)
        {

            if (index >= 1 && index <= 7)
            {
                GameObject gameObject = Instantiate(cars[index - 1], position, rotation);
            }
            else
            {
                GameObject gameObject = Instantiate(cars[0], position, rotation);
                Debug.Log("Error in Respawn manager, index is less than one or greator than 7");
                //GameObject.Instantiate(Resources.Load("Car1"), mainSpawnPoint.transform.position, mainSpawnPoint.transform.rotation);
            }

            RCC.RegisterPlayerVehicle(GameObject.FindObjectOfType<RCC_CarControllerV3>());
            rcc_SceneManager = GameObject.FindObjectOfType<RCC_SceneManager>();
            //rccsceneManager.activePlayerVehicle.KillOrStartEngine();

            //rccsceneManager.activePlayerVehicle.StartEngine();
            
            rcc_SceneManager.activePlayerVehicle.StartEngine();
            
        }
        StartCoroutine(WaitBeforeDestroying(oldcar));
    }
    public void ResetCar()            // when car is flipped over
    {
        if (rcc_SceneManager.activePlayerVehicle)
        {
            
            if (rcc_SceneManager.activePlayerVehicle.speed < 5 && !rb.isKinematic)
            {
                if (rcc_SceneManager.activePlayerVehicle.transform.eulerAngles.z < 300 && rcc_SceneManager.activePlayerVehicle.transform.eulerAngles.z > 60)
                {
                    resetingCar = true;
                    resetTime += Time.deltaTime;
                    if (resetTime > 3)
                    {
                        resetingCar = false;
                        fallen = true;
                        resetTime = 0f;
                    }
                }
                else
                {
                    resetingCar = false;
                    resetTime = 0f;
                }

            }
            else
            {
                resetingCar = false;
                resetTime = 0f;
            }
        }

    }

    public bool IsResetingCar()
    {
        return resetingCar;
    }
    public void SetFallen(bool falling)
    {
        fallen = falling;
    }
    public bool GetFallen()
    {
        return fallen;
    }
}
