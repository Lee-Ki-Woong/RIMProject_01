using Cysharp.Threading.Tasks;
using UnityEngine;

public class BaseUI : MonoBehaviour
{
    public bool isAssetSyncLoad { get; private set; } = false;
    public bool isAssetAsyncLoad { get; private set; } = false;

    protected virtual async UniTask LoadAssetAsync()
    {
        isAssetAsyncLoad = true;
    }

    protected virtual void LoadAssetSync()
    {
        isAssetSyncLoad = true;
    }
}
