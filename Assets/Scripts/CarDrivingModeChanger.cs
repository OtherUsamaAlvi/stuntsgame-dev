using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarDrivingModeChanger : MonoBehaviour
{
    private int index = 3;
    RotateingStuntsConrroler rotateing;
    private void Awake()
    {
        rotateing = FindObjectOfType<RotateingStuntsConrroler>();
    }
    private void Start()
    {

        if (rotateing)
            rotateing.gameObject.SetActive(false);
        RCC.SetBehavior(2);
        RCC.SetBehavior(index);
    }
  
}
