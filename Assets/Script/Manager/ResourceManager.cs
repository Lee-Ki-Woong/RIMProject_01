using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class ResourceManager : MonoBehaviour
{
    // [Instance]
    public static ResourceManager Instance {  get; private set; }


    // [Field]
    private Dictionary<string, AsyncOperationHandle> m_handleDic = new();
    private Dictionary<string, int> m_handleCountDic = new();


    // [Life Cycle]
    private void Awake()
    {
        if(Instance == null) Instance = this;
    }


    // [Load Asset]
    public async UniTask<T> LoadAsset<T> (string address, CancellationToken token = default) where T : Object
    {
        if(m_handleDic.TryGetValue(address, out AsyncOperationHandle handle) && handle.IsValid())
        {
            await handle.ToUniTask(cancellationToken : token);
            m_handleCountDic[address]++;
            return handle.Result as T;
        }

        AsyncOperationHandle<T> newHandle = Addressables.LoadAssetAsync<T>(address);
        m_handleDic.Add(address, newHandle);
        m_handleCountDic.Add(address, 1);

        try
        {
            await newHandle.ToUniTask(cancellationToken: token);
            return newHandle.Result;
        }
        catch (System.OperationCanceledException)
        {
            m_handleCountDic.Remove(address);
            m_handleDic.Remove(address);
            if (newHandle.IsValid()) Addressables.Release(newHandle);


            return null;
        }
        catch (System.Exception ex)
        {
            Debug.LogException(ex);

            if (m_handleDic.ContainsKey(address))
            {
                m_handleCountDic.Remove(address);
                m_handleDic.Remove(address);
                if (newHandle.IsValid()) Addressables.Release(newHandle);
            }

            return null;
        }
    }


    // [UnLoad Asset]
    public void UnLoadAsset<T>(string address) where T : Object
    {
        if (m_handleCountDic.ContainsKey(address))
        {
            m_handleCountDic[address] -= 1;

            if (m_handleCountDic[address] <= 0)
            {
                if (m_handleDic.TryGetValue(address, out AsyncOperationHandle handle))
                {
                    Addressables.Release(handle);
                    m_handleDic.Remove(address);
                    m_handleCountDic.Remove(address);
                }
            }
        }
    }
}
