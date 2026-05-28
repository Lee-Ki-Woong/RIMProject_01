using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class ResourceManager : MonoBehaviour
{
    public static ResourceManager Instance {  get; private set; }

    private Dictionary<string, AsyncOperationHandle> m_assetHandle = new();
    private Dictionary<string, int> m_assetLoadCount = new();

    private void Awake()
    {
        AwakeSetting();
    }

    private void AwakeSetting()
    {
        SingleTon();
    }

    private void SingleTon()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    public async UniTask<T> LoadAssetAsync<T>(string address) where T : Object
    {
        if(m_assetHandle.TryGetValue(address, out AsyncOperationHandle operationHandle) && operationHandle.IsValid())
        {
            await operationHandle.ToUniTask();

            if (operationHandle.Status == AsyncOperationStatus.Failed)
            {
                return null;
            }

            m_assetLoadCount[address]++;
            return operationHandle.Result as T;
        }

        AsyncOperationHandle newOperationHandle = Addressables.LoadAssetAsync<T>(address);
        m_assetHandle.Add(address, newOperationHandle);
        m_assetLoadCount.Add(address, 1);

        try
        {
            await newOperationHandle.ToUniTask();
            return newOperationHandle.Result as T;
        }
        catch (System.OperationCanceledException)
        {
            UnLoadAsset(address);
            return null;
        }
        catch (System.Exception e)
        {
            UnLoadAsset(address);
            Debug.LogException(e);
            return null;
        }
    }

    public void UnLoadAsset(string address)
    {
        if(m_assetLoadCount.ContainsKey(address))
        {
            m_assetLoadCount[address]--;

            if (m_assetLoadCount[address] <= 0)
            {
                ReleaseAsset(address);
            }
        }
    }

    private void ReleaseAsset(string address)
    {
        if(m_assetHandle.TryGetValue(address, out AsyncOperationHandle operationHandle))
        {
            m_assetHandle.Remove(address);
            m_assetLoadCount.Remove(address);
            Addressables.Release(operationHandle);
        }
    }
}
