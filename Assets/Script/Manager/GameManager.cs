using UnityEngine;

public class GameManager : BaseMonoManager<GameManager>
{
    [SerializeField] private GameObject Prefab_UIManager;
    [SerializeField] private GameObject Prefab_GameObjectManager;


    private UIManager UI;
    private ResourceManager Resource;
    private GameDataManager GameData;
    private GameObjectManager GameObject;
    private NetworkManager Network;

    public PlayerModel PlayerModel { get; private set; }

    #region Awake
    protected override void Awake()
    {
        base.Awake();
        AwakeSetting();
    }

    private void AwakeSetting()
    {
        QualitySettings.vSyncCount = 1;

        DontDestroyGameManager();
        CreateCSharpManager();
        
        MonoManagerCheck();
        CreateMonoManagerAndCheckManagerScript();
    }

    private void DontDestroyGameManager()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    private void CreateCSharpManager()
    {
        GameData = new GameDataManager();
        Network = new NetworkManager();
        Resource = new ResourceManager();
    }

    private void MonoManagerCheck()
    {
        if (Prefab_UIManager == null)
        {
            this.LogError("UIManager가 할당되지 않았습니다!!");
        }

        if (Prefab_GameObjectManager == null)
        {
            this.LogError("GameObjectManager가 할당되지 않았습니다!!");
        }
    }

    private void CreateMonoManagerAndCheckManagerScript()
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

        if (this.TryInstantiate(Prefab_GameObjectManager, this.transform, out GameObject gameObjectManagerInstance))
        {
            if (gameObjectManagerInstance.TryGetComponent(out GameObjectManager gameObjectManager))
            {
                GameObject = gameObjectManager;
            }
            else
            {
                this.LogError("GameObjectManager Prefab에 GameObjectManager 컴포넌트가 없습니다!!");
            }
        }
    }
    #endregion

    #region Start
    private void Start()
    {
        StartSetting();
    }

    private void StartSetting()
    {
        LoadDataOnStart();
    }

    private void LoadDataOnStart()
    {
        Load();
    }
    #endregion

    private void SavePlayerModel()
    {
        Network.RequestSavePlayerModel(PlayerModel);
    }

    private void LoadPlayerModel()
    {
        PlayerModel = Network.RequestLoadPlayerModel();
    }

    public void Save()
    {
        SavePlayerModel();
    }

    public void Load()
    {
        LoadPlayerModel();
    }

    public void GameQuit()
    {
        Save();
        Application.Quit();
    }

    public void PlayerGetScore(int score)
    {
        if (PlayerModel.Score < score)
        {
            PlayerModel.Score = score;
        }

        Save();
    }
}
