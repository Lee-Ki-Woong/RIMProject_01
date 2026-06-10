using System.IO;
using UnityEngine;

public class NetworkManager : BaseManager<NetworkManager>
{
    private string GetPath()
    {
        string path = Path.Combine(Application.persistentDataPath, "RIM_ProjectSaveFile.json");
        return path;
    }

    public void RequestSaveSaveData(SaveData saveData)
    {
        if (saveData == null) return;

        string json = JsonUtility.ToJson(saveData, true);
        File.WriteAllText(GetPath(), json);
        Debug.Log("저장 완료!!" + GetPath());
    }

    public SaveData RequestLoadSavaData()
    {
        string path = GetPath();

        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData saveData = JsonUtility.FromJson<SaveData>(json);
            Debug.Log("로드 완료!");
            return saveData;
        }
        else
        {
            Debug.Log("새로운 세이브데이터를 생성합니다!");
            return CreateNewSaveData();
        }
    }

    private SaveData CreateNewSaveData()
    {
        SaveData saveData = new();
        saveData.Score = 0;
        saveData.Language = GameUtil.SetLanguage(Language.Korean);
        saveData.IsFirstStart = false;

        return saveData;
    }
}
