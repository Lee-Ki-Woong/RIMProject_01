using Cysharp.Threading.Tasks;
using UnityEngine;

public abstract class BasePresenter
{
    protected bool m_isAssetLoad;

    public BasePresenter() { }

    public abstract UIType UIType_This { get; }

    protected abstract UniTask LoadAssetAsync();

    protected abstract void LoadData();

    protected void Log(string text)
    {
        Debug.Log($"{this} : " + text);
    }

    protected void LogWarning(string text)
    {
        Debug.LogWarning($"{this} : " + text);
    }

    protected void LogError(string text)
    {
        Debug.LogError($"{this} : " + text);
    }

    protected void LoadLogError(string text)
    {
        LogError($"{text}에 맞는 데이터가 UIDataManager에 없습니다!!");
    }
}

public abstract class BasePresenterTwo : BasePresenter
{
    protected abstract void SubscribeEvents();

    protected abstract void UnsubscribeEvents();
}