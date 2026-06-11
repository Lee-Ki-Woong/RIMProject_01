using Cysharp.Threading.Tasks;
using TMPro;
using UnityEngine;

public class InGamePopupPresenter : BasePresenter
{
    private InGamePopup m_inGamePopupUI;


    private Sprite m_sprite_background;

    private Sprite m_sprite_menuButton;

    private TMP_FontAsset m_fontAsset_baseFont;

    private string m_resumeButtonText;
    private string m_restartButtonText;
    private string m_gameOptionButtonText;
    private string m_mainMenuButtonText;

    public override UIType UIType_This { get; } = UIType.InGamePopup;

    public void InitInGamePopup(InGamePopup inGamePopup)
    {
        if (m_inGamePopupUI == null)
        {
            m_inGamePopupUI = inGamePopup;
        }

        if (m_isAssetLoad == false)
        {
            m_inGamePopupUI.ActiveFalse();
            LoadAssetAsync().Forget();
        }
        else
        {
            m_inGamePopupUI.ActiveTrue();
        }

        SubscribeEvents();
        LoadData();
    }

    protected override void SubscribeEvents()
    {
        m_inGamePopupUI.OnResumeGame += OnClick_ResumeButton;
        m_inGamePopupUI.OnRestartGame += OnClick_RestartButton;
        m_inGamePopupUI.OnOpenGameOption += OnClick_GameOptionButton;
        m_inGamePopupUI.OnReturnMainMenu += OnClick_MainButton;
    }

    protected override void UnsubscribeEvents()
    {
        m_inGamePopupUI.OnResumeGame -= OnClick_ResumeButton;
        m_inGamePopupUI.OnRestartGame -= OnClick_RestartButton;
        m_inGamePopupUI.OnOpenGameOption -= OnClick_GameOptionButton;
        m_inGamePopupUI.OnReturnMainMenu -= OnClick_MainButton;
    }

    protected async override UniTask LoadAssetAsync()
    {
        var (sprite_background, sprite_menuButton, fontAsset_baseFont) = await UniTask.WhenAll(
            LoadUtil.Async.LoadSpriteAsync(AddressUtil.Async.Sprite.UI.InGamePopup.Background),
            LoadUtil.Async.LoadSpriteAsync(AddressUtil.Async.Sprite.UI.InGamePopup.Button),
            LoadUtil.Async.LoadFontAssetAsync(AddressUtil.Async.Font.BaseFont)
            );

        m_sprite_background = sprite_background;
        m_sprite_menuButton = sprite_menuButton;
        m_fontAsset_baseFont = fontAsset_baseFont;

        m_inGamePopupUI.SetAsset(m_sprite_background, m_sprite_menuButton, m_fontAsset_baseFont);

        m_isAssetLoad = true;
        m_inGamePopupUI.ActiveTrue();
    }

    protected override void LoadData()
    {
        string dataId = (DataUtil.DataKey.UI.InGamePopup);

        if (UIDataManager.Instance.InGamePopupDataList.TryGetValue(dataId, out InGamePopupData inGamePopupData) == false)
        {
            LoadLogError(dataId);
            return;
        }

        m_resumeButtonText = inGamePopupData.ResumeButton;
        m_restartButtonText = inGamePopupData.ReStartButton;
        m_gameOptionButtonText = inGamePopupData.GameOptionButton;
        m_mainMenuButtonText = inGamePopupData.MainMenuButton;

        m_inGamePopupUI.SetData(m_resumeButtonText, m_restartButtonText, m_gameOptionButtonText, m_mainMenuButtonText);
    }

    private void OnClick_ResumeButton()
    {
        UnsubscribeEvents();
        UIManager.Instance.CloseUI(UIType.InGamePopup);
    }

    private void OnClick_RestartButton()
    {

    }

    private void OnClick_GameOptionButton()
    {

    }

    private void OnClick_MainButton()
    {
        UnsubscribeEvents();
        UIManager.Instance.CloseUI(UIType.InGamePopup);
    }
}
