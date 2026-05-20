using Cysharp.Threading.Tasks;
using System;
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

}
