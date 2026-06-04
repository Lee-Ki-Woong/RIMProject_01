using Cysharp.Threading.Tasks;
using System;
using UnityEngine;
using UnityEngine.UI;

public class PartySlotButton : MonoBehaviour
{
    [SerializeField] private Button Button_CharacterSlot;
    [SerializeField] private Image Image_CharacterStading;

    private string m_characterId;

    private event Action<string> m_buttonEvent; 

    public async UniTask LoadAssetAsync(CharacterData characterData)
    {
        this.ActiveFalse();

        Sprite sprite = await LoadUtil.Async.LoadSpriteAsync(characterData.CharacterStandPath);

        SetAsset(sprite);

        this.ActiveTrue();
    }

    private void SetAsset(Sprite sprite)
    {
        Image_CharacterStading.sprite = sprite;
    }

    public void SetData(CharacterData characterData)
    {
        m_characterId = characterData.Id;
    }

    public void SetEvent(Action<string> action)
    {
        m_buttonEvent = action;
        UnBindButtonEvent();
        BindButtonEvent();

    }

    private void BindButtonEvent()
    {
        if (this.ComponentChecking(ref Button_CharacterSlot) == false)
        {
            return;
        }

        Button_CharacterSlot.onClick.AddListener(OnClick_Button);
    }

    private void UnBindButtonEvent()
    {
        if (this.ComponentChecking(ref Button_CharacterSlot) == false)
        {
            return;
        }
        Button_CharacterSlot.onClick.RemoveListener(OnClick_Button);
    }

    private void OnClick_Button()
    {
        m_buttonEvent?.Invoke(m_characterId);
    }
}
