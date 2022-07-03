using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject loadingScreen;
    public GameObject mainMenuCars;

    public LevelsData levelsData;
    public PersistableSO persistableSO;

    public GameObject privacyPolicy;

    SAudioManager audioManager;
    
    private void Start()
    {
        if (levelsData.showPrivacy)
        {
            privacyPolicy.SetActive(true);
            mainMenuCars.SetActive(false);
        }
        else
        {
            loadingScreen.SetActive(true);
            mainMenuCars.SetActive(false);
        }

        audioManager = FindObjectOfType<SAudioManager>();

        audioManager.Stop("Theme");
        audioManager.ModifySoundParams("MenuMusic", 1f, true);
    }
    public void Accept_Privacy()
    {
        levelsData.showPrivacy = false;
        persistableSO.SaveData();
        privacyPolicy.SetActive(false);
        loadingScreen.SetActive(true);        
    }

    public void ReadMore()
    {
        Application.OpenURL("http://darsontech.com/privacypolicy.html");
    }

}
