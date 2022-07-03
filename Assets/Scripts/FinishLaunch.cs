using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishLaunch : MonoBehaviour
{
   
    private RCC_Camera RccCamera;
    
    private RCC_SceneManager rcc_SceneManager;
    //[SerializeField]
   // public RCC_UIController Boostbutton;
    //public GameObject BoostParticles;
    private Vector3 defaultgravity;
    private Rigidbody rb;
    private LaunchTracker Launchers;
    float TPSMaximumFOV;
    float TPSMinimumFOV;
    private void Awake()
    {
        RccCamera = FindObjectOfType<RCC_Camera>();
        rcc_SceneManager = FindObjectOfType<RCC_SceneManager>();
        Launchers = FindObjectOfType<LaunchTracker>();
    }
    private void Start()
    {

        TPSMaximumFOV =RccCamera.TPSMaximumFOV;
        TPSMinimumFOV= RccCamera.TPSMinimumFOV;

        defaultgravity = Physics.gravity;
    }
    private void FixedUpdate()
    {
        if (rcc_SceneManager.activePlayerVehicle)
        {
            rb = rcc_SceneManager.activePlayerVehicle.GetComponent<Rigidbody>();
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "CarTrigger" || other.gameObject.tag == "Player")
        {
            Launchers.islaunched = false;
            if (rcc_SceneManager.activePlayerVehicle.useDamage == false)
                finishlaunch();
        }
    }

    public void finishlaunch()
    {
        //BoostParticles.SetActive(false);
     //   Boostbutton.pressing = false;
        //rb.mass = 1500;
        if (Physics.gravity != defaultgravity)
        {
            //Physics.gravity = defaultgravity;
        }
        if (Time.timeScale != 1f)
        {
            Time.timeScale = 1f;
        }
        TPSZoomReset();
        //RccCamera.useOrbitInTPSCameraMode = true;
        RccCamera.TPSRotationDamping = 3f;
        RccCamera.TPSTiltMultiplier = 2f;
        if (rcc_SceneManager.activePlayerVehicle.useDamage == false)
        {
            StartCoroutine(WaitToEnableDamage());

        }
        
        
    }

    IEnumerator WaitToEnableDamage()
    {
        yield return new WaitForSeconds(0.5f);
        rcc_SceneManager.activePlayerVehicle.useDamage = true;
    }

    public void TPSZoomReset()
    {
        //ORBIT();
        RccCamera.TPSMaximumFOV = TPSMaximumFOV;
        RccCamera.TPSMinimumFOV = TPSMinimumFOV;

    }
}
