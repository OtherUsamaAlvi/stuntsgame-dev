using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundMaterialChanger : MonoBehaviour
{
    public Material[] GroundMaterial;
    public GameObject Ground;
    [SerializeField]
    private int index = 0;
    /*
     
    private float x, y;
    float scaleX;
    float scaleY;
    float amplitudeX = 4f, omegaX = 1.0f;
    float ind;


    */

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Ground.GetComponent<Renderer>().material = GroundMaterial[index];

    }
    private void FixedUpdate()
    {
        /* 
         
         scaleX = Mathf.Cos(Time.time) * 0.5f + 1;
         scaleY = Mathf.Sin(Time.time) * 0.5f + 1;

        if (index==3)
        {
            y = amplitudeX * Mathf.Cos(omegaX * ind);
            x = amplitudeX * Mathf.Cos(omegaX * ind);
            ind += Time.deltaTime;
            Ground.GetComponent<Renderer>().material.SetTextureOffset("_MainTex", new Vector2(x, y));
        }


        */
    }
    public void RightGroundMaterial()
    {

        if (index >= 0 && index != GroundMaterial.Length)
        {
            index++;
        }
        if (index == GroundMaterial.Length)
        {
            index = 0;
        }
    }
    public void LeftGroundMaterial()
    {


        if (index <= GroundMaterial.Length && index != -1)
        {
            index--;
        }

        if (index == -1)
        {
            index = GroundMaterial.Length - 1;
        }
    }
}
