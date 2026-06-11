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

    public event Action OnFirstMenuClicked;
    public event Action OnSecondMenuClicked;
    public event Action OnThirdMenuClicked;
    public event Action OnFourthMenuClicked;
    public event Action OnFifthMenuClicked;

    public event Action OnUIExit;

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

    private void OnEnable()
    {
        BindButtonEvent();
    }

    private void BindButtonEvent()
    {
        FirstMenuButton.Button.onClick.AddListener(InvokeFirstButtonClicked);
        SecondMenuButton.Button.onClick.AddListener(InvokeSecondButtonClicked);
        ThirdMenuButton.Button.onClick.AddListener(InvokeThirdButtonClicked);
        FourthMenuButton.Button.onClick.AddListener(InvokeFourthButtonClicked);
        FifthMenuButton.Button.onClick.AddListener(InvokeFifthButtonCliecked);
    }

    private void OnDisable()
    {
        UnBindButtonEvent();
        InvokeUIExit();
    }

    private void UnBindButtonEvent()
    {

        FirstMenuButton.Button.onClick.RemoveAllListeners();
        SecondMenuButton.Button.onClick.RemoveAllListeners();
        ThirdMenuButton.Button.onClick.RemoveAllListeners();
        FourthMenuButton.Button.onClick.RemoveAllListeners();
        FifthMenuButton.Button.onClick.RemoveAllListeners();
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

    public void SetData(string[] texts)
    {

        if (texts.Length != 5)
        {
            Debug.LogError($"{this.gameObject} : 전달받은 Text의 값이 5개가 아닙니다!!");
            return;
        }

        for (int i = 0; i < 5; i++)
        {
            if (string.IsNullOrEmpty(texts[i]))
            {
                Menus[i].Button.ActiveFalse();
                continue;
            }
            else
            {
                Menus[i].Button.ActiveTrue();
            }

            Menus[i].Text.text = texts[i];
        }
    }

    private void InvokeFirstButtonClicked()
    {
        OnFirstMenuClicked?.Invoke();
    }

    private void InvokeSecondButtonClicked()
    {
        OnSecondMenuClicked?.Invoke();
    }

    private void InvokeThirdButtonClicked()
    {
        OnThirdMenuClicked?.Invoke();
    }

    private void InvokeFourthButtonClicked()
    {
        OnFourthMenuClicked?.Invoke();
    }

    private void InvokeFifthButtonCliecked()
    {
        OnFifthMenuClicked?.Invoke();
    }

    private void InvokeUIExit()
    {
        OnUIExit?.Invoke();
    }
}