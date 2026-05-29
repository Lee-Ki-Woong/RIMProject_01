using Cysharp.Threading.Tasks;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.UI;

public class MainMenu : BaseUI
{
    [SerializeField] private AssetReference Sprite_Menu;
    [SerializeField] private AssetReference Sprite_HighlightedMenu;
    [SerializeField] private AssetReference Font_Menu;

    [System.Serializable]
    private class Menu_Layout
    {
        public GameObject gameObject;
        public Button Button;
        public Image Image;
        public TMP_Text Text;
    }

    [SerializeField] private Menu_Layout[] menus;

    private void Awake()
    {
        IsAssetAsyncLoad = false;

        AwakeSetting();
    }

    private void AwakeSetting()
    {
        foreach(Menu_Layout menu in menus)
        {
            if(menu.gameObject != null)
            {
                menu.gameObject.ComponentChecking(ref menu.Button);
                menu.gameObject.ComponentChecking(ref menu.Image);
                menu.gameObject.ComponentChecking(ref menu.Text);
            }
        }
    }

    // [Load Asset]
    public override async UniTask LoadAssetAsync()
    {
        (Sprite loadedSprite, Sprite loadedHighlightedSprite, TMP_FontAsset loadedFont) = await UniTask.WhenAll
            (
            LoadUtil.Async.LoadSpriteAsync(Sprite_Menu.RuntimeKey.ToString()),
            LoadUtil.Async.LoadSpriteAsync(Sprite_HighlightedMenu.RuntimeKey.ToString()),
            LoadUtil.Async.LoadFontAssetAsync(Font_Menu.RuntimeKey.ToString())
            );

        foreach (Menu_Layout menu in menus)
        {
            menu.Image.sprite = loadedSprite;
            menu.Button.SetButtonSprite(loadedHighlightedSprite);
            menu.Text.font = loadedFont;
        }

        IsAssetAsyncLoad = true;
    }

    public void SetData(UIData uiData)
    {
        string[] texts = uiData.Texts;
        Action[] actions = uiData.Actions;

        if (texts.Length != actions.Length)
        {
            Debug.LogError($"{this.gameObject} : 전달받은 texts와 Actions의 Length 값이 동일하지 않습니다!!");
            return;
        }

        for (int i = 1; i < Math.Min(texts.Length, menus.Length); i++)
        {
            if (string.IsNullOrEmpty(texts[i]) || actions[i] == null)
            {
                if (string.IsNullOrEmpty(texts[i]))
                {
                    Debug.LogWarning($"{this.gameObject} : texts {i}의 값이 null이거나 Empty입니다!!");
                }

                if (actions[i] == null)
                {
                    Debug.LogWarning($"{this.gameObject} : action {i}의 값이 null이거나 Empty입니다!!");
                }

                menus[i].gameObject.SetActive(false);
                continue;
            }

            if (menus[i].gameObject.activeSelf == false)
            {
                menus[i].gameObject.SetActive(true);
            }

            menus[i].Text.text = texts[i];
            menus[i].Button.onClick.RemoveAllListeners();
            menus[i].Button.onClick.AddListener(actions[i].Invoke);
        }
    }
}
