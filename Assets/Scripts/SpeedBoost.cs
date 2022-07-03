using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedBoost : MonoBehaviour
{


    public RCC_SceneManager rcc_SceneManager;
    public float speedmultiplyer=50f;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.tag == "CarTrigger" || other.gameObject.tag == "Player")
        {
            rcc_SceneManager.activePlayerVehicle.speed = rcc_SceneManager.activePlayerVehicle.speed * speedmultiplyer;

        }
    }
}
