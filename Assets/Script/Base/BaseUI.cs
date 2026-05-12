using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.CompilerServices;
using UnityEngine;

public class BaseUI : MonoBehaviour
{
    // [Field]
    
    

    // [OpenUI]
    protected virtual async UniTaskVoid OpenUIAsync(UIType uiType)
    {
        await UIManager.Instance.OepnUIAsync(uiType, destroyCancellationToken);

    }
}
