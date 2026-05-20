using Cysharp.Threading.Tasks;
using UnityEngine;

public abstract class BaseUI : MonoBehaviour
{
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
