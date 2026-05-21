using Cysharp.Threading.Tasks;
using TMPro;
using UnityEngine;

public abstract class BaseUI : MonoBehaviour
{
    // [Property]
    public bool m_isAssetLoad {  get; protected set; }

    
    // [Method]
    protected virtual void CloseUI(UIType uiType)
    {
        UIManager.Instance.CloseUI(uiType);
    }

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

    public virtual async UniTask SetAssetAsync() { }


    protected async UniTask<Sprite> MenuBtnSprite()
    {
        Sprite sprite = await LoadUtil.LoadSpriteAsync("Sprite/MainUI/MenuBtn", destroyCancellationToken);
        return sprite;
    }

    protected async UniTask<TMP_FontAsset> BaseFont()
    {
        TMP_FontAsset fontAsset = await LoadUtil.LoadFontAssetAsync("Font/BaseFont", destroyCancellationToken);
        return fontAsset;
    }

}
