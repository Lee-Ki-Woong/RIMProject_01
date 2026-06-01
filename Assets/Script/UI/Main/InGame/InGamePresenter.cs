using Cysharp.Threading.Tasks;
using UnityEngine;

public class InGamePresenter : BasePresenter
{
    public InGame InGame { get; private set; }

    private Sprite Sprite_MenuButton;
    private Sprite Sprite_MissonButton;


    public void InitInGame(InGame inGame)
    {
        InGame = inGame;
    }
    public override async UniTask LoadAndSetAssetAsync()
    {
        if (IsAssetLoad)
        {
            return;
        }

        var (menuSprite, missonSprite) = await UniTask.WhenAll
            (
            LoadUtil.Async.LoadSpriteAsync("Sprite/UI/InGame/MenuButton"),
            LoadUtil.Async.LoadSpriteAsync("Sprite/UI/Main/MissonButton")
            );

        Sprite_MenuButton = menuSprite;
        Sprite_MissonButton = missonSprite;

        IsAssetLoad = true;

        InGame.SetAsset(Sprite_MenuButton, Sprite_MissonButton);
    }

    public void LeaveInGame()
    {
        UIManager.Instance.CloseUI(UIType.InGame);
    }
}
