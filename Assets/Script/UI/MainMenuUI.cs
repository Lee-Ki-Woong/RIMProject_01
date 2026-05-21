using Cysharp.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuUI : BaseUI
{
    // [SerializeField]
    [Header("Play Game")]
    [SerializeField] private Button Button_PlayGame;
    [SerializeField] private Image Image_PlayGameButton;
    [SerializeField] private TMP_Text Text_PlayGame;

    [Header("My Collection")]
    [SerializeField] private Button Button_MyCollection;
    [SerializeField] private Image Image_MyCollectionButton;
    [SerializeField] private TMP_Text Text_MyCollection;

    [Header("Shop")]
    [SerializeField] private Button Button_Shop;
    [SerializeField] private Image Image_ShopButton;
    [SerializeField] private TMP_Text Text_Shop;

    [Header("Game Option")]
    [SerializeField] private Button Button_GameOption;
    [SerializeField] private Image Image_GameOptionButton;
    [SerializeField] private TMP_Text Text_GameOption;

    [Header("Exit Game")]
    [SerializeField] private Button Button_ExitGame;
    [SerializeField] private Image Image_ExitGameButton;
    [SerializeField] private TMP_Text Text_ExitGame;

    // [Field]


    // [Life Cycle]
    private void Awake()
    {
        m_isAssetLoad = false;
        BindAllBtnEvent();
    }


    // [Set Asset]
    public override async UniTask SetAssetAsync()
    {
        (Sprite menuBtnSprite, TMP_FontAsset baseFont) = await UniTask.WhenAll(MenuBtnSprite(), BaseFont());


        Image_PlayGameButton.sprite = menuBtnSprite;
        Text_PlayGame.font = baseFont;
        Text_PlayGame.text = "게임 시작";


        Image_MyCollectionButton.sprite = menuBtnSprite;
        Text_MyCollection.font = baseFont;
        Text_MyCollection.text = "내 콜렉션";

        Image_ShopButton.sprite = menuBtnSprite;
        Text_Shop.font = baseFont;
        Text_Shop.text = "샵";

        Image_GameOptionButton.sprite = menuBtnSprite;
        Text_GameOption.font = baseFont;
        Text_GameOption.text = "게임 옵션";

        Image_ExitGameButton.sprite = menuBtnSprite;
        Text_ExitGame.font = baseFont;
        Text_ExitGame.text = "게임 종료";

        m_isAssetLoad = true;
    }


    // [Bind All Btn Event]
    private void BindAllBtnEvent()
    {
        Button_PlayGame.onClick.AddListener(OnClick_PlayGame);
        Button_MyCollection.onClick.AddListener(OnClick_OpenMyCollectionUI);
        Button_Shop.onClick.AddListener(OnClick_OpenShopUI);
        Button_GameOption.onClick.AddListener(OnClick_OpenGameOptionUI);

        Button_ExitGame.onClick.AddListener(OnClick_ExitGame);
    }


    // [Bind Btn Event]
    private void OnClick_PlayGame()
    {
        CloseUI(UIType.MainMenu);
        UIManager.Instance.OpenGameStartUI();
    }

    private void OnClick_OpenMyCollectionUI()
    {
        CloseUI(UIType.MainMenu);
        UIManager.Instance.OpenMyCollectionUI();
    }

    private void OnClick_OpenShopUI()
    {
        CloseUI(UIType.MainMenu);
    }

    private void OnClick_OpenGameOptionUI()
    {
        CloseUI(UIType.MainMenu);
        UIManager.Instance.OpenGameOptionUI();
    }

    private void OnClick_ExitGame()
    {
        GameManager.Instance.GameQuit();
    }
}
