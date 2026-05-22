using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class BackGroundUI : BaseMainUI
{
    // [SerializeField]
    [Header("Title Image")]
    [SerializeField] private Image Image_BackGroundTitleText;
    [SerializeField] private Image Image_BackGroundTitleImage;
    [SerializeField] private Animation Animation_BackGroundTitle;


    // [Field]


    // [Life Cycle]
    private void Awake()
    {
        IsAssetLoad = false;
    }


    // [Set Asset]
    public override async UniTask SetAssetAsync()
    {
        (Sprite titleTextSprite, Sprite titleImageSprite) = await UniTask.WhenAll
            (
            LoadUtil.LoadSpriteAsync("Sprite/BackGround/TitleText", destroyCancellationToken),
            LoadUtil.LoadSpriteAsync("Sprite/BackGround/TitleImage", destroyCancellationToken)
            );

        Image_BackGroundTitleText.sprite = titleTextSprite;
        Image_BackGroundTitleImage.sprite = titleImageSprite;

        IsAssetLoad = true;
    }



}
