using Cysharp.Threading.Tasks;
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
    private Dictionary<UIType, UIData> m_uiDataDic = new();
    private Dictionary<UIRootType, bool> m_activeCanvas = new();

    private T CreateUI<T>(UIType uiType) where T : BaseUI
    {
        if (m_uiDic.TryGetValue(uiType, out BaseUI baseUI))
        {
            return baseUI as T;
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
        this.TryGetInstantiate(prefab, canvas.transform, out GameObject ui);

        if (ui == null)
        {
            return null;
        }

        BaseUI newBaseUI = ui.GetComponent<BaseUI>();

        if (newBaseUI == null)
        {
            this.LogError($"{ui.name}에 BaseUI 컴포넌트가 없습니다!!");
            return null;
        }

        newBaseUI.ActiveFalse();
        m_uiDic.Add(uiType, newBaseUI);
        return newBaseUI as T;
    }

    public async UniTask OpenUI<T>(UIType uiType) where T : BaseUI
    {
        if(m_activeCanvas.TryGetValue(GetUIRootType(uiType), out bool value))
        {
            if (value)
            {
                return;
            }
        }

        if (m_activeUI.Contains(uiType))
        {
            return;
        }

        T ui = CreateUI<T>(uiType);
        if (ui == null) return;

        if (ui.IsAssetSyncLoad == false)
        {
            ui.LoadAssetSync();
        }

        if (ui.isActiveAndEnabled == false)
        {
            try
            {
                await ui.LoadAssetAsync();
            }
            catch (System.OperationCanceledException)
            {
                return;
            }
        }

        ui.ActiveTrue();
        m_activeUI.Add(uiType);
        m_activeCanvas[GetUIRootType(uiType)] = true;
    }

    public async UniTask OpenUI<T>(UIType uiType, UIData uiData) where T : BaseUI
    {
        if (m_activeUI.Contains(uiType))
        {
            if (m_uiDataDic.TryGetValue(uiType, out UIData existingUIData))
            {
                if (existingUIData == uiData) return;
            }
        }

        T ui = CreateUI<T>(uiType);
        if (ui == null) return;

        if (ui.IsAssetSyncLoad == false)
        {
            ui.LoadAssetSync();
        }

        if (ui.isActiveAndEnabled == false)
        {
            try
            {
                await ui.LoadAssetAsync();
            }
            catch (System.OperationCanceledException)
            {
                return;
            }
        }

        RefreshUIData(ui, uiType, uiData);
        ui.ActiveTrue();
        m_activeUI.Add(uiType);
        m_activeCanvas[GetUIRootType(uiType)] = true;
    }

    private void RefreshUIData<T>(T ui, UIType uiType, UIData uiData) where T : BaseUI
    {
        m_uiDataDic[uiType] = uiData;
        ui.SetData(uiData);
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
            case UIType.MainMenu:
                {
                    return AddressUtil.Sync.UIType.MainMenu;
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
            case UIType.MainMenu:
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
