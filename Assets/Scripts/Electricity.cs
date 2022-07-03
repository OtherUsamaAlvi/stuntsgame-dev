using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Electricity : MonoBehaviour
{
    
    public GameObject SimpleLightningBoltPrefab;


    private float timer = 0f;

    public float ONtime = 5f;
    public float OFFtime = 10f;


    void Update()
    {

        timer += Time.deltaTime;
        if (timer < ONtime)
        {

            SimpleLightningBoltPrefab.SetActive(true);
        }
        else
        {
            SimpleLightningBoltPrefab.SetActive(false);
        }
        if (timer > OFFtime)
        {
            timer = 0;
        }
    }
}
