using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.UI;

public class MainMenu : BaseUI
{
    [SerializeField] private Image Image_TitleImage;
    [SerializeField] private Image Image_TitleText;

    [SerializeField] private AssetReference AssetReference_TitleImage;
    [SerializeField] private AssetReference AssetReference_TitleText;


    private void Awake()
    {
        AwakeSetting();
    }
    
    private void AwakeSetting()
    {
        LoadAssetAsync().Forget();
    }

    protected override async UniTask LoadAssetAsync()
    {
        Image_TitleImage.sprite = await LoadUtil.Async.LoadSpriteAsync(AssetReference_TitleImage.RuntimeKey.ToString());
        Image_TitleText.sprite = await LoadUtil.Async.LoadSpriteAsync(AssetReference_TitleText.RuntimeKey.ToString());
        await base.LoadAssetAsync();
    }
}
