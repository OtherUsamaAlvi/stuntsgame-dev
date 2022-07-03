using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakGlass : MonoBehaviour
{
    public Material[] GlassMaterials;
    public GameObject Glass;
    private new Renderer renderer;
    private Material[] mats;
    [SerializeField]
    private int glassState;
    private RCC_SceneManager RCC_SceneManager;
    // Start is called before the first frame update
    void Start()
    {
        renderer = Glass.GetComponent<Renderer>();
        mats = renderer.materials;
        glassState = 0;
        mats[2] = GlassMaterials[0];
        renderer.materials= mats;
        RCC_SceneManager = GameObject.FindObjectOfType<RCC_SceneManager>();
    }

    private void Update()
    {
        if(RCC_SceneManager==null)
        {
            RCC_SceneManager = GameObject.FindObjectOfType<RCC_SceneManager>();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (RCC_SceneManager.activePlayerVehicle)
        {
            if (!other.isTrigger)
            {
                if (RCC_SceneManager.activePlayerVehicle.speed > 100)
                {
                    if (glassState == 0)
                    {
                        mats[2] = GlassMaterials[1];
                        renderer.materials = mats;
                        glassState++;
                    }
                    else if (glassState == 1)
                    {
                        mats[2] = GlassMaterials[2];
                        renderer.materials = mats;
                        glassState++;
                    }
                    else
                    {
                        glassState++;
                    }
                }
            }
        }
    }
}
