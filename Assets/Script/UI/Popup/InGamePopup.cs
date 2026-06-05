using Cysharp.Threading.Tasks;
using System;
using TMPro;
using TMPro.EditorUtilities;
using UnityEngine;
using UnityEngine.UI;

public class InGamePopup : BaseUI
{
    [Serializable]
    private class Buttons
    {
        public GameObject GameObject;
        public Button Button;
        public Image Image;
        public TMP_Text Text;
    }

    [SerializeField] private Buttons[] Menus;
    [SerializeField] private Image Image_Background;


    private void Awake()
    {

    }

    public void OpenInGamePopup()
    {
        string[] texts = { "계속하기", "처음부터", "게임 옵션", "메인메뉴로" };
        Action[] actions = { OnClick_ResumeButton, null, null, OnClick_ReturnMainButton };

        UIData inGamePopupData = new()
        {
            Texts = texts,
            Actions = actions
        };

        SetData(inGamePopupData);
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

        for (int i = 0; i < Math.Min(texts.Length, Menus.Length); i++)
        {
            if (string.IsNullOrEmpty(texts[i]) || actions[i] == null)
            {
                if (string.IsNullOrEmpty(texts[i]))
                {
                    Debug.LogWarning($"{this.gameObject} : texts {i}의 값이 null이거나 Empty입니다!!");
                }

                if (actions[i] == null)
                {
                    Debug.LogWarning($"{this.gameObject} : action {i}의 값이 null이거나 Empty입니다!!");
                }

                Menus[i].GameObject.SetActive(false);
                continue;
            }

            if (Menus[i].GameObject.activeSelf == false)
            {
                Menus[i].GameObject.SetActive(true);
            }

            Menus[i].Text.text = texts[i];
            Menus[i].Button.onClick.RemoveAllListeners();
            Menus[i].Button.onClick.AddListener(actions[i].Invoke);
        }
    }


    public async UniTask LoadAssetAsync()
    {
        var (sprite_Background, sprite_Button, font) = await UniTask.WhenAll(
            LoadUtil.Async.LoadSpriteAsync(AddressUtil.Async.Sprite.UI.InGamePopup.Background),
            LoadUtil.Async.LoadSpriteAsync(AddressUtil.Async.Sprite.UI.InGamePopup.Button),
            LoadUtil.Async.LoadFontAssetAsync(AddressUtil.Async.Font.BaseFont)
            );

        Image_Background.sprite = sprite_Background;

        for(int i = 0; i < Menus.Length; i++)
        {
            Menus[i].Image.sprite = sprite_Button;
            Menus[i].Text.font = font;
        }
    }

    private void OnClick_ResumeButton()
    {
        UIManager.Instance.CloseUI(UIType.InGamePopup);
    }

    private void OnClick_RestartButton()
    {

    }

    private void OnClick_SettingButton()
    {

    }

    private void OnClick_ReturnMainButton()
    {
        UIManager.Instance.CloseUI(UIType.InGame);
        UIManager.Instance.CloseUI(UIType.InGamePopup);
        UIManager.Instance.OpenMainMenu().Forget();
        GameManager.Instance.Save();
    }

}
