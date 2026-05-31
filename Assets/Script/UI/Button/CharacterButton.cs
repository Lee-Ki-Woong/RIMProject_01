using Cysharp.Threading.Tasks;
using System;
using UnityEngine;
using UnityEngine.UI;

public class CharacterButton : MonoBehaviour
{
    [SerializeField] private Button Button_This;
    [SerializeField] private Image Image_Edge;
    [SerializeField] private Image Image_Character;
    [SerializeField] private GameObject GameObject_Selected;
    [SerializeField] private Image Image_Selected;


    // [Event]
    private event Action<string> m_buttonEvent;


    // [Field]
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

    // [Load Asset]
    public async UniTask LoadAssetAsync(CharacterData value)
    {

        var (edgeSprite, characterSprite) = await UniTask.WhenAll
            (
            LoadUtil.Async.LoadSpriteAsync(AddressUtil.Async.Sprite.UI.CharacterButton.Edge),
            LoadUtil.Async.LoadSpriteAsync(value.CharacterIconPath)
            );

        Image_Edge.sprite = edgeSprite;
        Image_Character.sprite = characterSprite;
    }

    public void SetEvent(string id, Action<string> onClickCallback)
    {
        m_iconDataId = id;
        m_buttonEvent += onClickCallback;

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
