using Cysharp.Threading.Tasks;
using UnityEngine;

public partial class UIManager : MonoBehaviour
{
    private string GetPath(UIRootType uiRootType, UIType uiType)
    {
        string path = $"UI/{uiRootType}/{uiType}";
        return path;
    }

    // [BackGroundUI]
    public async UniTaskVoid OpenBackGroundUI()
    {
        BackGroundUI backGroundUI = CreateUI<BackGroundUI>(UIRootType.BackGroundUI, UIType.BackGround);
        if (backGroundUI == null) return;

        backGroundUI.SetUIName("BackGroundUI");

        if(backGroundUI.m_isAssetLoad)
        {
            OpenUI(UIRootType.BackGroundUI, UIType.BackGround);
            return;
        }

        try
        {
            await backGroundUI.SetAssetAsync();
        }
        catch(System.OperationCanceledException)
        {
            return;
        }
        catch(System.Exception e)
        {
            Debug.LogException(e);
        }

        OpenUI(UIRootType.BackGroundUI, UIType.BackGround);
    }


    // [MainMenuUI]
    public async UniTaskVoid OpenMainMenuUI()
    {
        MainMenuUI mainMenuUI = CreateUI<MainMenuUI>(UIRootType.MainUI, UIType.MainMenu);
        if (mainMenuUI == null) return;

        mainMenuUI.SetUIName("MainMenuUI");

        if(mainMenuUI.m_isAssetLoad)
        {
            OpenUI(UIRootType.MainUI, UIType.MainMenu);
            return;
        }

        try
        {
            await mainMenuUI.SetAssetAsync();
        }
        catch(System.OperationCanceledException)
        {
            return;
        }

        OpenUI(UIRootType.MainUI, UIType.MainMenu);
    }

    public void OpenLoadingUI()
    {
    }

    public void OpenInGameOptionUI()
    {
    }
}
