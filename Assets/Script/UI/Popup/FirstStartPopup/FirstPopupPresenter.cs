using Cysharp.Threading.Tasks;
using TMPro;
using UnityEngine;

public class FirstPopupPresenter : BasePresenter
{
    private FirstPopup m_firstPopupUI;

    private Sprite m_sprite_background;
    private Sprite m_sprite_resumeButton;
    private TMP_FontAsset m_font_resumeButton;

    public override UIType UIType_This { get; } = UIType.FirstStartPopup;

    public void InitFirstPopup(FirstPopup firstPopup)
    {
        if(m_firstPopupUI == null)
        {
            m_firstPopupUI = firstPopup;
        }


        if (m_isAssetLoad == false)
        {
            m_firstPopupUI.ActiveFalse();
            LoadAssetAsync().Forget();
        }
        else
        {
            m_firstPopupUI.ActiveTrue();
        }

        SubscribeEvents();
        LoadData();
    }

    protected override void SubscribeEvents()
    {
        m_firstPopupUI.OnResumeButton += OnClick_ResumeButton;

        m_firstPopupUI.OnUIExit += On_UIExit;
    }

    protected override void UnsubscribeEvents()
    {
        m_firstPopupUI.OnResumeButton -= OnClick_ResumeButton;

        m_firstPopupUI.OnUIExit -= On_UIExit;
    }

    protected override async UniTask LoadAssetAsync()
    {
        var (sprite_background, sprite_resumeButton, font_resumeButton) = await UniTask.WhenAll
            (
            LoadUtil.Async.LoadSpriteAsync(AddressUtil.Async.Sprite.UI.FirstPopup.Background),
            LoadUtil.Async.LoadSpriteAsync(AddressUtil.Async.Sprite.UI.FirstPopup.ResumeButton),
            LoadUtil.Async.LoadFontAssetAsync(AddressUtil.Async.Font.BaseFont)
            );

        m_sprite_background = sprite_background;
        m_sprite_resumeButton = sprite_resumeButton;
        m_font_resumeButton = font_resumeButton;
        m_firstPopupUI.SetAsset(m_sprite_background, m_sprite_resumeButton, m_font_resumeButton);

        m_isAssetLoad = true;
        m_firstPopupUI.ActiveTrue();
    }

    protected override void LoadData()
    {
        string tutorial = "이동 방법 : W A S D\n캐릭터 변경 - 1, 2, 3, 4\n게임 나가기 - ESC";
        string resume = "알겠습니다.";

        m_firstPopupUI.SetData(tutorial, resume);
    }

    private void OnClick_ResumeButton()
    {
        UIManager.Instance.CloseUI(UIType.FirstStartPopup);
    }

    private void On_UIExit()
    {
        UnsubscribeEvents();
    }
}
