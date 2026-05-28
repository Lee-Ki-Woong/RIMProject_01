using Cysharp.Threading.Tasks;
using UnityEngine;

public class BaseUI : MonoBehaviour
{
    public bool isAssetSyncLoad { get; private set; } = false;
    public bool isAssetAsyncLoad { get; private set; } = false;

    public virtual void ActiveTrue()
    {
        this.gameObject.SetActive(true);
    }

    public virtual void ActiveFalse()
    {
        this.gameObject.SetActive(false);
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
