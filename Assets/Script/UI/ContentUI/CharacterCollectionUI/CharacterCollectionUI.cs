using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CharacterCollectionUI : BaseUI
{
    // [SerializeField]
    [Header("Character Icon")]
    [SerializeField] private GameObject Prefab_CharacterIcon;

    [Header("Layout Group")]
    [SerializeField] private GameObject Layout_CharacterMain;
    [SerializeField] private GameObject Layout_CharacterSkillList;

    [System.Serializable]
    private struct CharacterInfo_Layout
    {
        public Button Button;
        public Image Image;
        public TMP_Text Text;
        public GameObject GameObject_Selected;
        public Image Image_Selected;
    }

    [System.Serializable]
    private struct CharacterMain_Layout
    {
        public TMP_Text Text_ReadOnly;
        public TMP_Text Text_Data;
    }


    [SerializeField] private CharacterInfo_Layout CharacterMain;
    [SerializeField] private CharacterInfo_Layout CharacterSkill;
    [SerializeField] private CharacterInfo_Layout CharacterDescription;

    [SerializeField] private CharacterMain_Layout CharacterName;
    [SerializeField] private CharacterMain_Layout CharacterOtherName;
    [SerializeField] private CharacterMain_Layout CharacterClass;
    [SerializeField] private CharacterMain_Layout CharacterLevel;
    [SerializeField] private CharacterMain_Layout CharacterExp;
    [SerializeField] private CharacterMain_Layout CharacterHp;

    [SerializeField] private TMP_Text CharacterSimpleInfo;


    private void Start()
    {
        FirstSetting().Forget();
    }


    private async UniTaskVoid FirstSetting()
    {
        await SetAssetAsync();
        BindAllButtonEvent();
        OnClick_CharacterMainButton();
    }

    private void BindAllButtonEvent()
    {
        CharacterMain.Button.onClick.AddListener(OnClick_CharacterMainButton);
        CharacterSkill.Button.onClick.AddListener(OnClick_CharacterSkillButton);
        CharacterDescription.Button.onClick.AddListener(OnClick_CharacterDescriptionButton);
    }


    private void OnClick_CharacterMainButton()
    {
        Layout_CharacterMain.SetActive(true);
        Layout_CharacterSkillList.SetActive(false);

        CharacterMain.GameObject_Selected.SetActive(true);
        CharacterSkill.GameObject_Selected.SetActive(false);
        CharacterDescription.GameObject_Selected.SetActive(false);
    }

    private void OnClick_CharacterSkillButton()
    {
        Layout_CharacterMain.SetActive(false);
        Layout_CharacterSkillList.SetActive(true);

        CharacterMain.GameObject_Selected.SetActive(false);
        CharacterSkill.GameObject_Selected.SetActive(true);
        CharacterDescription.GameObject_Selected.SetActive(false);
    }

    private void OnClick_CharacterDescriptionButton()
    {

        CharacterMain.GameObject_Selected.SetActive(false);
        CharacterSkill.GameObject_Selected.SetActive(false);
        CharacterDescription.GameObject_Selected.SetActive(true);
    }


    public override async UniTask SetAssetAsync()
    {
        (Sprite baseSprite, Sprite hightlightAndPressedSprite, Sprite selectedSprite,TMP_FontAsset baseFontAsset) = await UniTask.WhenAll
            (
            LoadUtil.LoadSpriteAsync("Sprite/CharacterCollectionUI/BaseSprite",destroyCancellationToken),
            LoadUtil.LoadSpriteAsync("Sprite/CharacterCollectionUI/HighlightedSprite", destroyCancellationToken),
            LoadUtil.LoadSpriteAsync("Sprite/CharacterCollectionUI/SelectedSprite", destroyCancellationToken),
            BaseFont()
            );
        ButtonBaseSetting(CharacterMain, baseSprite, hightlightAndPressedSprite, selectedSprite, baseFontAsset, "캐릭터 정보");
        ButtonBaseSetting(CharacterSkill, baseSprite, hightlightAndPressedSprite, selectedSprite, baseFontAsset, "캐릭터 스킬");
        ButtonBaseSetting(CharacterDescription, baseSprite, hightlightAndPressedSprite, selectedSprite, baseFontAsset, "캐릭터 스토리");

        TextBaseSetting(CharacterName, baseFontAsset, "이름");
        TextBaseSetting(CharacterOtherName, baseFontAsset, "이명");
        TextBaseSetting(CharacterClass, baseFontAsset, "직업");
        TextBaseSetting(CharacterLevel, baseFontAsset, "레벨");
        TextBaseSetting(CharacterExp, baseFontAsset, "경험치");
        TextBaseSetting(CharacterHp, baseFontAsset, "체력");
}

    private void ButtonBaseSetting(CharacterInfo_Layout menuButton, Sprite baseSprite, Sprite hightlightAndPressedSprite, Sprite selectedSprite, TMP_FontAsset font, string text)
    {
        menuButton.Image.sprite = baseSprite;
        menuButton.Button.SetButtonSprite(hightlightAndPressedSprite, hightlightAndPressedSprite);

        menuButton.Image_Selected.sprite = selectedSprite;

        menuButton.Text.font = font;
        menuButton.Text.text = text;

        if(menuButton.GameObject_Selected.activeSelf)
        {
            menuButton.GameObject_Selected.SetActive(false);
        }
    }

    private void TextBaseSetting(CharacterMain_Layout textList, TMP_FontAsset font, string text)
    {
        textList.Text_ReadOnly.font = font;
        textList.Text_ReadOnly.text = text;

        textList.Text_Data.font = font;
    }

}
