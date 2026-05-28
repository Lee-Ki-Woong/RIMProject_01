using System;
using UnityEngine;
using UnityEngine.UI;

public class BaseButton : BaseUI
{
    [SerializeField] Button ThisButton;
    [SerializeField] Image ThisImage;

    Action buttonEvent;


    private void Awake()
    {
        AwakeSetting();    
    }

    private void AwakeSetting()
    {
        ButtonChecking();
        ImageChecking();
    }

    private bool ButtonChecking()
    {
        if (ThisButton == null)
        {

            if (ThisButton = this.gameObject.GetComponent<Button>())
            {
                Log("임시로 이 오브젝트의 버튼을 GetComponent를 사용하여 할당하였습니다!!");
                return true;
            }

            LogError("ThisButton이 null입니다. 인스펙터에서 확인해주세요!!");
            this.gameObject.SetActive(false);
            return false;
        }

        return true;
    }

    private bool ImageChecking()
    {
        if (ThisImage == null)
        {
            if (ThisImage = this.gameObject.GetComponent<Image>())
            {
                Log("임시로 이 오브젝트의 Image를 GetComponent를 사용하여 할당하였습니다!!");
                return true;
            }

            LogError("ThisImager가 null입니다. 인스펙터에서 확인해주세요!!");
            this.gameObject.SetActive(false);
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

    }

    public void GetEvent(Action buttonCallback)
    {
        buttonEvent = buttonCallback;
        BindButtonEvent();
    }

    private void BindButtonEvent()
    {
        if (ButtonChecking())
        {
            return;
        }

        if (buttonEvent != null)
        {
            ThisButton.onClick.AddListener(buttonEvent.Invoke);
        }
    }
}
