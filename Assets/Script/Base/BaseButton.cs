using Cysharp.Threading.Tasks;
using System;
using UnityEngine;
using UnityEngine.UI;

public class BaseButton : MonoBehaviour
{
    [SerializeField] private Button Button_This;
    [SerializeField] private Image Image_This;

    private event Action m_buttonEvent;


    private void Awake()
    {
        AwakeSetting();
    }

    private void AwakeSetting()
    {
        this.ComponentChecking(ref Button_This);
        this.ComponentChecking(ref Image_This);
    }

    public void SetAsset(Sprite buttonSprite, Sprite buttonHighlightedSprite)
    {
        if (this.ComponentChecking(ref Button_This) == false || this.ComponentChecking(ref Image_This) == false)
        {
            return;
        }

        Image_This.sprite = buttonSprite;
        Button_This.SetButtonSprite(buttonHighlightedSprite);
    }

    public void SetEvent(Action buttonCallback)
    {
        m_buttonEvent = buttonCallback;
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
        m_buttonEvent?.Invoke();
    }
}