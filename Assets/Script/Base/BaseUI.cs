using Cysharp.Threading.Tasks;
using UnityEngine;

public class BaseUI : MonoBehaviour
{
    protected bool isAssetSyncLoad = false;
    protected bool isAssetAsyncLoad = false;

    protected virtual async UniTask LoadAssetAsync()
    {
        isAssetAsyncLoad = true;
    }

    protected virtual void LoadAssetSync()
    {
        isAssetSyncLoad = true;
    }
}
