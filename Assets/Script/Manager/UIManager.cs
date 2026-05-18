using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public partial class UIManager : MonoBehaviour
{
    // [Instance]
    public static UIManager Instance { get; private set; }


    // [SerializeField]
    [SerializeField] private Canvas BackGroundUICanvas;
    [SerializeField] private Canvas MainUICanvas;
    [SerializeField] private Canvas ContentUICanvas;
    [SerializeField] private Canvas PopupUICanvas;
    [SerializeField] private Canvas TopUICanvas;
    
    
    // [Field]
    private Dictionary<UIType, GameObject> m_uiListDic = new();
    private HashSet<UIType> m_checkUIHash = new();


    // [Life Cycle]
    private void Awake()
    {
        if(Instance == null) Instance = this;
    }


    // [Open UI]
    public void OpenUI(UIRootType uiRootType, UIType uiType)
    {
        if(m_checkUIHash.Contains(uiType)) return;


        if (m_uiListDic.TryGetValue(uiType, out GameObject ui))
        {
            ui.SetActive(true);
            m_checkUIHash.Add(uiType);
            return;
        }

        CreateUI(uiRootType, uiType, this.destroyCancellationToken).Forget();
    }


    // [CreateUI]
    private async UniTaskVoid CreateUI(UIRootType uiRootType, UIType uiType, CancellationToken token)
    {
        m_checkUIHash.Add(uiType);
        string path = this.GetPath(uiRootType, uiType);

        try
        {
            GameObject ui = await LoadUtil.LoadPrefab(path, token);

            if (ui == null)
            {
                Debug.LogError($"{this.gameObject.name} : {path}의 로드에 실패하였습니다.");
                m_checkUIHash.Remove(uiType);
                return;
            }

            Canvas canvas = SetCanvas(uiRootType);
            if (canvas == null) return;

            GameObject uiInstance = Instantiate(ui, canvas.transform);
            m_uiListDic.Add(uiType, uiInstance);
            uiInstance.SetActive(true);
        }
        catch(System.OperationCanceledException)
        {
            m_checkUIHash.Remove(uiType);
        }
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


    // [SetUIInCanvas]
    private Canvas SetCanvas(UIRootType uiRootType)
    {
        switch (uiRootType)
        {
            case UIRootType.BackGroundUI: return BackGroundUICanvas;
            case UIRootType.MainUI: return MainUICanvas;
            case UIRootType.ContentUI: return ContentUICanvas;
            case UIRootType.PopupUI: return PopupUICanvas;
            case UIRootType.TopUI: return TopUICanvas;
            default:
                {
                    Debug.LogError($"{uiRootType}에 알맞는 Canvas가 없습니다!! 다시 확인하여 주세요.");
                    return null;
                }
        }
    }
}
