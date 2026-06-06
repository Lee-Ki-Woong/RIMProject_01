using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public enum NewMainMenuType
{
    MainMenu,
    GameStart,
    MyCollection,
    Shop,
    GameOption
}

public class NewMainMenuPresenter : BasePresenter
{
    public NewMainMenu MainMenuUI { get; private set; }

    private MainMenuType m_currentMenuType = MainMenuType.MainMenu; // 현재 메뉴 상태 저장
    private Dictionary<MainMenuType, string[]> m_menuTextDataDic = new(); // 텍스트 캐싱

    private Sprite m_sprite_titleText;
    private Sprite m_sprite_titleImage;
    private Sprite m_sprite_menuButton;
    private Sprite m_sprite_menuButton_Highlighted;

    private TMP_FontAsset m_font_menuFont;

    public void InitMainMenu(NewMainMenu mainMenu)
    {
        MainMenuUI = mainMenu;

        // 1. View의 클릭 이벤트를 구독
        SubscribeEvents();
        InitMenuTextData(); // 메뉴 텍스트 데이터 미리 세팅
    }

    protected void SubscribeEvents()
    {
        MainMenuUI.OnMenuButtonClicked += HandleMenuButtonClicked;
    }

    private void InitMenuTextData()
    {
        m_menuTextDataDic.Add(MainMenuType.MainMenu, new string[] { "게임시작", "내 콜렉션", "샵", "게임 옵션", "게임 종료" });
        m_menuTextDataDic.Add(MainMenuType.GameStart, new string[] { "스토리 모드", "무한 모드", "", "", "돌아가기" });
        m_menuTextDataDic.Add(MainMenuType.MyCollection, new string[] { "캐릭터 콜렉션", "무기 콜렉션", "아티팩트 콜렉션", "", "돌아가기" });
        m_menuTextDataDic.Add(MainMenuType.Shop, new string[] { "캐릭터 샵", "무기 샵", "아티팩트 샵", "", "돌아가기" });
        m_menuTextDataDic.Add(MainMenuType.GameOption, new string[] { "게임 옵션", "사운드 옵션", "", "", "돌아가기" });
    }

    public override async UniTask LoadAndSetAssetAsync()
    {
        if (IsAssetLoad)
        {
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

        m_sprite_titleText = titleText;
        m_sprite_titleImage = titleImage;
        m_sprite_menuButton = menuButton;
        m_sprite_menuButton_Highlighted = menuButtonHighlighted;
        m_font_menuFont = menuFont;

        IsAssetLoad = true;

        MainMenuUI.SetAsset(m_sprite_titleText, m_sprite_titleImage, m_sprite_menuButton, m_sprite_menuButton_Highlighted, m_font_menuFont);

    }

    public void OpenMainMenuUI()
    {
        ChangeMenuState(MainMenuType.MainMenu);
    }

    // 2. View에 텍스트 갱신 지시 (Action 주입 제거)
    private void ChangeMenuState(MainMenuType targetMenuType)
    {
        m_currentMenuType = targetMenuType;
        MainMenuUI.UpdateMenuTexts(m_menuTextDataDic[targetMenuType]);
    }

    // 3. View에서 전달받은 인덱스와 현재 상태를 조합하여 로직 실행
    private void HandleMenuButtonClicked(int index)
    {
        switch (m_currentMenuType)
        {
            case MainMenuType.MainMenu:
                if (index == 0) ChangeMenuState(MainMenuType.GameStart);
                else if (index == 1) ChangeMenuState(MainMenuType.MyCollection);
                else if (index == 2) ChangeMenuState(MainMenuType.Shop);
                else if (index == 3) ChangeMenuState(MainMenuType.GameOption);
                else if (index == 4) GameManager.Instance.GameQuit();
                break;

            case MainMenuType.GameStart:
                if (index == 1) UIManager.Instance.OpenEndlessGameMode().Forget();
                else if (index == 4) ChangeMenuState(MainMenuType.MainMenu); // 돌아가기
                break;

            case MainMenuType.MyCollection:
                if (index == 0) UIManager.Instance.OpenCharacterCollection().Forget();
                else if (index == 1) UIManager.Instance.OpenNewCharacterCollection().Forget();
                else if (index == 4) ChangeMenuState(MainMenuType.MainMenu); // 돌아가기
                break;

            case MainMenuType.Shop:
            case MainMenuType.GameOption:
                if (index == 4) ChangeMenuState(MainMenuType.MainMenu); // 돌아가기
                break;
        }
    }

    public void CloseMainMenuUI()
    {
        UIManager.Instance.CloseUI(UIType.MainMenu);
    }

    // 4. 메모리 누수 방지
    public void UnsubscribeEvents()
    {
        if (MainMenuUI == null)
        {
            return;
        }

        MainMenuUI.OnMenuButtonClicked -= HandleMenuButtonClicked;
    }
}