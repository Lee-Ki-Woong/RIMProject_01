using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MainMenuPresenter : BasePresenter
{

    public MainMenu MainMenuUI { get; private set; }

    private Sprite Sprite_TitleText;
    private Sprite Sprite_TitleImage;
    private Sprite Sprite_MenuButton;
    private Sprite Sprite_MenuButton_Highlighted;

    private TMP_FontAsset TMPFont_MenuFont;

    public void InitMainMenu(MainMenu mainMenu)
    {
        MainMenuUI = mainMenu;
    }

    public void OpenMainMenuUI()
    {
        OnClick_MainMenuButton();
    }

    public override async UniTask LoadAndSetAssetAsync()
    {
        if (IsAssetLoad)
        {
            MainMenuUI.SetAsset(Sprite_TitleText, Sprite_TitleImage, Sprite_MenuButton, Sprite_MenuButton_Highlighted, TMPFont_MenuFont);
            return;
        }

        var (titleText, titleImage, menuButton, menuButtonHighlighted, menuFont) = await UniTask.WhenAll
            (
            LoadUtil.Async.LoadSpriteAsync(AddressUtil.Async.Sprite.UI.MainMenu.TitleText),
            LoadUtil.Async.LoadSpriteAsync(AddressUtil.Async.Sprite.UI.MainMenu.TitleImage),
            LoadUtil.Async.LoadSpriteAsync(AddressUtil.Async.Sprite.UI.Button_Empty),
            LoadUtil.Async.LoadSpriteAsync(AddressUtil.Async.Sprite.UI.MainMenu.MenuButton_Highlighted),
            LoadUtil.Async.LoadFontAssetAsync(AddressUtil.Async.Font.BaseFont)
            );

        Sprite_TitleText = titleText;
        Sprite_TitleImage = titleImage;
        Sprite_MenuButton = menuButton;
        Sprite_MenuButton_Highlighted = menuButtonHighlighted;
        TMPFont_MenuFont = menuFont;

        IsAssetLoad = true;

        MainMenuUI.SetAsset(Sprite_TitleText, Sprite_TitleImage, Sprite_MenuButton, Sprite_MenuButton_Highlighted, TMPFont_MenuFont);
    }

    private Dictionary<MainMenuType, UIData> m_mainMenuDataDic = new();

    private UIData CreateMainMenuUIData(MainMenuType mainMenuType, string[] stringArray, Action[] actionArray)
    {
        if (m_mainMenuDataDic.TryGetValue(mainMenuType, out UIData mainMenuUIData))
        {
            return mainMenuUIData;
        }

        UIData newMainMenuUIData = new()
        {
            Texts = stringArray,
            Actions = actionArray,
        };

        m_mainMenuDataDic.Add(mainMenuType, newMainMenuUIData);

        return newMainMenuUIData;
    }

    private void OnClick_MainMenuButton()
    {
        OpenMainMenu();
    }

    private void OnClick_GameStartButton()
    {
        OpenGameStartMenu();
    }

    private void OnClick_MyCollectionButton()
    {
        OpenMyCollectionMenu();
    }

    private void OnClick_ShopButton()
    {
        OpenShopMenu();
    }

    private void OnClick_GameOptionButton()
    {
        OpenGameOptionMenu();
    }

    private void OnClick_GameExitButton()
    {
        GameManager.Instance.GameQuit();
    }

    private void OnClick_CharacterCollectionButton()
    {
        UIManager.Instance.OpenCharacterCollection().Forget();
    }

    private void OnClick_EndlessGameModeButton()
    {
        UIManager.Instance.OpenEndlessGameMode().Forget();
    }
    public void OnClick_NewCharacterCollectionButton()
    {
        UIManager.Instance.OpenNewCharacterCollection().Forget();
    }




    private void OpenMainMenu()
    {
        string[] mainMenuText = { "게임시작", "내 콜렉션", "샵", "게임 옵션", "게임 종료" };
        Action[] mainMenuAction = { OnClick_GameStartButton, OnClick_MyCollectionButton, OnClick_ShopButton, OnClick_GameOptionButton, OnClick_GameExitButton };

        UIData mainMenuData = CreateMainMenuUIData(MainMenuType.MainMenu, mainMenuText, mainMenuAction);

        MainMenuUI.SetData(mainMenuData);
    }

    private void OpenGameStartMenu()
    {
        string[] gameStartMenuText = { "스토리 모드", "무한 모드", "", "", "돌아가기" };
        Action[] gameStartMenuAction = { null, OnClick_EndlessGameModeButton, null, null, OnClick_MainMenuButton };

        UIData gameStartMenuData = CreateMainMenuUIData(MainMenuType.GameStart, gameStartMenuText, gameStartMenuAction);

        MainMenuUI.SetData(gameStartMenuData);
    }

    private void OpenMyCollectionMenu()
    {
        string[] myCollectionMenuText = { "캐릭터 콜렉션", "무기 콜렉션", "아티팩트 콜렉션", "", "돌아가기" };
        Action[] myCollectionMenuAction = { OnClick_CharacterCollectionButton, null, null, null, OnClick_MainMenuButton };

        UIData myCollectionMenuData = CreateMainMenuUIData(MainMenuType.MyCollection, myCollectionMenuText, myCollectionMenuAction);

        MainMenuUI.SetData(myCollectionMenuData);
    }

    private void OpenShopMenu()
    {
        string[] shopMenuText = { "캐릭터 샵", "무기 샵", "아티팩트 샵", "", "돌아가기" };
        Action[] shopMenuAction = { null, null, null, null, OnClick_MainMenuButton };

        UIData shopMenuData = CreateMainMenuUIData(MainMenuType.Shop, shopMenuText, shopMenuAction);

        MainMenuUI.SetData(shopMenuData);

    }

    private void OpenGameOptionMenu()
    {
        string[] gameOptionMenuText = { "게임 옵션", "사운드 옵션", "", "", "돌아가기" };
        Action[] gameOptionMenuAction = { null, null, null, null, OnClick_MainMenuButton };

        UIData gameOptionMenuData = CreateMainMenuUIData(MainMenuType.GameOption, gameOptionMenuText, gameOptionMenuAction);

        MainMenuUI.SetData(gameOptionMenuData);
    }
}

