using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuUI : BaseUI
{
    // [SerializeField]
    [SerializeField] private Image TitleImage;

    [SerializeField] private Button PlayGameBtn;
    [SerializeField] private Button MyColletionBtn;
    [SerializeField] private Button ShopBtn;
    [SerializeField] private Button GameOptionBtn;
    [SerializeField] private Button ExitGameBtn;


    // [Field]
    public bool isAssetLoad { get; private set; }

    // [Life Cycle]
    private void Awake()
    {
        isAssetLoad = false;
    }


    // [Set Asset]
    public override async UniTask SetAssetAsync()
    {
        TitleImage.sprite = await LoadUtil.LoadSprite("Sprite/MainMenu/TitleTextSprite", destroyCancellationToken);

        isAssetLoad = true;
    }


    // [BindBtnEvent]
    private void OnClick_PlayGame()
    {

    }

    private void OnClick_OpenMyCollectionUI()
    {

    }

    private void OnClick_OpenShopUI()
    {

    }

    private void OnClick_OpenGameOptionUI()
    {

    }

    private void OnClick_ExitGame()
    {

    }
}
