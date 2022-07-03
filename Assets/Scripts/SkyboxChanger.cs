using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyboxChanger : MonoBehaviour
{
    public Material[] skyboxes;
    [SerializeField]
    private int index=0;
    // Use this for initialization
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        RenderSettings.skybox = skyboxes[index];

    }

    public void RightSkyBox()
    {

        if (index >= 0 && index != skyboxes.Length)
        {
            index++;
        }
        if(index== skyboxes.Length)
        {
            index = 0;
        }
    }
    public void LeftSkyBox()
    {

        
        if (index <= skyboxes.Length && index!=-1)
        {
            index--;
        }

        if (index == -1)
        {
            index=skyboxes.Length-1;
        }
    }

    
}
