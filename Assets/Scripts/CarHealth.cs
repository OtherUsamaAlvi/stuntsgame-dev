using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarHealth : MonoBehaviour
{
    [SerializeField]
    private float Health=100;
    private OnObsitcleColusions OnObsitcleColusions;
    // Update is called once per frame
    void Update()
    {
        float tempHelth;
        OnObsitcleColusions = FindObjectOfType<OnObsitcleColusions>();
        if(OnObsitcleColusions)
        {
            tempHelth = OnObsitcleColusions.getCarhealth();
            if(tempHelth!=0)
            {
                Health = tempHelth;
            }
        }
       
    }
    public float getHelth()
    {
        return Health;
    }
}
