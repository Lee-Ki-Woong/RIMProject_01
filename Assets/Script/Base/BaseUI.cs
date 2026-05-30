using Cysharp.Threading.Tasks;
using UnityEngine;

public abstract class BaseUI : MonoBehaviour
{
    public bool IsSetAsset { get; protected set; } = false;


    public abstract void SetData(UIData uiData);
}
