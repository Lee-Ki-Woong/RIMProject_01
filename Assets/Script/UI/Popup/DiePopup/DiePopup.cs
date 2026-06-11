using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DiePopup : BaseUI
{
    [SerializeField] private Image Image_BackgroundImage;

    [SerializeField] private Button Button_ExitButton;
    [SerializeField] private Image Image_ExitButtonImage;

    [SerializeField] private TMP_Text Text_GameOverText;
    [SerializeField] private TMP_Text Text_ScoreTitleText;

    [SerializeField] private TMP_Text Text_ExitButtonText;
    [SerializeField] private TMP_Text Text_ScoreText;

    public event Action OnExitButton;

    public event Action OnUIExit;

    private void OnEnable()
    {
        Time.timeScale = 0;
        BindButtonEvent();
    }

    private void BindButtonEvent()
    {
        Button_ExitButton.onClick.AddListener(InvokeOnExitButton);
    }

    private void OnDisable()
    {
        Time.timeScale = 1;
        UnBindButtonEvent();
        InvokeOnExit();
    }

    private void UnBindButtonEvent()
    {
        Button_ExitButton.onClick.RemoveAllListeners();
    }

    public void SetAsset(Sprite sprite_background, Sprite sprite_exitButton, TMP_FontAsset font_basefont)
    {
        Image_BackgroundImage.sprite = sprite_background;
        Image_ExitButtonImage.sprite = sprite_exitButton;
        Text_ScoreTitleText.font = font_basefont;
        Text_GameOverText.font = font_basefont;
        Text_ExitButtonText.font = font_basefont;
        Text_ScoreText.font = font_basefont;
    }

    public void SetData(string gameOverText,string scoreTitle , string exitButtonText)
    {
        Text_GameOverText.text = gameOverText;
        Text_ScoreTitleText.text = scoreTitle;
        Text_ExitButtonText.text = exitButtonText;
    }

    private void InvokeOnExitButton()
    {
        OnExitButton?.Invoke();
    }

    private void InvokeOnExit()
    {
        OnUIExit?.Invoke();
    }

}
