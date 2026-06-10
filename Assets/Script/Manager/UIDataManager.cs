using System;
using System.Collections.Generic;
using UnityEngine;

public class UIDataManager : BaseManager<UIDataManager>
{
    public Dictionary<string, MainMenuData> MainMenuDataList { get; private set; } = new();
    public Dictionary<string, CharacterCollectionData> CharaterCollectionDataList {  get; private set; } = new();
    public Dictionary<string, LanguagePopupData> LanguagePopupDataList { get; private set; } = new();
    public Dictionary<string, InGamePopupData> InGamePopupDataList {  get; private set; } = new();

    public Dictionary<string, DiePopupData> DiePopupDataList { get; private set; } = new();

    public UIDataManager()
    {
        if(Instance == this)
        {
            LoadAllData();
        }
    }

    public void ReloadAllData()
    {
        LoadAllData();
    }

    private void LoadAllData()
    {
        LoadMainMenuData();
        LoadCharacterCollectionData();
        LoadLanguagePopupData();
        LoadInGamePopupData();
        LoadDiePopupData();
    }

    private void LoadMainMenuData()
    {
        MainMenuDataList = LoadData<MainMenuData>(DataUtil.DataFile.UI.MainMenuData);
    }

    private void LoadCharacterCollectionData()
    {
        CharaterCollectionDataList = LoadData<CharacterCollectionData>(DataUtil.DataFile.UI.CharacterCollectionData);
    }

    private void LoadLanguagePopupData()
    {
        LanguagePopupDataList = LoadData<LanguagePopupData>(DataUtil.DataFile.UI.LanguagePopupData);
    }

    private void LoadInGamePopupData()
    {
        InGamePopupDataList = LoadData<InGamePopupData>(DataUtil.DataFile.UI.InGamePopupData);
    }

    private void LoadDiePopupData()
    {
        DiePopupDataList = LoadData<DiePopupData>(DataUtil.DataFile.UI.DiePopupData);
    }

    [Serializable]
    private class SerializableWrapper<T>
    {
        public List<T> m_data;
    }

    private Dictionary<string, T> LoadData<T>(string path) where T : UIDataBase
    {

        string language = GameUtil.GetLanguage(GameManager.Instance.Language);
        string resourcePath = $"Json/{path}_{language}";
        TextAsset textAsset = LoadUtil.Sync.LoadTextAsset(resourcePath);

        if (textAsset == null)
        {
            this.LogError($"{resourcePath} 경로에 리소스가 없습니다! 다시 확인해주세요!!");
            return null;
        }

        try
        {
            string jsonData = textAsset.text;
            string wrapperData = "{\"m_data\":" + jsonData + "}";
            SerializableWrapper<T> wrapper = JsonUtility.FromJson<SerializableWrapper<T>>(wrapperData);
            if (wrapper.m_data != null)
            {
                Debug.Log($"{resourcePath}의 데이터가 {wrapper.m_data.Count}만큼 로드 되었습니다!!");
                Dictionary<string, T> newDictionary = new(wrapper.m_data.Count);
                foreach (T data in wrapper.m_data)
                {
                    newDictionary.Add(data.Id, data);
                }
                return newDictionary;
            }
            else
            {
                this.LogError($"{resourcePath}의 데이터가 없습니다 다시 확인해주세요!!");
                return null;
            }
        }
        catch (Exception e)
        {
            Debug.LogException(e);
            return null;
        }
    }
}
