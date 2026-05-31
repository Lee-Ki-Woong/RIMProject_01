using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MainMenuPresenter : BasePresenter
{

    public MainMenu MainMenu { get; private set; }

    private Sprite Sprite_TitleText;
    private Sprite Sprite_TitleImage;
    private Sprite Sprite_MenuButton;
    private Sprite Sprite_MenuButton_Highlighted;
    private TMP_FontAsset TMPFont_MenuFont;

    public void InitMainMenu(MainMenu mainMenu)
    {
        MainMenu = mainMenu;
    }

    public override async UniTask LoadAndSetAssetAsync()
    {
        if (IsAssetLoad)
        {
            return;
        }

        var (titleText, titleImage, menuButtonSprite, menuButtonHighlightedSprite, menuFont) = await UniTask.WhenAll
            (
            LoadUtil.Async.LoadSpriteAsync(AddressUtil.Async.Sprite.UI.MainMenu.TitleText),
            LoadUtil.Async.LoadSpriteAsync(AddressUtil.Async.Sprite.UI.MainMenu.TitleImage),
            LoadUtil.Async.LoadSpriteAsync(AddressUtil.Async.Sprite.UI.MainMenu.MenuButton),
            LoadUtil.Async.LoadSpriteAsync(AddressUtil.Async.Sprite.UI.MainMenu.MenuButton_Highlighted),
            LoadUtil.Async.LoadFontAssetAsync(AddressUtil.Async.Font.Base)
            );

        Sprite_TitleText = titleText;
        Sprite_TitleImage = titleImage;
        Sprite_MenuButton = menuButtonSprite;
        Sprite_MenuButton_Highlighted = menuButtonHighlightedSprite;
        TMPFont_MenuFont = menuFont;

        IsAssetLoad = true;

        MainMenu.SetAsset(Sprite_TitleText, Sprite_TitleImage, Sprite_MenuButton, Sprite_MenuButton_Highlighted, TMPFont_MenuFont);
    }

    public void GoMainMenu()
    {
        OpenMainMenu();
    }

    public void LeaveMainMenu()
    {
        UIManager.Instance.CloseUI(UIType.MainMenu);
    }

    private enum MainMenuType
    {
        MainMenu,
        GameStart,
        MyCollection,
        Shop,
        GameOption
    }

    private Dictionary<MainMenuType, UIData> m_mainMenuDataDic = new();

    private string[] CreateStringArray(params string[] strings)
    {
        return strings;
    }

    private Action[] CreateActionArray(params Action[] actions)
    {
        return actions;
    }

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

    private void OnClick_ReturnButton()
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
    }

    private void OnClick_CharacterCollectionButton()
    {
        OpenCharacterCollection();
    }

    private void OnClick_EndlessGameModeButton()
    {
        OpenEndlessGameMode();
    }

    private void OpenMainMenu()
    {
        string[] mainMenuText = CreateStringArray("게임시작", "내 콜렉션", "샵", "게임 옵션", "게임 종료");
        Action[] mainMenuAction = CreateActionArray(OnClick_GameStartButton, OnClick_MyCollectionButton, OnClick_ShopButton, OnClick_GameOptionButton, OnClick_GameExitButton);

        UIData mainMenuData = CreateMainMenuUIData(MainMenuType.MainMenu, mainMenuText, mainMenuAction);

        MainMenu.SetData(mainMenuData);
    }

    private void OpenGameStartMenu()
    {
        string[] gameStartMenuText = CreateStringArray("스토리 모드", "무한 모드", "", "", "돌아가기");
        Action[] gameStartMenuAction = CreateActionArray(null, OnClick_EndlessGameModeButton, null, null, OnClick_ReturnButton);

        UIData gameStartMenuData = CreateMainMenuUIData(MainMenuType.GameStart, gameStartMenuText, gameStartMenuAction);

        MainMenu.SetData(gameStartMenuData);
    }

    private void OpenMyCollectionMenu()
    {
        string[] myCollectionMenuText = CreateStringArray("캐릭터 콜렉션", "무기 콜렉션", "아티팩트 콜렉션", "", "돌아가기");
        Action[] myCollectionMenuAction = CreateActionArray(null, null, null, null, OnClick_ReturnButton);

        UIData myCollectionMenuData = CreateMainMenuUIData(MainMenuType.MyCollection, myCollectionMenuText, myCollectionMenuAction);

        MainMenu.SetData(myCollectionMenuData);
    }

    private void OpenShopMenu()
    {
        string[] shopMenuText = CreateStringArray("캐릭터 샵", "무기 샵", "아티팩트 샵", "", "돌아가기");
        Action[] shopMenuAction = CreateActionArray(OnClick_CharacterCollectionButton, null, null, null, OnClick_ReturnButton);

        UIData shopMenuData = CreateMainMenuUIData(MainMenuType.Shop, shopMenuText, shopMenuAction);

        MainMenu.SetData(shopMenuData);

    }

    private void OpenGameOptionMenu()
    {
        string[] gameOptionMenuText = CreateStringArray("게임 옵션", "사운드 옵션", "", "", "돌아가기");
        Action[] gameOptionMenuAction = CreateActionArray(null, null, null, null, OnClick_ReturnButton);

        UIData gameOptionMenuData = CreateMainMenuUIData(MainMenuType.GameOption, gameOptionMenuText, gameOptionMenuAction);

        MainMenu.SetData(gameOptionMenuData);
    }

    private void OpenCharacterCollection()
    {
        UIManager.Instance.OpenCharacterCollection().Forget();
    }

    private void OpenEndlessGameMode()
    {
        UIManager.Instance.OpenEndlessGameMode().Forget();
    }
}
