using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class WrongWayDetection : MonoBehaviour
{

    public RespawnManager respawnManager;
    [SerializeField]
    private float distanceofSpawnPoints;
    [SerializeField]
    private float CurrentdistanceNextSpawnPointCar;
    float lastXVal;
    private OnObsitcleColusions OnObsitcleColusions;
    public RCC_SceneManager RCC_SceneManager;
    public TextMeshProUGUI WrongWayText;
    [SerializeField]
    private bool isGoingWrongway;
    [SerializeField]
    private bool CanGoReverse;
    private int index;

    private void Awake()
    {
        index = 0;
        isGoingWrongway = false;
    }
    void FixedUpdate()
    {
        OnObsitcleColusions = FindObjectOfType<OnObsitcleColusions>();

        for (int i = 0; i < respawnManager.passedCheckpoints.Length; i++)
        {
            if (respawnManager.passedCheckpoints[i])
            {
                index = i;
            }
        }
        distanceofSpawnPoints = Vector3.Distance(respawnManager.spawmPoints[index].transform.position, respawnManager.spawmPoints[index+1].transform.position);
        if (RCC_SceneManager.activePlayerVehicle)
        {
            CanGoReverse = RCC_SceneManager.activePlayerVehicle.IsCarReverseing();
            CurrentdistanceNextSpawnPointCar = Vector3.Distance(RCC_SceneManager.activePlayerVehicle.transform.position, respawnManager.spawmPoints[index + 1].transform.position);
            if (CurrentdistanceNextSpawnPointCar < lastXVal)
            {
                if (RCC_SceneManager.activePlayerVehicle.speed < 10)
                {
                    
                }
                isGoingWrongway = false;
                //Update lastXVal
                lastXVal = CurrentdistanceNextSpawnPointCar;
            }

            else if (CurrentdistanceNextSpawnPointCar > lastXVal)
            {
                if (RCC_SceneManager.activePlayerVehicle.speed > 10)
                {
                    if (CanGoReverse|| !RCC_SceneManager.activePlayerVehicle.isGrounded)
                    {
                        isGoingWrongway = false;
                    }
                    else
                    {
                        if(!OnObsitcleColusions.isOnCircleRamp())
                        {
                            isGoingWrongway = true;
                        }
                       
                    }
                }
                

                //Update lastXVal
                lastXVal = CurrentdistanceNextSpawnPointCar;
            }
        }

        
        if (!isGoingWrongway)
        {
            WrongWayEnable();
        }
        else
        {
            WrongWayDisable();
        }
    }

    public void WrongWayEnable()
    {
        WrongWayText.text = " ";
    }
    public void WrongWayDisable()
    {
        StartCoroutine(waitTillWrongway());
    }
    IEnumerator waitTillWrongway()
    {
        yield return new WaitForSeconds(1.5f);
        if(isGoingWrongway)
        WrongWayText.text = "Wrongway";
    }
}
