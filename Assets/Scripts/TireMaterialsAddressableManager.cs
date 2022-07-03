using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.AddressableAssets.ResourceLocators;
using UnityEngine.ResourceManagement.AsyncOperations;

public class TireMaterialsAddressableManager : MonoBehaviour
{

    [SerializeField]
    private Material tireGreenMaterial;

    [SerializeField]
    private Material tireBlueMaterial;

    [SerializeField]
    private Material tireRedMaterial;

    [SerializeField]
    private Material tireWhiteMaterial;

    private AsyncOperationHandle monsterLoadingHandle;

    void Start()
    {
        if (Application.internetReachability != NetworkReachability.NotReachable)
        {
            Addressables.InitializeAsync().Completed += AddressableManager_Completed;
        }
    }

    private void AddressableManager_Completed(AsyncOperationHandle<IResourceLocator> obj)
    {
        Addressables.LoadAssetAsync<Texture2D>("greentiretexture").Completed += (green) =>
        {
            tireGreenMaterial.SetTexture("_BaseMap", green.Result);
        };

        Addressables.LoadAssetAsync<Texture2D>("redtiretexture").Completed += (green) =>
        {
            tireRedMaterial.SetTexture("_BaseMap", green.Result);
        };

        Addressables.LoadAssetAsync<Texture2D>("bluetiretexture").Completed += (green) =>
        {
            tireBlueMaterial.SetTexture("_BaseMap", green.Result);
        };

        Addressables.LoadAssetAsync<Texture2D>("whitetiretexture").Completed += (green) =>
        {
            tireWhiteMaterial.SetTexture("_BaseMap", green.Result);
        };

        Addressables.LoadAssetAsync<Texture2D>("greentiretexture").Completed += (green) =>
        {
            tireGreenMaterial.SetTexture("_BaseMap", green.Result);
        };
    }
}
