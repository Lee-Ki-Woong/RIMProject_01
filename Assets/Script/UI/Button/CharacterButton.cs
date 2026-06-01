using Cysharp.Threading.Tasks;
using System;
using UnityEngine;
using UnityEngine.UI;

public class CharacterButton : MonoBehaviour
{
    [SerializeField] private Button Button_This;
    [SerializeField] private Image Image_Edge;
    [SerializeField] private Image Image_Mask;
    [SerializeField] private Image Image_Character;
    [SerializeField] private GameObject GameObject_Selected;
    [SerializeField] private Image Image_Selected;

    private event Action<string> m_buttonEvent;

    private string m_iconDataId;

    private void Awake()
    {
        AwakeSetting();
    }

    private void AwakeSetting()
    {
        this.ComponentChecking(ref Button_This);
        this.ComponentChecking(ref Image_Edge);
        this.ComponentChecking(ref Image_Character);
        this.ComponentChecking(ref Image_Selected);
    }

    public async UniTask LoadAssetAsync(CharacterData value)
    {

        var (edgeSprite, maskSprite, characterSprite, selectedSprite) = await UniTask.WhenAll
            (
            LoadUtil.Async.LoadSpriteAsync(AddressUtil.Async.Sprite.UI.CharacterButton.Edge),
            LoadUtil.Async.LoadSpriteAsync(AddressUtil.Async.Sprite.UI.CharacterButton.Mask),
            LoadUtil.Async.LoadSpriteAsync(value.CharacterIconPath),
            LoadUtil.Async.LoadSpriteAsync(AddressUtil.Async.Sprite.UI.CharacterButton.Selected)
            );

        Image_Edge.sprite = edgeSprite;
        Image_Mask.sprite = maskSprite;
        Image_Character.sprite = characterSprite;
        Image_Selected.sprite = selectedSprite;
    }

    public void SetEvent(string id, Action<string> action)
    {
        m_iconDataId = id;
        m_buttonEvent += action;

        BindButtonEvent();
    }

    private void BindButtonEvent()
    {
        if (this.ComponentChecking(ref Button_This) == false)
        {
            return;
        }

        if (m_buttonEvent != null)
        {
            Button_This.onClick.RemoveAllListeners();
        }

        Button_This.onClick.AddListener(OnClick_Button);
    }

    private void OnClick_Button()
    {
        m_buttonEvent.Invoke(m_iconDataId);
    }
}
