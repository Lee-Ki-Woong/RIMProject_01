using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public partial class UIManager : MonoBehaviour
{
    // [SerializeField]
    [SerializeField] private GameObject BackGroundUICanvas;
    [SerializeField] private GameObject MainUICanvas;
    [SerializeField] private GameObject ContentUICanvas;
    [SerializeField] private GameObject PopupUICanvas;
    [SerializeField] private GameObject TopUICanvas;

    // [Field]
    public static UIManager Instance {  get; private set; }

    private Dictionary<UIType, GameObject> m_uiListDic = new();
    private HashSet<UIType> m_checkUIHash = new();

    private int sort = 0;

    // [Life Cycle]
    private void Awake()
    {
        if(Instance == null) Instance = this;
        LoadCanvas().Forget();
    }

    private void Start()
    {
    }

    // [Instantiate Canvas]
    private async UniTaskVoid LoadCanvas()
    {
        (BackGroundUICanvas, MainUICanvas, ContentUICanvas, PopupUICanvas, TopUICanvas)
        = await UniTask.WhenAll(
        InstantiateCanvasAsync(BackGroundUICanvas),
        InstantiateCanvasAsync(MainUICanvas),
        InstantiateCanvasAsync(ContentUICanvas),
        InstantiateCanvasAsync(PopupUICanvas),
        InstantiateCanvasAsync(TopUICanvas)
        );

        GetSort(BackGroundUICanvas);
        GetSort(MainUICanvas);
        GetSort(ContentUICanvas);
        GetSort(PopupUICanvas);
        GetSort(TopUICanvas);
    }


    private async UniTask<GameObject> InstantiateCanvasAsync(GameObject canvas)
    {
        if (canvas == null)
        {
            GameObject loadCanvas = await ResourceManager.Instance.LoadAsset<GameObject>("Base/Canvas", destroyCancellationToken);
            GameObject canvasInstance = Instantiate(loadCanvas);
            
            return canvasInstance;
        }

        return canvas;
    }


    private void GetSort(GameObject gameObject)
    {
        Canvas canvas = gameObject.GetComponent<Canvas>();
        canvas.sortingOrder = sort++;
    }


    // [OpenUI]
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

    // [CreateUI]
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

        GameObject uiInstance = Instantiate(ui);
        m_uiListDic.Add(uiType, uiInstance);
        uiInstance.SetActive(true);
    }


    // [CloseUI]
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
