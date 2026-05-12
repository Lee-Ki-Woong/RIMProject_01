using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public partial class UIManager : MonoBehaviour
{
    // [SerializeField]
    [SerializeField] private GameObject MyCanvas;

    // [Field]
    public static UIManager Instance {  get; private set; }
    private Transform UIFolder;

    private Dictionary<UIType, GameObject> m_uiListDic = new();
    private HashSet<UIType> m_checkUIHash = new();

    // [Life Cycle]
    private void Awake()
    {
        if(Instance == null) Instance = this;

        InstantiateCanvas().Forget();
    }

    // [Instantiate]
    private async UniTaskVoid InstantiateCanvas()
    {
        if (MyCanvas == null && UIFolder == null)
        {
            GameObject canvas = await ResourceManager.Instance.LoadAsset<GameObject>("Base/Canvas", destroyCancellationToken);
            MyCanvas = Instantiate(canvas);
            MyCanvas.name = "UIFolder";
            UIFolder = MyCanvas.transform;
        }
    }


    public void OpenUI(UIType uiType)
    {
        if (m_uiListDic.TryGetValue(uiType, out GameObject ui) == false)
        {
            CreateUI(uiType).Forget();
        }
    }


    private async UniTaskVoid CreateUI(UIType uiType)
    {
        string path = this.GetPath(uiType);
        GameObject ui = await GameUtil
    }



}
