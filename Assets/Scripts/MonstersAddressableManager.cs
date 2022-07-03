using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.AddressableAssets.ResourceLocators;
using UnityEngine.ResourceManagement.AsyncOperations;

public class MonstersAddressableManager : MonoBehaviour
{
    [SerializeField]
    private Transform c11Anchor;

    [SerializeField]
    private Transform chickAnchor1;

    [SerializeField]
    private Transform chickAnchor2;

    [SerializeField]
    private Transform beeAnchor1;

    [SerializeField]
    private Transform beeAnchor2;

    [SerializeField]
    private Transform c21Anchor;

    [SerializeField]
    private Transform c04Anchor;

    [SerializeField]
    private Transform maskTint06Anchor;

    [SerializeField]
    private Transform maskTint02Anchor;

    [SerializeField]
    private Transform seaDragonAnchor;

    [SerializeField]
    private Transform dolphanAnchor;

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
        monsterLoadingHandle = Addressables.InstantiateAsync("C11_Addressable", c11Anchor.position, c11Anchor.rotation);

        monsterLoadingHandle = Addressables.InstantiateAsync("Chick_bouncy_Addressable", chickAnchor1.position, chickAnchor1.rotation);
        monsterLoadingHandle = Addressables.InstantiateAsync("Chick_bouncy_Addressable", chickAnchor2.position, chickAnchor2.rotation);

        monsterLoadingHandle = Addressables.InstantiateAsync("Bee_Addressable", beeAnchor1.position, beeAnchor1.rotation);
        monsterLoadingHandle = Addressables.InstantiateAsync("Bee_Addressable", beeAnchor2.position, beeAnchor2.rotation);

        monsterLoadingHandle = Addressables.InstantiateAsync("C21_Addressable", c21Anchor.position, c21Anchor.rotation);

        monsterLoadingHandle = Addressables.InstantiateAsync("C04_Addressable", c04Anchor.position, c04Anchor.rotation);

        monsterLoadingHandle = Addressables.InstantiateAsync("Dolphan_Addressable", dolphanAnchor.position, dolphanAnchor.rotation);

        monsterLoadingHandle = Addressables.InstantiateAsync("MaskTint02_Addressable", maskTint02Anchor.position, maskTint02Anchor.rotation);

        monsterLoadingHandle = Addressables.InstantiateAsync("MaskTint06_Addressable", maskTint06Anchor.position, maskTint06Anchor.rotation);

        monsterLoadingHandle = Addressables.InstantiateAsync("SeaDragoPolyArtBlueMarine_Addressable", seaDragonAnchor.position, seaDragonAnchor.rotation);
    }
}
