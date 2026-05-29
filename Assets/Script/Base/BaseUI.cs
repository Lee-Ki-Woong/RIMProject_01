using Cysharp.Threading.Tasks;
using UnityEngine;

public class BaseUI : MonoBehaviour
{
    public bool IsAssetSyncLoad { get; protected set; } = false;
    public bool IsAssetAsyncLoad { get; protected set; } = false;

    public virtual async UniTask LoadAssetAsync()
    {
        IsAssetAsyncLoad = true;
    }

    public virtual void LoadAssetSync()
    {
        IsAssetSyncLoad = true;
    }
}
