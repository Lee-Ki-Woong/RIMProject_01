using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CharacterCollectionPresenter : BasePresenter
{
    public CharacterCollection CharacterCollectionUI { get; private set; }

    private GameObject m_prefab_characterBtuuon;
    private GameObject m_prefab_skillButton;
    private GameObject m_prefab_characterInfo;

    private Sprite m_sprite_background;
    private Sprite m_sprite_menuButton;
    private Sprite m_sprite_menuButton_Highlighted;
    private Sprite m_sprite_menuButton_Selected;
    private Sprite m_sprite_exitButton;

    private TMP_FontAsset Font_MenuFont;

    private CharacterCollectionInfo m_chracterCollectionInfo;
    private Transform m_skillSlot;

    private Sprite m_sprite_characterStanding;
    private Dictionary<Sprite, string> m_characterStandingAssetAddressDic = new();

    public void InitCharacterCollection(CharacterCollection characterCollection)
    {
        CharacterCollectionUI = characterCollection;
        m_skillSlot = characterCollection.SkillIconSlot;
    }

    public void OpenCharacterCollectionUI()
    {
        string[] texts = { "캐릭터 정보", "캐릭터 스킬" };
        Action[] actions = { OnClick_CharacterInfoButton, OnClick_ChracterSkillListButton, OnClick_ExitButton };

        UIData characterCollectionData = new()
        {
            Texts = texts,
            Actions = actions
        };

        CharacterCollectionUI.SetData(characterCollectionData);
        OnClick_CharacterInfoButton();
        OnClick_CharacterButton(CharacterCollectionUI.m_characterButtons[0].m_iconDataId);
    }

    public override async UniTask LoadAndSetAssetAsync()
    {
        if (IsAssetLoad)
        {
            return;
        }

        var (sprite_background, sprite_menuButton, sprite_menuButtonHighlighted, sprite_menuButtonSelected, sprite_exitButton, characterButton, skillButton, characterInfo, baseFont) = await UniTask.WhenAll
            (
            LoadUtil.Async.LoadSpriteAsync(AddressUtil.Async.Sprite.UI.CharacterCollection.Background),
            LoadUtil.Async.LoadSpriteAsync(AddressUtil.Async.Sprite.UI.Button_Empty),
            LoadUtil.Async.LoadSpriteAsync(AddressUtil.Async.Sprite.UI.CharacterCollection.MenuButton_Highlighted),
            LoadUtil.Async.LoadSpriteAsync(AddressUtil.Async.Sprite.UI.CharacterCollection.MenuButton_Selected),
            LoadUtil.Async.LoadSpriteAsync(AddressUtil.Async.Sprite.UI.CharacterCollection.ExitButton),
            LoadUtil.Async.LoadPrefabAsync(AddressUtil.Async.Prefab.Button.Character),
            LoadUtil.Async.LoadPrefabAsync(AddressUtil.Async.Prefab.Button.Skill),
            LoadUtil.Async.LoadPrefabAsync(AddressUtil.Async.Prefab.Panel.CharacterInfo),
            LoadUtil.Async.LoadFontAssetAsync(AddressUtil.Async.Font.BaseFont)
            );

        m_sprite_background = sprite_background;
        m_sprite_menuButton = sprite_menuButton;
        m_sprite_menuButton_Highlighted = sprite_menuButtonHighlighted;
        m_sprite_menuButton_Selected = sprite_menuButtonSelected;
        m_sprite_exitButton = sprite_exitButton;
        m_prefab_characterBtuuon = characterButton;
        m_prefab_skillButton = skillButton;
        m_prefab_characterInfo = characterInfo;
        Font_MenuFont = baseFont;

        CharacterCollectionUI.SetAsset(m_sprite_background, m_sprite_menuButton, m_sprite_menuButton_Highlighted ,m_sprite_menuButton_Selected, m_sprite_exitButton, Font_MenuFont);
        CreatePanelAndButtons();

        IsAssetLoad = true;

    }

    private void CreatePanelAndButtons()
    {
        Dictionary<string, CharacterData> characterData = GameDataManager.Instance.CharacterDataList;
        foreach (KeyValuePair<string, CharacterData> dataKV in characterData)
        {
            CharacterData data = dataKV.Value;
            if (data == null || data.Id == null) continue;

            CharacterCollectionUI.CreateCharacterButtons(m_prefab_characterBtuuon, data, OnClick_CharacterButton);
        }

        m_chracterCollectionInfo = CharacterCollectionUI.CreateCharacterInfo(m_prefab_characterInfo);
        m_chracterCollectionInfo.SetAsset(Font_MenuFont);
    }

    private void OnClick_CharacterButton(string id)
    {


        if (GameDataManager.Instance.CharacterDataList.TryGetValue(id, out CharacterData characterData))
        {
            m_chracterCollectionInfo.SetData(characterData);

            if(characterData.SkillList == null || characterData.SkillList.Length == 0)
            {
                Debug.LogError(characterData.Name + "캐릭터의 스킬 리스트가 비어있습니다.");
                return;
            }

            Sprite characterStaindingSprite = LoadUtil.Sync.LoadGeneric<Sprite>(characterData.CharacterStandPath);
            CharacterCollectionUI.SetCharacterStandingAsset(characterStaindingSprite);

            CharacterCollectionUI.DestroyAllSkillButtons();

            SkillData[] skillDataList = new SkillData[characterData.SkillList.Length];

            for(int i = 0; i < skillDataList.Length; i++)
            {
                GameDataManager.Instance.SkillDataList.TryGetValue(characterData.SkillList[i], out SkillData skillData);
                skillDataList[i] = skillData;

                if(skillData == null)
                {
                    Debug.LogError(characterData.Name + $"캐릭터의 {i}번째 스킬 데이터가 존재하지 않습니다.");
                    continue;
                }

                CharacterCollectionUI.CreateSkillButtons(m_prefab_skillButton, skillDataList[i], OnClick_SkillButton);
            }
        }
    }

    private void OnClick_SkillButton(string id)
    {

    }

    private void OnClick_CharacterInfoButton()
    {
        m_chracterCollectionInfo.ActiveTrue();
        m_skillSlot.gameObject.SetActive(false);

        CharacterCollectionUI.SelectedCharacterInfo();
    }

    private void OnClick_ChracterSkillListButton()
    {
        m_chracterCollectionInfo.ActiveFalse();
        m_skillSlot.gameObject.SetActive(true);

        CharacterCollectionUI.SelectedCharacterSkillList();
    }

    private void OnClick_ExitButton()
    {
        UIManager.Instance.CloseUI(UIType.CharacterCollection);
    }


    public void CloseCharacterCollectionUI()
    {
        UIManager.Instance.CloseUI(UIType.CharacterCollection);
    }

}
