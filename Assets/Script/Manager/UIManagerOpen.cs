using Cysharp.Threading.Tasks;
using System.Buffers.Text;
using UnityEngine;

public partial class UIManager : MonoBehaviour
{
    private string GetPath(UIRootType uiRootType, UIType uiType)
    {
        string path = $"UI/{uiRootType}/{uiType}";
        return path;
    }


    // [Oepn UI Async]
    public async UniTaskVoid OpenUIAsync<T> (UIRootType uiRootType, UIType uiType, string uiName) where T : BaseUI
    {
        T ui = CreateUI<T>(uiRootType, uiType);
        if (ui == null) return;

        if(ui.m_isAssetLoad)
        {
            OpenUI(uiRootType, uiType);
            return;
        }


        try
        {
            await ui.SetAssetAsync();
        }
        catch (System.OperationCanceledException)
        {
            return;
        }

        ui.SetUIName(uiName);
        OpenUI(uiRootType, uiType);
    }



    // [BackGroundUI]
    public void OpenBackGroundUI()
    {
        OpenUIAsync<BackGroundUI>(UIRootType.BackGroundUI, UIType.BackGround, "BackGroundUI").Forget();
    }


    // [MainMenuUI]
    public void OpenMainMenuUI()
    {
        OpenUIAsync<MainMenuUI>(UIRootType.MainUI, UIType.MainMenu, "MainMenuUI").Forget();
    }
    

    // [GameStartUI]
    public void OpenGameStartUI()
    {
        OpenUIAsync<GameStartUI>(UIRootType.MainUI, UIType.GameStart, "GameStartUI").Forget();
    }


    // [MyCollectionUI]
    public void OpenMyCollectionUI()
    {
        OpenUIAsync<MyCollectionUI>(UIRootType.MainUI, UIType.MyCollection, "MyCollectionUI").Forget();
    }
}
