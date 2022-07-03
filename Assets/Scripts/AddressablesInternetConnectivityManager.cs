using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddressablesInternetConnectivityManager : MonoBehaviour
{
    public bool ShowConnectivityPopUp(string sceneName)
    {
        bool showConnectivityPopUp = false;

        if (sceneName == "Stage 2")
        {
            showConnectivityPopUp = (PlayerPrefs.GetInt("Stage 2") == 1) ? false : true;
        }
        else if (sceneName == "Stage 3")
        {
            showConnectivityPopUp = (PlayerPrefs.GetInt("Stage 3") == 1) ? false : true;
        }
        else if (sceneName == "Stage 4")
        {
            showConnectivityPopUp = (PlayerPrefs.GetInt("Stage 4") == 1) ? false : true;
        }
        else if (sceneName == "Stage 5")
        {
            showConnectivityPopUp = (PlayerPrefs.GetInt("Stage 5") == 1) ? false : true;
        }
        else if (sceneName == "Stage 6")
        {
            showConnectivityPopUp = (PlayerPrefs.GetInt("Stage 6") == 1) ? false : true;
        }
        else if (sceneName == "Stage 7")
        {
            showConnectivityPopUp = (PlayerPrefs.GetInt("Stage 7") == 1) ? false : true;
        }
        else if (sceneName == "Stage 8")
        {
            showConnectivityPopUp = (PlayerPrefs.GetInt("Stage 8") == 1) ? false : true;
        }
        else if (sceneName == "Stage 9")
        {
            showConnectivityPopUp = (PlayerPrefs.GetInt("Stage 9") == 1) ? false : true;
        }
        else if (sceneName == "Stage 10")
        {
            showConnectivityPopUp = (PlayerPrefs.GetInt("Stage 10") == 1) ? false : true;
        }

        return showConnectivityPopUp;
    }
}
