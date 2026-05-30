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

    [SerializeField] private AssetReferenceT<Sprite> Asset_TitleText;
    [SerializeField] private AssetReferenceT<Sprite> Asset_TitleImage;
    [SerializeField] private AssetReferenceT<Sprite> Asset_MenuSprite;
    [SerializeField] private AssetReferenceT<Sprite> Asset_MenuHighlightedSprite;
    [SerializeField] private AssetReferenceT<TMP_FontAsset> Asset_MenuFont;

    [System.Serializable]
    private class MainMenuButton
    {
        public GameObject gameObject;
        public Button button;
        public Image image;
        public TMP_Text text;
    }

    [SerializeField] private MainMenuButton[] Menus;

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
        var (titleText, titleImage, loadedSprite, loadedHighlightedSprite, loadedFont) = await UniTask.WhenAll
            (
            LoadUtil.Async.LoadSpriteAsync(Asset_TitleText.RuntimeKey.ToString()),
            LoadUtil.Async.LoadSpriteAsync(Asset_TitleImage.RuntimeKey.ToString()),
            LoadUtil.Async.LoadSpriteAsync(Asset_MenuSprite.RuntimeKey.ToString()),
            LoadUtil.Async.LoadSpriteAsync(Asset_MenuHighlightedSprite.RuntimeKey.ToString()),
            LoadUtil.Async.LoadFontAssetAsync(Asset_MenuFont.RuntimeKey.ToString())
            );

        Sprite_TitleText.sprite = titleText;
        Sprite_TitleImage.sprite = titleImage;

        foreach (MainMenuButton menu in Menus)
        {
            menu.image.sprite = loadedSprite;
            menu.button.SetButtonSprite(loadedHighlightedSprite);
            menu.text   .font = loadedFont;
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

        for (int i = 0; i < Math.Min(texts.Length, Menus.Length); i++)
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

                Menus[i].gameObject.SetActive(false);
                continue;
            }

            if (Menus[i].gameObject.activeSelf == false)
            {
                Menus[i].gameObject.SetActive(true);
            }

            Menus[i].text.text = texts[i];
            Menus[i].button.onClick.RemoveAllListeners();
            Menus[i].button.onClick.AddListener(actions[i].Invoke);
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
        Action[] gameStartMenuAction = CreateActionArray(null, null, null, null, OnClick_MainMenuButton);

        UIData gameStartMenuData = CreateMainMenuUIData(MainMenuType.GameStart, gameStartMenuText, gameStartMenuAction);

        UIManager.Instance.OpenUI<MainMenu>(UIType.MainMenu, gameStartMenuData).Forget();

    }

    private void OpenMyCollectionMenu()
    {
        string[] myCollectionMenuText = CreateStringArray("캐릭터 콜렉션", "무기 콜렉션", "아티팩트 콜렉션", "", "돌아가기");
        Action[] myCollectionMenuAction = CreateActionArray(null, null, null, null, OnClick_MainMenuButton);

        UIData myCollectionMenuData = CreateMainMenuUIData(MainMenuType.MyCollection, myCollectionMenuText, myCollectionMenuAction);

        UIManager.Instance.OpenUI<MainMenu>(UIType.MainMenu, myCollectionMenuData).Forget();
    }

    private void OpenShopMenu()
    {
        string[] shopMenuText = CreateStringArray("캐릭터 샵", "무기 샵", "아티팩트 샵", "", "돌아가기");
        Action[] shopMenuAction = CreateActionArray(null, null, null, null, OnClick_MainMenuButton);

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