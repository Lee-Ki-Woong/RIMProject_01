using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CharacterCollection : BaseUI
{
    [SerializeField] private Transform CharacterIconSlot;
    [field: SerializeField] public Transform SkillIconSlot { get; private set; }
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

    public List<CharacterButton> m_characterButtons { get; private set; } = new();
    private List<SkillButton> m_skillButtons = new();

    public void SetAsset(Sprite background, Sprite menuButton, Sprite menuButtonHighlighted, Sprite menuButtonSelected, Sprite exitButton, TMP_FontAsset font)
    {
        if (IsSetAsset)
        {
            return;
        }

        Image_Background.sprite = background;
        Image_Exit.sprite = exitButton;

        InitAsset(CharacterInfo, menuButton, menuButtonHighlighted, menuButtonSelected, font);
        InitAsset(CharacterSkillList, menuButton, menuButtonHighlighted, menuButtonSelected, font);

        IsSetAsset = true;
    }

    public void SetCharacterStandingAsset(Sprite sprite)
    {
        Image_CharacterStanding.sprite = sprite;
    }

    private void InitAsset(MenuButton menu, Sprite menuButton, Sprite menuButtonHighlighted, Sprite menuButtonSelected, TMP_FontAsset font)
    {
        menu.Button.SetButtonSprite(menuButtonHighlighted);
        menu.Image.sprite = menuButton;
        menu.Image_Selected.sprite = menuButtonSelected;
        menu.Text.font = font;
    }

    public override void SetData(UIData uiData)
    {
        string[] texts = uiData.Texts;
        Action[] actions = uiData.Actions;

        InitData(CharacterInfo, texts[0], actions[0]);
        InitData(CharacterSkillList, texts[1], actions[1]);
        InitData(Button_Exit, actions[2]);
    }

    private void InitData(MenuButton menu, string text, Action action)
    {
        if (string.IsNullOrEmpty(text) || action == null)
        {
            if (string.IsNullOrEmpty(text))
            {
                Debug.LogWarning($"{this.gameObject} : text값이 null이거나 Empty입니다!!");
            }

            if (action == null)
            {
                Debug.LogWarning($"{this.gameObject} : action의 값이 null이거나 Empty입니다!!");
            }

            return;
        }

        menu.Text.text = text;
        menu.Button.onClick.RemoveAllListeners();
        menu.Button.onClick.AddListener(action.Invoke);
    }

    private void InitData(Button button, Action action)
    {
        if (action == null)
        {
            Debug.LogWarning($"{this.gameObject} : action의 값이 null이거나 Empty입니다!!");
            return;
        }

        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(action.Invoke);
    }

    public void CreateCharacterButtons(GameObject prefab, CharacterData data, Action<string> action)
    {
        if(this.TryInstantiate(prefab, CharacterIconSlot, out GameObject buttonInstance) == false)
        {
            return;
        }

        if(buttonInstance.TryGetComponent(out CharacterButton characterButton) == false)
        {
            this.LogError("CharacterButton에 CharacterButton 컴포넌트가 없습니다!!");
            return;
        }
        m_characterButtons.Add(characterButton);
        characterButton.LoadAssetAsync(data).Forget();


        characterButton.SetEvent(data, action);
    }

    public void DestroyCharacterButtons()
    {
        for (int i = 0; i < m_characterButtons.Count; i++)
        {
            if (m_characterButtons[i] != null)
            {
                Destroy(m_characterButtons[i].gameObject);
            }
        }
        m_characterButtons.Clear();
    }

    public void CreateSkillButtons(GameObject prefab, SkillData data, Action<string> action)
    {
        if (this.TryInstantiate(prefab, SkillIconSlot, out GameObject buttonInstance) == false)
        {
            return;
        }

        if (buttonInstance.TryGetComponent(out SkillButton skillButton) == false)
        {
            this.LogError("SkillButton에 SkillButton 컴포넌트가 없습니다!!");
            return;
        }

        if(data == null)
        {
            this.LogError("스킬 데이터가 없습니다!!");
            return;
        }

        m_skillButtons.Add(skillButton);
        skillButton.LoadAssetAsync(data).Forget();

        skillButton.SetEvent(data, action);
    }

    public void DestroyAllSkillButtons()
    {
        for (int i = 0; i < m_skillButtons.Count; i++)
        {
            if (m_skillButtons[i] != null)
            {
                Destroy(m_skillButtons[i].gameObject);
            }
        }
        m_skillButtons.Clear();
    }

    public CharacterCollectionInfo CreateCharacterInfo(GameObject prefab)
    {
        if (this.TryInstantiate(prefab, this.transform, out GameObject instance) == false)
        {
            return null;
        }

        if (instance.TryGetComponent(out CharacterCollectionInfo info) == false)
        {
            this.LogError($"{instance.name} 프리팹에 CharacterCollectionInfo 컴포넌트가 없습니다!! 확인해주세요!!");
            return null;
        }

        return info;
    }

    public void SelectedCharacterInfo()
    {
        CharacterSkillList.GameObject_Selected.SetActive(false);
        CharacterInfo.GameObject_Selected.SetActive(true);
    }

    public void SelectedCharacterSkillList()
    {
        CharacterInfo.GameObject_Selected.SetActive(false);
        CharacterSkillList.GameObject_Selected.SetActive(true);
    }
}
