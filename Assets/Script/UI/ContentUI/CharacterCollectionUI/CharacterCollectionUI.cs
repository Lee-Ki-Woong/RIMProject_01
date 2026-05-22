using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CharacterCollectionUI : BaseMainUI
{
    // [SerializeField]
    [Header("Character Icon")]
    [SerializeField] private GameObject Prefab_CharacterIcon;
    [SerializeField] private Transform CharacterIconSlot;

    private CharacterInfoList m_characterInfoList;
    private CharacterSkillList m_characterSkillList;


    [System.Serializable]
    private struct UIMenu_Layout
    {
        public Button Button;
        public Image Image;
        public TMP_Text Text;
        public GameObject GameObject_Selected;
        public Image Image_Selected;
    }

    [SerializeField] private UIMenu_Layout CharacterInfo;
    [SerializeField] private UIMenu_Layout CharacterSkill;


    [System.Serializable]
    private struct ExitButton_Layout
    {
        public Button Button;
        public Image Image;
    }

    [SerializeField] private ExitButton_Layout ExitButton;


    // [Field]
    private Dictionary<string, CharacterIcon> m_iconListDic = new();


    // [Life Cycle]


    private void Awake()
    {
        IsAssetLoad = false;
        FirstSetting().Forget();
    }


    private async UniTaskVoid FirstSetting()
    {
        await SetAssetAsync();
        BindAllButtonEvent();
        CreateCharacterInfoPanel();
        CreateCharacterSkillListPanel();
        OnClick_CharacterMainButton();
        ReadCharacterListAndCreateIcon();
    }


    // [Bind All Button Event]
    private void BindAllButtonEvent()
    {
        CharacterInfo.Button.onClick.AddListener(OnClick_CharacterMainButton);
        CharacterSkill.Button.onClick.AddListener(OnClick_CharacterSkillButton);
        ExitButton.Button.onClick.AddListener(OnClick_ExitButton);
    }


    // [Bind Button Event]
    private void OnClick_CharacterMainButton()
    {
        m_characterInfoList.gameObject.SetActive(true);
        m_characterSkillList.gameObject.SetActive(false);

        CharacterInfo.GameObject_Selected.SetActive(true);
        CharacterSkill.GameObject_Selected.SetActive(false);
    }

    private void OnClick_CharacterSkillButton()
    {
        m_characterInfoList.gameObject.SetActive(false);
        m_characterSkillList.gameObject.SetActive(true);

        CharacterInfo.GameObject_Selected.SetActive(false);
        CharacterSkill.GameObject_Selected.SetActive(true);
    }

    private void OnClick_ExitButton()
    {
        UIManager.Instance.CloseUI(UIType.CharacterCollection);
    }


    // [LoadAsset]
    public override async UniTask SetAssetAsync()
    {
        (Sprite baseSprite, Sprite hightlightAndPressedSprite, Sprite selectedSprite, TMP_FontAsset baseFontAsset) = await UniTask.WhenAll
            (
            LoadUtil.LoadSpriteAsync(PathUtil.Async.Sprite.CharacterCollection_Base, destroyCancellationToken),
            LoadUtil.LoadSpriteAsync(PathUtil.Async.Sprite.CharacterCollection_Highlighted, destroyCancellationToken),
            LoadUtil.LoadSpriteAsync(PathUtil.Async.Sprite.CharacterCollection_Selected, destroyCancellationToken),
            BaseFont()
            );

        GameObject charaterIcon = LoadUtil.LoadPrefab(PathUtil.Sync.Icon.CharacterIcon);

        Prefab_CharacterIcon = charaterIcon;
        ButtonBaseSetting(CharacterInfo, baseSprite, hightlightAndPressedSprite, selectedSprite, baseFontAsset, "캐릭터 정보");
        ButtonBaseSetting(CharacterSkill, baseSprite, hightlightAndPressedSprite, selectedSprite, baseFontAsset, "캐릭터 스킬");

        IsAssetLoad = true;
    }

    private void ButtonBaseSetting(UIMenu_Layout menuButton, Sprite baseSprite, Sprite hightlightAndPressedSprite, Sprite selectedSprite, TMP_FontAsset font, string text)
    {
        menuButton.Image.sprite = baseSprite;
        menuButton.Button.SetButtonSprite(hightlightAndPressedSprite, hightlightAndPressedSprite);

        menuButton.Image_Selected.sprite = selectedSprite;

        menuButton.Text.font = font;
        menuButton.Text.text = text;

        if (menuButton.GameObject_Selected.activeSelf)
        {
            menuButton.GameObject_Selected.SetActive(false);
        }
    }


    // [Create Panel]
    private void CreateCharacterInfoPanel()
    {
        GameObject prefab = LoadUtil.LoadPrefab(PathUtil.Sync.PanelPreFab.CharacterInfo);
        CharacterInfoList panelScript = Instantiate(prefab, this.transform).GetComponent<CharacterInfoList>();
        if(panelScript == null)
        {
            Debug.LogError($"{prefab.name} 프리팹에 CharacterInfoList 컴포넌트가 없습니다! 확인해주세요!!");
        }

        panelScript.gameObject.name = "CharacterInfo_Panel";
        m_characterInfoList = panelScript;
    }

    private void CreateCharacterSkillListPanel()
    {
        GameObject prefab = LoadUtil.LoadPrefab(PathUtil.Sync.PanelPreFab.CharacterSkillList);
        CharacterSkillList panelScript = Instantiate(prefab, this.transform).GetComponent< CharacterSkillList>();
        if(panelScript == null)
        {
            Debug.LogError($"{prefab.name} 프리팹에 CharacterSkillList 컴포넌트가 없습니다! 확인해주세요!!");
        }

        panelScript.gameObject.name = "CharacterSkillList_Panel";
        m_characterSkillList = panelScript;
    }


    // [Read Data And Create Icon]
    private void ReadCharacterListAndCreateIcon()
    {
        Dictionary<string, CharacterData> characterData = GameDataManager.Instance.CharacterDataList;
        foreach (KeyValuePair<string, CharacterData> dataKV in characterData)
        {
            CharacterData data = dataKV.Value;
            string id = dataKV.Key;
            if (id == null || data == null) continue;

            CreateIcon(data.Id);
        }
    }

    private void CreateIcon(string id)
    {
        GameObject iconInstance = Instantiate(Prefab_CharacterIcon, CharacterIconSlot);
        if (iconInstance == null) return;

        CharacterIcon characterIconScript = iconInstance.GetComponent<CharacterIcon>();
        if (characterIconScript == null) return;

        characterIconScript.SetData(id, OnClickChildButton);

        characterIconScript.SetAssetAsync().Forget();

        m_iconListDic.Add(id, characterIconScript);
    }


    // [Child Event]
    private void OnClickChildButton(string id)
    {
        if (m_characterInfoList == null || id == null) return;

        if (GameDataManager.Instance.CharacterDataList.TryGetValue(id, out CharacterData characterData))
        {
            m_characterInfoList.SetCharacterData(characterData);
        }
    }

}
