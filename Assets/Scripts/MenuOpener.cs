using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuOpener : MonoBehaviour
{
    public GameObject MainMenu;
    public bool pauseOnPress;

    // Update is called once per frame
    
    public void OpenCloseMenu()
    {
        if(MainMenu.activeSelf)
        {
            MainMenu.SetActive(false);
            Time.timeScale = 1;
        }
        else
        {
            MainMenu.SetActive(true);
            if (pauseOnPress == true)
            {
                Time.timeScale = 0;
            }
            else
            {
                Time.timeScale = 1;
            }
        }
    }
}
