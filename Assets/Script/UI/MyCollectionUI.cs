using Cysharp.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MyCollectionUI : BaseMainUI
{
    // [SerializeField]
    [Header("Character Collection")]
    [SerializeField] private Button Button_CharacterCollection;
    [SerializeField] private Image Image_CharacterCollectionButton;
    [SerializeField] private TMP_Text Text_CharacterCollection;

    [Header("Weapon Collection")]
    [SerializeField] private Button Button_WeaponCollection;
    [SerializeField] private Image Image_WeaponCollectionButton;
    [SerializeField] private TMP_Text Text_WeaponCollection;

    [Header("Artifact Collection")]
    [SerializeField] private Button Button_ArtifactCollection;
    [SerializeField] private Image Image_ArtifactCollctionButton;
    [SerializeField] private TMP_Text Text_ArtifactCollection;

    [Header("Return")]
    [SerializeField] private Button Button_Return;
    [SerializeField] private Image Image_ReturnButton;
    [SerializeField] private TMP_Text Text_Return;


    // [Lift Cycle]
    private void Awake()
    {
        IsAssetLoad = false;
        BindAllBtnEvent();
    }


    // [Set Asset]
    public override async UniTask SetAssetAsync()
    {
        (Sprite menuBtnSprite, TMP_FontAsset baseFont) = await UniTask.WhenAll(MenuBtnSprite(), BaseFont());

        Image_CharacterCollectionButton.sprite = menuBtnSprite;
        Text_CharacterCollection.font = baseFont;
        Text_CharacterCollection.text = "캐릭터 컬렉션";

        Image_WeaponCollectionButton.sprite = menuBtnSprite;
        Text_WeaponCollection.font = baseFont;
        Text_WeaponCollection.text = "무기 컬렉션";

        Image_ArtifactCollctionButton.sprite = menuBtnSprite;
        Text_ArtifactCollection.font = baseFont;
        Text_ArtifactCollection.text = "아티팩트 컬렉션";

        Image_ReturnButton.sprite = menuBtnSprite;
        Text_Return.font = baseFont;
        Text_Return.text = "돌아가기";

        IsAssetLoad = true;

    }


    // [Bind All Button Event]
    private void BindAllBtnEvent()
    {
        Button_CharacterCollection.onClick.AddListener(OnClick_CharacterCollection);
        Button_WeaponCollection.onClick.AddListener(OnClick_WeaponCollection);
        Button_ArtifactCollection.onClick.AddListener(OnClick_ArtifactCollection);

        Button_Return.onClick.AddListener(OnClick_Return);
    }


    // [Bind Btn Event]
    private void OnClick_CharacterCollection()
    {
        UIManager.Instance.OpenCharacterCollectionUI();
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
