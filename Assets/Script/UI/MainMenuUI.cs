using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuUI : BaseUI
{
    // [SerializeField]


    [Header("PlayGameBtn")]
    [SerializeField] private Button PlayGameBtn;
    [SerializeField] private Image PlayGameBtnImage;

    [Header("MyCollectionBtn")]
    [SerializeField] private Button MyColletionBtn;
    [SerializeField] private Image MyColletionBtnImage;

    [Header("ShopBtn")]
    [SerializeField] private Button ShopBtn;
    [SerializeField] private Image ShopBtnImage;

    [Header("GameOptionBtn")]
    [SerializeField] private Button GameOptionBtn;
    [SerializeField] private Image GameOptionBtnImage;

    [Header("ExitGameBtn")]
    [SerializeField] private Button ExitGameBtn;
    [SerializeField] private Image ExitGameBtnImage;

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
        PlayGameBtnImage.sprite = await LoadUtil.LoadSpriteAsync("Sprite/MainMenu/MenuBtn", destroyCancellationToken);
        MyColletionBtnImage.sprite = await LoadUtil.LoadSpriteAsync("Sprite/MainMenu/MenuBtn", destroyCancellationToken);
        ShopBtnImage.sprite = await LoadUtil.LoadSpriteAsync("Sprite/MainMenu/MenuBtn", destroyCancellationToken);
        GameOptionBtnImage.sprite = await LoadUtil.LoadSpriteAsync("Sprite/MainMenu/MenuBtn", destroyCancellationToken);
        ExitGameBtnImage.sprite = await LoadUtil.LoadSpriteAsync("Sprite/MainMenu/MenuBtn", destroyCancellationToken);

        m_isAssetLoad = true;
    }


    // [BindAllBtnEvent]
    private void BindAllBtnEvent()
    {
        PlayGameBtn.onClick.AddListener(OnClick_PlayGame);
        MyColletionBtn.onClick.AddListener(OnClick_OpenMyCollectionUI);
        ShopBtn.onClick.AddListener(OnClick_OpenShopUI);
        GameOptionBtn.onClick.AddListener(OnClick_OpenGameOptionUI);
        ExitGameBtn.onClick.AddListener(OnClick_ExitGame);
    }


    // [BindBtnEvent]
    private void OnClick_PlayGame()
    {
        CloseUI(UIType.MainMenu);
    }

    private void OnClick_OpenMyCollectionUI()
    {
        CloseUI(UIType.MainMenu);
    }

    private void OnClick_OpenShopUI()
    {
        CloseUI(UIType.MainMenu);
    }

    private void OnClick_OpenGameOptionUI()
    {
        CloseUI(UIType.MainMenu);
    }

    private void OnClick_ExitGame()
    {
        GameManager.Instance.GameQuit();
    }
}
