using UnityEngine;
using System.Collections.Generic;

public class GameManager : BaseManager<GameManager>
{
    [SerializeField] private GameObject Prefab_UIManager;
    [SerializeField] private GameObject Prefab_ResourceManager;
    [SerializeField] private GameObject Prefab_GameDataManager;
    [SerializeField] private GameObject Prefab_GameObjectManager;
    [SerializeField] private GameObject Prefab_NetWorkManager;


    private UIManager UI;
    private ResourceManager Resource;
    private GameDataManager GameData;
    private GameObjectManager GameObject;
    private NetworkManager Network;

    public PlayerModel PlayerModel {  get; private set; }
    

    protected override void Awake()
    {
        base.Awake();
        AwakeSetting();
    }

    private void AwakeSetting()
    {
        QualitySettings.vSyncCount = 1;
        ManagerCheck();
        DontDestroyGameManager();
        CreateManagerAndCheckManagerScript();
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

        if (Prefab_GameObjectManager == null)
        {
            this.LogError("GameObjectManager가 할당되지 않았습니다!!");
        }

        if (Prefab_NetWorkManager == null)
        {
            this.LogError("NetworkManager가 할당되지 않았습니다!!");
        }
    }

    private void CreateManagerAndCheckManagerScript()
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

        if (this.TryInstantiate(Prefab_NetWorkManager, this.transform, out GameObject networkNamagerInstance))
        {
            if (networkNamagerInstance.TryGetComponent(out NetworkManager networkManager))
            {
                Network = networkManager;
            }
            else
            {
                this.LogError("NetworkManager Prefab에 NetworkManager 컴포넌트가 없습니다!!");
            }
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

        Load();
    }


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

    public void PlayerGetStar(int starPoint)
    {
        PlayerModel.Score = starPoint;
    }
}
