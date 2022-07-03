using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfiniteNos : MonoBehaviour
{
    public RCC_SceneManager rcc_SceneManager;
    
    public GameObject BoostParticles; 
    // Update is called once per frame
    void FixedUpdate()
    {
        
            
        if (rcc_SceneManager.activePlayerVehicle)
        {
            rcc_SceneManager.activePlayerVehicle.NoS = 100f;
            if (BoostParticles)
            {
                BoostParticles.transform.position = rcc_SceneManager.activePlayerVehicle.transform.position;
                BoostParticles.transform.rotation = rcc_SceneManager.activePlayerVehicle.transform.rotation;
                
                if (Input.GetKeyDown("f"))
                {
                    BoostParticles.SetActive(true);
                }

                BoostParticles.SetActive(false);
                //StartCoroutine(WaitToEnablePhysics());

            }
        }
        
    }

    

    IEnumerator WaitToEnablePhysics()
    {
        yield return new WaitForSeconds(0.5f);
        BoostParticles.SetActive(false);
    }
}
