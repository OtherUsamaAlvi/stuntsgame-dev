using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FanObstacle : MonoBehaviour
{
    public float speed = 400;
    public GameObject SpeedPartacles;
    

    private float timer = 0f;

    public float ONtime = 5f;
    public float OFFtime = 10f;
    
    
    void Update()
    {
       
        timer += Time.deltaTime;
        if(timer< ONtime)
        {
            transform.Rotate(new Vector3(0, 0, 1) * speed * Time.deltaTime);  
            SpeedPartacles.SetActive(true);
        }
        else
        {
            SpeedPartacles.SetActive(false);
        }
        if(timer> OFFtime)
        {
            timer = 0;
        }
    }
}
