using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;

public class LanguagePopup : BaseUI
{
    [SerializeField] private Image Image_Background;

    [Serializable]
    private class MenuButton
    {
        public Button Button_This;
        public Image Image_Button;
        public TMP_Text Text_Button;
    }

    [SerializeField] private MenuButton Button_Korean;
    [SerializeField] private MenuButton Button_English;
    [SerializeField] private MenuButton Button_Exit;

    public event Action OnChangeLanguageKoreanButton;
    public event Action OnChangeLanguageEnglishButton;
    public event Action OnExitButton;

    public event Action OnUIExit;

    private void OnEnable()
    {
        BindButtonEvents();
    }

    private void BindButtonEvents()
    {
        Button_Korean.Button_This.onClick.AddListener(InvokeChangeLanguageKorean);
        Button_English.Button_This.onClick.AddListener(InvokeChangeLanguageEnglish);
        Button_Exit.Button_This.onClick.AddListener(InvokeExit);
    }

    private void OnDisable()
    {
        UnBindButtonEvents();
        InvokeUIExit();
    }

    private void UnBindButtonEvents()
    {
        Button_Korean.Button_This.onClick.RemoveAllListeners();
        Button_English.Button_This.onClick.RemoveAllListeners();
        Button_Exit.Button_This.onClick.RemoveAllListeners();
    }

    public void SetAsset(Sprite sprite_background, Sprite sprite_menuButtons, TMP_FontAsset fontAsset_baseFont)
    {
        Image_Background.sprite = sprite_background;

        Button_Korean.Image_Button.sprite = sprite_menuButtons;
        Button_Korean.Text_Button.font = fontAsset_baseFont;

        Button_English.Image_Button.sprite = sprite_menuButtons;
        Button_English.Text_Button.font = fontAsset_baseFont;
        
        Button_Exit.Image_Button.sprite = sprite_menuButtons;
        Button_Exit.Text_Button.font = fontAsset_baseFont;
    }

    public void SetData(string koreanButtonText, string englishButtonText, string exitButtonText)
    {
        Button_Korean.Text_Button.text = koreanButtonText;
        Button_English.Text_Button.text = englishButtonText;
        Button_Exit.Text_Button.text = exitButtonText;
    }

    private void InvokeChangeLanguageKorean()
    {
        OnChangeLanguageKoreanButton?.Invoke();
    }

    private void InvokeChangeLanguageEnglish()
    {
        OnChangeLanguageEnglishButton?.Invoke();
    }

    private void InvokeExit()
    {
        OnExitButton?.Invoke();
    }

    private void InvokeUIExit()
    { 
        OnUIExit?.Invoke();
    }
}
