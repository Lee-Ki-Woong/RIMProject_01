using Cysharp.Threading.Tasks;
using UnityEngine;

public class BaseUI : MonoBehaviour
{
    protected bool isAssetSyncLoad = false;
    protected bool isAssetAsyncLoad = false;

    protected void Log(string text)
    {
        Debug.Log($"{this.gameObject} : " + text);
    }

    protected void LogWarning(string text)
    {
        Debug.LogWarning($"{this.gameObject} : " + text);
    }

    protected void LogError(string text)
    {
        Debug.LogError($"{this.gameObject} : " + text);
    }

    protected virtual async UniTask LoadAssetAsync()
    {
        isAssetAsyncLoad = true;
    }

    protected virtual void LoadAssetSync()
    {
        isAssetSyncLoad = true;
    }
}
