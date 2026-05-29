using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.UI;

public class MainMenu : BaseUI
{
    [SerializeField] private Image Sprite_TitleText;
    [SerializeField] private Image Sprite_TitleImage;

    [SerializeField] private AssetReferenceT<Sprite> AR_Sprite_TitleText;
    [SerializeField] private AssetReferenceT<Sprite> AR_Sprite_TitleImage;
    [SerializeField] private AssetReferenceT<Sprite> AR_Sprite_Menu;
    [SerializeField] private AssetReferenceT<Sprite> AR_Sprite_HighlightedMenu;
    [SerializeField] private AssetReferenceT<TMP_FontAsset> AR_Font_Menu;

    [System.Serializable]
    private class Menu_Layout
    {
        public GameObject gameObject;
        public Button Button;
        public Image Image;
        public TMP_Text Text;
    }

    [SerializeField] private Menu_Layout[] menus;

    private void Awake()
    {
        IsAssetAsyncLoad = false;
    }

    private enum MainMenuType
    {
        MainMenu,
        GameStart,
        MyCollection,
        Shop,
        GameOption
    }

    private void Start()
    {
        OpenMainMenu();
    }

    public override async UniTask LoadAssetAsync()
    {
        (Sprite titleText, Sprite titleImage, Sprite loadedSprite, Sprite loadedHighlightedSprite, TMP_FontAsset loadedFont) = await UniTask.WhenAll
            (
            LoadUtil.Async.LoadSpriteAsync(AR_Sprite_TitleText.RuntimeKey.ToString()),
            LoadUtil.Async.LoadSpriteAsync(AR_Sprite_TitleImage.RuntimeKey.ToString()),
            LoadUtil.Async.LoadSpriteAsync(AR_Sprite_Menu.RuntimeKey.ToString()),
            LoadUtil.Async.LoadSpriteAsync(AR_Sprite_HighlightedMenu.RuntimeKey.ToString()),
            LoadUtil.Async.LoadFontAssetAsync(AR_Font_Menu.RuntimeKey.ToString())
            );

        Sprite_TitleText.sprite = titleText;
        Sprite_TitleImage.sprite = titleImage;

        foreach (Menu_Layout menu in menus)
        {
            menu.Image.sprite = loadedSprite;
            menu.Button.SetButtonSprite(loadedHighlightedSprite);
            menu.Text.font = loadedFont;
        }

        IsAssetAsyncLoad = true;
    }

    public override void SetData(UIData uiData)
    {
        string[] texts = uiData.Texts;
        Action[] actions = uiData.Actions;

        if (texts.Length != actions.Length)
        {
            Debug.LogError($"{this.gameObject} : 전달받은 texts와 Actions의 Length 값이 동일하지 않습니다!!");
            return;
        }

        for (int i = 0; i < Math.Min(texts.Length, menus.Length); i++)
        {
            if (string.IsNullOrEmpty(texts[i]) || actions[i] == null)
            {
                if (string.IsNullOrEmpty(texts[i]))
                {
                    Debug.LogWarning($"{this.gameObject} : texts {i}의 값이 null이거나 Empty입니다!!");
                }

                if (actions[i] == null)
                {
                    Debug.LogWarning($"{this.gameObject} : action {i}의 값이 null이거나 Empty입니다!!");
                }

                menus[i].gameObject.SetActive(false);
                continue;
            }

            if (menus[i].gameObject.activeSelf == false)
            {
                menus[i].gameObject.SetActive(true);
            }

            menus[i].Text.text = texts[i];
            menus[i].Button.onClick.RemoveAllListeners();
            menus[i].Button.onClick.AddListener(actions[i].Invoke);
        }
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


    // [OnClick Event]
    private void OnClick_MainMenuButton()
    {
        OpenMainMenu();
    }

    private void OnClick_GameStartMenuButton()
    {
        OpenGameStartMenu();
    }

    private void OnClick_MyCollectionMenuButton()
    {
        OpenMyCollectionMenu();
    }

    private void OnClick_ShopMenuButton()
    {
        OpenShopMenu();
    }

    private void OnClick_GameOptionMenuButton()
    {
        OpenGameOptionMenu();
    }

    private void OnClick_GameExitButton()
    {
    }

    // [Open Menu]
    private void OpenMainMenu()
    {
        string[] mainMenuText = CreateStringArray("게임시작", "내 콜렉션", "샵", "게임 옵션", "게임 종료");
        Action[] mainMenuAction = CreateActionArray(OnClick_GameStartMenuButton, OnClick_MyCollectionMenuButton, OnClick_ShopMenuButton, OnClick_GameOptionMenuButton, OnClick_GameExitButton);

        UIData mainMenuData = CreateMainMenuUIData(MainMenuType.MainMenu, mainMenuText, mainMenuAction);

        UIManager.Instance.OpenUI<MainMenu>( UIType.MainMenu, mainMenuData).Forget();
    }

    private void OpenGameStartMenu()
    {
        string[] gameStartMenuText = CreateStringArray("스토리 모드", "무한 모드", "", "", "돌아가기");
        Action[] gameStartMenuAction = CreateActionArray(null, null, null, null, OpenMainMenu);

        UIData gameStartMenuData = CreateMainMenuUIData(MainMenuType.GameStart, gameStartMenuText, gameStartMenuAction);

        UIManager.Instance.OpenUI<MainMenu>(UIType.MainMenu, gameStartMenuData).Forget();

    }

    private void OpenMyCollectionMenu()
    {
        string[] myCollectionMenuText = CreateStringArray("캐릭터 콜렉션", "무기 콜렉션", "아티팩트 콜렉션", "", "돌아가기");
        Action[] myCollectionMenuAction = CreateActionArray(OpenCharacterCollectionUI, null, null, null, OpenMainMenu);

        UIData myCollectionMenuData = CreateMainMenuUIData(MainMenuType.MyCollection, myCollectionMenuText, myCollectionMenuAction);

        UIManager.Instance.OpenUI<MainMenu>(UIType.MainMenu, myCollectionMenuData).Forget();
    }

    private void OpenShopMenu()
    {
        string[] shopMenuText = CreateStringArray("캐릭터 샵", "무기 샵", "아티팩트 샵", "", "돌아가기");
        Action[] shopMenuAction = CreateActionArray(null, null, null, null, OpenMainMenu);

        UIData shopMenuData = CreateMainMenuUIData(MainMenuType.Shop, shopMenuText, shopMenuAction);

        UIManager.Instance.OpenUI<MainMenu>(UIType.MainMenu, shopMenuData).Forget();
    }

    private void OpenGameOptionMenu()
    {
        string[] gameOptionMenuText = CreateStringArray("게임 옵션", "사운드 옵션", "", "", "돌아가기");
        Action[] gameOptionMenuAction = CreateActionArray(null, null, null, null, OpenMainMenu);

        UIData gameOptionMenuData = CreateMainMenuUIData(MainMenuType.GameOption, gameOptionMenuText, gameOptionMenuAction);

        UIManager.Instance.OpenUI<MainMenu>(UIType.MainMenu, gameOptionMenuData).Forget();
    }

    private void OpenCharacterCollectionUI()
    {
    }


}