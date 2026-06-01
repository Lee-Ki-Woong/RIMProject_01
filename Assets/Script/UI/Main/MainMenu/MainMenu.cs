using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : BaseUI
{
    [SerializeField] private Image Image_TitleText;
    [SerializeField] private Image Image_TitleImage;

    [System.Serializable]
    private class MainMenuButton
    {
        public GameObject GameObject;
        public Button Button;
        public Image Image;
        public TMP_Text Text;
    }

    [SerializeField] private MainMenuButton[] Menus;

    public void SetAsset(Sprite titleText, Sprite titleImage, Sprite menuButton, Sprite menuButtonHighlighted, TMP_FontAsset menuButtonFont)
    {
        if(IsSetAsset)
        {
            return;
        }

        Image_TitleText.sprite = titleText;
        Image_TitleImage.sprite = titleImage;

        foreach (MainMenuButton menu in Menus)
        {
            menu.Image.sprite = menuButton;
            menu.Button.SetButtonSprite(menuButtonHighlighted);
            menu.Text.font = menuButtonFont;
        }

        IsSetAsset = true;
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
}