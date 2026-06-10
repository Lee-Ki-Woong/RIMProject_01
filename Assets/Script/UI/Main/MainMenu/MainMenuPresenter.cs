using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MainMenuPresenter : BasePresenter
{
    private MainMenu m_mainMenuUI;

    private Sprite Sprite_TitleText;
    private Sprite Sprite_TitleImage;
    private Sprite Sprite_MenuButton;
    private Sprite Sprite_MenuButton_Highlighted;

    private TMP_FontAsset TMPFont_MenuFont;

    private string m_firstMenuButtonText;
    private string m_secondMenuButtonText;
    private string m_thirdMenuButtonText;
    private string m_fourthMenuButtonText;
    private string m_fifthMenuButtonText;

    private MainMenuType m_mainMenuType;
    private Dictionary<MainMenuType, Action[]> m_mainMenuActionList = new();

    public override UIType UIType_This { get; } = UIType.MainMenu;

    public void InitMainMenu(MainMenu mainMenu)
    {
        if (m_mainMenuUI == null)
        {
            m_mainMenuUI = mainMenu;
        }

        if (m_isAssetLoad == false)
        {
            m_mainMenuUI.ActiveFalse();
            LoadAssetAsync().Forget();
        }
        else
        {
            m_mainMenuUI.ActiveTrue();
        }

        SubscribeChangeLanguageEvents();

        LoadData();
    }

    protected override async UniTask LoadAssetAsync()
    {
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

        m_isAssetLoad = true;

        m_mainMenuUI.SetAsset(Sprite_TitleText, Sprite_TitleImage, Sprite_MenuButton, Sprite_MenuButton_Highlighted, TMPFont_MenuFont);
        m_mainMenuUI.ActiveTrue();

    }

    protected override void LoadData()
    {
        string dataId = GetMainMenuId(m_mainMenuType);
        Action[] actions = GetAction(m_mainMenuType);

        if (UIDataManager.Instance.MainMenuDataList.TryGetValue(dataId, out MainMenuData mainMenuData) == false)
        {
            LoadLogError(dataId);
            return;
        }

        m_firstMenuButtonText = mainMenuData.FirstButton;
        m_secondMenuButtonText = mainMenuData.SecondButton;
        m_thirdMenuButtonText = mainMenuData.ThirdButton;
        m_fourthMenuButtonText = mainMenuData.FourthButton;
        m_fifthMenuButtonText = mainMenuData.FifthButton;

        string[] texts = new string[5] { m_firstMenuButtonText, m_secondMenuButtonText, m_thirdMenuButtonText, m_fourthMenuButtonText, m_fifthMenuButtonText };

        m_mainMenuUI.SetData(texts, actions);
    }

    private string GetMainMenuId(MainMenuType mainMenuType)
    {
        switch (mainMenuType)
        {
            case MainMenuType.MainMenu:
                {
                    return "MainMenu_Main";
                }
            case MainMenuType.StartGame:
                {
                    return "MainMenu_StartGame";
                }
            case MainMenuType.Collection:
                {
                    return "MainMenu_Collection";
                }
            case MainMenuType.Shop:
                {
                    return "MainMenu_Shop";
                }
            case MainMenuType.GameOption:
                {
                    return "MainMenu_GameOption";
                }
            default:
                {
                    LogError("잘못된 접근입니다!! 메인화면으로 돌아갑니다");
                    return "MainMenu_Main";
                }
        }
    }

    private Action[] GetAction(MainMenuType mainMenuType)
    {
        switch (mainMenuType)
        {
            case MainMenuType.MainMenu:
                {
                    if (m_mainMenuActionList.TryGetValue(mainMenuType, out Action[] actions))
                    {
                        return actions;
                    }

                    actions = new Action[5]
                    {
                        OpenStartMenu,
                        OpenCollectionMenu,
                        OpenShopMenu,
                        OpenGameOptionMenu,
                        QuitGame
                    };

                    m_mainMenuActionList.Add(mainMenuType, actions);

                    return actions;
                }
            case MainMenuType.StartGame:
                {
                    if (m_mainMenuActionList.TryGetValue(mainMenuType, out Action[] actions))
                    {
                        return actions;
                    }

                    actions = new Action[5]
                    {
                        null,
                        OpenEndlessMode,
                        null,
                        null,
                        OpenMainMenu
                    };

                    m_mainMenuActionList.Add(mainMenuType, actions);

                    return actions;
                }
            case MainMenuType.Collection:
                {
                    if (m_mainMenuActionList.TryGetValue(mainMenuType, out Action[] actions))
                    {
                        return actions;
                    }

                    actions = new Action[5]
                    {
                        OpenCharacterCollection,
                        null,
                        null,
                        null,
                        OpenMainMenu
                    };

                    m_mainMenuActionList.Add(mainMenuType, actions);

                    return actions;
                }
            case MainMenuType.Shop:
                {

                    if (m_mainMenuActionList.TryGetValue(mainMenuType, out Action[] actions))
                    {
                        return actions;
                    }

                    actions = new Action[5]
                    {
                        null,
                        null,
                        null,
                        null,
                        OpenMainMenu
                    };

                    m_mainMenuActionList.Add(mainMenuType, actions);

                    return actions;
                }
            case MainMenuType.GameOption:
                {

                    if (m_mainMenuActionList.TryGetValue(mainMenuType, out Action[] actions))
                    {
                        return actions;
                    }

                    actions = new Action[5]
                    {
                        null, 
                        null,
                        OpenLanguagePopup, 
                        null ,
                        OpenMainMenu
                    };

                    m_mainMenuActionList.Add(mainMenuType, actions);

                    return actions;
                }
            default:
                {
                    LogError("예상치못한 오류가 발생하였습니다!!");
                    return null;
                }
        }
    }

    private void OpenMainMenu()
    {
        m_mainMenuType = MainMenuType.MainMenu;
        LoadData();
    }

    #region MainMenu
    private void OpenStartMenu()
    {
        m_mainMenuType = MainMenuType.StartGame;
        LoadData();
    }

    private void OpenCollectionMenu()
    {
        m_mainMenuType = MainMenuType.Collection;
        LoadData();
    }

    private void OpenShopMenu()
    {
        m_mainMenuType = MainMenuType.Shop;
        LoadData();
    }

    private void OpenGameOptionMenu()
    {
        m_mainMenuType = MainMenuType.GameOption;
        LoadData();
    }

    private void QuitGame()
    {
        UnsubscribeChangeLanguageEvents();
        GameManager.Instance.GameQuit();
    }
    #endregion

    #region StartGame
    private void OpenEndlessMode()
    {
        UnsubscribeChangeLanguageEvents();
        UIManager.Instance.OpenEndlessGameMode();
    }
    #endregion

    #region Collection
    private void OpenCharacterCollection()
    {
        UIManager.Instance.OpenCharacterCollection();
    }
    #endregion

    private void OpenLanguagePopup()
    {
        UIManager.Instance.OpenLanguagePopup();
    }


    private void SubscribeChangeLanguageEvents()
    {
        GameManager.Instance.OnLanguageChanged += On_ChangeLanguage;
    }

    private void UnsubscribeChangeLanguageEvents()
    {
        GameManager.Instance.OnLanguageChanged -= On_ChangeLanguage;
    }

    private void On_ChangeLanguage()
    {
        LoadData();
    }
}


