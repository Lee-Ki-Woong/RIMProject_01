using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FirstPopup : BaseUI
{ 
    [SerializeField] private Button Button_Resume;
    [SerializeField] private Image Image_ResumeButton;
    [SerializeField] private TMP_Text Text_ResumeButton;

    [SerializeField] private TMP_Text Text_Tutorial;
    
    [SerializeField] private Image Image_Background;

    public event Action OnResumeButton;
    public event Action OnUIExit;

    private void OnEnable()
    {
        Time.timeScale = 0;
        BindButtonEvent();
    }

    private void BindButtonEvent()
    {
        Button_Resume.onClick.AddListener(InvokeResumeButton);
    }

    private void OnDisable()
    {
        Time.timeScale = 1;
        UnBindButtonEvent();
        InvokeUIExit();
    }

    private void UnBindButtonEvent()
    {
        Button_Resume.onClick.RemoveAllListeners();
    }

    public void SetData(string tutorialText, string resumeText)
    {
        Text_Tutorial.text = tutorialText;
        Text_ResumeButton.text = resumeText;
    }

    public void SetAsset(Sprite sprite_background, Sprite sprite_resumeButton, TMP_FontAsset font)
    {
        Image_Background.sprite = sprite_background;
        Image_ResumeButton.sprite = sprite_resumeButton;
        Text_Tutorial.font = font;
        Text_ResumeButton.font = font;
    }

    private void InvokeResumeButton()
    {
        OnResumeButton?.Invoke();
    }

    private void InvokeUIExit()
    {
        OnUIExit?.Invoke();
    }
}