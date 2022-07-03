using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class CarModeChanger : MonoBehaviour
{
    private int index = 3;
    public TextMeshProUGUI CarMode;
    private void Start()
    {
        index = 3;
        RCC.SetBehavior(index);
    }
    void Update()
    {
        
        

        if(index==0)
        {
            CarMode.text = "Simulator";
        }
        if (index == 1)
        {
            CarMode.text = "Racing";
        }
        if (index == 2)
        {
            CarMode.text = "Drift";
        }
        if (index == 3)
        {
            CarMode.text = "Sami Arcade";
        }
        if (index == 4)
        {
            CarMode.text = "Fun";
        }
    }
    public void RightCArMode()
    {

        if (index >= 0 && index != 5)
        {
            index++;
        }
        if (index == 5)
        {
            index = 0;
        }
        RCC.SetBehavior(index);
    }
    public void LeftCArMode()
    {

        
        if (index <= 5 && index != -1)
        {
            index--;
        }

        if (index == -1)
        {
            index = 5 - 1;
        }
        RCC.SetBehavior(index);
    }
}
