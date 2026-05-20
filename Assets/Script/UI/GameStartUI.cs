using Cysharp.Threading.Tasks;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class GameStartUI : BaseUI
{
    // [SerializeField]
    [Header("StoryModeBtn")]
    [SerializeField] private Button StoryModeBtn;
    [SerializeField] private Image StoryModeBtnImage;

    [Header("ReturnBtn")]
    [SerializeField] private Button ReturnBtn;
    [SerializeField] private Image ReturnBtnImage;


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
        StoryModeBtnImage.sprite = await LoadUtil.LoadSpriteAsync("Sprite/MainUI/MenuBtn", destroyCancellationToken);
        ReturnBtnImage.sprite = await LoadUtil.LoadSpriteAsync("Sprite/MainUI/MenuBtn", destroyCancellationToken);

        m_isAssetLoad = true;
    }




    // [BindAllBtnEvent]
    private void BindAllBtnEvent()
    {
        StoryModeBtn.onClick.AddListener(OnClick_StoryMode);
        ReturnBtn.onClick.AddListener(OnClick_Return);
    }


    // [BindBtnEvent]
    private void OnClick_StoryMode()
    {
        
    }

    private void OnClick_Return()
    {
        CloseUI(UIType.GameStart);
        UIManager.Instance.OpenMainMenuUI().Forget();
    }

}
