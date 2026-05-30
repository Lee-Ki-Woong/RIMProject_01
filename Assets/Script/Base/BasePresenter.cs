using Cysharp.Threading.Tasks;

public abstract class BasePresenter
{
    public bool IsAssetLoad { get; protected set; }

    public abstract UniTask LoadAndSetAssetAsync();
}
