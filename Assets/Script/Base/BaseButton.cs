using Cysharp.Threading.Tasks;
using System;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.UI;

public class BaseButton : BaseUI
{
    [SerializeField] private Button ThisButton;
    [SerializeField] private Image ThisImage;

    [SerializeField] private AssetReference SpriteAddress;

    private event Action m_buttonEvent;


    private void Awake()
    {
        AwakeSetting();
    }

    private void AwakeSetting()
    {
        FieldChecking(ref ThisButton);
        FieldChecking(ref ThisImage);
    }

    private bool FieldChecking<T>(ref T component) where T : Component
    {
        if (component == null)
        {
            if (component = this.gameObject.GetComponent<T>())
            {
                this.LogWarning($"임시로 이 오브젝트의 {typeof(T).Name}를 GetComponent를 사용하여 할당하였습니다!!");
                return true;
            }

            this.LogError($"{typeof(T).Name} 가 null입니다. 인스펙터에서 확인해주세요!! 임시로 이 오브젝트를 끄겠습니다!!");
            this.ActiveFalse();
            return false;
        }

        return true;
    }

    private void Start()
    {
        StartSetting();
    }

    private void StartSetting()
    {
        LoadAssetAsync().Forget();
    }

    protected override async UniTask LoadAssetAsync()
    {
        ThisImage.sprite = await LoadUtil.Async.LoadSpriteAsync(SpriteAddress.RuntimeKey.ToString());
        base.LoadAssetAsync().Forget();
    }

    public void GetEvent(Action buttonCallback)
    {
        m_buttonEvent = buttonCallback;
        BindButtonEvent();
    }

    private void BindButtonEvent()
    {
        if (FieldChecking(ref ThisButton) == false)
        {
            return;
        }

        if (m_buttonEvent != null)
        {
            ThisButton.onClick.RemoveAllListeners();
            ThisButton.onClick.AddListener(m_buttonEvent.Invoke);
        }
    }
}
