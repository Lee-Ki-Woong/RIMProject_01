using System.IO;
using UnityEngine;

public class NetworkManager : BaseManager<NetworkManager>
{
    protected override void Awake()
    {
        base.Awake();
    }

    private string GetPath()
    {
        string path = Path.Combine(Application.persistentDataPath, "RIM_ProjectSaveFile.json");
        return path;
    }

    public void RequestSavePlayerModel(PlayerModel playerModel)
    {
        if (playerModel == null) return;

        string json = JsonUtility.ToJson(playerModel, true);
        File.WriteAllText(GetPath(), json);
        Debug.Log("저장 완료!!" + GetPath());
    }

    public PlayerModel RequestLoadPlayerModel()
    {
        string path = GetPath();

        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            PlayerModel playerModel = JsonUtility.FromJson<PlayerModel>(json);
            Debug.Log("로드 완료!");
            return playerModel;
        }
        else
        {
            Debug.Log("새로운 모델을 생성합니다!");
            return CreateNewPlayerModel();
        }
    }

    public PlayerModel CreateNewPlayerModel()
    {
        PlayerModel playerModel = new PlayerModel();
        playerModel.Score = 0;

        return playerModel;
    }
}
