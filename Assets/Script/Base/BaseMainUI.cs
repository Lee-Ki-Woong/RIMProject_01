using Cysharp.Threading.Tasks;
using TMPro;
using UnityEngine;

public abstract class BaseMainUI : BaseUI
{
    // [Method]
    protected virtual void CloseUI(UIType uiType)
    {
        UIManager.Instance.CloseUI(uiType);
    }

    protected async UniTask<Sprite> MenuBtnSprite()
    {
        Sprite sprite = await LoadUtil.LoadSpriteAsync("Sprite/MainUI/MenuBtn", destroyCancellationToken);
        return sprite;
    }
}
