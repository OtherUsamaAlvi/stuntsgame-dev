using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Networking;
using UnityEngine.ResourceManagement.AsyncOperations;

public class AddressableManagerScript : MonoBehaviour
{
    private void Awake()
    {
        if (!PlayerPrefs.HasKey("Stage 2"))
            PlayerPrefs.SetInt("Stage 2", 0);

        if (!PlayerPrefs.HasKey("Stage 3"))
            PlayerPrefs.SetInt("Stage 3", 0);

        if (!PlayerPrefs.HasKey("Stage 4"))
            PlayerPrefs.SetInt("Stage 4", 0);

        if (!PlayerPrefs.HasKey("Stage 5"))
            PlayerPrefs.SetInt("Stage 5", 0);

        if (!PlayerPrefs.HasKey("Stage 6"))
            PlayerPrefs.SetInt("Stage 6", 0);

        if (!PlayerPrefs.HasKey("Stage 7"))
            PlayerPrefs.SetInt("Stage 7", 0);

        if (!PlayerPrefs.HasKey("Stage 8"))
            PlayerPrefs.SetInt("Stage 8", 0);

        if (!PlayerPrefs.HasKey("Stage 9"))
            PlayerPrefs.SetInt("Stage 9", 0);

        if (!PlayerPrefs.HasKey("Stage 10"))
            PlayerPrefs.SetInt("Stage 10", 0);        
    }
    void Start()
    {
        int stagePlayerPref = 0;
        if (Application.internetReachability != NetworkReachability.NotReachable)
        {
            stagePlayerPref = PlayerPrefs.GetInt("Stage 2");
            if(stagePlayerPref == 0)
                Addressables.DownloadDependenciesAsync("Stage 2").Completed += stage2Completed;

            stagePlayerPref = PlayerPrefs.GetInt("Stage 3");
            if (stagePlayerPref == 0)
                Addressables.DownloadDependenciesAsync("Stage 3").Completed += stage3Completed;

            stagePlayerPref = PlayerPrefs.GetInt("Stage 4");
            if (stagePlayerPref == 0)
                Addressables.DownloadDependenciesAsync("Stage 4").Completed += stage4Completed;

            stagePlayerPref = PlayerPrefs.GetInt("Stage 5");
            if (stagePlayerPref == 0)
                Addressables.DownloadDependenciesAsync("Stage 5").Completed += stage5Completed;

            stagePlayerPref = PlayerPrefs.GetInt("Stage 6");
            if (stagePlayerPref == 0)
                Addressables.DownloadDependenciesAsync("Stage 6").Completed += stage6Completed;

            stagePlayerPref = PlayerPrefs.GetInt("Stage 7");
            if (stagePlayerPref == 0)
                Addressables.DownloadDependenciesAsync("Stage 7").Completed += stage7Completed;

            stagePlayerPref = PlayerPrefs.GetInt("Stage 8");
            if (stagePlayerPref == 0)
                Addressables.DownloadDependenciesAsync("Stage 8").Completed += stage8Completed;

            stagePlayerPref = PlayerPrefs.GetInt("Stage 9");
            if (stagePlayerPref == 0)
                Addressables.DownloadDependenciesAsync("Stage 9").Completed += stage9Completed;

            stagePlayerPref = PlayerPrefs.GetInt("Stage 10");
            if (stagePlayerPref == 0)
                Addressables.DownloadDependenciesAsync("Stage 10").Completed += stage10Completed;
        }
    }
    private void stage2Completed(AsyncOperationHandle obj)
    {
        if (obj.IsDone)
        {
            PlayerPrefs.SetInt("Stage 2", 1);
            Debug.Log("Stage 2 loaded");
        }
    }

    private void stage3Completed(AsyncOperationHandle obj)
    {
        if (obj.IsDone)
        {
            PlayerPrefs.SetInt("Stage 3", 1);
            Debug.Log("Stage 3 loaded");
        }
    }

    private void stage4Completed(AsyncOperationHandle obj)
    {
        if (obj.IsDone)
        {
            PlayerPrefs.SetInt("Stage 4", 1);
            Debug.Log("Stage 4 loaded");
        }
    }

    private void stage5Completed(AsyncOperationHandle obj)
    {
        if (obj.IsDone)
        {
            PlayerPrefs.SetInt("Stage 5", 1);
            Debug.Log("Stage 5 loaded");
        }
    }

    private void stage6Completed(AsyncOperationHandle obj)
    {
        if (obj.IsDone)
        {
            PlayerPrefs.SetInt("Stage 6", 1);
            Debug.Log("Stage 6 loaded");
        }
    }

    private void stage7Completed(AsyncOperationHandle obj)
    {
        if (obj.IsDone)
        {
            PlayerPrefs.SetInt("Stage 7", 1);
            Debug.Log("Stage 7 loaded");
        }
    }

    private void stage8Completed(AsyncOperationHandle obj)
    {
        if (obj.IsDone)
        {
            PlayerPrefs.SetInt("Stage 8", 1);
            Debug.Log("Stage 8 loaded");
        }
    }

    private void stage9Completed(AsyncOperationHandle obj)
    {
        if (obj.IsDone)
        {
            PlayerPrefs.SetInt("Stage 9", 1);
            Debug.Log("Stage 9 loaded");
        }
    }

    private void stage10Completed(AsyncOperationHandle obj)
    {
        if (obj.IsDone)
        {
            PlayerPrefs.SetInt("Stage 10", 1);
            Debug.Log("Stage 10 loaded");
        }
    }

}
