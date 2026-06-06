using Cysharp.Threading.Tasks;

public abstract class BasePresenter
{
    public bool IsAssetLoad { get; protected set; } = false;

    public BasePresenter() { }

    public abstract UniTask LoadAndSetAssetAsync();

    //protected abstract void SubscribeEvents();

    //public abstract void UnsubscribeEvents();
}
