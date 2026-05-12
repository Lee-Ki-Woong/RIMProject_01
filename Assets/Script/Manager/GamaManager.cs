using UnityEngine;

public class GameManager : MonoBehaviour
{
    // [Field]
    public static GameManager Insatance { get; private set; }

    public PlayerModel PlayerModel { get; private set; }

    // [Life Cycle]
    private void Awake()
    {
        if (Insatance == null) Insatance = this;
        DontDestroyOnLoad(this.gameObject);
    }

    private void SavePlayerModel()
    {
        NetworkManager.Instance.RequestSavePlayerModel(PlayerModel);
    }

    private void LoadPlayerModel()
    {
        PlayerModel = NetworkManager.Instance.RequestLoadPlayerModel();
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
