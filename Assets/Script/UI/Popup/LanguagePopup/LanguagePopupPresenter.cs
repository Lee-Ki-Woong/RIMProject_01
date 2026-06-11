using Cysharp.Threading.Tasks;
using TMPro;
using UnityEngine;

public class LanguagePopupPresenter : BasePresenter
{
    private LanguagePopup m_languagePopupUI;


    private Sprite m_sprite_background;
    private Sprite m_sprite_menuButtons;

    private TMP_FontAsset m_fontAsset_baseFont;

    private string m_koreanButtonText;
    private string m_englishButtonText;
    private string m_exitButtonText;

    public override UIType UIType_This { get; } = UIType.LanguagePopup;

    public void InitLanguagePopup(LanguagePopup languagePopup)
    {
        if (m_languagePopupUI == null)
        {
            m_languagePopupUI = languagePopup;
        }

        if(m_isAssetLoad == false)
        {
            m_languagePopupUI.ActiveFalse();
            LoadAssetAsync().Forget();
        }
        else
        {
            m_languagePopupUI.ActiveTrue();
        }

        SubscribeEvents();
        SubscribeLanguageEvent();
        LoadData();
    }

    protected override void SubscribeEvents()
    {
        m_languagePopupUI.OnChangeLanguageKoreanButton += OnClick_ChangeLanguageKoreanButton;
        m_languagePopupUI.OnChangeLanguageEnglishButton += OnClick_ChangeLanguageEnglishButton;
        m_languagePopupUI.OnExitButton += OnClick_ExitButton;

        m_languagePopupUI.OnUIExit += On_UIExit;
    }

    private void SubscribeLanguageEvent()
    {
        GameManager.Instance.OnLanguageChanged += On_ChangeLanguage;
    }

    protected override void UnsubscribeEvents()
    {
        m_languagePopupUI.OnChangeLanguageKoreanButton -= OnClick_ChangeLanguageKoreanButton;
        m_languagePopupUI.OnChangeLanguageEnglishButton -= OnClick_ChangeLanguageEnglishButton;
        m_languagePopupUI.OnExitButton -= OnClick_ExitButton;

        m_languagePopupUI.OnUIExit -= On_UIExit;
    }

    private void UnsubscribeLanguageEvent()
    {
        GameManager.Instance.OnLanguageChanged -= On_ChangeLanguage;
    }

    protected async override UniTask LoadAssetAsync()
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

        m_languagePopupUI.SetAsset(m_sprite_background, m_sprite_menuButtons, m_fontAsset_baseFont);

        m_isAssetLoad = true;
        m_languagePopupUI.ActiveTrue();
    }

    protected override void LoadData()
    {
        string dataId = DataUtil.DataKey.UI.LanguagePopup;

        if (UIDataManager.Instance.LanguagePopupDataList.TryGetValue(dataId, out LanguagePopupData languagePopupData) == false)
        {
            LoadLogError(dataId);
            return;
        }

        m_koreanButtonText = languagePopupData.KoreanButton;
        m_englishButtonText = languagePopupData.EnglishButton;
        m_exitButtonText = languagePopupData.ExitButton;

        m_languagePopupUI.SetData(m_koreanButtonText, m_englishButtonText, m_exitButtonText);
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
        UIManager.Instance.CloseUI(UIType.LanguagePopup);
    }

    private void On_ChangeLanguage()
    {
        LoadData();
    }

    private void On_UIExit()
    {
        UnsubscribeEvents();
        UnsubscribeLanguageEvent();
    }
}
