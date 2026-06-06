using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NewCharacterCollection : BaseUI
{
    // [캡슐화] 모든 내부 UI 요소와 Transform은 철저히 private으로 숨깁니다.
    [SerializeField] private Transform CharacterIconSlot;
    [SerializeField] private Transform SkillIconSlot;
    [SerializeField] private Image Image_Background;

    [SerializeField] private Button Button_Exit;
    [SerializeField] private Image Image_Exit;

    [SerializeField] private Image Image_CharacterStanding;

    [System.Serializable]
    private class MenuButton
    {
        public Button Button;
        public Image Image;
        public TMP_Text Text;
        public GameObject GameObject_Selected;
        public Image Image_Selected;
    }

    [SerializeField] private MenuButton CharacterInfo;
    [SerializeField] private MenuButton CharacterSkillList;

    private List<CharacterButton> m_characterButtons = new();
    private List<SkillButton> m_skillButtons = new();

    private CharacterCollectionInfo m_characterCollectionInfo;

    // =========================================================
    // Presenter가 구독할 이벤트 정의 (View -> Presenter 통신용)
    // =========================================================
    public event Action OnExitClicked;
    public event Action OnCharacterInfoTabClicked;
    public event Action OnCharacterSkillTabClicked;
    public event Action<string> OnCharacterSelected;
    public event Action<string> OnSkillSelected;

    private void Awake()
    {
        // 내부 UI 요소의 자체 클릭 이벤트를 View의 public 이벤트로 연결합니다.
        Button_Exit.onClick.AddListener(() => OnExitClicked?.Invoke());
        CharacterInfo.Button.onClick.AddListener(() => OnCharacterInfoTabClicked?.Invoke());
        CharacterSkillList.Button.onClick.AddListener(() => OnCharacterSkillTabClicked?.Invoke());
    }

    // =========================================================
    // UI 초기화 및 데이터 세팅 (Presenter -> View 명령)
    // =========================================================
    public void SetAsset(Sprite background, Sprite menuButton, Sprite menuButtonHighlighted, Sprite menuButtonSelected, Sprite exitButton, TMP_FontAsset font)
    {
        if (IsSetAsset) return;

        Image_Background.sprite = background;
        Image_Exit.sprite = exitButton;

        InitAsset(CharacterInfo, menuButton, menuButtonHighlighted, menuButtonSelected, font);
        InitAsset(CharacterSkillList, menuButton, menuButtonHighlighted, menuButtonSelected, font);

        CharacterInfo.Text.text = "캐릭터 정보";
        CharacterSkillList.Text.text = "캐릭터 스킬";

        IsSetAsset = true;
    }

    private void InitAsset(MenuButton menu, Sprite menuButton, Sprite menuButtonHighlighted, Sprite menuButtonSelected, TMP_FontAsset font)
    {
        menu.Button.SetButtonSprite(menuButtonHighlighted);
        menu.Image.sprite = menuButton;
        menu.Image_Selected.sprite = menuButtonSelected;
        menu.Text.font = font;
    }

    public void InitCharacterInfoPanel(GameObject prefab, TMP_FontAsset font)
    {
        if (this.TryInstantiate(prefab, this.transform, out GameObject instance))
        {
            if (instance.TryGetComponent(out CharacterCollectionInfo info))
            {
                m_characterCollectionInfo = info;
                m_characterCollectionInfo.SetAsset(font);
            }
            else
            {
                this.LogError($"{instance.name} 프리팹에 CharacterCollectionInfo 컴포넌트가 없습니다!!");
            }
        }
    }

    public override void SetData(UIData uiData)
    {
        
    }

    // =========================================================
    // 리스트 생성 및 갱신 로직
    // =========================================================
    public void PopulateCharacterList(List<CharacterData> characterDataList, GameObject prefab)
    {
        DestroyCharacterButtons();

        foreach (var data in characterDataList)
        {
            if (this.TryInstantiate(prefab, CharacterIconSlot, out GameObject buttonInstance))
            {
                if (buttonInstance.TryGetComponent(out CharacterButton characterButton))
                {
                    m_characterButtons.Add(characterButton);
                    characterButton.LoadAssetAsync(data).Forget();

                    // Action을 주입받는 대신, View의 이벤트를 발생시키도록 람다식을 할당합니다.
                    characterButton.SetEvent(data, (id) => OnCharacterSelected?.Invoke(id));
                }
            }
        }
    }

    public void PopulateSkillList(SkillData[] skillDataList, GameObject prefab)
    {
        DestroyAllSkillButtons();

        foreach (var data in skillDataList)
        {
            if (data == null) continue;

            if (this.TryInstantiate(prefab, SkillIconSlot, out GameObject buttonInstance))
            {
                if (buttonInstance.TryGetComponent(out SkillButton skillButton))
                {
                    m_skillButtons.Add(skillButton);
                    skillButton.LoadAssetAsync(data).Forget();

                    skillButton.SetEvent(data, (id) => OnSkillSelected?.Invoke(id));
                }
            }
        }
    }

    public void SetCharacterStandingAsset(Sprite sprite)
    {
        Image_CharacterStanding.sprite = sprite;
    }

    public void UpdateCharacterInfoData(CharacterData data)
    {
        if (m_characterCollectionInfo != null)
        {
            m_characterCollectionInfo.SetData(data);
        }
    }

    // =========================================================
    // 탭 전환 상태 관리 (Presenter가 Transform을 직접 조작하지 못하게 함)
    // =========================================================
    public void SwitchToCharacterInfoTab()
    {
        CharacterSkillList.GameObject_Selected.SetActive(false);
        CharacterInfo.GameObject_Selected.SetActive(true);

        SkillIconSlot.gameObject.SetActive(false);
        if (m_characterCollectionInfo != null) m_characterCollectionInfo.ActiveTrue();
    }

    public void SwitchToSkillListTab()
    {
        CharacterInfo.GameObject_Selected.SetActive(false);
        CharacterSkillList.GameObject_Selected.SetActive(true);

        if (m_characterCollectionInfo != null) m_characterCollectionInfo.ActiveFalse();
        SkillIconSlot.gameObject.SetActive(true);
    }

    private void DestroyCharacterButtons()
    {
        foreach (var btn in m_characterButtons)
        {
            if (btn != null) Destroy(btn.gameObject);
        }
        m_characterButtons.Clear();
    }

    private void DestroyAllSkillButtons()
    {
        foreach (var btn in m_skillButtons)
        {
            if (btn != null) Destroy(btn.gameObject);
        }
        m_skillButtons.Clear();
    }
}