using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyAway : MonoBehaviour
{
    private float forwardForce;
    float selected;
    int selectedSound;
    bool addforce;
    RCC_SceneManager rcc_SceneManager;
    SAudioManager sAudioManager;
    RCC_CarControllerV3 car;
    Rigidbody rb;
    Collider coldr;
    private float[] values = {0.1f,-0.1f};
    private int[] soundValues = { 1, 2 };
    
     

    void Start()
    {
        sAudioManager = FindObjectOfType<SAudioManager>();
        rcc_SceneManager = FindObjectOfType<RCC_SceneManager>();
        selected = values[Random.Range(0, values.Length)];
        selectedSound =soundValues[Random.Range(0, soundValues.Length)];
        addforce = false;
        rb = GetComponent<Rigidbody>();
        coldr = GetComponent<Collider>();
        forwardForce = 100;
       
    }
    private void OnTriggerEnter(Collider other)
    {
        
        if(!sAudioManager)
        {
            sAudioManager = FindObjectOfType<SAudioManager>();
        }
        if (other.gameObject.CompareTag("CarTrigger"))
        {
            StartCoroutine(waitOF());
            if (sAudioManager)
            {
                if (selectedSound == 1)
                {
                    sAudioManager.Play("MonsterBounce1");
                }
                else
                {
                    sAudioManager.Play("MonsterBounce2");
                }
            }
            else
            {
                Debug.Log("Warning!!! No Audio Manager Found In Scene");
            }
        }
        else
        {
            if(addforce == true)
            {
                addforce = false;
            }
        }
    }
    
    IEnumerator waitOF()
    {
        yield return new WaitForSeconds(0.1f);

        if(car)
        {
            if(car.speed>50)
            {

                addforce = true;
            }
        }
        
        
      
    }

    private void FixedUpdate()
    {
        car = rcc_SceneManager.activePlayerVehicle;


        if (addforce == true)
        {
            coldr.enabled = false;
            rb.AddRelativeForce(new Vector3(selected, 0, -5f) * forwardForce);
            
            StartCoroutine(waitBeforeDestroy());
        }
    }

    IEnumerator waitBeforeDestroy()
    {
        
        yield return new WaitForSeconds(2f);
        Destroy(gameObject);
        

    }

}
