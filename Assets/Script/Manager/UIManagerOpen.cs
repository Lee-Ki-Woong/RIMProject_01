using Cysharp.Threading.Tasks;
using UnityEngine;

public partial class UIManager : MonoBehaviour
{
    public string GetPath(UIRootType uiRootType, UIType uiType)
    {
        string path = $"UI/{uiRootType}/{uiType}";
        return path;
    }

    public async UniTaskVoid OpenBackGroundUI()
    {
        BackGroundUI backGroundUI = CreateUI<BackGroundUI>(UIRootType.BackGroundUI, UIType.BackGround);
        if (backGroundUI == null) return;

        backGroundUI.SetUIName("BackGroundUI");


        OpenUI(UIRootType.BackGroundUI, UIType.BackGround);
    }

    public async UniTaskVoid OpenMainUI()
    {

        MainMenuUI mainMenuUI = CreateUI<MainMenuUI>(UIRootType.MainUI, UIType.MainMenu);
        if (mainMenuUI == null) return;

        mainMenuUI.SetUIName("MainMenuUI");

        if(mainMenuUI.isAssetLoad)
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
