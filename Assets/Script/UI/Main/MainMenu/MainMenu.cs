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
        public Button Button;
        public Image Image;
        public TMP_Text Text;
    }

    [SerializeField] private MainMenuButton FirstMenuButton;
    [SerializeField] private MainMenuButton SecondMenuButton;
    [SerializeField] private MainMenuButton ThirdMenuButton;
    [SerializeField] private MainMenuButton FourthMenuButton;
    [SerializeField] private MainMenuButton FifthMenuButton;

    private MainMenuButton[] Menus;

    private void Awake()
    {
        Menus = new MainMenuButton[5]
        {
            FirstMenuButton,
            SecondMenuButton,
            ThirdMenuButton,
            FourthMenuButton,
            FifthMenuButton
        };
    }

    public void SetAsset(Sprite titleText, Sprite titleImage, Sprite menuButton, Sprite menuButtonHighlighted, TMP_FontAsset menuButtonFont)
    {
        Image_TitleText.sprite = titleText;
        Image_TitleImage.sprite = titleImage;

        foreach (MainMenuButton menu in Menus)
        {
            menu.Image.sprite = menuButton;
            menu.Button.SetButtonSprite(menuButtonHighlighted);
            menu.Text.font = menuButtonFont;
        }
    }

    public void SetData(string[] texts, Action[] actions)
    {

        if (texts.Length != actions.Length)
        {
            Debug.LogError($"{this.gameObject} : 전달받은 texts와 Actions의 Length 값이 동일하지 않습니다!!");
            return;
        }

        for (int i = 0; i < Math.Min(texts.Length, Menus.Length); i++)
        {
            if (string.IsNullOrEmpty(texts[i]) || actions[i] == null)
            {
                Menus[i].Button.gameObject.SetActive(false);
                continue;
            }

            if (Menus[i].Button.gameObject.activeSelf == false)
            {
                Menus[i].Button.gameObject.SetActive(true);
            }

            Menus[i].Text.text = texts[i];
            Menus[i].Button.onClick.RemoveAllListeners();
            Menus[i].Button.onClick.AddListener(actions[i].Invoke);
        }
    }
}