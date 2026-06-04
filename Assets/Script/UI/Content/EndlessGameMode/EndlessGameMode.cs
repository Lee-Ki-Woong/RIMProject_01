using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class EndlessGameMode : BaseUI
{
    [SerializeField] private Transform CharacterIconSlot;
    [SerializeField] private Transform SkillIconSlot;
    [SerializeField] private Image Image_Background;

    [SerializeField] private TMP_Text TMPText_ScoreTitle;
    [SerializeField] private TMP_Text TMPText_ScoreData;

    [Serializable]
    private class Menu
    {
        public Button Button_This;
        public Image Image_This;
        public TMP_Text TMPText_This;
    }

    [SerializeField] private Menu Menu_StartGame;
    [SerializeField] private Menu Menu_SelectCharacter;

    [SerializeField] private Button Button_Exit;
    [SerializeField] private Image Image_Exit;

    [Serializable]
    private class Party
    {
        public Button Button;
        public Image Image_CharacterIcon;
    }

    public List<Character> Characters { get; private set; } = new();
    public List<CharacterButton> CharacterButtons { get; private set; } = new();
    public List<SkillButton> SkillButtons { get; private set; } = new();

    public void SetAsset(Sprite background, Sprite selectCharacterButton, Sprite startGameButton, Sprite exitButton, TMP_FontAsset font)
    {
        if (IsSetAsset)
        {
            return;
        }

        Image_Background.sprite = background;

        Menu_StartGame.Image_This.sprite = selectCharacterButton;
        Menu_StartGame.TMPText_This.font = font;

        Menu_SelectCharacter.Image_This.sprite = startGameButton;
        Menu_SelectCharacter.TMPText_This.font = font;

        TMPText_ScoreData.font = font;
        TMPText_ScoreTitle.font = font;

        Image_Exit.sprite = exitButton;

        IsSetAsset = true;
    }

    public override void SetData(UIData uiData)
    {
        string[] texts = uiData.Texts;
        Action[] actions = uiData.Actions;

        InitData(Menu_SelectCharacter, texts[0], actions[0]);
        InitData(Menu_StartGame, texts[1], actions[1]);
        InitData(Button_Exit, actions[2]);
    }

    public void SetScore(PlayerModel playerModel)
    {
        TMPText_ScoreData.text = $"{playerModel.Score}";
    }

    private void InitData(Menu menu, string text, Action action)
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

        menu.TMPText_This.text = text;
        menu.Button_This.onClick.RemoveAllListeners();
        menu.Button_This.onClick.AddListener(action.Invoke);
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
        if (this.TryInstantiate(prefab, CharacterIconSlot, out GameObject buttonInstance) == false)
        {
            return;
        }

        if (buttonInstance.TryGetComponent(out CharacterButton characterButton) == false)
        {
            this.LogError("CharacterButton에 CharacterButton 컴포넌트가 없습니다!!");
            return;
        }
        CharacterButtons.Add(characterButton);
        characterButton.LoadAssetAsync(data).Forget();

        characterButton.SetEvent(data, action);
    }

    public void DestroyCharacterButtons()
    {
        for (int i = 0; i < CharacterButtons.Count; i++)
        {
            if (CharacterButtons[i] != null)
            {
                Destroy(CharacterButtons[i].gameObject);
            }
        }
        CharacterButtons.Clear();
    }

    public EndlessCharacterInfo CreateCharacterInfo(GameObject prefab)
    {
        if (this.TryInstantiate(prefab, this.transform, out GameObject instance) == false)
        {
            return null;
        }

        if (instance.TryGetComponent(out EndlessCharacterInfo info) == false)
        {
            this.LogError($"{instance.name} 프리팹에 EndlessCharacterInfo 컴포넌트가 없습니다!! 확인해주세요!!");
            return null;
        }

        return info;
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

        if (data == null)
        {
            this.LogError("스킬 데이터가 없습니다!!");
            return;
        }

        SkillButtons.Add(skillButton);
        skillButton.LoadAssetAsync(data).Forget();

        skillButton.SetEvent(data, action);
    }

    public void DestroyAllSkillButtons()
    {
        for (int i = 0; i < SkillButtons.Count; i++)
        {
            if (SkillButtons[i] != null)
            {
                Destroy(SkillButtons[i].gameObject);
            }
        }
        SkillButtons.Clear();
    }

}
