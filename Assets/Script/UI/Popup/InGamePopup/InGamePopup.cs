using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InGamePopup : BaseUI
{
    [SerializeField] private Image Image_Background;

    [Serializable]
    private class MenuButton
    {
        public Button Button;
        public Image Image;
        public TMP_Text Text;
    }

    [SerializeField] private MenuButton ResumeButton;
    [SerializeField] private MenuButton RestartButton;
    [SerializeField] private MenuButton GameOptionButton;
    [SerializeField] private MenuButton MainMenuButton;

    private MenuButton[] menuButtons;

    public event Action OnResumeGame;
    public event Action OnRestartGame;
    public event Action OnOpenGameOption;
    public event Action OnReturnMainMenu;

    public event Action OnUIExit;

    private void Awake()
    {
        menuButtons = new MenuButton[4]
        {
            ResumeButton,
            RestartButton,
            GameOptionButton,
            MainMenuButton
        };
    }

    private void OnEnable()
    {
        Time.timeScale = 0;
        InvokeUIExit();
        BindButtonEvent();
    }

    private void BindButtonEvent()
    {
        ResumeButton.Button.onClick.AddListener(InvokeOnResumeButton);
        RestartButton.Button.onClick.AddListener(InvokeOnRestartButton);
        GameOptionButton.Button.onClick.AddListener(InvokeOnGameOptionButton);
        MainMenuButton.Button.onClick.AddListener(InvokeOnMainMenuButton);
    }

    private void OnDisable()
    {
        Time.timeScale = 1;
        UnBindButtonEvent();
        InvokeUIExit();
    }

    private void UnBindButtonEvent()
    {
        ResumeButton.Button.onClick.RemoveAllListeners();
        RestartButton.Button.onClick.RemoveAllListeners();
        GameOptionButton.Button.onClick.RemoveAllListeners();
        MainMenuButton.Button.onClick.RemoveAllListeners();
    }

    public void SetAsset(Sprite sprite_background, Sprite sprite_menuButtons, TMP_FontAsset fontAsset_baseFont)
    {
        Image_Background.sprite = sprite_background;

        foreach(MenuButton menuButton in menuButtons)
        {
            menuButton.Image.sprite = sprite_menuButtons;
            menuButton.Text.font = fontAsset_baseFont;
        }
    }

    public void SetData(string resumeButtonText, string restartButtonText, string gameOptionButtonText, string mainMenuButtonText)
    {
        ResumeButton.Text.text = resumeButtonText;
        RestartButton.Text.text = restartButtonText;
        GameOptionButton.Text.text = gameOptionButtonText;
        MainMenuButton.Text.text = mainMenuButtonText;
    }

    private void InvokeOnResumeButton()
    {
        OnResumeGame?.Invoke();
    }

    private void InvokeOnRestartButton()
    {
        OnRestartGame?.Invoke();
    }

    private void InvokeOnGameOptionButton()
    {
        OnOpenGameOption?.Invoke();
    }

    private void InvokeOnMainMenuButton()
    {
        OnReturnMainMenu?.Invoke();
    }

    private void InvokeUIExit()
    {
        OnUIExit?.Invoke();
    }
}
