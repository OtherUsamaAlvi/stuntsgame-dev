using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// Disable Rotating Stunt control on car and enable RCC
public class RotatingStuntDisabler : MonoBehaviour
{
    [SerializeField]
    RotateingStuntsConrroler rotatingCarController;

    private void Awake()
    {
        rotatingCarController = FindObjectOfType<RotateingStuntsConrroler>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "CarTrigger" || other.gameObject.tag == "Player")
        {
            rotatingCarController.gameObject.SetActive(false);
            
        }
    }
}
