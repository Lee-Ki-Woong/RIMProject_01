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
    [SerializeField] private Transform CharacterSlot;
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

    [SerializeField] private Image Image_CharacterStanding;

    [SerializeField] private Menu Menu_StartGame;
    [SerializeField] private Menu Menu_SelectCharacter;

    [SerializeField] private Button Button_Exit;
    [SerializeField] private Image Image_Exit;

    public List<CharacterButton> CharacterButtons { get; private set; } = new();
    public List<SkillButton> SkillButtons { get; private set; } = new();
    public Dictionary<CharacterData, PartySlotButton> PartySlotButtons { get; private set; } = new();

    public void SetAsset(Sprite background, Sprite selectCharacterButton, Sprite startGameButton, Sprite exitButton, TMP_FontAsset font)
    {
        if (IsSetAsset)
        {
            return;
        }

        Image_Background.sprite = background;

        Menu_StartGame.Image_This.sprite = startGameButton;
        Menu_StartGame.TMPText_This.font = font;

        Menu_SelectCharacter.Image_This.sprite = selectCharacterButton;
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

        InitData(Menu_StartGame, texts[0], actions[0]);
        InitData(Button_Exit, actions[1]);
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

    public void SetAssetCharacterStanding(Sprite sprite)
    {
        Image_CharacterStanding.sprite = sprite;
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

    public void CreatePartySlot(GameObject prefab, CharacterData characterData, Action<string> action)
    {
        if (PartySlotButtons.ContainsKey(characterData))
        {
            this.LogError($"{characterData.Name}은 이미 파티에 있습니다!!");
            return;
        }

        if (this.TryInstantiate(prefab, CharacterSlot, out GameObject instance) == false)
        {
            return;
        }

        if (instance.TryGetComponent(out PartySlotButton partySlotButton) == false)
        {
            this.LogError("PartySlotButton에 PartySlotButton 컴포넌트가 없습니다!!");
            return;
        }

        PartySlotButtons.Add(characterData, partySlotButton);
        partySlotButton.SetData(characterData);
        partySlotButton.LoadAssetAsync(characterData).Forget();
        partySlotButton.SetEvent(action);
    }

    public void DestroyPartySlot(CharacterData characterData)
    {
        if (PartySlotButtons.TryGetValue(characterData, out PartySlotButton partySlotButton) == false)
        {
            this.LogError($"{characterData.Name}과 매칭되는 PartySlot이 없습니다!!");
            return;
        }

        PartySlotButtons.Remove(characterData);
        Destroy(partySlotButton.gameObject);
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

    public void BindSelectCharacterButtonEvent(CharacterData characterData, Action<string> action)
    {
        Menu_SelectCharacter.Button_This.onClick.RemoveAllListeners();
        Menu_SelectCharacter.Button_This.onClick.AddListener(() => action?.Invoke(characterData.Id));
    }

    //public Transform SetCharacterSlotTransform()
    //{
    //    if (CharacterSlot[0] == null)
    //    {
    //        return CharacterSlot[0];
    //    }
    //    else if(CharacterSlot[1] == null)
    //    {
    //        return CharacterSlot[1];
    //    }
    //    else if (CharacterSlot[2] == null)
    //    {
    //        return CharacterSlot[2];
    //    }
    //    else if (CharacterSlot[3] == null)
    //    {
    //        return CharacterSlot[3];
    //    }
    //    else if (CharacterSlot[4] == null)
    //    {
    //        return CharacterSlot[4];
    //    }
    //    else
    //    {
    //        this.LogError("캐릭터 슬롯이 전부 꽉 찬 상태입니다!!");
    //        return null;
    //    }
    //}
}
