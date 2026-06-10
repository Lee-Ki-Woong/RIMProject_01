using Cysharp.Threading.Tasks;
using TMPro;
using UnityEngine;

public class LanguagePopupPresenter : BasePresenterTwo
{
    private LanguagePopup m_lnaguagePopupUI;


    private Sprite m_sprite_background;
    private Sprite m_sprite_menuButtons;

    private TMP_FontAsset m_fontAsset_baseFont;

    private string m_koreanButtonText;
    private string m_englishButtonText;
    private string m_exitButtonText;

    public void InitLanguagePopup(LanguagePopup languagePopup)
    {
        if (m_lnaguagePopupUI == null)
        {
            m_lnaguagePopupUI = languagePopup;
        }

        if(m_isAssetLoad == false)
        {
            m_lnaguagePopupUI.ActiveFalse();
            LoadAndSetAssetAsync().Forget();
        }
        else
        {
            m_lnaguagePopupUI.ActiveTrue();
        }

        SubscribeEvents();
        SubscribeLanguageEvent();
        LoadUIData();
    }

    protected override void SubscribeEvents()
    {
        m_lnaguagePopupUI.OnChangeLanguageKorean += OnClick_ChangeLanguageKoreanButton;
        m_lnaguagePopupUI.OnChangeLanguageEnglish += OnClick_ChangeLanguageEnglishButton;
        m_lnaguagePopupUI.OnExit += OnClick_ExitButton;
    }

    private void SubscribeLanguageEvent()
    {
        GameManager.Instance.OnLanguageChanged += On_ChangeLanguage;
    }

    protected override void UnsubscribeEvents()
    {
        m_lnaguagePopupUI.OnChangeLanguageKorean -= OnClick_ChangeLanguageKoreanButton;
        m_lnaguagePopupUI.OnChangeLanguageEnglish -= OnClick_ChangeLanguageEnglishButton;
        m_lnaguagePopupUI.OnExit -= OnClick_ExitButton;
    }

    private void UnsubscribeLanguageEvent()
    {
        GameManager.Instance.OnLanguageChanged -= On_ChangeLanguage;
    }

    protected async override UniTask LoadAndSetAssetAsync()
    {
        var (sprite_background, sprite_menuButtons, fontAsset_baseFont) = await UniTask.WhenAll
            (
            LoadUtil.Async.LoadSpriteAsync(AddressUtil.Async.Sprite.UI.LanguagePopup.Background),
            LoadUtil.Async.LoadSpriteAsync(AddressUtil.Async.Sprite.UI.LanguagePopup.MenuButtons),
            LoadUtil.Async.LoadFontAssetAsync(AddressUtil.Async.Font.BaseFont)
            );

        m_sprite_background = sprite_background;
        m_sprite_menuButtons = sprite_menuButtons;
        m_fontAsset_baseFont = fontAsset_baseFont;

        m_lnaguagePopupUI.SetAsset(m_sprite_background, m_sprite_menuButtons, m_fontAsset_baseFont);

        m_isAssetLoad = true;
        m_lnaguagePopupUI.ActiveTrue();
    }

    protected void LoadUIData()
    {
        UIDataManager.Instance.LanguagePopupList.TryGetValue(DataKeyUtil.UI.LanguagePopup, out LanguagePopupData languagePopupData);

        m_koreanButtonText = languagePopupData.KoreanButton;
        m_englishButtonText = languagePopupData.EnglishButton;
        m_exitButtonText = languagePopupData.ExitButton;

        m_lnaguagePopupUI.SetData(m_koreanButtonText, m_englishButtonText, m_exitButtonText);
    }

    private void OnClick_ChangeLanguageKoreanButton()
    {
        GameManager.Instance.ChangeLanguage(Language.Korean);
    }

    private void OnClick_ChangeLanguageEnglishButton()
    {
        GameManager.Instance.ChangeLanguage(Language.English);
    }

    private void OnClick_ExitButton()
    {
        UnsubscribeEvents();
        UnsubscribeLanguageEvent();
        UIManager.Instance.CloseUI(UIType.LanguagePopup);
    }

    private void On_ChangeLanguage()
    {
        LoadUIData();
    }
}
