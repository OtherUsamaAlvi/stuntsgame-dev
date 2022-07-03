using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class loadingGamePlay : MonoBehaviour
{

    //public Slider loadSlider;
    private float loadSlider;
    public float loadTime;
    public GameObject mainMenuCars;
    public GameObject mainMenu;


    bool once;
    static int ldng;

    void Start()
    {
        try
        {
            //loadSlider.value = 0;
            loadSlider = 0;
            once = false;
            mainMenuCars.SetActive(false);
            
        }
        catch (Exception)
        {

           
        } 
    }

    void Update()
    {        
        try
        {
            //loadSlider.value += Time.deltaTime * loadTime;
            loadTime -= Time.deltaTime;

            if (loadTime <= 0 && !once)
            {                               
                this.gameObject.SetActive(false);
                mainMenuCars.SetActive(true);
                mainMenu.SetActive(true);

                once = true;
            }            
        }
        catch (Exception)
        {

            
        }
        
    }

}
