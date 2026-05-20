using Cysharp.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuUI : BaseUI
{
    // [SerializeField]
    [Header("PlayGameBtn")]
    [SerializeField] private Button PlayGameBtn;
    [SerializeField] private Image PlayGameBtnImage;
    [SerializeField] private TMP_Text PlayGameText;

    [Header("MyCollectionBtn")]
    [SerializeField] private Button MyCollectionBtn;
    [SerializeField] private Image MyCollectionBtnImage;
    [SerializeField] private TMP_Text MyCollectionText;

    [Header("ShopBtn")]
    [SerializeField] private Button ShopBtn;
    [SerializeField] private Image ShopBtnImage;
    [SerializeField] private TMP_Text ShopText;

    [Header("GameOptionBtn")]
    [SerializeField] private Button GameOptionBtn;
    [SerializeField] private Image GameOptionBtnImage;
    [SerializeField] private TMP_Text GameOptionText;

    [Header("ExitGameBtn")]
    [SerializeField] private Button ExitGameBtn;
    [SerializeField] private Image ExitGameBtnImage;
    [SerializeField] private TMP_Text ExitGameText;

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
        PlayGameBtnImage.sprite = await MenuBtn();
        PlayGameText.font = await BaseFont();
        PlayGameText.text = "게임 시작";


        MyCollectionBtnImage.sprite = await MenuBtn();
        MyCollectionText.font = await BaseFont();
        MyCollectionText.text = "내 콜렉션";

        ShopBtnImage.sprite = await MenuBtn();
        ShopText.font = await BaseFont();
        ShopText.text = "샵";

        GameOptionBtnImage.sprite = await MenuBtn();
        GameOptionText.font = await BaseFont();
        GameOptionText.text = "게임 옵션";

        ExitGameBtnImage.sprite = await MenuBtn();
        ExitGameText.font = await BaseFont();
        ExitGameText.text = "게임 종료";

        m_isAssetLoad = true;
    }


    // [Bind All Btn Event]
    private void BindAllBtnEvent()
    {
        PlayGameBtn.onClick.AddListener(OnClick_PlayGame);
        MyCollectionBtn.onClick.AddListener(OnClick_OpenMyCollectionUI);
        ShopBtn.onClick.AddListener(OnClick_OpenShopUI);
        GameOptionBtn.onClick.AddListener(OnClick_OpenGameOptionUI);

        ExitGameBtn.onClick.AddListener(OnClick_ExitGame);
    }


    // [Bind Btn Event]
    private void OnClick_PlayGame()
    {
        CloseUI(UIType.MainMenu);
        UIManager.Instance.OpenGameStartUI().Forget();
    }

    private void OnClick_OpenMyCollectionUI()
    {
        CloseUI(UIType.MainMenu);
        UIManager.Instance.OpenMyCollectionUI().Forget();
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
