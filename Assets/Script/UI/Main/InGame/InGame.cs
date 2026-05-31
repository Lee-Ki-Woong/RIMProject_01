using UnityEngine;
using UnityEngine.UI;

public class InGame : BaseUI
{
    [SerializeField] private Button Button_Menu;
    [SerializeField] private Image Image_Menu;

    [SerializeField] private Image Image_Misson;
    [SerializeField] private Text Text_MissonTitle;
    [SerializeField] private Text Text_MissonDescription;

    public void SetAsset(Sprite menuSprite, Sprite missonSprite)
    {
        Image_Menu.sprite = menuSprite;
        Image_Misson.sprite = missonSprite;
    }

    public override void SetData(UIData uiData)
    {
    }



}
