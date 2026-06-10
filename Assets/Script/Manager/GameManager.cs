using UnityEngine;
using System;

public class GameManager : BaseMonoManager<GameManager>
{
    [SerializeField] private GameObject Prefab_UIManager;
    [SerializeField] private GameObject Prefab_GameObjectManager;

    private GameDataManager GameData;
    private UIDataManager UIData;
    private ResourceManager Resource;
    private NetworkManager Network;

    private UIManager UI;
    private GameObjectManager GameObject;
    

    public SaveData GameSaveData { get; private set; }



    public Language Language { get; private set; }
    
    public event Action OnLanguageChanged;


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

        bool successChecking = MonoManagerCheck();

        if (successChecking == true)
        {
            CreateMonoManagerAndCheckManagerScript();
        }
    }

    private void DontDestroyGameManager()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    private void CreateCSharpManager()
    {
        Resource = new ResourceManager();
        GameData = new GameDataManager();
        UIData =new UIDataManager();
        Network = new NetworkManager();
    }

    private bool MonoManagerCheck()
    {
        if (Prefab_UIManager == null)
        {
            this.LogError("UIManager가 할당되지 않았습니다!!");
            return false;
        }

        if (Prefab_GameObjectManager == null)
        {
            this.LogError("GameObjectManager가 할당되지 않았습니다!!");
            return false;
        }

        return true;
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
        ChangeLanguage(GameUtil.GetLanguage(GameSaveData.Language));
    }

    private void LoadDataOnStart()
    {
        Load();
    }
    #endregion





    private void Save()
    {
        Network.RequestSaveSaveData(GameSaveData);
    }

    private void Load()
    {
        GameSaveData = Network.RequestLoadSavaData();
    }

    public void GameQuit()
    {
        Save();
        Application.Quit();
    }




    public void PlayerGetScore(int score)
    {
        if (GameSaveData.Score >= score)
        {
            return;
        }

        GameSaveData.Score = score;
        Save();
    }

    public void ChangeLanguage(Language language)
    {
        if (Language == language)
        {
            return;
        }

        Language = language;
        GameSaveData.Language = GameUtil.SetLanguage(Language);
        Save();

        UIDataManager.Instance.ReloadAllData();
        OnLanguageChanged?.Invoke();
    }
}
