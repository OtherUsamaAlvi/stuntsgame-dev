using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DriftandAirtimeTracker : MonoBehaviour
{

    public RCC_SceneManager rcc_SceneManager;
    public float pointsMultiplyer=10;
    private float pointsValue = 0;
    [SerializeField]
    private float totalPoints=0;
    public TextMeshProUGUI textMeshPro;
    public TextMeshProUGUI driftingOrAirtime;
    public OnObsitcleColusions onObsitcleColusions;
    [SerializeField]
    private bool isInAir=false;
    // Start is called before the first frame update
    void Start()
    {
        pointsValue = 0;
        onObsitcleColusions = GameObject.FindObjectOfType<OnObsitcleColusions>();
        isInAir = false;

    }

    

    void FixedUpdate()
    {
        if(!onObsitcleColusions)
        {
            onObsitcleColusions = GameObject.FindObjectOfType<OnObsitcleColusions>();
        }
        if (rcc_SceneManager.activePlayerVehicle)
        {
            pointsValue += Time.deltaTime* pointsMultiplyer;
            if (rcc_SceneManager.activePlayerVehicle.driftingNow|| !rcc_SceneManager.activePlayerVehicle.isGrounded)
            {
                if (rcc_SceneManager.activePlayerVehicle.isGrounded)
                {
                    driftingOrAirtime.text = "Drifting";
                    DisplayDriftValue(pointsValue);
                    isInAir = false;
                }
                else
                {
                    
                    //textMeshPro.gameObject.SetActive(false);
                    if (onObsitcleColusions)
                    {
                        if(!onObsitcleColusions.IsGrounded())
                        {
                            isInAir = true;
                            driftingOrAirtime.text = "Airtime";
                            DisplayDriftValue(pointsValue);
                        }
                        else
                        {
                            pointsValue = 0;
                            isInAir = false;
                        }
                       
                    }
                   
                }
      
            }
            else
            {
                textMeshPro.gameObject.SetActive(false);
                pointsValue = 0;
            }

            if(pointsValue==0)
            {
                textMeshPro.gameObject.SetActive(false);

            }
        }
    }
    public void DisplayDriftValue(float driftvalue)
    {
        textMeshPro.gameObject.SetActive(true);
        if (driftvalue < 0)
        {
            driftvalue = 0;
        }
        
        int Value = Mathf.FloorToInt(driftvalue);
        totalPoints += Mathf.FloorToInt(driftvalue);
        textMeshPro.text = Value.ToString();

        
    }
    public float GetTotalPoints()
    {
        return totalPoints;
    }
}
