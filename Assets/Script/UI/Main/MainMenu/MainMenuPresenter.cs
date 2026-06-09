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

    private MainMenuType m_mainMenuType;
    private Dictionary<MainMenuType, Action[]> m_mainMenuActionList = new();

    public void InitMainMenu(MainMenu mainMenu)
    {
        if (m_mainMenuUI == null)
        {
            m_mainMenuUI = mainMenu;
        }

        if (m_isAssetLoad == false)
        {
            m_mainMenuUI.ActiveFalse();
            LoadAndSetAssetAsync().Forget();
        }
        else
        {
            m_mainMenuUI.ActiveTrue();
        }

            OpenMainMenu();
    }

    protected override async UniTask LoadAndSetAssetAsync()
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

    private void OpenMenu(MainMenuType mainMenuType)
    {
        string dataId = GetMainMenuId(mainMenuType);
        if (UIDataManager.Instance.MainMenuDataList.TryGetValue(dataId, out MainMenuData mainMenuData) == false)
        {
            LogError(dataId + $"에 알맞는 MainMenuData가 없습니다!!");
            return;
        }

        string[] texts = new string[5] { mainMenuData.FirstButton, mainMenuData.SecondButton, mainMenuData.ThirdButton, mainMenuData.FourthButton, mainMenuData.FifthButton };
        Action[] actions = GetAction(mainMenuType);
        
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
        switch(mainMenuType)
        {
            case MainMenuType.MainMenu:
                {
                    if(m_mainMenuActionList.TryGetValue(mainMenuType, out Action[] actions))
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
                    if(m_mainMenuActionList.TryGetValue(mainMenuType, out Action[] actions))
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
                    if(m_mainMenuActionList.TryGetValue(mainMenuType, out Action[] actions))
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
                        null,
                        null,
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
        OpenMenu(MainMenuType.MainMenu);
    }

    #region MainMenu
    private void OpenStartMenu()
    {
        OpenMenu(MainMenuType.StartGame);
    }

    private void OpenCollectionMenu()
    {
        OpenMenu(MainMenuType.Collection);
    }

    private void OpenShopMenu()
    {
        OpenMenu(MainMenuType.Shop);
    }

    private void OpenGameOptionMenu()
    {
        OpenMenu(MainMenuType.GameOption);
    }

    private void QuitGame()
    {
        GameManager.Instance.GameQuit();
    }
    #endregion

    private void OpenEndlessMode()
    {
        UIManager.Instance.OpenEndlessGameMode();
    }

    private void OpenCharacterCollection()
    {
        UIManager.Instance.OpenCharacterCollection();
    }
}

