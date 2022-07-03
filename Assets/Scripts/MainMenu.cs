using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject loadingScreen;

    OnObsitcleColusions onObsitcleColusions;

    public Canvas connectivityPopUp;

    private LevelLoader levelLoader;
    private AddressablesInternetConnectivityManager addressablesInternetConnectivityManager;

    private void Start()
    {
        levelLoader = GetComponent<LevelLoader>();

        addressablesInternetConnectivityManager = GetComponent<AddressablesInternetConnectivityManager>();
    }

    private void Update()
    {
        if(!onObsitcleColusions)
        {
            onObsitcleColusions = FindObjectOfType<OnObsitcleColusions>();
        }
    }

    //public GameObject image;
    public GameObject Main_Menu;
    public RespawnManager respawnManager;
    public void CareerMode()
    {
        if (Main_Menu)
        {
            if (Main_Menu.activeSelf)
            {
                Main_Menu.SetActive(false);
                Time.timeScale = 1;
            }
        }
        
        SceneManager.LoadScene("SelectCarMainMenu");
    }
    public void SelectCar()
    {
        //image.SetActive(false);
    }
    public void FreeMode()
    {
        if (Main_Menu)
        {
            if (Main_Menu.activeSelf)
            {
                Main_Menu.SetActive(false);
                Time.timeScale = 1;
            }
        }

        var sceneName = "SkatePark";

        bool showConnectivityPopUp = addressablesInternetConnectivityManager.ShowConnectivityPopUp(sceneName);

        if (showConnectivityPopUp && Application.internetReachability == NetworkReachability.NotReachable)
        {
            connectivityPopUp.enabled = true;
            Debug.Log("Please connect to the internet");
        }
        else
        {
            loadingScreen.SetActive(true);
            levelLoader.LoadLevel(sceneName);
        }
    }
    public void Quit()
    {
        if (Main_Menu)
        {
            if (Main_Menu.activeSelf)
            {
                Main_Menu.SetActive(false);
                Time.timeScale = 1;
            }
        }
        Application.Quit();
    }
    public void Respawn()
    {
        respawnManager.SetFallen(true);
        onObsitcleColusions.setResetTimeToZero();
        if (Main_Menu)
        {
            if (Main_Menu.activeSelf)
            {
                Main_Menu.SetActive(false);
                Time.timeScale = 1;
            }
        }
        

    }
    public void LoadMainMenu()
    {
        if (Main_Menu)
        {
            if (Main_Menu.activeSelf)
            {
                Main_Menu.SetActive(false);
                Time.timeScale = 1;
            }
        }

        SceneManager.LoadScene("SelectCarMainMenu");
    }
}
