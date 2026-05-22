using Cysharp.Threading.Tasks;
using TMPro;
using UnityEngine;

public abstract class BaseUI : MonoBehaviour
{
    // [Property]
    public bool IsAssetLoad { get; protected set; }


    // [Method]
    public void SetActiveTrue()
    {
        this.gameObject.SetActive(true);
    }

    public void SetActiveFalse()
    {
        this.gameObject.SetActive(false);
    }

    public void SetUIName(string name)
    {
        this.gameObject.name = name;
    }

    protected async UniTask<TMP_FontAsset> BaseFont()
    {
        TMP_FontAsset fontAsset = await LoadUtil.LoadFontAssetAsync("Font/BaseFont", destroyCancellationToken);
        return fontAsset;
    }

    public abstract UniTask SetAssetAsync();
}
