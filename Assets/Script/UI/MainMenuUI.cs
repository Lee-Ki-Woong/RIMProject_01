using UnityEngine;
using UnityEngine.UI;

public class MainMenuUI : BaseUI
{
    // [SerializeField]
    [SerializeField] private Button PlayBGMBtn;


    // [Life Cycle]
    private void Awake()
    {
        BindBtnEvent();
    }

    // [BindBtnEvent]
    private void BindBtnEvent()
    {
        PlayBGMBtn.onClick.AddListener(OnClickPlayBGMBtn);
    }

    // [Btn Handles]
    private void OnClickPlayBGMBtn()
    {
        SoundManager.Instance.PlayBGM();
    }

}
