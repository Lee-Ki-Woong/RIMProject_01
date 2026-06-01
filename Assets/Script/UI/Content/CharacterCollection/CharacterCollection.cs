using Cysharp.Threading.Tasks;
using NUnit.Framework.Internal;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CharacterCollection : BaseUI
{
    [SerializeField] private Transform CharacterIconSlot;
    [SerializeField] private Image Image_Background;

    [SerializeField] private Button Button_Exit;
    [SerializeField] private Image Image_Exit;

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

        if (texts.Length != actions.Length)
        {
            Debug.LogError($"{this.gameObject} : 전달받은 texts와 Actions의 Length 값이 동일하지 않습니다!!");
            return;
        }

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

    public void CreateButtons(GameObject prefab, CharacterData data, Action<string> action)
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

        characterButton.LoadAssetAsync(data).Forget();

        string id = data.Id;

        characterButton.SetEvent(id, action);
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

    public CharacterCollectionSkillList CreateCharacterSkillList(GameObject prefab)
    {
        if (this.TryInstantiate(prefab, this.transform, out GameObject instance) == false)
        {
            return null;
        }

        if (instance.TryGetComponent(out CharacterCollectionSkillList skillList) == false)
        {
            this.LogError($"{instance.name} 프리팹에 CharacterCollectionSkillList 컴포넌트가 없습니다!! 확인해주세요!!");
            return null;
        }

        return skillList;
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
