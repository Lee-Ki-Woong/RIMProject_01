using Cysharp.Threading.Tasks;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.UI;

public class SkillButton : MonoBehaviour
{
    [SerializeField] private Button Button_This;
    [SerializeField] private Image Image_Button;
    [SerializeField] private Image Image_Skill;
    [SerializeField] private TMP_Text Text_SkillName;

    private event Action<string> m_buttonEvent;

    public string m_iconDataId { get; private set; }

    private void Awake()
    {
        AwakeSetting();
    }

    private void AwakeSetting()
    {
        this.ComponentChecking(ref Button_This);
        this.ComponentChecking(ref Image_Skill);
        this.ComponentChecking(ref Text_SkillName);

        if (transform.parent.gameObject.activeSelf == false)
        {
            this.ActiveFalse();
        }
    }

    public async UniTask LoadAssetAsync(SkillData skillData)
    {
        this.ActiveFalse();

        var (skillSprite, buttonSprite, selectedSprite, font) = await UniTask.WhenAll(
            LoadUtil.Async.LoadSpriteAsync(skillData.SkillIconPath),
            LoadUtil.Async.LoadSpriteAsync(AddressUtil.Async.Sprite.UI.Button_Empty),
            LoadUtil.Async.LoadSpriteAsync(AddressUtil.Async.Sprite.UI.SkillButton.Selected),
            LoadUtil.Async.LoadFontAssetAsync(AddressUtil.Async.Font.BaseFont)
            );

        Button_This.SetButtonSprite(selectedSprite);
        Image_Button.sprite = buttonSprite;
        Image_Skill.sprite = skillSprite;
        Text_SkillName.font = font;

        this.ActiveTrue();
    }

    public void SetEvent(SkillData skillData, Action<string> action)
    {
        m_iconDataId = skillData.Id;
        Text_SkillName.text = skillData.Name;
        m_buttonEvent = action;

        UnBindButtonEvent();
        BindButtonEvent();
    }

    private void BindButtonEvent()
    {
        if (this.ComponentChecking(ref Button_This) == false)
        {
            return;
        }

        Button_This.onClick.AddListener(OnClick_Button);
    }

    private void UnBindButtonEvent()
    {
        if (this.ComponentChecking(ref Button_This) == false)
        {
            return;
        }
        Button_This.onClick.RemoveListener(OnClick_Button);
    }

    private void OnClick_Button()
    {
        m_buttonEvent.Invoke(m_iconDataId);
    }
}
