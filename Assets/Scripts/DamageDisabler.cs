using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DamageDisabler : MonoBehaviour
{
    [SerializeField]
    public RCC_SceneManager rcc_SceneManager;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "CarTrigger" || other.gameObject.tag == "Player")
        {
            rcc_SceneManager.activePlayerVehicle.useDamage = false;
            
            StartCoroutine(WaitToEnableDamage());
        }
        
       
    }
    IEnumerator WaitToEnableDamage()
    {
        yield return new WaitForSeconds(0.5f);
        rcc_SceneManager.activePlayerVehicle.useDamage = true;
    }
}
