using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CharacterCollection : BaseUI
{
    [SerializeField] private Transform CharacterIconSlot;
    [SerializeField] private Image Image_Background;

    [System.Serializable]
    private class MenuButton_Layout
    {
        public Button Button;
        public Image Image;
        public TMP_Text Text;
        public GameObject GameObject_Selected;
        public Image Image_Selected;
    }

    [SerializeField] private MenuButton_Layout CharacterInfo;
    [SerializeField] private MenuButton_Layout CharacterSkill;

    [SerializeField] private Button Button_Exit;
    [SerializeField] private Image Image_Exit;



    public override void SetData(UIData uiData)
    {
    }
}
