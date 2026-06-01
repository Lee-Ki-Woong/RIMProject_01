using UnityEngine;

public class GameManager : BaseManager<GameManager>
{
    [SerializeField] GameObject Prefab_UIManager;
    [SerializeField] GameObject Prefab_ResourceManager;
    [SerializeField] GameObject Prefab_GameDataManager;


    private UIManager UI;
    private ResourceManager Resource;
    private GameDataManager GameData;

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
        if (Prefab_UIManager == null)
        {
            this.LogError("UIManager가 할당되지 않았습니다!!");
        }

        if (Prefab_ResourceManager == null)
        {
            this.LogError("ResourceManager가 할당되지 않았습니다!!");
        }

        if (Prefab_GameDataManager == null)
        {
            this.LogError("GameDataManager가 할당되지 않았습니다!!");
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
        if (this.TryInstantiate(Prefab_UIManager, this.transform, out GameObject uiManagerInstance))
        {
            if (uiManagerInstance.TryGetComponent(out UIManager uiManager))
            {
                UI = uiManager;
            }
            else
            {
                this.LogError("UIManager Prefab에 UIManager 컴포넌트가 없습니다!!");
            }
        }

        if (this.TryInstantiate(Prefab_ResourceManager, this.transform, out GameObject resourceManagerInstance))
        {
            if (resourceManagerInstance.TryGetComponent(out ResourceManager resourceManager))
            {
                Resource = resourceManager;
            }
            else
            {
                this.LogError("ResourceManager Prefab에 ResourceManager 컴포넌트가 없습니다!!");
            }
        }

        if (this.TryInstantiate(Prefab_GameDataManager, this.transform, out GameObject gameDataManagerInstance))
        {
            if (gameDataManagerInstance.TryGetComponent(out GameDataManager gameDataManager))
            {
                GameData = gameDataManager;
            }
            else
            {
                this.LogError("GameDataManager Prefab에 GameDataManager 컴포넌트가 없습니다!!");
            }
        }
    }
}
