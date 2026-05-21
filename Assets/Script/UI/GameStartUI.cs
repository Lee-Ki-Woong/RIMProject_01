using Cysharp.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameStartUI : BaseUI
{
    // [SerializeField]
    [Header("Story Mode")]
    [SerializeField] private Button Button_StoryMode;
    [SerializeField] private Image Image_StoryModeButton;
    [SerializeField] private TMP_Text Text_StoryMode;

    [Header("Endless Mode")]
    [SerializeField] private Button Button_EndlessMode;
    [SerializeField] private Image Image_EndlessModeButton;
    [SerializeField] private TMP_Text Text_EndlessMode;

    [Header("Return")]
    [SerializeField] private Button Button_Return;
    [SerializeField] private Image Image_ReturnButton;
    [SerializeField] private TMP_Text Text_Return;

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


        Image_StoryModeButton.sprite = menuBtnSprite;
        Text_StoryMode.font = baseFont;
        Text_StoryMode.text = "스토리 모드";

        Image_EndlessModeButton.sprite = menuBtnSprite;
        Text_EndlessMode.font = baseFont;
        Text_EndlessMode.text = "무한 모드";

        Image_ReturnButton.sprite = menuBtnSprite;
        Text_Return.font = baseFont;
        Text_Return.text = "돌아가기";

        m_isAssetLoad = true;
    }


    // [Bind All Btn Event]
    private void BindAllBtnEvent()
    {
        Button_StoryMode.onClick.AddListener(OnClick_StoryMode);

        Button_Return.onClick.AddListener(OnClick_Return);
    }


    // [Bind Btn Event]
    private void OnClick_StoryMode()
    {
        
    }

    private void OnClick_Return()
    {
        CloseUI(UIType.GameStart);
        UIManager.Instance.OpenMainMenuUI();
    }

}
