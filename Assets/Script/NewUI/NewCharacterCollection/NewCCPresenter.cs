using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class NewCharacterCollectionPresenter : BasePresenter
{
    public NewCharacterCollection CharacterCollectionUI { get; private set; }

    private GameObject m_prefab_characterButton;
    private GameObject m_prefab_skillButton;
    private GameObject m_prefab_characterInfo;

    private Sprite m_sprite_background;
    private Sprite m_sprite_menuButton;
    private Sprite m_sprite_menuButton_Highlighted;
    private Sprite m_sprite_menuButton_Selected;
    private Sprite m_sprite_exitButton;

    private TMP_FontAsset m_font_menuFont;

    // View와 연결 (초기화 시 이벤트 구독)
    public void InitCharacterCollection(NewCharacterCollection characterCollection)
    {
        CharacterCollectionUI = characterCollection;
        SubscribeEvents();
    }

    protected void SubscribeEvents()
    {
        CharacterCollectionUI.OnExitClicked += OnClick_ExitButton;
        CharacterCollectionUI.OnCharacterInfoTabClicked += OnClick_CharacterInfoTab;
        CharacterCollectionUI.OnCharacterSkillTabClicked += OnClick_CharacterSkillTab;
        CharacterCollectionUI.OnCharacterSelected += HandleCharacterSelected;
        CharacterCollectionUI.OnSkillSelected += HandleSkillSelected;
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
        m_prefab_characterButton = characterButton;
        m_prefab_skillButton = skillButton;
        m_prefab_characterInfo = characterInfo;
        m_font_menuFont = baseFont;

        // View 초기화 명령
        CharacterCollectionUI.SetAsset(m_sprite_background, m_sprite_menuButton, m_sprite_menuButton_Highlighted, m_sprite_menuButton_Selected, m_sprite_exitButton, m_font_menuFont);
        CharacterCollectionUI.InitCharacterInfoPanel(m_prefab_characterInfo, m_font_menuFont);

        CreateCharacterList();

        IsAssetLoad = true;
    }

    public void OpenCharacterCollectionUI()
    {
        OnClick_CharacterInfoTab();

        // 첫 번째 캐릭터 강제 선택 처리 로직
        var firstCharacter = GameDataManager.Instance.CharacterDataList.Values.FirstOrDefault();
        if (firstCharacter != null)
        {
            HandleCharacterSelected(firstCharacter.Id);
        }
    }

    private void CreateCharacterList()
    {
        // 데이터만 추출하여 View에 리스트 생성 위임
        List<CharacterData> characterList = GameDataManager.Instance.CharacterDataList.Values.ToList();
        CharacterCollectionUI.PopulateCharacterList(characterList, m_prefab_characterButton);
    }

    // =========================================================
    // 이벤트 핸들러 (비즈니스 로직 처리)
    // =========================================================
    private void HandleCharacterSelected(string id)
    {
        if (GameDataManager.Instance.CharacterDataList.TryGetValue(id, out CharacterData characterData))
        {
            // 1. 정보 패널 업데이트
            CharacterCollectionUI.UpdateCharacterInfoData(characterData);

            // 2. 스탠딩 이미지 로드 및 업데이트
            Sprite characterStandingSprite = LoadUtil.Sync.LoadGeneric<Sprite>(characterData.CharacterStandPath);
            CharacterCollectionUI.SetCharacterStandingAsset(characterStandingSprite);

            // 3. 스킬 데이터 검증 및 배열화
            if (characterData.SkillList == null || characterData.SkillList.Length == 0)
            {
                Debug.LogError(characterData.Name + " 캐릭터의 스킬 리스트가 비어있습니다.");
                CharacterCollectionUI.PopulateSkillList(new SkillData[0], m_prefab_skillButton); // 스킬 비우기
                return;
            }

            SkillData[] skillDataList = new SkillData[characterData.SkillList.Length];
            for (int i = 0; i < skillDataList.Length; i++)
            {
                GameDataManager.Instance.SkillDataList.TryGetValue(characterData.SkillList[i], out SkillData skillData);
                if (skillData == null)
                {
                    Debug.LogError(characterData.Name + $" 캐릭터의 {i}번째 스킬 데이터가 존재하지 않습니다.");
                }
                skillDataList[i] = skillData;
            }

            // 4. 스킬 리스트 생성 위임
            CharacterCollectionUI.PopulateSkillList(skillDataList, m_prefab_skillButton);
        }
    }

    private void HandleSkillSelected(string id)
    {
        // 스킬 선택 시 처리 로직
    }

    private void OnClick_CharacterInfoTab()
    {
        CharacterCollectionUI.SwitchToCharacterInfoTab();
    }

    private void OnClick_CharacterSkillTab()
    {
        CharacterCollectionUI.SwitchToSkillListTab();
    }

    private void OnClick_ExitButton()
    {
        UIManager.Instance.CloseUI(UIType.CharacterCollection);
    }

    public void UnsubscribeEvents()
    {
        if (CharacterCollectionUI == null) return;

        CharacterCollectionUI.OnExitClicked -= OnClick_ExitButton;
        CharacterCollectionUI.OnCharacterInfoTabClicked -= OnClick_CharacterInfoTab;
        CharacterCollectionUI.OnCharacterSkillTabClicked -= OnClick_CharacterSkillTab;
        CharacterCollectionUI.OnCharacterSelected -= HandleCharacterSelected;
        CharacterCollectionUI.OnSkillSelected -= HandleSkillSelected;

    }
}