using Cysharp.Threading.Tasks;
using System;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.UI;

public class InGame : BaseUI
{
    [Serializable]
    private class MenuPopupButton
    {
        public Button Button_MenuPopup;
        public Image Image_MenuPopup;
        public AssetReferenceT<Sprite> Asset_MenuPopupButtonSprite;
    }

    [SerializeField] private MenuPopupButton MenuPopup;

    [Serializable]
    private class UltimateSlider
    {
        public Slider Slider_Ultimate;
        public Button Button_Ultimate;
        public Image Image_Gauge;
        public AssetReferenceT<Sprite> Asset_GaugeSprite;

        public Image Image_Background;
        public AssetReferenceT<Sprite> Asset_BackgroundSprite;
    }

    [SerializeField] private UltimateSlider Ultimate;

    [Serializable]
    private class PartyList
    {
        public Slider Slider_CharacterHp;

        public Button Button_Character;
        public Image Image_Character;
        public AssetReferenceT<Sprite> Asset_CharacterSprite;

        public Slider Slider_CharacterUltimate;
        public Button Button_Ultimate;
        public Image Image_Ultimate;
        public AssetReferenceT<Sprite> Asset_UltimateSprite;

    }

    [SerializeField] private PartyList CharacterOne;
    [SerializeField] private PartyList CharacterTwo;
    [SerializeField] private PartyList CharacterThree;

    private void Awake()
    {
        IsAssetAsyncLoad = false;
    }

    public async override UniTask LoadAssetAsync()
    {
    }



}
