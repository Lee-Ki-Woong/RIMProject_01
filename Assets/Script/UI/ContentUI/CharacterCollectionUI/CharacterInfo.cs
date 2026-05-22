using Cysharp.Threading.Tasks;
using TMPro;
using UnityEngine;

public class CharacterInfoList : BasePanel
{
    [System.Serializable]
    private struct CharacterMain_Layout
    {
        public TMP_Text Text_ReadOnly;
        public TMP_Text Text_Data;
    }

    [SerializeField] private CharacterMain_Layout CharacterName;
    [SerializeField] private CharacterMain_Layout CharacterOtherName;
    [SerializeField] private CharacterMain_Layout CharacterClass;
    [SerializeField] private CharacterMain_Layout CharacterLevel;
    [SerializeField] private CharacterMain_Layout CharacterExp;
    [SerializeField] private CharacterMain_Layout CharacterHp;

    [SerializeField] private TMP_Text CharacterSimpleInfo;

    private void Awake()
    {
        IsAssetLoad = false;
        SetAssetAsync().Forget();
    }


    public override async UniTask SetAssetAsync()
    {
        TMP_FontAsset baseFontAsset = await BaseFont();

        TextBaseSetting(CharacterName, baseFontAsset, "이름");
        TextBaseSetting(CharacterOtherName, baseFontAsset, "이명");
        TextBaseSetting(CharacterClass, baseFontAsset, "직업");
        TextBaseSetting(CharacterLevel, baseFontAsset, "레벨");
        TextBaseSetting(CharacterExp, baseFontAsset, "경험치");
        TextBaseSetting(CharacterHp, baseFontAsset, "체력");
        CharacterSimpleInfo.font = baseFontAsset;

        IsAssetLoad = true;
    }

    public void SetCharacterData(CharacterData data)
    {
        CharacterName.Text_Data.text = data.Name;
        CharacterOtherName.Text_Data.text = data.OtherName;
        CharacterClass.Text_Data.text = data.Class;
        CharacterHp.Text_Data.text = data.Hp.ToString();
        CharacterSimpleInfo.text = data.Description;
    }

    private void TextBaseSetting(CharacterMain_Layout textList, TMP_FontAsset font, string text)
    {
        textList.Text_ReadOnly.font = font;
        textList.Text_ReadOnly.text = text;

        textList.Text_Data.font = font;
    }
}
