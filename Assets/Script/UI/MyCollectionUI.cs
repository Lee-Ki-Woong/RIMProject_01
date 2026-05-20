using Cysharp.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MyCollectionUI : BaseUI
{
    // [SerializeField]
    [Header("CharacterCollection")]
    [SerializeField] private Button CharacterCollectionBtn;
    [SerializeField] private Image CharacterCollectionBtnImage;
    [SerializeField] private TMP_Text CharacterCollectionText;

    [Header("WeaponCollection")]
    [SerializeField] private Button WeaponCollectionBtn;
    [SerializeField] private Image WeaponCollectionBtnImage;
    [SerializeField] private TMP_Text WeaponCollectionText;

    [Header("ArtifactCollection")]
    [SerializeField] private Button ArtifactCollectionBtn;
    [SerializeField] private Image ArtifactCollectionBtnImage;
    [SerializeField] private TMP_Text ArtifactCollectionText;

    [Header("Return")]
    [SerializeField] private Button ReturnBtn;
    [SerializeField] private Image ReturnBtnImage;
    [SerializeField] private TMP_Text ReturnText;


    // [Lift Cycle]
    private void Awake()
    {
        m_isAssetLoad = false;
        BindAllBtnEvent();
    }


    // [Set Asset]
    public override async UniTask SetAssetAsync()
    {
        (Sprite menuBtnSprite, TMP_FontAsset baseFont) = await UniTask.WhenAll(MenuBtnSprite(), BaseFont());

        CharacterCollectionBtnImage.sprite = menuBtnSprite;
        CharacterCollectionText.font = baseFont;
        CharacterCollectionText.text = "캐릭터 컬렉션";

        WeaponCollectionBtnImage.sprite = menuBtnSprite;
        WeaponCollectionText.font = baseFont;
        WeaponCollectionText.text = "무기 컬렉션";

        ArtifactCollectionBtnImage.sprite = menuBtnSprite;
        ArtifactCollectionText.font = baseFont;
        ArtifactCollectionText.text = "아티팩트 컬렉션";

        ReturnBtnImage.sprite = menuBtnSprite;
        ReturnText.font = baseFont;
        ReturnText.text = "돌아가기";

    }


    // [Bind All Btn Event]
    private void BindAllBtnEvent()
    {
        CharacterCollectionBtn.onClick.AddListener(OnClick_CharacterCollection);
        WeaponCollectionBtn.onClick.AddListener(OnClick_WeaponCollection);
        ArtifactCollectionBtn.onClick.AddListener(OnClick_ArtifactCollection);

        ReturnBtn.onClick.AddListener(OnClick_Return);
    }


    // [Bint Btn Event]
    private void OnClick_CharacterCollection()
    {

    }

    private void OnClick_WeaponCollection()
    {

    }

    private void OnClick_ArtifactCollection()
    {

    }

    private void OnClick_Return()
    {
        CloseUI(UIType.MyCollection);
        UIManager.Instance.OpenMainMenuUI();
    }
}
