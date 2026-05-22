using Cysharp.Threading.Tasks;
using UnityEngine;

public partial class UIManager : MonoBehaviour
{
    // [Oepn UI Async]
    private async UniTaskVoid OpenUIAsync<T> (UIRootType uiRootType, UIType uiType, string uiName) where T : BaseMainUI
    {
        T ui = CreateUI<T>(uiRootType, uiType);
        if (ui == null) return;
        
        if(ui.name != uiName)
        {
            ui.SetUIName(uiName);
        }

        if (ui.IsAssetLoad)
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

        OpenUI(uiRootType, uiType);
    }


    // [BackGround UI]
    public void OpenBackGroundUI()
    {
        OpenUIAsync<BackGroundUI>(UIRootType.BackGroundUI, UIType.BackGround, "BackGroundUI").Forget();
    }

    // [Main Menu UI]
    public void OpenMainMenuUI()
    {
        OpenUIAsync<MainMenuUI>(UIRootType.MainUI, UIType.MainMenu, "MainMenuUI").Forget();
    }

    // [Game Start UI]
    public void OpenGameStartUI()
    {
        OpenUIAsync<GameStartUI>(UIRootType.MainUI, UIType.GameStart, "GameStartUI").Forget();
    }

    // [My Collection UI]
    public void OpenMyCollectionUI()
    {
        OpenUIAsync<MyCollectionUI>(UIRootType.MainUI, UIType.MyCollection, "MyCollectionUI").Forget();
    }

    // [Game Option UI]
    public void OpenGameOptionUI()
    {
        OpenUIAsync<GameOptionUI>(UIRootType.MainUI, UIType.GameOption, "GameOptionUI").Forget();
    }

    // [Character Collection UI]
    public void OpenCharacterCollectionUI()
    {
        OpenUIAsync<CharacterCollectionUI>(UIRootType.ContentUI, UIType.CharacterCollection, "CharacterCollectionUI").Forget();
    }
}
