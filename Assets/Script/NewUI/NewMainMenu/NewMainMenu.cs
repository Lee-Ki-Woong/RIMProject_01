using System;
using TMPro;
using TMPro.EditorUtilities;
using UnityEngine;
using UnityEngine.UI;

public class NewMainMenu : BaseUI
{
    [SerializeField] private Image Image_TitleText;
    [SerializeField] private Image Image_TitleImage;

    // 버튼과 텍스트를 배열로 관리한다고 가정합니다.
    [Serializable]
    private class Menu
    {
        public GameObject GameObject;
        public Button Button;
        public Image Image;
        public TMP_Text Text;
    }

    [SerializeField] private Menu[] Menus;

    // Presenter가 구독할 단일 이벤트 (몇 번째 버튼이 눌렸는가?)
    public event Action<int> OnMenuButtonClicked;

    private void Awake()
    {
        for (int i = 0; i < Menus.Length; i++)
        {
            int index = i; // 클로저(Closure) 문제 방지를 위해 로컬 변수 캡처
            Menus[i].Button.onClick.AddListener(() => OnMenuButtonClicked?.Invoke(index));
        }
    }

    public void SetAsset(Sprite titleText, Sprite titleImage, Sprite menuBtn, Sprite menuBtnHighlight, TMP_FontAsset font)
    {
        if (IsSetAsset)
        {
            return;
        }

        Image_TitleText.sprite = titleText;
        Image_TitleImage.sprite = titleImage;

        foreach (Menu menu in Menus)
        {
            menu.Image.sprite = menuBtn;
            menu.Button.SetButtonSprite(menuBtnHighlight);
            menu.Text.font = font;
        }

        IsSetAsset = true;
    }

    public override void SetData(UIData uiData)
    {
        
    }

    // Presenter가 호출할 텍스트 갱신 명령
    public void UpdateMenuTexts(string[] texts)
    {
        for (int i = 0; i < Menus.Length; i++)
        {
            if (i < texts.Length && !string.IsNullOrEmpty(texts[i]))
            {
                Menus[i].GameObject.SetActive(true);
                Menus[i].Text.text = texts[i];
            }
            else
            {
                Menus[i].GameObject.SetActive(false);
            }
        }
    }
}