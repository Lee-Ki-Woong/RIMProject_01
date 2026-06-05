using Cysharp.Threading.Tasks;
using System;
using TMPro;
using UnityEngine;

public class InGamePresenter : BasePresenter
{
    public InGame InGame { get; private set; }

    private Sprite Sprite_MenuButton;
    private Sprite Sprite_MissonButton;
    private TMP_FontAsset Font_BaseFont;


    public void InitInGame(InGame inGame)
    {
        InGame = inGame;
    }
    public void OepnInGameUI()
    {
        UIData inGameData = new()
        {
            Texts = new string[] { "메뉴 팝업", "미션 정보"},
            Actions = new Action[] { OnClick_OpenMenuPopup, null }
        };

        InGame.SetData(inGameData);

    }

    public override async UniTask LoadAndSetAssetAsync()
    {
        if (IsAssetLoad)
        {
            return;
        }

        var (menuSprite, missonSprite, font) = await UniTask.WhenAll
            (
            LoadUtil.Async.LoadSpriteAsync(AddressUtil.Async.Sprite.UI.InGame.MenuPopupButton),
            LoadUtil.Async.LoadSpriteAsync(AddressUtil.Async.Sprite.UI.InGame.Misson),
            LoadUtil.Async.LoadFontAssetAsync(AddressUtil.Async.Font.BaseFont)
            );

        Sprite_MenuButton = menuSprite;
        Sprite_MissonButton = missonSprite;
        Font_BaseFont = font;


        InGame.SetAsset(Sprite_MenuButton, Sprite_MissonButton, Font_BaseFont);

        IsAssetLoad = true;
    }


    private void OnClick_OpenMenuPopup()
    {
        UIManager.Instance.OpenInGamePopup().Forget();
    }
}
