using Cysharp.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameOptionUI : BaseMainUI
{
    // [SerializeField]
    [Header("Game Option")]
    [SerializeField] private Button Button_GameOption;
    [SerializeField] private Image Image_GameOptionButton;
    [SerializeField] private TMP_Text Text_GameOption;

    [Header("Sound Option")]
    [SerializeField] private Button Button_SoundOption;
    [SerializeField] private Image Image_SoundOptionButton;
    [SerializeField] private TMP_Text Text_SoundOption;

    [Header("Return")]
    [SerializeField] private Button Button_Return;
    [SerializeField] private Image Image_ReturnButton;
    [SerializeField] private TMP_Text Text_Return;


    // [Life Cycle]
    private void Awake()
    {
        IsAssetLoad = false;
        BindAllBtnEvent();
    }


    // [Set Asset]
    public override async UniTask SetAssetAsync()
    {
        (Sprite menuBtnSprite, TMP_FontAsset baseFont) = await UniTask.WhenAll(MenuBtnSprite(), BaseFont());

        Image_GameOptionButton.sprite = menuBtnSprite;
        Text_GameOption.font = baseFont;
        Text_GameOption.text = "게임 옵션";

        Image_SoundOptionButton.sprite = menuBtnSprite;
        Text_SoundOption.font = baseFont;
        Text_SoundOption.text = "사운드 옵션";

        Image_ReturnButton.sprite = menuBtnSprite;
        Text_Return.font = baseFont;
        Text_Return.text = "돌아가기";

        IsAssetLoad = true;
    }


    // [Bind All Btn Event]
    private void BindAllBtnEvent()
    {
        Button_GameOption.onClick.AddListener(OnClick_GameOptionButton);
        Button_SoundOption.onClick.AddListener(OnClick_SoundOptionButton);
        Button_Return.onClick.AddListener(OnClick_ReturnButton);
    }


    // [Bind Btn Event]
    private void OnClick_GameOptionButton()
    {

    }

    private void OnClick_SoundOptionButton()
    {

    }

    private void OnClick_ReturnButton()
    {
        CloseUI(UIType.GameOption);
        UIManager.Instance.OpenMainMenuUI();
    }

}
