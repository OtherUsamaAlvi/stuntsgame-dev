using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    public void LoadLevel(string sceneName)
    {
        if (sceneName == "Stage 1"  || sceneName == "SkatePark")
        {
            SceneManager.LoadSceneAsync(sceneName);
        }
        else
        {
            Addressables.LoadSceneAsync(sceneName, UnityEngine.SceneManagement.LoadSceneMode.Single);
            PlayerPrefs.SetInt(sceneName, 1);
        }
    }
}
