using System.Collections.Generic;
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
    private Dictionary<UIType, BaseUI> m_uiListDic = new();
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


        if (m_uiListDic.TryGetValue(uiType, out BaseUI baseUI))
        {
            baseUI.SetActiveTrue();
            m_checkUIHash.Add(uiType);
            return;
        }

        BaseUI newBaseUI = CreateUI<BaseUI>(uiRootType, uiType);
        if (newBaseUI == null) return;

        newBaseUI.SetActiveTrue();
        m_checkUIHash.Add(uiType);
    }


    // [Create UI]
    public T CreateUI<T>(UIRootType uiRootType, UIType uiType) where T : BaseUI
    {
        if(m_uiListDic.TryGetValue(uiType, out BaseUI baseUI))
        {
            return baseUI as T;
        }

        string path = this.GetPath(uiRootType, uiType);


        GameObject prefab = LoadUtil.LoadPrefab(path);
        
        if (prefab == null)
        {
            Debug.LogError($"{this.gameObject.name} : {path}의 로드에 실패하였습니다!!");
            return null;
        }


        Canvas canvas = SetCanvas(uiRootType);

        if (canvas == null)
        {
            Debug.LogError($"{this.gameObject.name} : {uiRootType}의 캔버스가 없습니다!!");
            return null;
        }


        BaseUI baseUIComponent = Instantiate(prefab, canvas.transform).GetComponent<BaseUI>();

        if(baseUIComponent == null)
        {
            Debug.LogError("BaseUI 컴포넌트가 없습니다!!");
            return null;
        }


        baseUIComponent.SetActiveFalse();

        m_uiListDic.Add(uiType, baseUIComponent);
        return baseUIComponent as T;
    }


    // [CloseUI]
    public void CloseUI(UIType uiType)
    {
        if (m_checkUIHash.Contains(uiType))
        {
            if (m_uiListDic.TryGetValue(uiType, out BaseUI baseUI))
            {
                baseUI.SetActiveFalse();
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
