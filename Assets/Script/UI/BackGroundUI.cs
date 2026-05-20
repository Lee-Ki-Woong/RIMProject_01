using Cysharp.Threading.Tasks;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class BackGroundUI : BaseUI
{
    // [SerializeField]
    [Header("TitleImage")]
    [SerializeField] private Image BackGroundTitleText;
    [SerializeField] private Image BackGroundTitleImage;
    [SerializeField] private Animation BackGroundTitleAnimation;


    // [Field]


    // [Life Cycle]
    private void Awake()
    {
        m_isAssetLoad = false;
    }


    // [Set Asset]
    public override async UniTask SetAssetAsync()
    {
        BackGroundTitleText.sprite = await LoadUtil.LoadSpriteAsync("Sprite/BackGround/TitleText", destroyCancellationToken);
        BackGroundTitleImage.sprite = await LoadUtil.LoadSpriteAsync("Sprite/BackGround/TitleImage", destroyCancellationToken);

        m_isAssetLoad = true;
    }



}
