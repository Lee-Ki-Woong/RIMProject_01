using Cysharp.Threading.Tasks;
using UnityEngine;

public class GameManager : BaseManager<GameManager>
{
    [SerializeField] private UIManager UI;
    [SerializeField] private ResourceManager Resource;

    protected override void Awake()
    {
        base.Awake();
        AwakeSetting();
    }

    private void AwakeSetting()
    {
        ManagerCheck();
        DontDestroyGameManager();
    }

    private void ManagerCheck()
    {
        if (UI == null)
        {
            this.LogError("UIManager가 할당되지 않았습니다!!");
        }

        if (Resource == null)
        {
            this.LogError("ResourceManager가 할당되지 않았습니다!!");
        }
    }

    private void DontDestroyGameManager()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    private void Start()
    {
        StartSetting();
    }
    
    private void StartSetting()
    {
        OpenFirstUI();
    }

    private void OpenFirstUI()
    {
        if(UI == null)
        {
            return;
        }

        UI.OpenUI<MainMenu>(UIType.MainMenu).Forget();
    }
}
