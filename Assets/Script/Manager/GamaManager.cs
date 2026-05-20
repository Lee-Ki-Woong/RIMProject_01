using UnityEngine;

public class GameManager : MonoBehaviour
{
    // [Instance]
    public static GameManager Instance { get; private set; }


    // [Managers]
    [field : SerializeField] public ResourceManager Resource {  get; private set; }
    [field : SerializeField] public UIManager UI {  get; private set; }
    [field : SerializeField] public GameDataManager GameData { get; private set; }
    [field : SerializeField] public SoundManager Sound {  get; private set; }
    [field : SerializeField] public NetworkManager Network { get; private set; }


    // [EntityModles]
    public PlayerModel PlayerModel { get; private set; }


    // [Life Cycle]
    private void Awake()
    {
        if (Instance == null) Instance = this;
        DontDestroyOnLoad(this.gameObject);
    }

    private void Start()
    {
        UI.OpenBackGroundUI().Forget();
        UI.OpenMainUI().Forget();
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

    public void PlayerGetStar(int starPoint)
    {
        PlayerModel.StarList += (short)starPoint;
    }
}
