using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CharacterCollectionPresenter : BasePresenter
{
    public CharacterCollection CharacterCollectionUI { get; private set; }

    private GameObject Prefab_CharacterButton;
    private GameObject Prefab_CharacterInfo;
    private GameObject Prefab_characterSkillList;

    private Sprite Sprite_Backgorund;
    private Sprite Sprite_MenuButton;
    private Sprite Sprite_MenuButton_Highlighted;
    private Sprite Sprite_MenuButton_Selected;
    private Sprite Sprite_ExitButton;

    private TMP_FontAsset Font_MenuFont;

    private CharacterCollectionInfo m_chracterCollectionInfo;
    private CharacterCollectionSkillList m_chracterCollectionSkillList;

    public void InitCharacterCollection(CharacterCollection characterCollection)
    {
        CharacterCollectionUI = characterCollection;
    }

    public override async UniTask LoadAndSetAssetAsync()
    {
        if (IsAssetLoad)
        {
            return;
        }

        var (sprite_background, sprite_menuButton, sprite_menuButtonHighlighted, sprite_menuButtonSelected, sprite_exitButton, caracterButton, caracterInfo, caracterSkillList, baseFont) = await UniTask.WhenAll
            (
            LoadUtil.Async.LoadSpriteAsync(AddressUtil.Async.Sprite.UI.CharacterCollection.Background),
            LoadUtil.Async.LoadSpriteAsync(AddressUtil.Async.Sprite.UI.CharacterCollection.MenuButton),
            LoadUtil.Async.LoadSpriteAsync(AddressUtil.Async.Sprite.UI.CharacterCollection.MenuButton_Highlighted),
            LoadUtil.Async.LoadSpriteAsync(AddressUtil.Async.Sprite.UI.CharacterCollection.MenuButton_Selected),
            LoadUtil.Async.LoadSpriteAsync(AddressUtil.Async.Sprite.UI.CharacterCollection.ExitButton),

            LoadUtil.Async.LoadPrefabAsync(AddressUtil.Async.Prefab.Button.Character),
            LoadUtil.Async.LoadPrefabAsync(AddressUtil.Async.Prefab.Panel.CharacterInfo),
            LoadUtil.Async.LoadPrefabAsync(AddressUtil.Async.Prefab.Panel.CharacterSkill),
            LoadUtil.Async.LoadFontAssetAsync(AddressUtil.Async.Font.Base)
            );

        Sprite_Backgorund = sprite_background;
        Sprite_MenuButton = sprite_menuButton;
        Sprite_MenuButton_Highlighted = sprite_menuButtonHighlighted;
        Sprite_MenuButton_Selected = sprite_menuButtonSelected;
        Sprite_ExitButton = sprite_exitButton;
        Prefab_CharacterButton = caracterButton;
        Prefab_CharacterInfo = caracterInfo;
        Prefab_characterSkillList = caracterSkillList;
        Font_MenuFont = baseFont;

        CharacterCollectionUI.SetAsset(Sprite_Backgorund, Sprite_MenuButton, Sprite_MenuButton_Highlighted ,Sprite_MenuButton_Selected, Sprite_ExitButton, Font_MenuFont);
        CreatePanelAndButtons();

        IsAssetLoad = true;
    }

    public void SetCharacterCollection()
    {
        string[] texts = { "캐릭터 정보", "캐릭터 스킬", "나가기" };
        Action[] actions = { OnClick_CharacterInfoButton, OnClick_ChracterSkillListButton, OnClick_ExitButton};

        UIData characterCollectionData = new()
        {
            Texts = texts,
            Actions = actions
        };

        CharacterCollectionUI.SetData(characterCollectionData);
    }

    private void CreatePanelAndButtons()
    {
        Dictionary<string, CharacterData> characterData = GameDataManager.Instance.CharacterDataList;
        foreach (KeyValuePair<string, CharacterData> dataKV in characterData)
        {
            CharacterData data = dataKV.Value;
            if (data == null || data.Id == null) continue;

            CharacterCollectionUI.CreateButtons(Prefab_CharacterButton, data, OnClick_CharacterButton);
        }

        m_chracterCollectionInfo = CharacterCollectionUI.CreateCharacterInfo(Prefab_CharacterInfo);
        m_chracterCollectionInfo.SetAsset(Font_MenuFont);

        m_chracterCollectionSkillList = CharacterCollectionUI.CreateCharacterSkillList(Prefab_characterSkillList);
        m_chracterCollectionSkillList.SetAsset(Font_MenuFont);
    }

    private void OnClick_CharacterButton(string id)
    {
        if (GameDataManager.Instance.CharacterDataList.TryGetValue(id, out CharacterData characterData))
        {
            m_chracterCollectionInfo.SetData(characterData);
            m_chracterCollectionSkillList.SetData(characterData);
        }
    }

    private void OnClick_CharacterInfoButton()
    {
        m_chracterCollectionSkillList.ActiveFalse();
        m_chracterCollectionInfo.ActiveTrue();

        CharacterCollectionUI.SelectedCharacterInfo();
    }

    private void OnClick_ChracterSkillListButton()
    {
        m_chracterCollectionInfo.ActiveFalse();
        m_chracterCollectionSkillList.ActiveTrue();

        CharacterCollectionUI.SelectedCharacterSkillList();
    }

    private void OnClick_ExitButton()
    {
        UIManager.Instance.CloseUI(UIType.CharacterCollection);
    }
}
