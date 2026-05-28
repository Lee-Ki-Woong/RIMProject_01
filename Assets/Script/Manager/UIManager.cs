using System.Collections.Generic;
using UnityEngine;

public class UIManager : BaseManager<UIManager>
{
    [SerializeField] private Canvas BackgroundCanvas;
    [SerializeField] private Canvas MainCanvas;
    [SerializeField] private Canvas PopupCanvas;
    [SerializeField] private Canvas LoadingCanvas;

    private Dictionary<UIType, BaseUI> m_uiDic = new();
    private HashSet<UIType> m_activeUI = new();
    private Dictionary<UIRootType, bool> m_activeCanvas = new();

    private BaseUI CreateUI(UIType uiType)
    {
        if (m_uiDic.TryGetValue(uiType, out BaseUI baseUI))
        {
            return baseUI;
        }

        string address = GetAddress(uiType);

        if (string.IsNullOrEmpty(address))
        {
            return null;
        }


        Canvas canvas = SetCanvas(GetUIRootType(uiType));

        if (canvas == null)
        {
            return null;
        }

        GameObject prefab = LoadUtil.Sync.LoadPrefab(address);

        if (prefab == null)
        {
            return null;
        }

        GameObject instantiateGameObject = this.InstantiateGameObject(prefab, canvas.transform);

        if (instantiateGameObject == null)
        {
            return null;
        }

        BaseUI newBaseUI = instantiateGameObject.GetComponent<BaseUI>();

        if (newBaseUI == null)
        {
            this.LogError($"{instantiateGameObject.name}에 BaseUI 컴포넌트가 없습니다!!");
            return null;
        }

        newBaseUI.ActiveFalse();
        m_uiDic.Add(uiType, newBaseUI);
        return newBaseUI;
    }

    public T OpenUI<T>(UIType uiType) where T : BaseUI
    {
        if (m_activeUI.Contains(uiType))
        {
            this.LogWarning($"{uiType}의 UI는 열려있습니다!!");
            return null;
        }

        if(m_activeCanvas.TryGetValue(GetUIRootType(uiType), out bool isActive) && isActive)
        {
            this.LogWarning($"{GetUIRootType(uiType)} Canvas에는 이미 열려있는 UI가 있습니다!!");
            return null;
        }

        BaseUI ui = CreateUI(uiType);

        if (ui == null)
        {
            return null;
        }

        T castedUI = ui as T;

        if(castedUI == null)
        {
            this.LogError($"{uiType}의 UI는 {typeof(T)}로 캐스팅할 수 없습니다!!");
            return null;
        }

        castedUI.ActiveTrue();
        m_activeUI.Add(uiType);
        m_activeCanvas[GetUIRootType(uiType)] = true;
        return castedUI;
    }

    public void CloseUI(UIType uiType)
    {
        if (m_activeUI.Contains(uiType) == false)
        {
            this.LogWarning($"{uiType}의 UI는 열려있지 않습니다!!");
            return;
        }

        if (m_uiDic.TryGetValue(uiType, out BaseUI baseUI) == false)
        {
            this.LogError($"{uiType}의 UI를 찾을 수 없습니다!!");
            return;
        }

        baseUI.ActiveFalse();
        m_activeUI.Remove(uiType);
        m_activeCanvas[GetUIRootType(uiType)] = false;
    }

    private string GetAddress(UIType uiType)
    {
        switch (uiType)
        {
            case UIType.BackGround:
                {
                    return AddressUtil.Sync.UIType.BackGround;
                }
            case UIType.MainMenu:
                {
                    return AddressUtil.Sync.UIType.MainMenu;
                }
            case UIType.CharacterCollection:
                {
                    return AddressUtil.Sync.UIType.CharacterCollection;
                }
            default:
                {
                    this.LogError($"{uiType}에 알맞는 Path가 없습니다!!");
                    return null;
                }
        }
    }

    private UIRootType GetUIRootType(UIType uiType)
    {
        switch (uiType)
        {
            case UIType.BackGround:
                {
                    return UIRootType.Background;
                }
            case UIType.MainMenu:
                {
                    return UIRootType.Main;
                }
            case UIType.CharacterCollection:
                {
                    return UIRootType.Main;
                }
            default:
                {
                    this.LogError($"{uiType}에 알맞는 UIRootType이 없습니다!!");
                    return default;
                }
        }
    }

    private Canvas SetCanvas(UIRootType uiRootType)
    {
        switch (uiRootType)
        {
            case UIRootType.Background:
                {
                    return BackgroundCanvas;
                }
            case UIRootType.Main:
                {
                    return MainCanvas;
                }
            case UIRootType.Popup:
                {
                    return PopupCanvas;
                }
            case UIRootType.Loading:
                {
                    return LoadingCanvas;
                }
            default:
                {
                    this.LogError($"{uiRootType}에 알맞는 Canvas가 없습니다!!");
                    return null;
                }
        }
    }
}
