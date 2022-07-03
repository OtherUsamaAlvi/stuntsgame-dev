using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarSoundManager : MonoBehaviour
{
    public bool soundsForMobile;
    RCC_SceneManager rcC_SceneManager;
    SAudioManager audioManagerl;
    int playIdle,playNos, playAcceleration, playAccelerationMed, playAccelerationHigh, playbreak = 0;
    [SerializeField]
    GameObject gas, breake;
    [SerializeField]
    RCC_UIController gasButton, breakeButton;
    // Start is called before the first frame update

    private void Awake()
    {
        gas = GameObject.FindGameObjectWithTag("Gas");
        breake = GameObject.FindGameObjectWithTag("Brake");


    }

    private void OnEnable()
    {
        gasButton = gas.GetComponent<RCC_UIController>();
        breakeButton = breake.GetComponent<RCC_UIController>();
    }
    
    void Start()
    {
        rcC_SceneManager = FindObjectOfType<RCC_SceneManager>();
        audioManagerl = FindObjectOfType<SAudioManager>();
    }
    public float inputvertical;
    
   
    private void FixedUpdate()
    {
        inputvertical = Input.GetAxis("Vertical");
        if (rcC_SceneManager.activePlayerVehicle)
        {
            if(rcC_SceneManager.activePlayerVehicle.engineRunning)
            {

                if (playIdle == 0)                                                
                {
                    PlayEngineIdle();
                }
                if(playNos==0)
                {
                    if(rcC_SceneManager.activePlayerVehicle.usingNOS)
                    {
                        PlayNos();
                    }
                    
                }
                if(playNos==1)
                {
                    if (!rcC_SceneManager.activePlayerVehicle.usingNOS)
                    {
                        StopNos();
                    }
                }
                if(playAcceleration==0)
                {
                    if(soundsForMobile==true)
                    {
                        if (gasButton.pressing == true)
                        {

                            PlayAcceleration();
                        }
                    }
                    else
                    {
                        if ((Input.GetAxis("Vertical") < 0 || Input.GetAxis("Vertical") > 0))
                        {

                            PlayAcceleration();
                        }
                    }

                }
                if(playAcceleration==1)
                {
                    if(soundsForMobile == true)
                    {
                        if (gasButton.pressing == false)
                        {
                            StopAcceleration();
                        }
                    }
                    else
                    {
                        if (Input.GetAxis("Vertical") == 0)
                        {

                            StopAcceleration();
                        }
                    }
                    
                   
                }
                if (playbreak == 0)
                {

                    if (soundsForMobile == true)
                    {
                        if (breakeButton.pressing == true)
                        {
                            if (rcC_SceneManager.activePlayerVehicle.speed > 5|| rcC_SceneManager.activePlayerVehicle.driftingNow)
                            {
                                PlayBreake();
                            }
                        }
                    }
                    else
                    {

                        if (Input.GetKeyDown("space")|| Input.GetKeyDown(KeyCode.DownArrow))
                        {
                            if (rcC_SceneManager.activePlayerVehicle.speed > 5 || rcC_SceneManager.activePlayerVehicle.driftingNow)
                            {
                                PlayBreake();
                            }

                        }
                    }
                

                }

                if(playbreak==1)
                {
                    if (rcC_SceneManager.activePlayerVehicle.speed < 5)
                    {
                        StopBreake();
                    }
                }
               

            }
        }
    }

    private void StopBreake()
    {
        audioManagerl.Stop("Breake");
        playbreak = 0;
    }
    private void PlayBreake()
    {
        audioManagerl.Play("Breake");
        playbreak = 1;
    }

    private void PlayAcceleration()
    {
        if(rcC_SceneManager.activePlayerVehicle.speed<50)
        {
            audioManagerl.Play("CarAcceleration");
            audioManagerl.Stop("CarAccelerationmed");
            audioManagerl.Stop("CarAccelerationHigh");
        }
        else if(rcC_SceneManager.activePlayerVehicle.speed < 100 && rcC_SceneManager.activePlayerVehicle.speed > 50)
        {
            audioManagerl.Stop("CarAcceleration");
            audioManagerl.Stop("CarAccelerationHigh");
            audioManagerl.Play("CarAccelerationMed");
        }
        else
        {
            audioManagerl.Stop("CarAccelerationMed");
            audioManagerl.Stop("CarAcceleration");
            audioManagerl.Play("CarAccelerationHigh");
        }
        playAcceleration = 1;
    }

    private void StopAcceleration()
    {
        audioManagerl.Stop("CarAcceleration");
        playAcceleration = 0;
    }

    private void StopNos()
    {
        audioManagerl.Stop("NOs");
        playNos = 0;
    }

    private void PlayNos()
    {
        audioManagerl.Play("NOs");
        playNos = 1;
    }

    private void PlayEngineIdle()
    {
        audioManagerl.Play("EngineIdle");
        playIdle = 1;
    }
}


