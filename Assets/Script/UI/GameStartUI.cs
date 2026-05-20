using Cysharp.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameStartUI : BaseUI
{
    // [SerializeField]
    [Header("StoryModeBtn")]
    [SerializeField] private Button StoryModeBtn;
    [SerializeField] private Image StoryModeBtnImage;
    [SerializeField] private TMP_Text StoryModeText;

    [Header("ReturnBtn")]
    [SerializeField] private Button ReturnBtn;
    [SerializeField] private Image ReturnBtnImage;
    [SerializeField] private TMP_Text ReturnText;

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
        StoryModeBtnImage.sprite = await MenuBtn();
        StoryModeText.font = await BaseFont();
        StoryModeText.text = "스토리 모드";

        ReturnBtnImage.sprite = await MenuBtn();
        ReturnText.font = await BaseFont();
        ReturnText.text = "돌아가기";

        m_isAssetLoad = true;
    }




    // [Bind All Btn Event]
    private void BindAllBtnEvent()
    {
        StoryModeBtn.onClick.AddListener(OnClick_StoryMode);

        ReturnBtn.onClick.AddListener(OnClick_Return);
    }


    // [Bind Btn Event]
    private void OnClick_StoryMode()
    {
        
    }

    private void OnClick_Return()
    {
        CloseUI(UIType.GameStart);
        UIManager.Instance.OpenMainMenuUI().Forget();
    }

}
