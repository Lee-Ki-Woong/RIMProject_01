using Cysharp.Threading.Tasks;
using TMPro;
using UnityEngine;

public class DiePopupPresenter : BasePresenter
{
    private DiePopup m_diePopupUI;


    private Sprite m_sprite_background;

    private Sprite m_sprite_exitButton;

    private TMP_FontAsset m_fontAsset_fontbase;

    private string m_gameOverText;
    private string m_scoreText;
    private string m_exitButtonText;

    public override UIType UIType_This { get; } = UIType.DiePopup;

    public void InitDiePopup(DiePopup diePopup)
    {
        if (m_diePopupUI == null)
        {
            m_diePopupUI = diePopup;
        }

        if (m_isAssetLoad == false)
        {
            m_diePopupUI.ActiveFalse();
            LoadAssetAsync().Forget();
        }
        else
        {
            m_diePopupUI.ActiveTrue();
        }

        SubscribeEvents();
        LoadData();
    }

    protected override void SubscribeEvents()
    {
        m_diePopupUI.OnExitButton += OnClick_ExitButton;

        m_diePopupUI.OnUIExit += On_UIExit;
    }

    protected override void UnsubscribeEvents()
    {
        m_diePopupUI.OnExitButton -= OnClick_ExitButton;

        m_diePopupUI.OnUIExit -= On_UIExit;
    }

    protected async override UniTask LoadAssetAsync()
    {
        var (sprite_background, sprite_exitButton, fontAsset_fontbase) = await UniTask.WhenAll
            (
            LoadUtil.Async.LoadSpriteAsync(AddressUtil.Async.Sprite.UI.DiePopup.Background),
            LoadUtil.Async.LoadSpriteAsync(AddressUtil.Async.Sprite.UI.DiePopup.ExitButton),
            LoadUtil.Async.LoadFontAssetAsync(AddressUtil.Async.Font.BaseFont)
            );

        m_sprite_background = sprite_background;
        m_sprite_exitButton = sprite_exitButton;
        m_fontAsset_fontbase = fontAsset_fontbase;

        m_diePopupUI.SetAsset(m_sprite_background, m_sprite_exitButton, m_fontAsset_fontbase);

        m_isAssetLoad = true;
        m_diePopupUI.ActiveTrue();
    }

    protected override void LoadData()
    {
        string dataId = DataUtil.DataKey.UI.DiePopup;

        if (UIDataManager.Instance.DiePopupDataList.TryGetValue(dataId, out DiePopupData diePopupData) == false)
        {
            LoadLogError(dataId);
            return;
        }

        m_gameOverText = diePopupData.GameOverText;
        m_scoreText = diePopupData.ScoreText;
        m_exitButtonText = diePopupData.ExitButton;

        m_diePopupUI.SetData(m_gameOverText, m_scoreText, m_exitButtonText);
    }

    private void OnClick_ExitButton()
    {
        UIManager.Instance.CloseUI(UIType.DiePopup);
    }

    private void On_UIExit()
    {
        UnsubscribeEvents();
    }
}
