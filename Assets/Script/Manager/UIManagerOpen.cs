using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.CompilerServices;
using UnityEngine;
using UnityEngine.InputSystem;

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


    // [GameStartUI]
    public async UniTaskVoid OpenGameStartUI()
    {
        GameStartUI gameStartUI = CreateUI<GameStartUI>(UIRootType.MainUI, UIType.GameStart);
        if (gameStartUI == null) return;

        gameStartUI.SetUIName("GameStartUI");

        if(gameStartUI.m_isAssetLoad)
        {
            OpenUI(UIRootType.MainUI, UIType.GameStart);
            return;
        }

        try
        {
            await gameStartUI.SetAssetAsync();
        }
        catch(System.OperationCanceledException)
        {
            return;
        }

        OpenUI(UIRootType.MainUI, UIType.GameStart);
    }


    // [MyCollectionUI]
    public async UniTaskVoid OpenMyCollectionUI()
    {
        MyCollectionUI myCollectionUI = CreateUI<MyCollectionUI>(UIRootType.MainUI, UIType.MyCollection);
        if(myCollectionUI == null) return;

        myCollectionUI.SetUIName("MyCollectionUI");

        if(myCollectionUI.m_isAssetLoad)
        {
            OpenUI(UIRootType.MainUI, UIType.MyCollection);
            return;
        }

        try
        {
            await myCollectionUI.SetAssetAsync();
        }
        catch (System.OperationCanceledException)
        {
            return;
        }

        OpenUI(UIRootType.MainUI, UIType.MyCollection);
    }
}
