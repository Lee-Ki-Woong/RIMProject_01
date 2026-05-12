using Cysharp.Threading.Tasks;
using System.Collections.Generic;
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

    private void Start()
    {
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

        this.OpenBackGround();
        
        this.OpenMainUI();
    }


    public void OpenUI(UIType uiType)
    {
        if(m_checkUIHash.Contains(uiType)) return;


        if (m_uiListDic.TryGetValue(uiType, out GameObject ui))
        {
            ui.SetActive(true);
            m_checkUIHash.Add(uiType);
            return;
        }

        CreateUI(uiType).Forget();
    }


    private async UniTaskVoid CreateUI(UIType uiType)
    {
        m_checkUIHash.Add(uiType);
        string path = this.GetPath(uiType);
        GameObject ui = await LoadUtil.LoadPrefab(path);

        if (ui == null)
        {
            Debug.LogError($"{this.gameObject.name} : {uiType}의 로드에 실패하였습니다.");
            m_checkUIHash.Remove(uiType);
            return;
        }

        GameObject uiInstance = Instantiate(ui, UIFolder);
        m_uiListDic.Add(uiType, uiInstance);
        uiInstance.SetActive(true);
    }


    public void CloseUI(UIType uiType)
    {
        if (m_checkUIHash.Contains(uiType))
        {
            if (m_uiListDic.TryGetValue(uiType, out GameObject ui))
            {
                ui.SetActive(false);
                m_checkUIHash.Remove(uiType);
            }
        }
    }
}
