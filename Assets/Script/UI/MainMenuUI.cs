using UnityEngine;
using UnityEngine.UI;

public class MainMenuUI : BaseUI
{
    // [SerializeField]
    [SerializeField] private Button PlayGameBtn;
    [SerializeField] private Button MyColletionBtn;
    [SerializeField] private Button ShopBtn;
    [SerializeField] private Button GameOptionBtn;
    [SerializeField] private Button ExitGameBtn;


    // [Life Cycle]
    private void Awake()
    {
        BindBtnEvent();
    }

    // [BindBtnEvent]
    private void BindBtnEvent()
    {
    }

    // [Btn Handles]


}
