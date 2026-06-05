using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InGame : BaseUI
{
    [SerializeField] private Button Button_MenuPopup;
    [SerializeField] private Image Image_MenuPopup;

    [SerializeField] private Image Image_Misson;
    [SerializeField] private TMP_Text Text_MissonTitle;
    [SerializeField] private TMP_Text Text_MissonDescription;

    [SerializeField] private Slider Slider_MainCharacterHp;

    [Serializable]
    private class SubCharacterMenu
    {
        public Button Button_CharacterButton;
        public Image Image_CharacterIcon;
        public Slider Slider_SubCharacterHp;
    }

    [SerializeField] private SubCharacterMenu[] SubCharadcters = new SubCharacterMenu[3];


    public void SetAsset(Sprite menuSprite, Sprite missonSprite, TMP_FontAsset font)
    {
        Image_MenuPopup.sprite = menuSprite;
        Image_Misson.sprite = missonSprite;
        Text_MissonTitle.font = font;
        Text_MissonDescription.font = font;
    }

    public override void SetData(UIData uiData)
    {
        InitData(uiData.Actions[0]);
    }

    private void InitData(Action action)
    {
        if (action == null)
        {
            Debug.LogWarning($"{this.gameObject} : action의 값이 null이거나 Empty입니다!!");
            return;
        }

        Button_MenuPopup.onClick.RemoveAllListeners();
        Button_MenuPopup.onClick.AddListener(action.Invoke);
    }

    public void SetHp(float currentHp, float maxHp)
    {
        if (maxHp <= 0) return;
        Slider_MainCharacterHp.value = currentHp / maxHp;
    }


}
