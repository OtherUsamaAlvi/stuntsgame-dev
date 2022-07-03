using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RotateingStuntsConrroler : MonoBehaviour
{
    public LevelsData levelsData;
    public RCC_SceneManager rcc_SceneManager;
    public RCC_Settings rccSettingsAsset;
    [SerializeField]
    private float speed = 170f;
    
    public float currentPlayerRotation;
    public float lastFramePlayerRotation;
    public string rotationStatus;
    private float numOfSpins = 0;
    [SerializeField]
    private float Spin_Start_Time;
    public bool isrotated = false;
    [SerializeField]
    public float Customgravity = 7f;
    public bool isrotating = false;
    public bool useCustomGravity = false;
    private Vector3 LowGravity;


    public float SpinPointsMultiplyer = 10;
    private float SpinValue = 0;
    //[SerializeField]
    private float SpinPoints = 0;
    public TextMeshProUGUI textMeshPro;
    public TextMeshProUGUI RotationXMultiplyer;

    // Start is called before the first frame update
    private void Start()
    {
        if (rcc_SceneManager.activePlayerVehicle)
        {
            lastFramePlayerRotation = rcc_SceneManager.activePlayerVehicle.transform.rotation.eulerAngles.y;
        }
        rotationStatus = "Stationary";


    }

    private void OnEnable()
    {
        if (rcc_SceneManager.activePlayerVehicle)
        {
            lastFramePlayerRotation = rcc_SceneManager.activePlayerVehicle.transform.rotation.eulerAngles.y;
        }
        rotationStatus = "Stationary";
    }
    private void FixedUpdate()
    {
        

        if (useCustomGravity)
        {
            LowGravity = new Vector3(0.0f, -Customgravity, 0.0f);
            Physics.gravity = LowGravity;
        }

        float steerInput=0;
        
        float y = Input.GetAxisRaw("Horizontal");
        if (RCC.GetController()==1)
        {
            steerInput = RCC_MobileButtons.Instance.inputs.steerInput;
            rcc_SceneManager.activePlayerVehicle.transform.Rotate(new Vector3(0, steerInput, 0) * speed * Time.deltaTime);
        }
        else
        {
            rcc_SceneManager.activePlayerVehicle.transform.Rotate(new Vector3(0, y, 0) * speed * Time.deltaTime);

        }


        if (y != 0|| steerInput!=0)         
        {
            isrotating = true;
        }
        else
        {
            isrotating = false;
            numOfSpins = 0;
        }

        if(isrotating)
        {
            Spin_Start_Time+=0.1f;
            SpinValue += Time.deltaTime * SpinPointsMultiplyer;
            DisplaySpinValue(SpinValue);
        }
        else
        {
            Spin_Start_Time = 0f;

            textMeshPro.gameObject.SetActive(false);
            SpinValue = 0;
            numOfSpins = 0;
        }

        //Spincheck
        currentPlayerRotation = rcc_SceneManager.activePlayerVehicle.transform.rotation.eulerAngles.y;
        currentPlayerRotation = Mathf.Round(currentPlayerRotation);
        
        if (currentPlayerRotation > lastFramePlayerRotation)
        {
            if (!((lastFramePlayerRotation >= 356 && lastFramePlayerRotation <= 360) || (currentPlayerRotation >= 356 && currentPlayerRotation<=360)))
            {
                rotationStatus = "Clockwise";
            }

        }
        else if (currentPlayerRotation < lastFramePlayerRotation)
        {
            if (!((lastFramePlayerRotation >= 0 && lastFramePlayerRotation <= 6) || (currentPlayerRotation >= 0 && currentPlayerRotation <= 6)))
            {
                rotationStatus = "Anticlockwise";
            }

        }
        else
        {

            rotationStatus = "Stationary";

        }



        
        if (Spin_Start_Time >= 10.5f)
        {
            numOfSpins += 1;
            SpinValue=SpinValue * (numOfSpins + 1);
            Spin_Start_Time = 0;
            levelsData.AddRotationpoints();
        }
        


        /*

        #region Clockwise
        if (rotationStatus.Equals("Clockwise"))
        {
            if (currentPlayerRotation >= 350 && currentPlayerRotation <= 360)    // clockwise x >= 350 && x <= 360
            {
                if (isrotated == false)
                {
                    if (Spin_Start_Time >= 7f)
                    {
                        numOfSpins += 1;
                        isrotated = true;
                    }
                }

            }
         

            if (currentPlayerRotation >= 0 && currentPlayerRotation <= 20)
            {
                if (isrotated == true)
                {
                    isrotated = false;
                }


            }

        }
        #endregion



        #region Anticlockwise

        if (rotationStatus.Equals("Anticlockwise"))
        {
            if (currentPlayerRotation >= 0 && currentPlayerRotation <= 10)    // Anticlockwise x >= 350 && x <= 360
            {
                if (isrotated == false)
                {
                    if (Spin_Start_Time >= 7f)
                    {
                        numOfSpins += 1;
                        isrotated = true;
                    }
                }

            }

           

            if (currentPlayerRotation >= 340  && currentPlayerRotation <= 360 )
            {
                if (isrotated == true)
                {
                    isrotated = false;
                }


            }
        }

        #endregion


        */


        lastFramePlayerRotation = currentPlayerRotation;
        
    }

    public void OnDisable()
    {
        
        Spin_Start_Time = 0f;
        isrotating = false;
        if(textMeshPro)
        {
            textMeshPro.gameObject.SetActive(false);
        }
        
        SpinValue = 0;
        numOfSpins = 0;
    }

    public void setRotationSpeed(float rotationSpeed)
    {
        speed = rotationSpeed;
    }
  public void DisplaySpinValue(float spinvalue)
    {
        textMeshPro.gameObject.SetActive(true);
        if (spinvalue < 0)
        {
            spinvalue = 0;
        }

        int Value = Mathf.FloorToInt(spinvalue);
        SpinPoints += Mathf.FloorToInt(spinvalue);
        textMeshPro.text = Value.ToString();
        if (numOfSpins > 0)
        {
            RotationXMultiplyer.text = string.Format("x {0}", numOfSpins + 1);
        }
        else
        {
            RotationXMultiplyer.text = " ";
        }
    }
}

