using Cysharp.Threading.Tasks;
using UnityEngine;

public class InGamePresenter : BasePresenter
{
    public InGame InGame { get; private set; }

    private Sprite Sprite_Menu;
    private Sprite Sprite_Misson;


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
            LoadUtil.Async.LoadSpriteAsync("Sprite/UI/InGame/Menu"),
            LoadUtil.Async.LoadSpriteAsync("Sprite/UI/Main/Misson")
            );

        Sprite_Menu = menuSprite;
        Sprite_Misson = missonSprite;

        IsAssetLoad = true;

        InGame.SetAsset(Sprite_Menu, Sprite_Misson);
    }

    public void LeaveInGame()
    {
        UIManager.Instance.CloseUI(UIType.InGame);
    }
}
