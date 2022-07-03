using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Disable RCC  on car and enable Rotating Stunt control
public class RotatingStuntEnabler : MonoBehaviour
{
    public GameObject RotatingCarController;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "CarTrigger" || other.gameObject.tag == "Player")
        {
            RotatingCarController.SetActive(true);
        }

    }

}
