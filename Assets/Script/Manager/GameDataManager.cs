using System;
using System.Collections.Generic;
using UnityEngine;

public class GameDataManager : MonoBehaviour
{
    // [Instance]
    public static GameDataManager Instance { get; private set; }


    // [Field]
    public Dictionary<string, CharacterData> CharacterDataList { get; private set; } = new();


    // [Serializable Wrapper]
    [Serializable]
    private struct SerializableWrapper<T>
    {
        public List<T> m_data;
    }


    // [Life Cycle]
    private void Awake()
    {
        if (Instance == null) Instance = this;
        LoadUtil.LoadAllData();
    }


    // [Load Data In Path]
    private Dictionary<string, T> LoadData<T>(string path) where T : GameDataBase
    {
        string resourcePath = $"Json/{path}";

        TextAsset loadTextAsset = Resources.Load<TextAsset>(resourcePath);

        if(loadTextAsset == null)
        {
            Debug.LogError($"{resourcePath} 경로에 리소스가 없습니다! 다시 확인해주세요!!");
            return new Dictionary<string, T>();
        }
        
        try
        {
            string jsonData = loadTextAsset.text;
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
        }
        catch (System.Exception e)
        {
            Debug.LogException(e);
        }

        return new Dictionary<string, T>();
    }


    // [Load Data]
    public void LoadCharacterData()
    {
        CharacterDataList = LoadData<CharacterData>("CharacterData");
    }
}
